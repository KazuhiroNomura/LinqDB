using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using System.Reflection;
using System.Reflection.Emit;
using LinqDB.Helpers;
// ReSharper disable PossibleNullReferenceException
namespace LinqDB.Optimizers;
partial class Optimizer{
    /// <summary>
    /// Optimizerで最適化されたExpressionからDynamicMethodを作る専用
    /// </summary>
    internal abstract class A作成_IL:VoidExpressionTraverser{
        protected int 番号;
        /// <summary>
        /// ParameterExpressionに対応するLocalBuilder
        /// </summary>
        protected Dictionary<ParameterExpression,LocalBuilder> Dictionary_Parameter_LocalBuilder=new();
        protected readonly 判定_InstanceMethodか 判定_InstanceMethodか;
        /// <summary>
        /// LabetlTargetの同一参照に対応するILのLabel
        /// </summary>
        protected Dictionary<LabelTarget,Label>? Dictionary_Name_Label;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="判定_InstanceMethodか"></param>
        protected A作成_IL(判定_InstanceMethodか 判定_InstanceMethodか) {
            this.判定_InstanceMethodか =判定_InstanceMethodか;
        }
        internal Dictionary<ConstantExpression,(FieldInfo Disp,MemberExpression Member)> DictionaryConstant{
            get=>this.判定_InstanceMethodか.DictionaryConstant;
            set=>this.判定_InstanceMethodか.DictionaryConstant=value;
        }

        internal Dictionary<DynamicExpression,(FieldInfo Disp,MemberExpression Member)> DictionaryDynamic=default!;
        private Dictionary<LambdaExpression,(FieldInfo Disp,MemberExpression Member,MethodBuilder Impl)>_DictionaryLambda_Fields=default!;
        internal Dictionary<LambdaExpression,(FieldInfo Disp,MemberExpression Member,MethodBuilder Impl)> DictionaryLambda{
            get=>this._DictionaryLambda_Fields;
            set{
                this._DictionaryLambda_Fields=value;
                this.判定_InstanceMethodか.Lambdas=value.Keys;
            }
        }
        internal IReadOnlyDictionary<ParameterExpression,(FieldInfo Disp,MemberExpression Member)> Dictionaryラムダ跨ぎParameter{
            get=>this.判定_InstanceMethodか.Dictionaryラムダ跨ぎParameter;
            set=>this.判定_InstanceMethodか.Dictionaryラムダ跨ぎParameter=value;
        }
        internal ParameterExpression DispParameter=default!;
        internal bool インスタンスメソッドか;
        /// <summary>
        /// その時点で可視のParameter
        /// </summary>
        protected IList<ParameterExpression>? Parameters;
        /// <summary>
        /// 書き込み先ILGenerator
        /// </summary>
        protected ILGenerator? I;
        private void PrivateCall(MethodInfo Method){
            var I = this.I;
            Debug.Assert(I is not null);
            if(Method.ReflectedType.IsValueType) {
                if(Method.IsVirtual) {
                    I.Constrained(Method.ReflectedType);
                    I.Callvirt(Method);
                } else {
                    I.Call(Method);
                }
            } else if(Method.IsStatic||Method.IsFinal||Method.ReflectedType.IsSealed) {
                I.Call(Method);
            } else if(Method.IsVirtual) {
                I.Callvirt(Method);
            } else {
                I.Call(Method);
            }
        }
        private void PrivateCall(MethodInfo Method,ReadOnlyCollection<Expression> Expressions){
            var Method_Parameters = Method.GetParameters();
            var Expressions_Count = Expressions.Count;
            var ArrayTry値 = new LocalBuilder[Expressions_Count];
            for(var a = 0;a<Expressions_Count;a++)
                if(Expressions[a].NodeType==ExpressionType.Try)
                    ArrayTry値[a]=this.PrivateTry値を代入した変数((TryExpression)Expressions[a]);
            var I=this.I!;
            for(var a = 0;a<Expressions_Count;a++) {
                var Expression = Expressions[a];
                if(Method_Parameters[a].ParameterType.IsByRef) {
                    // ReSharper disable once SwitchStatementMissingSomeCases
                    switch(Expression.NodeType){
                        case ExpressionType.ArrayIndex:{
                            var Binary=(BinaryExpression)Expression;
                            this.Traverse(Binary.Left);
                            this.Traverse(Binary.Right);
                            Debug.Assert(Binary.Method is null);
                            I.Ldelema(Binary.Type);
                            break;
                        }
                        case ExpressionType.Parameter:{
                            var Parameter=(ParameterExpression)Expression;
                            //Debug.Assert(this.Parameters is not null);
                            var index=this.Parameters!.IndexOf(Parameter);
                            if(index>=0){
                                if(this.インスタンスメソッドか) index++;
                                if(Parameter.IsByRef)I.Ldarg((ushort)index);
                                else I.Ldarga((ushort)index);
                            } else{
                                Debug.Assert(this.Dictionary_Parameter_LocalBuilder is not null);
                                Debug.Assert(this.Dictionary_Parameter_LocalBuilder.ContainsKey(Parameter));
                                I.Ldloca(this.Dictionary_Parameter_LocalBuilder[Parameter]);
                            }
                            break;
                        }
                        case ExpressionType.Try:{
                            I.Ldloca(ArrayTry値[a]);
                            break;
                        }
                        case ExpressionType.MemberAccess:{
                            var Member=(MemberExpression)Expression;
                            var Member_Member=Member.Member;
                            this.PointerTraverseNulllable(Member.Expression);
                            if(Member_Member.MemberType==MemberTypes.Property){
                                I.Call(((PropertyInfo)Member_Member).GetMethod);
                                var 変数=I.M_DeclareLocal_Stloc(Method_Parameters[a].ParameterType);
                                I.Ldloca(変数);
                            } else{
                                Debug.Assert(Member_Member.MemberType==MemberTypes.Field);
                                var Member_Field=(FieldInfo)Member_Member;
                                if(Member_Field.IsStatic) {
                                    I.Ldsflda(Member_Field);
                                } else {
                                    I.Ldflda(Member_Field);
                                }
                            }
                            break;
                        }
                        default:{
                            this.Traverse(Expression);
                            var 変数=I.M_DeclareLocal_Stloc(Method_Parameters[a].ParameterType);
                            I.Ldloca(変数);
                            break;
                        }
                    }
                } else if(Expression.NodeType==ExpressionType.Try){
                    this.I!.Ldloc(ArrayTry値[a]);
                } else{
                    this.Traverse(Expression);
                }
            }
            this.PrivateCall(Method);
        }
        private void 共通UnaryExpression(UnaryExpression Unary,Action<ILGenerator>IL出力){
            var Unary_Operand=Unary.Operand;
            var Unary_Operand_Type=Unary_Operand.Type;
            var I=this.I;
            Debug.Assert(I is not null);
            this.Traverse(Unary_Operand);
            if(Unary_Operand_Type.IsNullable()) {
                var Operand = I.M_DeclareLocal_Stloc(Unary_Operand_Type);
                I.Ldloca(Operand);
                I.Call(Unary_Operand_Type.GetProperty(nameof(Nullable<int>.HasValue))!.GetMethod);
                var HasValueがtrueだった = I.DefineLabel();
                I.Brtrue_S(HasValueがtrueだった);
                I.Ldloc(Operand);
                var HasValueがfalseだった = I.DefineLabel();
                I.Br_S(HasValueがfalseだった);
                I.MarkLabel(HasValueがtrueだった);
                I.Ldloca(Operand);
                I.Call(Unary_Operand_Type.GetMethod(nameof(Nullable<int>.GetValueOrDefault),Type.EmptyTypes)!);
                if(Unary.Method is not null)I.Call(Unary.Method);
                else IL出力(I);
                I.Newobj(Unary.Type.GetConstructors()[0]);
                I.MarkLabel(HasValueがfalseだった);
            } else if(Unary.Method is not null) {
                I.Call(Unary.Method);
            } else {
                IL出力(I);
            }
        }
        private void 共通四則演算(BinaryExpression Binary,OpCode OpCode){
            var Binary_Right=Binary.Right;
            var Binary_Left=Binary.Left;
            var Binary_Type=Binary.Type;
            var Binary_Right_Type=Binary_Right.Type;
            var Binary_Left_Type=Binary_Left.Type;
            var I=this.I;
            Debug.Assert(I is not null);
            if(Binary_Right_Type.IsNullable()){
                Debug.Assert(Binary_Left_Type.IsNullable());
                Debug.Assert(Binary_Left_Type==Binary_Right_Type);
                var GetValueOrDefault = Binary_Type.GetMethod(nameof(Nullable<int>.GetValueOrDefault),Type.EmptyTypes);
                Debug.Assert(GetValueOrDefault is not null);
                var get_HasValue = Binary_Type.GetProperty(nameof(Nullable<int>.HasValue))!.GetMethod;
                LocalBuilder Left,Right;
                if(Binary_Right.NodeType==ExpressionType.Try){
                    Right=this.PrivateTry値を代入した変数((TryExpression)Binary_Right);
                    this.Traverse(Binary.Left);
                    Left=I.M_DeclareLocal_Stloc(Binary_Left_Type);
                    I.Ldloca(Left);
                    I.Call(get_HasValue);
                } else{
                    this.Traverse(Binary_Left);
                    Left=I.M_DeclareLocal_Stloc(Binary_Left_Type);
                    I.Ldloca(Left);
                    I.Call(get_HasValue);
                    this.Traverse(Binary_Right);
                    Right=I.M_DeclareLocal_Stloc(Binary_Right_Type);
                }
                I.Ldloca(Right);
                I.Call(get_HasValue);
                I.And();
                var HasValueだった=I.DefineLabel();
                I.Brtrue_S(HasValueだった);
                I.M_Initobjで値型を初期化してスタックに積む(Binary_Type);
                var 終了 =I.DefineLabel();
                I.Br_S(終了);
                I.MarkLabel(HasValueだった);
                I.Ldloca(Left);
                I.Call(GetValueOrDefault);
                I.Ldloca(Right);
                I.Call(GetValueOrDefault);
                if(Binary.Method is not null)I.Call(Binary.Method);
                else                         I.Emit(OpCode);
                I.Newobj(Binary_Type.GetConstructors()[0]);
                I.MarkLabel(終了);
            } else{
                if(Binary_Right.NodeType==ExpressionType.Try){
                    var Try値=this.PrivateTry値を代入した変数((TryExpression)Binary_Right);
                    this.Traverse(Binary_Left);
                    I.Ldloc(Try値);
                } else{
                    this.Traverse(Binary_Left);
                    this.Traverse(Binary_Right);
                }
                if(Binary.Method is not null)I.Call(Binary.Method);
                else I.Emit(OpCode);
            }
        }
        /// <summary>
        /// 符号付きと符号無しの四則演算
        /// </summary>
        /// <param name="Binary"></param>
        /// <param name="Signed"></param>
        /// <param name="Unsigned"></param>
        private void 共通四則演算(BinaryExpression Binary,OpCode Signed,OpCode Unsigned){
            Debug.Assert(Signed!=Unsigned);
            this.共通四則演算(
                Binary,
                IsUnsigned(Binary.Left.Type)&&IsUnsigned(Binary.Right.Type)
                    ?Unsigned
                    :Signed
            );
        }
        private void 格納先設定(Expression e){
            // ReSharper disable once SwitchStatementMissingSomeCases
            switch(e.NodeType){
                case ExpressionType.Index:{
                    var Index=(IndexExpression)e;
                    this.PointerTraverseNulllable(Index.Object);
                    this.TraverseExpressions(Index.Arguments);
                    //foreach(var Argument in Index.Arguments)this.Traverse(Argument);
                    break;
                }
                case ExpressionType.MemberAccess:{
                    this.PointerTraverseNulllable(((MemberExpression)e).Expression);
                    break;
                }
                case ExpressionType.Parameter:{
                    var Parameter=(ParameterExpression)e;
                    var index = this.Parameters!.IndexOf(Parameter);
                    if(index>=0){
                        if(Parameter.IsByRef){
                            if(this.インスタンスメソッドか) index++;
                            this.I!.Ldarg((ushort)index);
                        }
                    } else if(this.Dictionaryラムダ跨ぎParameter.TryGetValue(Parameter,out var Disp_Member)) {
                        this.格納先設定(Disp_Member.Member);
                    }
                    break;
                }
                default:
                    throw new NotSupportedException($"{e.NodeType}はサポートされてない");
            }
        }
        private void Void格納先に格納(Expression e){
            var I = this.I!;
            switch(e.NodeType){
                case ExpressionType.Parameter:{
                    var Parameter=(ParameterExpression)e;
                    var index = this.Parameters!.IndexOf(Parameter);
                    if(index>=0) {
                        if(Parameter.IsByRef) {
                            I.Stobj(Parameter.Type);
                        } else {
                            if(this.インスタンスメソッドか) index++;
                            I.Starg((ushort)index);
                        }
                    } else{
                        if(this.Dictionaryラムダ跨ぎParameter.TryGetValue(Parameter,out var Disp_Member)){
                            this.Void格納先に格納(Disp_Member.Member);
                        } else{
                            Debug.Assert(this.Dictionary_Parameter_LocalBuilder is not null);
                            Debug.Assert(this.Dictionary_Parameter_LocalBuilder.ContainsKey(Parameter));
                            I.Stloc(this.Dictionary_Parameter_LocalBuilder[Parameter]);
                        }
                    }
                    break;
                }
                case ExpressionType.Index:{
                    var Index=(IndexExpression)e;
                    if(Index.Indexer is not null) {
                        Debug.Assert(Index.Indexer.SetMethod is not null);
                        Debug.Assert(Index.Object is not null);
                        this.PrivateCall(Index.Indexer.SetMethod!);
                    } else if(Index.Arguments.Count==1){
                        I.Stelem(e.Type);
                        Debug.Assert(Index.Object is not null);
                        Debug.Assert(Index.Object!.Type.GetArrayRank()==1);
                    } else {
                        Debug.Assert(Index.Object is not null);
                        I.Call(Index.Object!.Type.GetMethod("Set")!);
                        Debug.Assert(Index.Arguments.Count==Index.Object.Type.GetArrayRank());
                    }
                    break;
                }
                case ExpressionType.MemberAccess:{
                    var Member=(MemberExpression)e;
                    var Member_Member=Member.Member;
                    if(Member_Member.MemberType==MemberTypes.Field){
                        var Member_Field=(FieldInfo)Member_Member;
                        if(Member_Field.IsStatic) {
                            I.Stsfld(Member_Field);
                        } else {
                            I.Stfld(Member_Field);
                        }
                    } else{
                        Debug.Assert(Member_Member.MemberType==MemberTypes.Property&&((PropertyInfo)Member_Member).SetMethod is not null);
                        this.PrivateCall(((PropertyInfo)Member_Member).SetMethod!);
                    }
                    break;
                }
                default:
                    throw new NotSupportedException($"{e.NodeType}はサポートされてない");
            }
        }
        private static bool IsUnsigned(Type p)=>
            p==typeof(byte)||p==typeof(ushort)||p==typeof(char)||p==typeof(uint)||p==typeof(ulong)||p==typeof(UIntPtr);
        private void 共通IncrementDecrement(UnaryExpression Unary,OpCode AddSub) => this.共通UnaryExpression(Unary,I => {
            var Unary_Type = Unary.Type;
            if(Unary_Type.IsNullable()) {
                Unary_Type=Unary_Type.GetGenericArguments()[0];
            }
            if(
                Unary_Type==typeof(sbyte)||
                Unary_Type==typeof(short)||
                Unary_Type==typeof(int)||
                Unary_Type==typeof(byte)||
                Unary_Type==typeof(ushort)||
                Unary_Type==typeof(uint)
            ) {
                I.Ldc_I4_1();
            } else if(Unary_Type==typeof(long)||Unary_Type==typeof(ulong)) {
                I.Ldc_I8(1L);
            } else if(Unary_Type==typeof(float)) {
                I.Ldc_R4(1F);
            } else {
                Debug.Assert(Unary_Type==typeof(double));
                I.Ldc_R8(1D);
            }
            I.Emit(AddSub);
        });
        private void 格納先設定IncrementDecrement(UnaryExpression Unary,OpCode AddSub){
            var Unary_Operand=Unary.Operand;
            this.格納先設定(Unary_Operand);
            this.共通IncrementDecrement(Unary,AddSub);
        }
        protected override void Add(BinaryExpression Binary)=>
            this.共通四則演算(Binary,OpCodes.Add);
        protected override void AddChecked(BinaryExpression Binary)=>
            this.共通四則演算(Binary,OpCodes.Add_Ovf,OpCodes.Add_Ovf_Un);
        protected override void Subtract(BinaryExpression Binary)=>
            this.共通四則演算(Binary,OpCodes.Sub);
        protected override void SubtractChecked(BinaryExpression Binary)=>
            this.共通四則演算(Binary,OpCodes.Sub_Ovf,OpCodes.Sub_Ovf_Un);
        protected override void Multiply(BinaryExpression Binary)=>
            this.共通四則演算(Binary,OpCodes.Mul);
        protected override void MultiplyChecked(BinaryExpression Binary)=>
            this.共通四則演算(Binary,OpCodes.Mul_Ovf,OpCodes.Mul_Ovf_Un);
        protected override void Divide(BinaryExpression Binary)=>
            this.共通四則演算(Binary,OpCodes.Div,OpCodes.Div_Un);
        protected override void Modulo(BinaryExpression Binary)=>
            this.共通四則演算(Binary,OpCodes.Rem,OpCodes.Rem_Un);
        protected override void Power(BinaryExpression Binary) =>
            this.共通四則演算(Binary,OpCodes.Rem,OpCodes.Rem_Un);
        private void AndOr(BinaryExpression Binary,bool Andか) {
            var Binary_Right = Binary.Right;
            var Binary_Left = Binary.Left;
            var Binary_Type = Binary.Type;
            var I = this.I!;
            if(Binary_Type.IsNullable()) {
                var GetValueOrDefault = Binary_Type.GetMethod(nameof(Nullable<int>.GetValueOrDefault),Type.EmptyTypes)!;
                var get_HasValue = Binary_Type.GetProperty(nameof(Nullable<int>.HasValue))!.GetMethod;
                if(Binary_Type==typeof(bool?)) {
                    this.Traverse(Binary.Left);
                    var Left = I.M_DeclareLocal_Stloc(Binary_Type);
                    LocalBuilder Right;
                    if(Binary_Right.NodeType==ExpressionType.Try) {
                        Right=this.PrivateTry値を代入した変数((TryExpression)Binary_Right);
                    } else {
                        //Booleanの場合
                        this.Traverse(Binary_Right);
                        Right=I.M_DeclareLocal_Stloc(Binary_Right.Type);
                    }
                    I.Ldloca(Left);
                    I.Call(GetValueOrDefault);
                    var Result_Right = I.DefineLabel();
                    I.Brtrue(Result_Right);
                    I.Ldloca(Right);
                    I.Call(GetValueOrDefault);
                    var Result_Left = I.DefineLabel();
                    I.Brtrue(Result_Left);
                    I.Ldloca(Left);
                    I.Call(get_HasValue);
                    I.Brfalse(Result_Right);
                    I.MarkLabel(Result_Left);
                    I.Ldloc(Andか?Left:Right);
                    var 終了 = I.DefineLabel();
                    I.Br(終了);
                    I.MarkLabel(Result_Right);
                    I.Ldloc(Andか?Right:Left);
                    I.MarkLabel(終了);
                } else {
                    this.Traverse(Binary.Left);
                    var Left = I.M_DeclareLocal_Stloc(Binary_Type);
                    LocalBuilder Right;
                    if(Binary_Right.NodeType==ExpressionType.Try) {
                        Right=this.PrivateTry値を代入した変数((TryExpression)Binary_Right);
                    } else {
                        //Booleanの場合
                        this.Traverse(Binary_Right);
                        Right=I.M_DeclareLocal_Stloc(Binary_Right.Type);
                    }
                    //if(!Left.HasValue)
                    //    goto Leftがnullだった;
                    I.Ldloca(Left);
                    I.Call(get_HasValue);
                    var Leftがnullだった = I.DefineLabel();
                    I.Brfalse(Leftがnullだった);
                    //if(!Right.HasValue)
                    //    goto Right_else;
                    I.Ldloca(Right);
                    I.Call(get_HasValue);
                    var Right_else = I.DefineLabel();
                    I.Brfalse(Right_else);
                    //                      I.M_MessageBox("Right is nullだった");
                    //Result=Left.GetValueOrDefault()&Right.GetValueOrDefault();
                    I.Ldloca(Left);
                    I.Call(GetValueOrDefault);
                    I.Ldloca(Right);
                    I.Call(GetValueOrDefault);
                    if(Binary.Method is not null) {
                        I.Call(Binary.Method);
                    } else if(Andか) {
                        I.And();
                    } else {
                        I.Or();
                    }
                    var Constructor = Binary_Type.GetConstructors()[0];
                    I.Newobj(Constructor);
                    //goto 終了;
                    var 終了 = I.DefineLabel();
                    I.Br(終了);
                    //Right_else:
                    I.MarkLabel(Right_else);
                    //if(Left.GetValueOrDefault())
                    //    goto nullを返す;
                    I.Ldloca(Left);
                    I.Call(GetValueOrDefault);
                    //if(op_FalseMethod is not null) {
                    //    I.Call(op_FalseMethod);
                    //}
                    var op_TrueMethod = GetValueOrDefault.ReturnType.GetMethod(op_True);
                    if(op_TrueMethod is not null) {
                        I.Call(op_TrueMethod);
                        I.Not();
                    }
                    var nullを返す = I.DefineLabel();
                    I.Brtrue(nullを返す);
                    //goto Leftを返す;
                    var Leftを返す = I.DefineLabel();
                    I.Br(Leftを返す);
                    //Leftがnullだった:
                    I.MarkLabel(Leftがnullだった);
                    //if(!Right.HasValue)
                    //    goto nullを返す;
                    I.Ldloca(Right);
                    I.Call(get_HasValue);
                    I.Brfalse(nullを返す);
                    //if(Right.GetValueOrDefault())
                    //    goto nullを返す;
                    I.Ldloca(Right);
                    I.Call(GetValueOrDefault);
                    if(op_TrueMethod is not null) {
                        I.Call(op_TrueMethod);
                        I.Not();
                    }
                    //if(op_FalseMethod is not null) {
                    //    I.Call(op_FalseMethod);
                    //}
                    //I.M_MessageBox("Right.GetValue");
                    I.Brtrue(nullを返す);
                    //Result=Right;
                    I.Ldloc(Right);
                    //goto 終了;
                    I.Br(終了);
                    //Leftを返す:
                    I.MarkLabel(Leftを返す);
                    //Result=Left;
                    I.Ldloc(Left);
                    //I.Ldc_I4_0();
                    //I.Newobj(Constructor);
                    //goto 終了;
                    I.Br(終了);
                    //nullを返す:
                    I.MarkLabel(nullを返す);
                    //Result=null;
                    //I.M_MessageBox("nullを返す:");
                    I.M_Initobjで値型を初期化してスタックに積む(Binary_Type);
                    //終了:
                    I.MarkLabel(終了);
                }
            } else {
                if(Binary_Right.NodeType==ExpressionType.Try) {
                    var Try値 = this.PrivateTry値を代入した変数((TryExpression)Binary_Right);
                    this.Traverse(Binary_Left);
                    I.Ldloc(Try値);
                } else {
                    this.Traverse(Binary_Left);
                    this.Traverse(Binary_Right);
                }
                if(Binary.Method is not null) {
                    I.Call(Binary.Method);
                } else if(Andか) {
                    I.And();
                } else {
                    I.Or();
                }
            }
        }

        protected override void And(BinaryExpression Binary)=>
            this.AndOr(Binary,true);
        protected override void Or(BinaryExpression Binary)=>
            this.AndOr(Binary,false);
        protected override void ExclusiveOr(BinaryExpression Binary)=>
            this.共通四則演算(Binary,OpCodes.Xor);
        protected override void LeftShift(BinaryExpression Binary)=>
            this.共通四則演算(Binary,OpCodes.Shl);
        protected override void RightShift(BinaryExpression Binary)=>
            this.共通四則演算(Binary,OpCodes.Shr,OpCodes.Shr_Un);
        protected override void Assign(BinaryExpression Binary){
            var Binary_Left=Binary.Left;
            this.格納先設定(Binary_Left);
            this.Traverse(Binary.Right);
            var I=this.I!;
            var 作業=I.M_DeclareLocal_Stloc_Ldloc(Binary_Left.Type);
            this.Void格納先に格納(Binary_Left);
            I.Ldloc(作業);
        }
        protected override void AndAlso(BinaryExpression Binary){
            //短絡演算子の戻り値の型はオペランドと同じ。
            //op_False(a) ? a : (a & b);
            var I = this.I!;
            //Debug.Assert(I is not null);
            var Binary_Type = Binary.Type;
            var Binary_Left = Binary.Left;
            Debug.Assert(Binary_Left.Type==Binary.Right.Type);
            Debug.Assert(Binary_Left.Type==Binary.Type);
            //var Binary_Left_NullableType = Binary_Left.Type;
            this.Traverse(Binary_Left);
            var 終了 = I.DefineLabel();
            if(Binary_Left.Type.IsNullable()) {
                Debug.Assert(Binary.Right.Type.IsNullable());
                var get_HasValue =Binary_Type.GetProperty(nameof(Nullable<int>.HasValue))!.GetMethod;
                var GetValueOrDefault= Binary_Type.GetMethod(nameof(Nullable<int>.GetValueOrDefault),Type.EmptyTypes)!;
                var GetValueOrDefault_ReturnType = GetValueOrDefault.ReturnType;
                var op_FalseMethod = GetValueOrDefault_ReturnType.GetMethod(op_False);
                var L = I.M_DeclareLocal_Stloc(Binary_Type);
                var Binary_Right = Binary.Right;
                //                    var Binary_Right_NullableType = Binary_Right.Type;
                var Lを出力して終了 = I.DefineLabel();
                var Rを出力して終了 = I.DefineLabel();
                if(op_FalseMethod is null) {
                    Debug.Assert(Binary_Type==typeof(bool?));
                    //if(L.GetValueOrDefault)
                    //    if(R.GetValueOrDefault)
                    //        Rはtrue,Lはtrue
                    //    else if(R.HasValue)
                    //        Rはfalse
                    //    else
                    //        Rはnull
                    //else if(L.HasValue)
                    //    Lはfalse
                    //else
                    //    if(R.GetValueOrDefault)
                    //        Lはnull
                    //    else if(R.HasValue)
                    //        Rはfalse
                    //    else
                    //        Lはnull
                    this.Traverse(Binary_Right);
                    var R = I.M_DeclareLocal_Stloc(Binary_Type);
                    //if(L.GetValueOrDefault)
                    I.Ldloca(L);
                    I.Call(GetValueOrDefault);
                    var Lがfalseだった = I.DefineLabel();
                    I.Brfalse_S(Lがfalseだった);
                    //    if(R.GetValueOrDefault)
                    I.Ldloca(R);
                    I.Call(GetValueOrDefault);
                    //        Rはtrue,Lはtrue
                    I.Brtrue_S(Lを出力して終了);
                    //    else if(R.HasValue)
                    I.Ldloca(R);
                    I.Call(get_HasValue);
                    //        Rはfalse
                    I.Brtrue_S(Rを出力して終了);
                    //    else
                    //        Rはnull
                    I.Br_S(Rを出力して終了);
                    I.MarkLabel(Lがfalseだった);
                    //else if(L.HasValue)
                    I.Ldloca(L);
                    I.Call(get_HasValue);
                    //    Lはfalse
                    I.Brtrue_S(Lを出力して終了);
                    //else
                    //    if(R.GetValueOrDefault)
                    I.Ldloca(R);
                    I.Call(GetValueOrDefault);
                    //        Lはnull
                    I.Brtrue_S(Lを出力して終了);
                    //    else if(R.HasValue)
                    I.Ldloca(R);
                    I.Call(get_HasValue);
                    //        Rはfalse
                    I.Brtrue_S(Rを出力して終了);
                    //    else
                    //        Lはnull
                    //                        I.Br(Lを出力して終了);
                    I.MarkLabel(Lを出力して終了);
                    I.Ldloc(L);
                    I.Br_S(終了);
                    I.MarkLabel(Rを出力して終了);
                    I.Ldloc(R);
                } else {
                    //if(L.HasValue)
                    //    L_GetValueOrDefault=L.GetValueOrDefault
                    //    if(R.HasValue)
                    //        if(op_False(L_GetValueOrDefault))
                    //            L
                    //        else
                    //            Nullable(L_GetValueOrDefault&R.GetValueOrDefault)
                    //    else
                    //        if(op_False(L_GetValueOrDefault))
                    //            L
                    //        else
                    //            null終了
                    //else
                    //    Lはnull終了
                    //if(L.HasValue)
                    I.Ldloca(L);
                    I.Call(get_HasValue);
                    I.Brfalse(Lを出力して終了);
                    //    L_GetValueOrDefault=L.GetValueOrDefault
                    I.Ldloca(L);
                    I.Call(GetValueOrDefault);
                    var L_GetValueOrDefault = I.M_DeclareLocal_Stloc(GetValueOrDefault_ReturnType);
                    //    if(R.HasValue)
                    this.Traverse(Binary_Right);
                    var R = I.M_DeclareLocal_Stloc(Binary_Type);
                    I.Ldloca(R);
                    I.Call(get_HasValue);
                    var Rがnullだった = I.DefineLabel();
                    I.Brfalse_S(Rがnullだった);
                    //        if(op_False(L_GetValueOrDefault))
                    I.Ldloc(L_GetValueOrDefault);
                    I.Call(op_FalseMethod);
                    var op_Falseではなかった = I.DefineLabel();
                    I.Brfalse_S(op_Falseではなかった);
                    //            L
                    I.Ldloc(L);
                    I.Br_S(終了);
                    //        else
                    I.MarkLabel(op_Falseではなかった);
                    //            Nullable(L_GetValueOrDefault&&R.GetValueOrDefault)
                    I.Ldloc(L_GetValueOrDefault);
                    I.Ldloca(R);
                    I.Call(GetValueOrDefault);
                    I.Call(Binary.Method);
                    I.Newobj(Binary_Type.GetConstructors()[0]);
                    I.Br_S(終了);
                    //    else
                    I.MarkLabel(Rがnullだった);
                    //        if(op_False(L_GetValueOrDefault))
                    I.Ldloc(L_GetValueOrDefault);
                    I.Call(op_FalseMethod);
                    I.Brfalse_S(Rを出力して終了);
                    //            L
                    I.Br_S(Lを出力して終了);
                    //        else
                    I.MarkLabel(Rを出力して終了);
                    //            null終了
                    I.Ldloc(R);
                    I.Br_S(終了);
                    //else
                    I.MarkLabel(Lを出力して終了);
                    //    Lはnull終了
                    I.Ldloc(L);
                }
            } else {
                var op_FalseMethod = Binary_Type.GetMethod(op_False);
                var 短絡評価 = I.DefineLabel();
                if(op_FalseMethod is null) {
                    Debug.Assert(Binary_Type==typeof(bool));
                    I.Brfalse(短絡評価);
                    this.Traverse(Binary.Right);
                    I.Br_S(終了);
                    I.MarkLabel(短絡評価);
                    I.Ldc_I4_0();
                } else {
                    var L = I.M_DeclareLocal_Stloc_Ldloc(Binary_Type);
                    I.Call(op_FalseMethod);
                    I.Brfalse_S(短絡評価);
                    I.Ldloc(L);
                    I.Br(終了);
                    I.MarkLabel(短絡評価);
                    I.Ldloc(L);
                    this.Traverse(Binary.Right);
                    Debug.Assert(Binary.Method is not null);
                    I.Call(Binary.Method!);
                }
            }
            I.MarkLabel(終了);
        }
        protected override void OrElse(BinaryExpression Binary){
            //if(op_True(x))return x;
            //else          return x|y;
            //if(x)return true;
            //else return y;
            var I=this.I!;
            //Debug.Assert(I is not null);
            var Binary_Type = Binary.Type;
            var Binary_Left =Binary.Left;
            Debug.Assert(Binary_Left.Type==Binary.Right.Type);
            Debug.Assert(Binary_Left.Type==Binary.Type);
            var Binary_Left_NullableType=Binary_Left.Type;
            this.Traverse(Binary_Left);
            var 終了=I.DefineLabel();
            if(Binary_Left.Type.IsNullable()){
                var get_HasValue = Binary_Type.GetProperty(nameof(Nullable<int>.HasValue))!.GetMethod;
                var GetValueOrDefault = Binary_Type.GetMethod(nameof(Nullable<int>.GetValueOrDefault),Type.EmptyTypes)!;
                var GetValueOrDefault_ReturnType = GetValueOrDefault.ReturnType;
                //                    var Binary_Left_Type =Binary_Left_NullableType.GetGenericArguments()[0];
                var op_TrueMethod = GetValueOrDefault_ReturnType.GetMethod(op_True);
                //var true終了=I.DefineLabel();
                //var false終了=I.DefineLabel();
                //                  var Binary_Type=Binary.Type;
                var L=I.M_DeclareLocal_Stloc(Binary_Left_NullableType);
                var Binary_Right=Binary.Right;
                var Binary_Right_NullableType=Binary_Right.Type;
                var Lを出力して終了=I.DefineLabel();
                var Rを出力して終了=I.DefineLabel();
                if(op_TrueMethod is null){
                    Debug.Assert(Binary_Type==typeof(bool?));
                    //if(L.GetValueOrDefault)
                    //    Lはtrue
                    //else if(L.HasValue)
                    //    Rはtrue,false,nullいずれか
                    //else
                    //    if(R.GetValueOrDefault)
                    //        Rはtrue
                    //    else
                    //        Lはnull
                    this.Traverse(Binary_Right);
                    var R=I.M_DeclareLocal_Stloc(Binary_Right_NullableType);
                    //if(L.GetValueOrDefault)
                    I.Ldloca(L);
                    I.Call(GetValueOrDefault);
                    var Lがfalseだった =I.DefineLabel();
                    I.Brfalse_S(Lがfalseだった);
                    //    Lはtrue
                    I.Br_S(Lを出力して終了);
                    I.MarkLabel(Lがfalseだった);
                    //else if(L.HasValue)
                    I.Ldloca(L);
                    I.Call(get_HasValue);
                    //                    var Lがnullだった=I.DefineLabel();
                    I.Brtrue_S(Rを出力して終了);
                    //    Rはtrue,false,nullいずれか
                    //else
                    //    if(R.GetValueOrDefault)
                    I.Ldloca(R);
                    I.Call(GetValueOrDefault);
                    //        Rはtrue
                    I.Brtrue_S(Rを出力して終了);
                    //    else
                    //        Lはnull
                    I.MarkLabel(Lを出力して終了);
                    I.Ldloc(L);
                    I.Br_S(終了);
                    I.MarkLabel(Rを出力して終了);
                    I.Ldloc(R);
                } else{
                    //if(L.HasValue)
                    //    L_GetValueOrDefault=L.GetValueOrDefault
                    //    if(R.HasValue)
                    //        if(op_True(L_GetValueOrDefault))
                    //            L
                    //        else
                    //            Nullable(L_GetValueOrDefault|R.GetValueOrDefault)
                    //    else
                    //        if(op_True(L_GetValueOrDefault))
                    //            L
                    //        else
                    //            null終了
                    //else
                    //    Lはnull終了
                    //if(L.HasValue)
                    I.Ldloca(L);
                    I.Call(get_HasValue);
                    I.Brfalse(Lを出力して終了);
                    //    L_GetValueOrDefault=L.GetValueOrDefault
                    I.Ldloca(L);
                    I.Call(GetValueOrDefault);
                    var L_GetValueOrDefault =I.M_DeclareLocal_Stloc(Binary_Type);
                    //    if(R.HasValue)
                    this.Traverse(Binary_Right);
                    var R=I.M_DeclareLocal_Stloc(Binary_Right_NullableType);
                    I.Ldloca(R);
                    I.Call(get_HasValue);
                    var Rがnullだった=I.DefineLabel();
                    I.Brfalse_S(Rがnullだった);
                    //        if(op_True(L_GetValueOrDefault))
                    I.Ldloc(L_GetValueOrDefault);
                    I.Call(op_TrueMethod);
                    var op_Falseではなかった=I.DefineLabel();
                    I.Brfalse_S(op_Falseではなかった);
                    //            L
                    I.Ldloc(L);
                    I.Br_S(終了);
                    //        else
                    I.MarkLabel(op_Falseではなかった);
                    //            Nullable(L_GetValueOrDefault|R.GetValueOrDefault)
                    I.Ldloc(L_GetValueOrDefault);
                    I.Ldloca(R);
                    I.Call(GetValueOrDefault);
                    I.Call(Binary.Method);
                    I.Newobj(Binary_Type.GetConstructors()[0]);
                    I.Br_S(終了);
                    //    else
                    I.MarkLabel(Rがnullだった);
                    //        if(op_True(L_GetValueOrDefault))
                    I.Ldloc(L_GetValueOrDefault);
                    I.Call(op_TrueMethod);
                    I.Brfalse_S(Rを出力して終了);
                    //            L
                    I.Br_S(Lを出力して終了);
                    //        else
                    I.MarkLabel(Rを出力して終了);
                    //            null終了
                    I.Ldloc(R);
                    I.Br_S(終了);
                    //else
                    I.MarkLabel(Lを出力して終了);
                    //    Lはnull終了
                    I.Ldloc(L);
                }
            } else{
                var op_TrueMethod=Binary_Left.Type.GetMethod(op_True);
                var 短絡評価=I.DefineLabel();
                if(op_TrueMethod is not null){
                    //if(op_False(a))return a;
                    //else          return a&b;
                    I.Call(op_TrueMethod);
                    I.Brtrue(短絡評価);
                    this.Traverse(Binary_Left);
                    this.Traverse(Binary.Right);
                    Debug.Assert(Binary.Method is not null);
                    I.Call(Binary.Method!);
                    I.Br_S(終了);
                    I.MarkLabel(短絡評価);
                    this.Traverse(Binary_Left);
                } else{
                    //if(!a)a,false
                    //else  a&b,true&b,b
                    I.Brtrue(短絡評価);
                    this.Traverse(Binary.Right);
                    I.Br_S(終了);
                    I.MarkLabel(短絡評価);
                    I.Ldc_I4_1();
                }
            }
            I.MarkLabel(終了);
        }

        protected override void Equal(BinaryExpression Binary)=>
            this.共通比較演算(Binary,OpCodes.Ceq);
        protected override void NotEqual(BinaryExpression Binary){
            var Binary_Right=Binary.Right;
            var Binary_Left=Binary.Left;
            var Binary_Right_Type=Binary_Right.Type;
            var Binary_Left_Type=Binary_Left.Type;
            var I=this.I!;
            //Debug.Assert(I is not null);
            if(Binary_Right_Type.IsNullable()){
                Debug.Assert(Binary_Left_Type.IsNullable());
                Debug.Assert(Binary_Left_Type==Binary_Right_Type);
                var GetValueOrDefault=Binary_Left_Type.GetMethod(nameof(Nullable<int>.GetValueOrDefault),Type.EmptyTypes);
                Debug.Assert(GetValueOrDefault is not null);
                LocalBuilder Left, Right;
                if(Binary_Right.NodeType==ExpressionType.Try) {
                    Right=this.PrivateTry値を代入した変数((TryExpression)Binary_Right);
                    this.Traverse(Binary.Left);
                    Left=I.M_DeclareLocal_Stloc(Binary_Left_Type);
                    I.Ldloca(Left);
                    I.Call(GetValueOrDefault);
                } else {
                    this.Traverse(Binary_Left);
                    Left=I.M_DeclareLocal_Stloc(Binary_Left_Type);
                    I.Ldloca(Left);
                    I.Call(GetValueOrDefault);
                    this.Traverse(Binary_Right);
                    Right=I.M_DeclareLocal_Stloc(Binary_Right_Type);
                }
                I.Ldloca(Right);
                I.Call(GetValueOrDefault);
                var 一致した = I.DefineLabel();
                if(Binary.Method is not null) {
                    I.Call(Binary.Method);
                    I.Brtrue_S(一致した);
                } else {
                    I.Bne_Un_S(一致した);
                }
                I.Ldloca(Left);
                var get_HasValue= Binary_Left_Type.GetProperty(nameof(Nullable<int>.HasValue))!.GetMethod;
                I.Call(get_HasValue);
                I.Ldloca(Right);
                I.Call(get_HasValue);
                I.Bne_Un_S(一致した);
                I.Ldc_I4_0();
                var 終了 = I.DefineLabel();
                I.Br_S(終了);
                I.MarkLabel(一致した);
                I.Ldc_I4_1();
                I.MarkLabel(終了);
            } else {
                if(Binary_Right.NodeType==ExpressionType.Try){
                    var Try値=this.PrivateTry値を代入した変数((TryExpression)Binary_Right);
                    this.Traverse(Binary_Left);
                    this.I!.Ldloc(Try値);
                } else{
                    this.Traverse(Binary_Left);
                    this.Traverse(Binary_Right);
                }
                if(Binary.Method is not null) {
                    I.Call(Binary.Method);
                } else {
                    I.Ceq();
                    I.M_Cne();
                }
            }
        }
        private void 共通比較演算(BinaryExpression Binary,OpCode OpCode){
            var Binary_Right=Binary.Right;
            var Binary_Left=Binary.Left;
            var Binary_Right_Type=Binary_Right.Type;
            var Binary_Left_Type=Binary_Left.Type;
            var I=this.I;
            Debug.Assert(I is not null);
            if(Binary_Right_Type.IsNullable()){
                Debug.Assert(Binary_Left_Type.IsNullable());
                Debug.Assert(Binary_Left_Type==Binary_Right_Type);
                var GetValueOrDefault=Binary_Left_Type.GetMethod(nameof(Nullable<int>.GetValueOrDefault),Type.EmptyTypes);
                Debug.Assert(GetValueOrDefault is not null);
                LocalBuilder Left, Right;
                if(Binary_Right.NodeType==ExpressionType.Try) {
                    Right=this.PrivateTry値を代入した変数((TryExpression)Binary_Right);
                    this.Traverse(Binary.Left);
                    Left=I.M_DeclareLocal_Stloc(Binary_Left_Type);
                    I.Ldloca(Left);
                    I.Call(GetValueOrDefault);
                } else {
                    this.Traverse(Binary_Left);
                    Left=I.M_DeclareLocal_Stloc(Binary_Left_Type);
                    I.Ldloca(Left);
                    I.Call(GetValueOrDefault);
                    this.Traverse(Binary_Right);
                    Right=I.M_DeclareLocal_Stloc(Binary_Right_Type);
                }
                I.Ldloca(Right);
                I.Call(GetValueOrDefault);
                if(Binary.Method is not null) {
                    I.Call(Binary.Method);
                } else {
                    I.Emit(OpCode);
                }
                var 一致した = I.DefineLabel();
                I.Brtrue_S(一致した);
                I.Ldc_I4_0();
                var 終了 = I.DefineLabel();
                I.Br_S(終了);
                I.MarkLabel(一致した);
                I.Ldloca(Left);
                var get_HasValue= Binary_Left_Type.GetProperty(nameof(Nullable<int>.HasValue))!.GetMethod;
                I.Call(get_HasValue);
                I.Ldloca(Right);
                I.Call(get_HasValue);
                I.Ceq();
                I.MarkLabel(終了);
            } else {
                if(Binary_Right.NodeType==ExpressionType.Try){
                    var Try値=this.PrivateTry値を代入した変数((TryExpression)Binary_Right);
                    this.Traverse(Binary_Left);
                    I.Ldloc(Try値);
                } else{
                    this.Traverse(Binary_Left);
                    this.Traverse(Binary_Right);
                }
                if(Binary.Method is not null){
                    I.Call(Binary.Method);
                } else{
                    I.Emit(OpCode);
                }
            }
        }
        /// <summary>
        /// GreaterThan,LessThanの共通処理
        /// </summary>
        /// <param name="Binary"></param>
        /// <param name="Signed"></param>
        /// <param name="Unsigned"></param>
        /// <returns>NotEqualで呼び出したらfalseを返す</returns>
        private void 共通GreaterThan_LessThan(BinaryExpression Binary,OpCode Signed,OpCode Unsigned){
            if(IsUnsigned(Binary.Left.Type)&&IsUnsigned(Binary.Right.Type)){
                this.共通比較演算(Binary,Unsigned);
            } else{
                this.共通比較演算(Binary,Signed);
            }
        }
        protected override void GreaterThan(BinaryExpression Binary)=>
            this.共通GreaterThan_LessThan(Binary,OpCodes.Cgt,OpCodes.Cgt_Un);
        protected override void LessThan(BinaryExpression Binary)=>
            this.共通GreaterThan_LessThan(Binary,OpCodes.Clt,OpCodes.Clt_Un);
        private void 共通GreaterThanOrEqual_LessThanOrEqual(BinaryExpression Binary,OpCode OpCode){
            Debug.Assert(IsUnsigned(Binary.Left.Type)==IsUnsigned(Binary.Right.Type));
            var Binary_Right=Binary.Right;
            var Binary_Left=Binary.Left;
            var Binary_Right_Type=Binary_Right.Type;
            var Binary_Left_Type=Binary_Left.Type;
            var I=this.I!;
            if(Binary_Right_Type.IsNullable()){
                Debug.Assert(Binary_Left_Type.IsNullable());
                Debug.Assert(Binary_Left_Type==Binary_Right_Type);
                var GetValueOrDefault=Binary_Left_Type.GetMethod(nameof(Nullable<int>.GetValueOrDefault),Type.EmptyTypes);
                Debug.Assert(GetValueOrDefault is not null);
                LocalBuilder Left, Right;
                if(Binary_Right.NodeType==ExpressionType.Try) {
                    Right=this.PrivateTry値を代入した変数((TryExpression)Binary_Right);
                    this.Traverse(Binary.Left);
                    Left=I.M_DeclareLocal_Stloc(Binary_Left_Type);
                    I.Ldloca(Left);
                    I.Call(GetValueOrDefault);
                } else {
                    this.Traverse(Binary_Left);
                    Left=I.M_DeclareLocal_Stloc(Binary_Left_Type);
                    I.Ldloca(Left);
                    I.Call(GetValueOrDefault);
                    this.Traverse(Binary_Right);
                    Right=I.M_DeclareLocal_Stloc(Binary_Right_Type);
                }
                I.Ldloca(Right);
                I.Call(GetValueOrDefault);
                if(Binary.Method is not null) {
                    I.Call(Binary.Method);
                } else {
                    I.Emit(OpCode);
                    I.M_Cne();
                }
                var 一致した = I.DefineLabel();
                I.Brtrue_S(一致した);
                I.Ldc_I4_0();
                var 終了 = I.DefineLabel();
                I.Br_S(終了);
                I.MarkLabel(一致した);
                I.Ldloca(Left);
                var get_HasValue= Binary_Left_Type.GetProperty(nameof(Nullable<int>.HasValue))!.GetMethod;
                I.Call(get_HasValue);
                I.Ldloca(Right);
                I.Call(get_HasValue);
                I.And();
                I.MarkLabel(終了);
            } else {
                if(Binary_Right.NodeType==ExpressionType.Try){
                    var Try値=this.PrivateTry値を代入した変数((TryExpression)Binary_Right);
                    this.Traverse(Binary_Left);
                    I.Ldloc(Try値);
                } else{
                    this.Traverse(Binary_Left);
                    this.Traverse(Binary_Right);
                }
                if(Binary.Method is not null) {
                    I.Call(Binary.Method);
                } else {
                    I.Emit(OpCode);
                    I.M_Cne();
                }
            }
        }
        protected override void GreaterThanOrEqual(BinaryExpression Binary)=>
            this.共通GreaterThanOrEqual_LessThanOrEqual(
                Binary,
                IsUnsigned(Binary.Left.Type)&&
                IsUnsigned(Binary.Right.Type)
                    ?OpCodes.Clt_Un
                    :OpCodes.Clt
            );
        protected override void LessThanOrEqual(BinaryExpression Binary)=>this.共通GreaterThanOrEqual_LessThanOrEqual(
            Binary,IsUnsigned(Binary.Left.Type)&&IsUnsigned(Binary.Right.Type)?OpCodes.Cgt_Un:OpCodes.Cgt);
        protected override void TypeAs(UnaryExpression Unary){
            Debug.Assert(Unary.Method is null);
            this.Traverse(Unary.Operand);
            this.I!.Isinst(Unary.Type);
        }
        protected override void TypeEqual(TypeBinaryExpression TypeBinary){
            //IL_0007: callvirt   System.Type GetType()/System.Object
            //IL_000c: ldtoken    System.String/
            //IL_0011: call       System.Type GetTypeFromHandle(System.RuntimeTypeHandle)/System.Type
            //IL_0016: ceq        
            var I=this.I!;
            var TypeBinary_Expression=TypeBinary.Expression;
            var TypeBinary_Expression_Type=TypeBinary_Expression.Type;
            this.Traverse(TypeBinary_Expression);
            if(TypeBinary_Expression_Type.IsValueType)
                I.Box(TypeBinary_Expression_Type);
            //I.Emit(OpCodes.Callvirt     ,Reflection.Object.GetType_);
            I.Call(Reflection.Object.GetType_);
            //I.Ldtoken(TypeBinary.TypeOperand);
            I.M_Metadata(TypeBinary.TypeOperand);
            I.Ceq();
        }
        protected override void TypeIs(TypeBinaryExpression TypeBinary){
            var I = this.I!;
            var TypeBinary_Expression=TypeBinary.Expression;
            var TypeBinary_Expression_Type = TypeBinary_Expression.Type;
            if(TypeBinary_Expression_Type.IsNullable()&&TypeBinary_Expression_Type.GetGenericArguments()[0]==TypeBinary.TypeOperand) {
                this.PointerTraverseNulllable(TypeBinary_Expression);
                I.Call(TypeBinary_Expression_Type.GetProperty(nameof(Nullable<int>.HasValue))!.GetMethod);
            } else {
                this.Traverse(TypeBinary_Expression);
                if(TypeBinary_Expression_Type.IsValueType)
                    I.Box(TypeBinary_Expression.Type);
                I.Isinst(TypeBinary.TypeOperand);
                I.Ldnull();
                I.Cgt_Un();
            }
        }
        private static readonly Action<ILGenerator> DelegateOnesComplement = I => I.Not();
        protected override void OnesComplement(UnaryExpression Unary)=>this.共通UnaryExpression(Unary,DelegateOnesComplement);
        protected override void Constant(ConstantExpression Constant){
            var I=this.I!;
            if(Constant.Value is null){
                if(Constant.Type.IsNullable()){
                    var Constant_Type=Constant.Type;
                    var Nullable変数=I.DeclareLocal(Constant_Type);
                    I.Ldloca(Nullable変数);
                    I.Initobj(Constant_Type);
                    I.Ldloc(Nullable変数);
                }else I.Ldnull();
            }else if(Constant.Type.IsValueType){
                if(this.DictionaryConstant.TryGetValue(Constant,out var Disp_Member))this.MemberAccess(Disp_Member.Member);
                else{
                    //Expression.Constant(1),Expression.Constant(1,typeof(Object))という可能性もある
                    var Constant_Value = Constant.Value;
                    var Constant_Value_Type = Constant_Value.GetType();
                    var Constant_Value_Type2 = Constant_Value_Type.IsEnum?Enum.GetUnderlyingType(Constant_Value_Type):Constant_Value_Type;
                    Debug.Assert(Constant_Value_Type2.IsValueType);
                    if     (Constant_Value_Type2==typeof(sbyte ))I.Ldc_I4_S((sbyte)Constant_Value);
                    else if(Constant_Value_Type2==typeof(short ))I.Ldc_I4((short)Constant_Value);
                    else if(Constant_Value_Type2==typeof(int   ))I.Ldc_I4((int)Constant_Value);
                    else if(Constant_Value_Type2==typeof(long  ))I.Ldc_I8((long)Constant_Value);
                    else if(Constant_Value_Type2==typeof(IntPtr))
                        if(IntPtr.Size==4)I.Ldc_I4((int)(IntPtr)Constant_Value);
                        else              I.Ldc_I8((long)(IntPtr)Constant_Value);
                    else if(Constant_Value_Type2==typeof(byte  ))I.Ldc_I4((byte)Constant_Value);
                    else if(Constant_Value_Type2==typeof(ushort))I.Ldc_I4((ushort)Constant_Value);
                    else if(Constant_Value_Type2==typeof(uint  ))I.Ldc_I4((int)(uint)Constant_Value);
                    else if(Constant_Value_Type2==typeof(ulong ))I.Ldc_I8((long)(ulong)Constant_Value);
                    else if(Constant_Value_Type2==typeof(UIntPtr))
                        if(UIntPtr.Size==4)I.Ldc_I4((int)(UIntPtr)Constant_Value);
                        else               I.Ldc_I8((long)(UIntPtr)Constant_Value);
                    else if(Constant_Value_Type2==typeof(bool))
                        if((bool)Constant_Value)I.Ldc_I4_1();
                        else                    I.Ldc_I4_0();
                    else if(Constant_Value_Type2==typeof(char  ))I.Ldc_I4((char)Constant_Value);
                    else if(Constant_Value_Type2==typeof(float ))I.Ldc_R4((float)Constant_Value);
                    else if(Constant_Value_Type2==typeof(double))I.Ldc_R8((double)Constant_Value);
                    if(Constant.Type.IsNullable())I.Newobj(Constant.Type.GetConstructors()[0]);
                    else if(!Constant.Type.IsValueType)I.Box(Constant.Type);
                }
            } else{
                if(Constant.Type==typeof(string))I.Ldstr((string)Constant.Value);
                else if(this.DictionaryConstant.TryGetValue(Constant,out var Disp_Member))this.MemberAccess(Disp_Member.Member);
                else throw new NotSupportedException(Constant.Value.ToString());
            }
        }
        protected override void ArrayIndex(BinaryExpression Binary){
            Debug.Assert(Binary.Method is null);
            this.Traverse(Binary.Left);
            this.Traverse(Binary.Right);
            this.I!.Ldelem(Binary.Type);
        }
        /// <summary>
        /// Ldloc Array
        /// Ldlen
        /// </summary>
        /// <param name="Unary"></param>
        protected override void ArrayLength(UnaryExpression Unary){
            Debug.Assert(Unary.Method is null);
            this.Traverse(Unary.Operand);
            this.I!.Ldlen();
        }
        private void ConvertNullableMethod(UnaryExpression Unary){
            var I=this.I!;
            var Unary_Type = Unary.Type;
            var Unary_Operand_Type = Unary.Operand.Type;
            if(Unary_Type.IsNullable()) {
                //(Int32?)****
                var Unary_ElementType = Unary_Type.GetGenericArguments()[0];
                if(Unary_Operand_Type.IsNullable()) {
                    //(Int32?)Double?
                    //Unary.Operand.HasValue?new Unary.Type?(Unary.Operand.GetValueOrDefault()):default(Unary?)
                    var HasValueがfalseだった = I.DefineLabel();
                    var HasValueがtrueだった = I.DefineLabel();
                    var Nullable変数 = I.M_DeclareLocal_Stloc(Unary_Operand_Type);
                    I.Ldloca(Nullable変数);
                    I.Call(Unary_Operand_Type.GetProperty(nameof(Nullable<int>.HasValue))!.GetMethod);
                    I.Brtrue_S(HasValueがtrueだった);
                    I.M_Initobjで値型を初期化してスタックに積む(Unary_Type);
                    I.Br_S(HasValueがfalseだった);
                    I.MarkLabel(HasValueがtrueだった);
                    I.Ldloca(Nullable変数);
                    I.Call(Unary_Operand_Type.GetMethod(nameof(Nullable<int>.GetValueOrDefault),Type.EmptyTypes)!);
                    //(Unary)Unary_Operand
                    //Unary.Operand.HasValue?Unary.Method(Unary.Operand.GetDefaultValue()):default(Unary)
                    if(Unary.Method is not null)I.Call(Unary.Method);
                    //(Unary)Unary_Operand
                    //Unary.Operand.HasValue?new Unary?(Unary.Operand.GetDefaultValue()):default(Unary?)
                    else this.PrimitiveConvert(Unary_Operand_Type,Unary_ElementType);
                    I.Newobj(Unary_Type.GetConstructors()[0]);
                    I.MarkLabel(HasValueがfalseだった);
                } else if(Unary_Operand_Type.IsValueType) {
                    if(Unary.Method is not null) {
                        //Unary.Operand.HasValue?Unary.Method(Unary.Operand.GetDefaultValue()):default(Unary?)
                        I.Call(Unary.Method);
                    } else if(Unary_Operand_Type.IsPrimitive){
                        //Unary.Operand.HasValue?new Unary?(Unary.Operand.GetDefaultValue()):default(Unary?)
                        //Double→Double?もありうる
                        Debug.Assert(Unary_ElementType.IsPrimitive);
                        this.PrimitiveConvert(Unary_Operand_Type,Unary_ElementType);
                    }
                    I.Newobj(Unary_Type.GetConstructors()[0]);
                } else {
                    //(Int32?)Object
                    //Unary.Operand is not null?new Unary?((Unary)Unary.Operand):default(Unary?)
                    var HasValueがfalseだった = I.DefineLabel();
                    var HasValueがtrueだった = I.DefineLabel();
                    var Class変数 = I.M_DeclareLocal_Stloc(Unary_Operand_Type);
                    I.Ldloc(Class変数);
                    I.Brtrue_S(HasValueがtrueだった);
                    var Nullable変数 = I.DeclareLocal(Unary_Type);
                    I.Ldloca(Nullable変数);
                    I.Initobj(Unary_Type);
                    I.Ldloc(Nullable変数);
                    I.Br_S(HasValueがfalseだった);
                    I.MarkLabel(HasValueがtrueだった);
                    I.Ldloc(Class変数);
                    if(Unary.Method is not null)I.Call(Unary.Method);
                    else I.Unbox_Any(Unary_ElementType);
                    I.Newobj(Unary_Type.GetConstructors()[0]);
                    I.MarkLabel(HasValueがfalseだった);
                }
            } else if(Unary_Type.IsValueType) {
                if(Unary_Operand_Type.IsNullable()) {
                    //(UnaUnary.Operand.HasValue
                    var Nullable変数 = I.M_DeclareLocal_Stloc(Unary_Operand_Type);
                    I.Ldloca(Nullable変数);
                    var get_Value=Unary_Operand_Type.GetProperty(nameof(Nullable<int>.Value))!.GetMethod;
                    I.Call(get_Value);
                    //Unary.Operand.HasValue?Unary.Method(Unary.Operand.GetDefaultValue()):default(Unary?)
                    if(Unary.Method is not null)I.Call(Unary.Method);
                    //Unary.Operand.HasValue?new Unary?(Unary.Operand.GetDefaultValue()):default(Unary?)
                    else if(get_Value.ReturnType.IsPrimitive)this.PrimitiveConvert(get_Value.ReturnType,Unary_Type);
                } else if(Unary.Method is not null) {
                    //Unary.Method(Unary.Operand):
                    I.Call(Unary.Method);
                } else if(Unary_Operand_Type.IsPrimitive) {
                    this.PrimitiveConvert(Unary_Operand_Type,Unary.Type);
                } else {
                    //(Unary.Type)Unary.Operand
                    Debug.Assert(!Unary_Operand_Type.IsValueType);
                    I.Unbox_Any(Unary_Type);
                }
            } else {
                if(Unary_Operand_Type.IsNullable()) {
                    //Int32?→Object
                    //(Unary.Type)Unary.Operand
                    var HasValueがfalseだった = I.DefineLabel();
                    var HasValueがtrueだった = I.DefineLabel();
                    var Nullable変数 = I.M_DeclareLocal_Stloc(Unary_Operand_Type);
                    I.Ldloca(Nullable変数);
                    I.Call(Unary_Operand_Type.GetProperty(nameof(Nullable<int>.HasValue))!.GetMethod);
                    I.Brtrue_S(HasValueがtrueだった);
                    I.Ldnull();
                    I.Br_S(HasValueがfalseだった);
                    I.MarkLabel(HasValueがtrueだった);
                    I.Ldloca(Nullable変数);
                    var GetValueOrDefault = Unary_Operand_Type.GetMethod(nameof(Nullable<int>.GetValueOrDefault),Type.EmptyTypes)!;
                    I.Call(GetValueOrDefault);
                    //Unary.Operand.HasValue?Unary.Type.explicit(Unary.Operand.GetValueOrDefault()):default(Unary.Type)
                    if(Unary.Method is not null) I.Call(Unary.Method);
                    //Unary.Operand.HasValue?(Unary.Type)Unary.Operand:null
                    else I.Box(GetValueOrDefault.ReturnType);
                    I.MarkLabel(HasValueがfalseだった);
                } else {
                    if(Unary.Method is not null)                             I.Call(Unary.Method);
                    else if(Unary_Operand_Type.IsValueType)                  I.Box(Unary_Operand_Type);
                    else if(!Unary_Type.IsAssignableFrom(Unary_Operand_Type))I.Castclass(Unary_Type);
                }
            }
        }
        private void PrimitiveConvert(Type 変換元Type,Type 変換先Type) {
            if(変換先Type==typeof(sbyte))this.I!.Conv_I1();
            else if(変換先Type==typeof(short))this.I!.Conv_I2();
            else if(変換先Type==typeof(int))this.I!.Conv_I4();
            else if(変換先Type==typeof(long))this.I!.Conv_I8();
            else if(変換先Type==typeof(IntPtr))this.I!.Conv_I();
            else if(変換先Type==typeof(byte))this.I!.Conv_U1();
            else if(変換先Type==typeof(ushort))this.I!.Conv_U2();
            else if(変換先Type==typeof(char))this.I!.Conv_U2();
            else if(変換先Type==typeof(uint))this.I!.Conv_U4();
            else if(変換先Type==typeof(ulong))this.I!.Conv_U8();
            else if(変換先Type==typeof(UIntPtr))this.I!.Conv_U();
            else if(変換先Type==typeof(float)) {
                if(IsUnsigned(変換元Type))this.I!.Conv_R_Un();
                else this.I!.Conv_R4();
            }else if(変換先Type==typeof(double))this.I!.Conv_R8();
        }
        /// <summary>
        /// (Int32)式
        /// </summary>
        /// <param name="Unary"></param>
        protected override void Convert(UnaryExpression Unary){
            var Unary_Operand =Unary.Operand;
            this.Traverse(Unary_Operand);
            this.ConvertNullableMethod(Unary);
        }
        protected override void ConvertChecked(UnaryExpression Unary){
            this.Traverse(Unary.Operand);
            var Unary_Type=Unary.Type;
            Debug.Assert(Unary.Operand.Type!=Unary_Type);
            if(Unary_Type.IsPrimitive&&Unary.Operand.Type.IsPrimitive){
                if(Unary_Type==typeof(sbyte)){
                    if(IsUnsigned(Unary.Operand.Type))this.I!.Conv_Ovf_I1_Un();
                    else                              this.I!.Conv_Ovf_I1();
                }else if(Unary_Type==typeof(short)){
                    if(IsUnsigned(Unary.Operand.Type)) this.I!.Conv_Ovf_I2_Un();
                    else this.I!.Conv_Ovf_I2();
                }else if(Unary_Type==typeof(int)){
                    if(IsUnsigned(Unary.Operand.Type)) this.I!.Conv_Ovf_I4_Un();
                    else this.I!.Conv_Ovf_I4();
                }else if(Unary_Type==typeof(long)){
                    if(IsUnsigned(Unary.Operand.Type))this.I!.Conv_Ovf_I8_Un();
                    else this.I!.Conv_Ovf_I8();
                }else if(Unary_Type==typeof(IntPtr)){
                    if(IsUnsigned(Unary.Operand.Type))this.I!.Conv_Ovf_I_Un();
                    //UInt32→IntPtr,UIntPtr→IntPtrに変化出来ない。
                    else this.I!.Conv_Ovf_I();
                }else if(Unary_Type==typeof(byte)){
                    if(IsUnsigned(Unary.Operand.Type))this.I!.Conv_Ovf_U1_Un();
                    else this.I!.Conv_Ovf_U1();
                }else if(Unary_Type==typeof(ushort)||Unary_Type==typeof(char)){
                    if(IsUnsigned(Unary.Operand.Type))this.I!.Conv_Ovf_U2_Un();
                    else this.I!.Conv_Ovf_U2();
                }else if(Unary_Type==typeof(uint)){
                    if(IsUnsigned(Unary.Operand.Type)) this.I!.Conv_Ovf_U4_Un();
                    else this.I!.Conv_Ovf_U4();
                }else if(Unary_Type==typeof(ulong)){
                    if(IsUnsigned(Unary.Operand.Type))this.I!.Conv_Ovf_U8_Un();
                    else this.I!.Conv_Ovf_U8();
                }else if(Unary_Type==typeof(UIntPtr)){
                    Debug.Assert(IsUnsigned(Unary.Operand.Type));
                    this.I!.Conv_Ovf_U_Un();
                } else if(Unary_Type==typeof(float)){
                    if(IsUnsigned(Unary.Operand.Type))this.I!.Conv_R_Un();
                    else this.I!.Conv_R4();
                }else{
                    Debug.Assert(Unary_Type==typeof(double));
                    this.I!.Conv_R8();
                }
                return;
            }
            this.ConvertNullableMethod(Unary);
        }
        protected override void Increment(UnaryExpression Unary)=>this.共通IncrementDecrement(Unary,OpCodes.Add);
        protected override void Decrement(UnaryExpression Unary)=>this.共通IncrementDecrement(Unary,OpCodes.Sub);
        private static readonly Action<ILGenerator> DelegateNegate = I => I.Neg();
        protected override void Negate(UnaryExpression Unary)=>this.共通UnaryExpression(Unary,DelegateNegate);
        protected override void NegateChecked(UnaryExpression Unary)=>this.共通UnaryExpression(Unary,DelegateNegate);
        private void Bindings(LocalBuilder Local,ReadOnlyCollection<MemberBinding> Bindings){
            var I=this.I!;
            foreach(var Binding in Bindings){
                I.Ldloc(Local);
                switch(Binding.BindingType){
                    case MemberBindingType.Assignment:{
                        var MemberAssignment=(MemberAssignment)Binding;
                        var MemberAssignment_Expression=MemberAssignment.Expression;
                        this.Traverse(MemberAssignment_Expression);
                        var Binding_Member=Binding.Member;
                        if(MemberAssignment_Expression.Type.IsValueType)
                            if(Binding_Member.MemberType==MemberTypes.Field){
                                Debug.Assert(!((FieldInfo)Binding_Member).IsStatic);
                                I.Stfld((FieldInfo)Binding_Member);
                            } else{
                                Debug.Assert(Binding_Member.MemberType==MemberTypes.Property);
                                this.PrivateCall(((PropertyInfo)Binding_Member).SetMethod!);
                            }
                        else if(Binding_Member.MemberType==MemberTypes.Field){
                            Debug.Assert(!((FieldInfo)Binding_Member).IsStatic);
                            I.Stfld((FieldInfo)Binding_Member);
                        } else{
                            Debug.Assert(Binding_Member.MemberType==MemberTypes.Property);
                            this.PrivateCall(((PropertyInfo)Binding_Member).SetMethod!);
                        }
                        Debug.Assert(Binding_Member.MemberType==MemberTypes.Property||
                                     Binding_Member.MemberType==MemberTypes.Field);
                        break;
                    }
                    case MemberBindingType.MemberBinding:{
                        var MemberMemberBinding=(MemberMemberBinding)Binding;
                        Type Local2Type;
                        var Binding_Member=Binding.Member;
                        if(Binding_Member.MemberType==MemberTypes.Field){
                            var Binding_Field=(FieldInfo)Binding_Member;
                            Local2Type=Binding_Field.FieldType;
                            if(Local2Type.IsValueType){
                                I.Ldflda(Binding_Field);
                                Local2Type=Local2Type.MakeByRefType();
                            } else{
                                I.Ldfld(Binding_Field);
                            }
                        } else{
                            Debug.Assert(Binding_Member.MemberType==MemberTypes.Property);
                            var PropertyInfo =(PropertyInfo)Binding_Member;
                            this.PrivateCall(PropertyInfo.GetMethod!);
                            Local2Type=PropertyInfo.PropertyType;
                            Debug.Assert(Local2Type.IsClass);
                        }
                        var Local2=I.M_DeclareLocal_Stloc(Local2Type);
                        this.Bindings(Local2,MemberMemberBinding.Bindings);
                        break;
                    }
                    case MemberBindingType.ListBinding:{
                        Type LocalType;
                        var Binding_Member=Binding.Member;
                        if(Binding_Member.MemberType==MemberTypes.Field){
                            var FieldInfo=(FieldInfo)Binding_Member;
                            LocalType=FieldInfo.FieldType;
                            if(LocalType.IsValueType){
                                I.Ldflda(FieldInfo);
                                LocalType=LocalType.MakeByRefType();
                            } else{
                                I.Ldfld(FieldInfo);
                            }
                        } else{
                            Debug.Assert(Binding_Member.MemberType==MemberTypes.Property);
                            var PropertyInfo=(PropertyInfo)Binding_Member;
                            this.PrivateCall(PropertyInfo.GetMethod!);
                            LocalType=PropertyInfo.PropertyType;
                            Debug.Assert(LocalType.IsClass);
                        }
                        var MemberListBinding=(MemberListBinding)Binding;
                        var MemberListBinding_Initializers=MemberListBinding.Initializers;
                        if(MemberListBinding_Initializers.Count==1){
                            var MemberListBinding_Initializer=MemberListBinding_Initializers[0];
                            var AddMethod=MemberListBinding_Initializer.AddMethod;
                            this.PrivateCall(AddMethod,MemberListBinding_Initializer.Arguments);
                            if(AddMethod.ReturnType!=typeof(void)){
                                I.Pop();
                            }
                        } else{
                            var Local2=I.M_DeclareLocal_Stloc(LocalType);
                            foreach(var Initializer in MemberListBinding_Initializers){
                                I.Ldloc(Local2);
                                var AddMethod=Initializer.AddMethod;
                                this.PrivateCall(AddMethod,Initializer.Arguments);
                                if(AddMethod.ReturnType!=typeof(void))
                                    I.Pop();
                            }
                        }
                        break;
                    }
                    default:
                        throw new NotSupportedException($"{Binding.BindingType}はサポートされてない");
                }
            }
        }

        protected override void MemberInit(MemberInitExpression MemberInit){
            this.New(MemberInit.NewExpression);
            var MemberInit_Type=MemberInit.Type;
            var I=this.I!;
            var local=I.M_DeclareLocal_Stloc(MemberInit_Type);
            if(MemberInit_Type.IsValueType) {
                I.Ldloca(local);
                var Local1 = I.M_DeclareLocal_Stloc(MemberInit_Type.MakeByRefType());
                this.Bindings(Local1,MemberInit.Bindings);
            } else {
                this.Bindings(local,MemberInit.Bindings);
            }
            I.Ldloc(local);
        }

        protected override void Call(MethodCallExpression MethodCall){
            this.PointerTraverseNulllable(MethodCall.Object);
            this.PrivateCall(MethodCall.Method,MethodCall.Arguments);
        }

        [SuppressMessage("ReSharper","HeuristicUnreachableCode")]
        [SuppressMessage("ReSharper","ConditionIsAlwaysTrueOrFalse")]
        protected override void New(NewExpression New){
            if(New.Constructor is not null){
                this.TraverseExpressions(New.Arguments);
                this.I!.Newobj(New.Constructor);
            } else{
                //値型の引数無しコンストラクタはこれ
                var I=this.I;
                Debug.Assert(I is not null);
                Debug.Assert(New.Arguments.Count==0);
                I.M_Initobjで値型を初期化してスタックに積む(New.Type);
            }
        }

        protected override void NewArrayBound(NewArrayExpression NewArray){
            var NewArray_Expressions=NewArray.Expressions;
            var NewArray_Expressions_Count=NewArray_Expressions.Count;
            if(NewArray_Expressions_Count==1){
                this.Traverse(NewArray_Expressions[0]);
                this.I!.Newarr(NewArray.Type.GetElementType()!);
            } else{
                var Types=new Type[NewArray_Expressions_Count];
                for(var a=0;a<NewArray_Expressions_Count;a++){
                    var NewArray_Expression=NewArray_Expressions[a];
                    Types[a]=NewArray_Expression.Type;
                    this.Traverse(NewArray_Expression);
                }
                this.I!.Newobj(NewArray.Type.GetConstructor(Types)!);
            }
        }

        protected override void NewArrayInit(NewArrayExpression NewArray){
            var I=this.I;
            Debug.Assert(I is not null);
            var NewArray_Expressions=NewArray.Expressions;
            var NewArray_Type=NewArray.Type;
            var NewArray_Expressions_Count=NewArray_Expressions.Count;
            I.Ldc_I4(NewArray_Expressions_Count);
            var ElementType=NewArray_Type.GetElementType();
            I.Newarr(ElementType);
            var Array=I.M_DeclareLocal_Stloc(NewArray_Type);
            for(var a=0;a<NewArray_Expressions_Count;a++){
                I.Ldloc(Array);
                I.Ldc_I4(a);
                this.Traverse(NewArray_Expressions[a]);
                I.Stelem(ElementType);
            }
            I.Ldloc(Array);
        }
        private Label Labelを参照か作成する(LabelTarget LabelTarget){
            Debug.Assert(this.Dictionary_Name_Label is not null);
            if(this.Dictionary_Name_Label.TryGetValue(LabelTarget,out var Label))return Label;
            Label=this.I!.DefineLabel();
            this.Dictionary_Name_Label.Add(LabelTarget,Label);
            return Label;
        }
        private void Default(Type Type){
            if(Type==typeof(void)){ 
            } else if(Type.IsValueType){
                this.I!.M_Initobjで値型を初期化してスタックに積む(Type);
            } else{
                this.I!.Ldnull();
            }
        }
        protected override void Default(DefaultExpression Default)=>this.Default(Default.Type);
        protected override void Goto(GotoExpression Goto){
            this.TraverseNulllable(Goto.Value);
            this.I!.Br(this.Labelを参照か作成する(Goto.Target));
        }
        protected override void Index(IndexExpression Index){
            var Index_Object = Index.Object;
            this.PointerTraverseNulllable(Index_Object);
            this.TraverseExpressions(Index.Arguments);
            //foreach(var Argument in Index.Arguments){
            //    this.Traverse(Argument);
            //}
            if(Index.Indexer is not null) {
                this.PrivateCall(Index.Indexer.GetMethod!);
            }else if(Index.Arguments.Count==1){
                Debug.Assert(Index_Object.Type.GetMethod("Get") is not null);
                this.I!.Ldelem(Index.Type);
            } else{
                this.I!.Call(Index_Object.Type.GetMethod("Get")!);
            }
        }
        protected override void Invoke(InvocationExpression Invocation){
            Debug.Assert(Invocation.Expression.Type.IsClass);
            //Select(p=>p+1).Select(p=>p+1)
            //Select(t=selector).Select(t)
            //foreach(var p in set)
            //    Invoke(t,Invoke(t=selector,p))
            //になる。
            //todo Invokeは先行評価してた場合その評価順が問題になる
            var Invocation_Expression =Invocation.Expression;
            this.Traverse(Invocation_Expression);
            Debug.Assert(Invocation_Expression.Type.GetMethod(nameof(Action.Invoke))!.IsVirtual);
            var Method =Invocation_Expression.Type.GetMethod(nameof(Action.Invoke))!;
            var Invocation_Arguments =Invocation.Arguments;
            var Parameters=Method.GetParameters();
            var Parameters_Length=Parameters.Length;
            Debug.Assert(Parameters_Length==Invocation_Arguments.Count);
            for(var a=0;a<Parameters_Length;a++){
                if(Parameters[a].ParameterType.IsByRef){
                    this.PointerTraverseNulllable(Invocation_Arguments[a]);
                } else{
                    this.Traverse(Invocation_Arguments[a]);
                }
            }
            this.I!.Callvirt(Method);
        }
        protected override void ListInit(ListInitExpression ListInit){
            this.New(ListInit.NewExpression);
            var I=this.I!;
            var ListInit_Type=ListInit.Type;
            var 変数=I.M_DeclareLocal_Stloc(ListInit_Type);
            foreach(var Initializer in ListInit.Initializers){
                if(ListInit_Type.IsValueType)
                    I.Ldloca(変数);
                else
                    I.Ldloc(変数);
                var AddMethod=Initializer.AddMethod;
                this.PrivateCall(AddMethod,Initializer.Arguments);
                if(AddMethod.ReturnType!=typeof(void))
                    I.Pop();
            }
            I.Ldloc(変数);
        }
        protected override void Loop(LoopExpression Loop){
            var I=this.I;
            Debug.Assert(I is not null);
            var ContinueLabel =I.M_DefineLabel_MarkLabel();
            if(Loop.ContinueLabel is not null){
                var Dictionary_Name_Label=this.Dictionary_Name_Label;
                var Loop_ContinueLabel=Loop.ContinueLabel;
                Debug.Assert(Dictionary_Name_Label is not null);
                Dictionary_Name_Label.Add(Loop_ContinueLabel,ContinueLabel);
                if(Loop.BreakLabel is not null){
                    var Loop_BreakLabel=Loop.BreakLabel;
                    var BreakLabel=I.DefineLabel();
                    Dictionary_Name_Label.Add(Loop_BreakLabel,BreakLabel);
                    this.VoidTraverse(Loop.Body);
                    I.Br(ContinueLabel);
                    I.MarkLabel(BreakLabel);
                    Dictionary_Name_Label.Remove(Loop_BreakLabel);
                } else{
                    this.VoidTraverse(Loop.Body);
                    I.Br(ContinueLabel);
                }
                Dictionary_Name_Label.Remove(Loop_ContinueLabel);
            } else if(Loop.BreakLabel is not null){
                var Dictionary_Name_Label=this.Dictionary_Name_Label;
                var Loop_BreakLabel=Loop.BreakLabel;
                var BreakLabel=I.DefineLabel();
                Debug.Assert(Dictionary_Name_Label is not null);
                Dictionary_Name_Label.Add(Loop_BreakLabel,BreakLabel);
                this.VoidTraverse(Loop.Body);
                I.Br(ContinueLabel);
                I.MarkLabel(BreakLabel);
                Dictionary_Name_Label.Remove(Loop_BreakLabel);
            } else{
                this.VoidTraverse(Loop.Body);
                I.Br(ContinueLabel);
            }
        }
        //protected override void Block(BlockExpression Block) {
        //    var Dictionary_Parameter_LocalBuilder = this.Dictionary_Parameter_LocalBuilder;
        //    Debug.Assert(Dictionary_Parameter_LocalBuilder is not null);
        //    var Block_Expressions = Block.Expressions;
        //    var I = this.I!;
        //    foreach(var Block_Variable in Block.Variables)
        //        Dictionary_Parameter_LocalBuilder.Add(Block_Variable,I.DeclareLocal(Block_Variable.Type));
        //    var Block_Expressions_Count_1 = Block_Expressions.Count-1;
        //    for(var a = 0;a<Block_Expressions_Count_1;a++)
        //        this.VoidTraverse(Block_Expressions[a]);
        //    //Blockは最後の式の型を返す。それを有効にする。
        //    this.Traverse(Block_Expressions[Block_Expressions_Count_1]);
        //    foreach(var Block_Variable in Block.Variables)
        //        Dictionary_Parameter_LocalBuilder.Remove(Block_Variable);
        //}
        protected override void Parameter(ParameterExpression Parameter) {
            var I = this.I!;
            var index = this.Parameters!.IndexOf(Parameter);
            if(index>=0) {
                if(this.インスタンスメソッドか)index++;
                I.Ldarg((ushort)index);
                if(Parameter.IsByRef)I.Ldobj(Parameter.Type);
            } else {
                if(this.Dictionaryラムダ跨ぎParameter.TryGetValue(Parameter,out var Disp_Member)) {
                    this.MemberAccess(Disp_Member.Member);
                } else if(this.DispParameter==Parameter){
                    I.Ldarg_0();
                } else {
                    Debug.Assert(this.Dictionary_Parameter_LocalBuilder is not null);
                    I.Ldloc(this.Dictionary_Parameter_LocalBuilder[Parameter]);
                }
            }
        }
        private void PointerAssign(BinaryExpression Assign){
            var Assign_Left=Assign.Left;
            var I=this.I!;
            var NodeType=Assign_Left.NodeType;
            var Assign_Type=Assign.Type;
            if(Assign_Type.IsValueType){
                switch(NodeType){
                    case ExpressionType.Index: {
                        var Index = (IndexExpression)Assign_Left;
                        var Index_Object = Index.Object;
                        this.PointerTraverseNulllable(Index_Object);
                        this.TraverseExpressions(Index.Arguments);
                        //foreach(var Argument in Index.Arguments)
                          //  this.Traverse(Argument);
                        var Index_Object_Type = Index_Object.Type;
                        Debug.Assert(Index_Object_Type.IsArray);
                        Debug.Assert(Index.Indexer is null);
                        Debug.Assert(Index.Arguments.Count==Index_Object_Type.GetArrayRank());
                        var Index_Arguments =Index.Arguments;
                        if(Index_Arguments.Count==1){
                            I.Ldelema(Index.Type);
                            I.Dup();
                            this.Traverse(Assign.Right);
                            I.Stobj(Assign_Type);
                        } else {
                            Debug.Assert(Index.Indexer is null);
                            Debug.Assert(Index_Object_Type.GetMethod("Set")!.GetParameters().Length==Index.Arguments.Count+1);
                            this.Traverse(Assign.Right);
                            var Local = I.M_DeclareLocal_Stloc_Ldloc(Assign_Type);
                            I.Call(Index_Object_Type.GetMethod("Set"));
                            I.Ldloca(Local);
                        }
                        break;
                    }
                    case ExpressionType.MemberAccess:{
                        var Member=(MemberExpression)Assign_Left;
                        var Member_Member=Member.Member;
                        if(Member_Member.MemberType==MemberTypes.Field){
                            var Member_Field=(FieldInfo)Member_Member;
                            if(Member_Field.IsStatic) {
                                I.Ldsflda(Member_Field);
                            } else {
                                this.PointerTraverseNulllable(Member.Expression);
                                I.Ldflda(Member_Field);
                            }
                            I.Dup();
                            this.Traverse(Assign.Right);
                            I.Stobj(Assign_Type);
                        } else{
                            var Member_Property=(PropertyInfo)Member_Member;
                            this.PointerTraverseNulllable(Member.Expression);
                            this.Traverse(Assign.Right);
                            var Local=I.M_DeclareLocal_Stloc_Ldloc(Assign_Type);
                            this.PrivateCall(Member_Property.SetMethod!);
                            I.Ldloca(Local);
                        }
                        break;
                    }
                    default:{
                        Debug.Assert(NodeType==ExpressionType.Parameter);
                        var Parameter=(ParameterExpression)Assign_Left;
                        //Debug.Assert(this.Parameters is not null);
                        var index =this.Parameters!.IndexOf(Parameter);
                        if(index>=0) {
                            if(this.インスタンスメソッドか) index++;
                            if(Parameter.IsByRef) {
                                I.Ldarg((ushort)index);
                                I.Dup();
                                this.Traverse(Assign.Right);
                                I.Stobj(Assign_Type);
                            } else {
                                this.Traverse(Assign.Right);
                                I.Starg((ushort)index);
                                I.Ldarga((ushort)index);
                            }
                        } else {
                            this.Traverse(Assign.Right);
                            Debug.Assert(this.Dictionary_Parameter_LocalBuilder is not null);
                            var Local = this.Dictionary_Parameter_LocalBuilder[Parameter];
                            I.Stloc(Local);
                            I.Ldloca(Local);
                        }
                        break;
                    }
                }
            } else{
                switch(NodeType){
                    case ExpressionType.Index: {
                        var Index = (IndexExpression)Assign_Left;
                        var Index_Object = Index.Object;
                        this.PointerTraverseNulllable(Index_Object);
                        this.TraverseExpressions(Index.Arguments);
                        //foreach(var Argument in Index.Arguments) {
                        //    this.Traverse(Argument);
                        //}
                        var Index_Object_Type = Index_Object.Type;
                        Debug.Assert(Index_Object_Type.IsArray);
                        Debug.Assert(Index.Indexer is null);
                        Debug.Assert(Index.Arguments.Count==Index_Object_Type.GetArrayRank());
                        this.Traverse(Assign.Right);
                        var Local = I.M_DeclareLocal_Stloc_Ldloc(Assign_Type);
                        var Index_Arguments = Index.Arguments;
                        if(Index_Arguments.Count==1) {
                            I.Stelem(Index.Type);
                        } else {
                            Debug.Assert(Index.Indexer is null);
                            Debug.Assert(Index_Object_Type.GetMethod("Set")!.GetParameters().Length==Index.Arguments.Count+1);
                            I.Call(Index_Object_Type.GetMethod("Set"));
                        }
                        I.Ldloc(Local);
                        break;
                    }
                    case ExpressionType.MemberAccess:{
                        this.Traverse(Assign.Right);
                        var Local=I.M_DeclareLocal_Stloc(Assign_Type);
                        var Member=(MemberExpression)Assign_Left;
                        var Member_Member=Member.Member;
                        if(Member_Member.MemberType==MemberTypes.Field){
                            var Member_Field=(FieldInfo)Member_Member;
                            if(Member_Field.IsStatic){
                                I.Ldloc(Local);
                                I.Stsfld(Member_Field);
                            } else{
                                this.PointerTraverseNulllable(Member.Expression);
                                I.Ldloc(Local);
                                I.Stfld(Member_Field);
                            }
                        } else{
                            var Member_Property=(PropertyInfo)Member_Member;
                            this.PointerTraverseNulllable(Member.Expression);
                            I.Ldloc(Local);
                            this.PrivateCall(Member_Property.SetMethod!);
                        }
                        I.Ldloc(Local);
                        break;
                    }
                    default:{
                        Debug.Assert(NodeType==ExpressionType.Parameter);
                        var Parameter =(ParameterExpression)Assign_Left;
                        var index =this.Parameters!.IndexOf(Parameter);
                        if(index>=0){
                            if(this.インスタンスメソッドか) index++;
                            if(Parameter.IsByRef){
                                I.Ldarg((ushort)index);
                                this.Traverse(Assign.Right);
                                var Local=I.M_DeclareLocal_Stloc_Ldloc(Assign_Type);
                                I.Stobj(Assign_Type);
                                I.Ldloc(Local);
                            } else{
                                this.Traverse(Assign.Right);
                                I.Starg((ushort)index);
                                I.Ldarg((ushort)index);
                            }
                        } else{
                            this.Traverse(Assign.Right);
                            Debug.Assert(this.Dictionary_Parameter_LocalBuilder is not null);
                            var Local=this.Dictionary_Parameter_LocalBuilder[Parameter];
                            I.Stloc(Local);
                            I.Ldloc(Local);
                        }
                        break;
                    }
                }
            }
        }

        private void PointerTraverseNulllable(Expression? e){
            if(e is null)
                return;
            // ReSharper disable once SwitchStatementMissingSomeCases
            switch(e.NodeType){
                case ExpressionType.ArrayIndex:{
                    var Binary=(BinaryExpression)e;
                    this.Traverse(Binary.Left);
                    this.Traverse(Binary.Right);
                    Debug.Assert(Binary.Method is null);
                    var e_Type=e.Type;
                    if(e_Type.IsValueType)
                        this.I!.Ldelema(e_Type);
                    else
                        this.I!.Ldelem(e_Type);
                    break;
                }
                case ExpressionType.Assign:{
                    this.PointerAssign((BinaryExpression)e);
                    break;
                }
                case ExpressionType.MemberAccess:{
                    var Member=(MemberExpression)e;
                    var Member_Member=Member.Member;
                    if(Member_Member.MemberType==MemberTypes.Field){
                        var Member_Field=(FieldInfo)Member_Member;
                        if(Member_Field.IsStatic){
                            if(Member.Type.IsValueType) {
                                this.I!.Ldsflda(Member_Field);
                            } else {
                                this.I!.Ldsfld(Member_Field);
                            }
                        } else{
                            this.PointerTraverseNulllable(Member.Expression);
                            if(Member.Type.IsValueType) {
                                this.I!.Ldflda(Member_Field);
                            } else {
                                this.I!.Ldfld(Member_Field);
                            }
                        }
                    } else{
                        var Member_Expression = Member.Expression;
                        this.PointerTraverseNulllable(Member_Expression);
                        this.PrivateCall(((PropertyInfo)Member_Member).GetMethod!);
                        this.I!.M_Valueは参照(e.Type);
                    }
                    break;
                }
                case ExpressionType.Parameter:{
                    this.PointerParameterNulllable((ParameterExpression)e);
                    //var Parameter=(ParameterExpression)e;
                    //var index =this.Parameters!.IndexOf(Parameter);
                    //if(index>=0) {
                    //    if(this.RootConstant is not null) {
                    //        index++;
                    //    }
                    //    if(Parameter.Type.IsValueType) {
                    //        this.I!.Ldarga((UInt16)index);
                    //    } else {
                    //        this.I!.Ldarg((UInt16)index);
                    //    }
                    //} else {
                    //    if(Parameter.Type.IsValueType) {
                    //        this.I!.Ldloca(this.DictionaryParameterLocal[Parameter]);
                    //    } else {
                    //        this.I!.Ldloc(this.DictionaryParameterLocal[Parameter]);
                    //    }
                    //}
                    break;
                }
                case ExpressionType.Unbox:{
                    var Unary=(UnaryExpression)e;
                    this.Traverse(Unary.Operand);
                    Debug.Assert(Unary.Type.IsValueType);
                    this.I!.Unbox(Unary.Type);
                    break;
                }
                default:{
                    this.Traverse(e);
                    this.I!.M_Valueは参照(e.Type);
                    break;
                }
            }
        }
        protected override void MemberAccess(MemberExpression Member){
            var Member_Expression=Member.Expression;
            this.PointerTraverseNulllable(Member_Expression);
            var Member_Member=Member.Member;
            if(Member_Member.MemberType==MemberTypes.Field){
                var Member_Field=(FieldInfo)Member_Member;
                if(Member_Field.IsStatic)
                    this.I!.Ldsfld(Member_Field);
                else
                    this.I!.Ldfld(Member_Field);
            } else{
                this.PrivateCall(((PropertyInfo)Member_Member).GetMethod!);
            }
        }
        protected override void Switch(SwitchExpression Switch){
            var Switch_SwitchValue=Switch.SwitchValue;
            this.Traverse(Switch_SwitchValue);
            var I=this.I!;
            var EndSwitch =I.DefineLabel();
            //ジャンプテーブルは余裕があれば実装する
            var Switch_SwitchValue_Type=Switch_SwitchValue.Type;
            if(Switch_SwitchValue_Type.IsEnum)
                Switch_SwitchValue_Type=Switch_SwitchValue_Type.GetEnumUnderlyingType();
            var Switch_Comparison=Switch.Comparison;
            if(Switch_Comparison is not null){
                var Switch_SwitchValue変数=I.M_DeclareLocal_Stloc(Switch_SwitchValue_Type);
                foreach(var Switch_Case in Switch.Cases){
                    var Switch_Case_TestValues=Switch_Case.TestValues;
                    var 次のCase=I.DefineLabel();
                    var Case条件に一致した=I.DefineLabel();
                    var Case_TestValues_Count_1=Switch_Case_TestValues.Count-1;
                    for(var a=0;a<Case_TestValues_Count_1;a++){
                        I.Ldloc(Switch_SwitchValue変数);
                        this.Traverse(Switch_Case_TestValues[a]);
                        I.Call(Switch_Comparison);
                        I.Brtrue(Case条件に一致した);
                    }
                    I.Ldloc(Switch_SwitchValue変数);
                    this.Traverse(Switch_Case_TestValues[Case_TestValues_Count_1]);
                    I.Call(Switch_Comparison);
                    I.Brfalse(次のCase);
                    I.MarkLabel(Case条件に一致した);
                    this.Traverse(Switch_Case.Body);
                    I.Br(EndSwitch);
                    I.MarkLabel(次のCase);
                }
            } else{
                if(
                    Switch_SwitchValue_Type==typeof(byte)||Switch_SwitchValue_Type==typeof(sbyte)||
                    Switch_SwitchValue_Type==typeof(ushort)||Switch_SwitchValue_Type==typeof(short)||
                    Switch_SwitchValue_Type==typeof(uint)||Switch_SwitchValue_Type==typeof(int)
                ){
                    const int Case数下限=4,Case数上限=256;
                    var 最小=int.MaxValue;
                    var 最大=int.MinValue;
                    var SortedDictionaryExpression=new SortedDictionary<int,Expression>();
                    foreach(var Switch_Case in Switch.Cases){
                        var Switch_Case_Body=Switch_Case.Body;
                        foreach(var Switch_Case_TestValue in Switch_Case.TestValues){
                            if(Switch_Case_TestValue.NodeType!=ExpressionType.Constant)
                                goto CaseにConstant以外が含まれていた場合;
                            dynamic Value=((ConstantExpression)Switch_Case_TestValue).Value!;
                            var 値=(int)Value;
                            if(最小>値)
                                最小=値;
                            if(最大<値)
                                最大=値;
                            SortedDictionaryExpression.Add(値,Switch_Case_Body);
                        }
                    }
                    var Case数=最大-最小+1;
                    if(Case数 is >=Case数下限 and <=Case数上限){
                        var Expressions=new Expression?[Case数];
                        foreach(var Expression in SortedDictionaryExpression)
                            Expressions[Expression.Key-最小]=Expression.Value;
                        I.Ldc_I4(最小);
                        I.Sub();
                        var JumpTables=new Label[Case数];
                        for(var a = 0;a<Case数;a++)
                            JumpTables[a]=I.DefineLabel();
                        I.Switch(JumpTables);
                        this.Traverse(Switch.DefaultBody);
                        I.Br(EndSwitch);
                        for(var a=0;a<Case数-1;a++){
                            I.MarkLabel(JumpTables[a]);
                            var Expression=Expressions[a];
                            if(Expression is null)continue;//case値が離散的でスカスカの場合
                            this.Traverse(Expression);
                            I.Br(EndSwitch);
                        }
                        I.MarkLabel(JumpTables[Case数-1]);
                        this.Traverse(Expressions[Case数-1]);
                        I.MarkLabel(EndSwitch);
                        return;
                    }
                }
                CaseにConstant以外が含まれていた場合:
                var Switch_SwitchValue変数=I.M_DeclareLocal_Stloc(Switch_SwitchValue_Type);
                foreach(var Switch_Case in Switch.Cases){
                    var Switch_Case_TestValues=Switch_Case.TestValues;
                    var 次のCase=I.DefineLabel();
                    var Case条件に一致した=I.DefineLabel();
                    var Case_TestValues_Count_1=Switch_Case_TestValues.Count-1;
                    for(var a=0;a<Case_TestValues_Count_1;a++){
                        I.Ldloc(Switch_SwitchValue変数);
                        this.Traverse(Switch_Case_TestValues[a]);
                        I.Beq(Case条件に一致した);
                    }
                    I.Ldloc(Switch_SwitchValue変数);
                    this.Traverse(Switch_Case_TestValues[Case_TestValues_Count_1]);
                    I.Bne_Un(次のCase);
                    I.MarkLabel(Case条件に一致した);
                    this.Traverse(Switch_Case.Body);
                    I.Br(EndSwitch);
                    I.MarkLabel(次のCase);
                }
            }
            this.Traverse(Switch.DefaultBody);
            I.MarkLabel(EndSwitch);
        }
        private void PrivateFilter1(CatchBlock Try_Handler){
            var I = this.I!;
            var Isinst = I.DefineLabel();
            I.Brtrue(Isinst);
            I.Ldc_I4_0();
            var endfilter = I.DefineLabel();
            I.Br(endfilter);
            I.MarkLabel(Isinst);
            this.Traverse(Try_Handler.Filter);
            I.MarkLabel(endfilter);
            //I.Endfilter();
            I.BeginCatchBlock(null);
        }
        private void PrivateFilter0(CatchBlock Try_Handler,LocalBuilder Variable) {
            //throw new NotSupportedException(Properties.Resources.DynamicMethodでFilterはサポートされていない);
            var I = this.I!;
            I.BeginExceptFilterBlock();
            //I.Stloc(Variable);
            //I.Ldloc(Variable);
            I.Isinst(Try_Handler.Test);
            I.Dup();
            //this.PrivateFilter1(Try_Handler);
            var Isinst = I.DefineLabel();
            I.Brtrue(Isinst);
            I.Pop();//Dup,Popは不要では？
            I.Ldc_I4_0();
            var endfilter = I.DefineLabel();
            I.Br(endfilter);
            I.MarkLabel(Isinst);
            I.Stloc(Variable);
            //I.Ldloc(Variable);
            this.Traverse(Try_Handler.Filter);
            I.MarkLabel(endfilter);
            //I.Endfilter();
            I.BeginCatchBlock(null);
            //var Isinst = I.DefineLabel();
            //I.Brtrue(Isinst);
            //I.Ldc_I4_0();//理由不明
            //var endfilter = I.DefineLabel();
            //I.Br(endfilter);
            //I.MarkLabel(Isinst);
            //this.Traverse(Try_Handler.Filter);
            //I.MarkLabel(endfilter);
            //I.Endfilter();
            ////filterがある場合はcatch変数がなくpushされない
            //I.BeginCatchBlock(null);
        }
        private void PrivateFilter0(CatchBlock Try_Handler) {
            //throw new NotSupportedException(Properties.Resources.DynamicMethodでFilterはサポートされていない);
            var I = this.I!;
            I.BeginExceptFilterBlock();
            I.Isinst(Try_Handler.Test);
            I.Dup();
            //this.PrivateFilter1(Try_Handler);
            var Isinst = I.DefineLabel();
            I.Brtrue(Isinst);
            I.Ldc_I4_0();
            var endfilter = I.DefineLabel();
            I.Br(endfilter);
            I.MarkLabel(Isinst);
            I.Pop();
            this.Traverse(Try_Handler.Filter);
            I.MarkLabel(endfilter);
            //I.Endfilter();
            I.BeginCatchBlock(null);
        }
        protected abstract void ProtectedFault(Expression? Fault);
        /// <summary>
        /// Filter,Catchブロック
        /// </summary>
        /// <param name="Try_Handler"></param>
        private void PrivateTryFilterCatch(CatchBlock Try_Handler){
            var I =this.I!;
            var Dictionary_Parameter_LocalBuilder =this.Dictionary_Parameter_LocalBuilder;
            Debug.Assert(Dictionary_Parameter_LocalBuilder is not null);
            if(Try_Handler.Variable is not null){
                var Try_Handler_Variable=Try_Handler.Variable;
                var Variable=I.DeclareLocal(Try_Handler_Variable.Type);
                Dictionary_Parameter_LocalBuilder.Add(Try_Handler_Variable,Variable);
                if(Try_Handler.Filter is null){
                    I.BeginCatchBlock(Try_Handler.Test);
                    I.Stloc(Variable);
                } else{
                    this.PrivateFilter0(Try_Handler,Variable);
                    I.Pop();
                }
                this.Traverse(Try_Handler.Body);
                Dictionary_Parameter_LocalBuilder.Remove(Try_Handler_Variable);
            } else{
                //if(Try_Handler.Filter is null){
                //    I.BeginCatchBlock(Try_Handler.Test);
                //    I.Pop();
                //} else
                //    this.ProtectedFilter(Try_Handler);
                //この時点で例外オブジェクトがpushされている。変数はないのでポップする
                if(Try_Handler.Filter is null){
                    I.BeginCatchBlock(Try_Handler.Test);
                    I.Pop();
                } else
                    this.PrivateFilter0(Try_Handler);
                this.Traverse(Try_Handler.Body);
            }
        }
        private LocalBuilder PrivateTry値を代入した変数(TryExpression Try){
            Debug.Assert(Try.Type!=typeof(void));
            var I=this.I!;
            var 変数 =I.DeclareLocal(Try.Type);
            var Leave先0=I.DefineLabel();
            I.BeginExceptionBlock();
            if(Try.Finally is not null) {
                if(Try.Handlers.Count>0) {
                    I.BeginExceptionBlock();
                    this.Traverse(Try.Body);
                    I.Stloc(変数);
                    var Leave先1 = I.DefineLabel();
                    //I.Leave(Leave先1);
                    foreach(var Try_Handler in Try.Handlers) {
                        this.PrivateTryFilterCatch(Try_Handler);
                        I.Stloc(変数);
                        //I.Leave(Leave先1);
                    }
                    I.EndExceptionBlock();
                    I.MarkLabel(Leave先1);
                } else {
                    this.Traverse(Try.Body);
                    I.Stloc(変数);
                }
                //I.Leave(Leave先0);
                I.BeginFinallyBlock();
                this.VoidTraverse(Try.Finally);
                //I.Endfinally();
            } else {
                Debug.Assert(Try.Handlers.Count>0);
                this.Traverse(Try.Body);
                I.Stloc(変数);
                foreach(var Try_Handler in Try.Handlers){
                    this.PrivateTryFilterCatch(Try_Handler);
                    I.Stloc(変数);
                }
            }
            I.EndExceptionBlock();
            I.MarkLabel(Leave先0);
            //if(Try.Finally is not null){
            //    if(Try.Handlers.Count>0){
            //        I.BeginExceptionBlock();
            //        var Leave先0=I.DefineLabel();
            //        {
            //            I.BeginExceptionBlock();
            //            this.Traverse(Try.Body);
            //            I.Stloc(変数);
            //            var Leave先1=I.DefineLabel();
            //            I.Leave(Leave先1);
            //            foreach(var Try_Handler in Try.Handlers){
            //                this.PrivateTryFilterCatch(Try_Handler);
            //                I.Pop();
            //                I.Leave(Leave先1);
            //            }
            //            I.EndExceptionBlock();
            //            I.MarkLabel(Leave先1);
            //        }
            //        I.BeginFinallyBlock();
            //        this.VoidTraverse(Try.Finally);
            //        I.Endfinally();
            //        I.EndExceptionBlock();
            //        I.MarkLabel(Leave先0);
            //    } else{
            //        I.BeginExceptionBlock();
            //        this.Traverse(Try.Body);
            //        I.Stloc(変数);
            //        var Leave先0=I.DefineLabel();
            //        I.Leave(Leave先0);
            //        I.BeginFinallyBlock();
            //        this.VoidTraverse(Try.Finally);
            //        I.Endfinally();
            //        I.EndExceptionBlock();
            //        I.MarkLabel(Leave先0);
            //    }
            //} else{
            //    Debug.Assert(Try.Handlers.Count>0);
            //    {
            //        I.BeginExceptionBlock();
            //        this.Traverse(Try.Body);
            //        I.Stloc(変数);
            //        var Leave先0=I.DefineLabel();
            //        I.Leave(Leave先0);
            //        foreach(var Try_Handler in Try.Handlers){
            //            this.PrivateTryFilterCatch(Try_Handler);
            //            I.Pop();
            //            I.Leave(Leave先0);
            //        }
            //        I.EndExceptionBlock();
            //        I.MarkLabel(Leave先0);
            //    }
            //}
            return 変数;
            /*
            var 変数 =I.DeclareLocal(Try.Type);
            I.BeginExceptionBlock();
            this.Traverse(Try.Body);
            I.Stloc(変数);
            var Leave先=I.DefineLabel();
            I.Leave(Leave先);
            foreach(var Try_Handler in Try.Handlers){
                this.PrivateTryFilterCatch(Try_Handler);
                I.Stloc(変数);
                I.Leave(Leave先);
            }
            this.ProtectedFault(Try.Fault);
            //I.EndExceptionBlock();
            if(Try.Finally is not null){
                I.BeginFinallyBlock();
                this.VoidTraverse(Try.Finally);//戻り値はfinallyにない。
                I.Endfinally();
                //I.EndExceptionBlock();
            }
            I.EndExceptionBlock();
            I.MarkLabel(Leave先);
            return 変数;
            */
        }
        protected override void Try(TryExpression Try){
            //ILレベルではtry～catch,catch,catch
            //try～finallyしかない
            //try～catch～finallyはtry{try～catch}finallにネストする
            //tryのleeaveはcatchの後ろかfinallyの後ろ
            var I =this.I!;
            if(Try.Type!=typeof(void)){
                I.Ldloc(this.PrivateTry値を代入した変数(Try));
            } else{
                //if(Try.Finally is not null){
                //    I.BeginExceptionBlock();
                //}
                //if(Try.Finally is not null)
                //    I.BeginExceptionBlock();
                I.BeginExceptionBlock();
                this.Traverse(Try.Body);
                I.Pop();
                var Leave先=I.DefineLabel();
                I.Leave(Leave先);
                foreach(var Try_Handler in Try.Handlers){
                    this.PrivateTryFilterCatch(Try_Handler);
                    I.Pop();
                    I.Leave(Leave先);
                }
                this.ProtectedFault(Try.Fault);
                //I.EndExceptionBlock();
                if(Try.Finally is not null){
                    //Debug.Assert(this.I is not null);
                    I.BeginFinallyBlock();
                    this.VoidTraverse(Try.Finally);
                    I.Endfinally();
                    //I.EndExceptionBlock();
                }
                I.EndExceptionBlock();
                //Debug.Assert(this.I is not null);
                I.MarkLabel(Leave先);
            }
        }
        protected override void Label(LabelExpression Label){
            this.TraverseNulllable(Label.DefaultValue);
            //Debug.Assert(this.I is not null);
            this.I!.MarkLabel(this.Labelを参照か作成する(Label.Target));
        }
        protected override void IsFalse(UnaryExpression Unary){
            var Unary_Operand=Unary.Operand;
            var Unary_Operand_Type=Unary_Operand.Type;
            var I=this.I!;
            // Debug.Assert(I is not null);
            this.Traverse(Unary_Operand);
            if(Unary_Operand_Type.IsNullable()){
                var Nullable変数=I.M_DeclareLocal_Stloc(Unary_Operand_Type);
                I.Ldloca(Nullable変数);
                I.Call(Unary_Operand_Type.GetProperty(nameof(Nullable<int>.HasValue))!.GetMethod);
                var HasValueがfalseだった=I.DefineLabel();
                var HasValueがtrueだった=I.DefineLabel();
                I.Brtrue_S(HasValueがtrueだった);
                I.Ldloc(Nullable変数);
                I.Br_S(HasValueがfalseだった);
                I.MarkLabel(HasValueがtrueだった);
                I.Ldloca(Nullable変数);
                I.Call(Unary_Operand_Type.GetMethod(nameof(Nullable<int>.GetValueOrDefault),Type.EmptyTypes)!);
                if(Unary.Method is not null){
                    I.Call(Unary.Method);
                } else{
                    I.M_Cne();
                }
                I.Newobj(Unary.Type.GetConstructors()[0]);
                I.MarkLabel(HasValueがfalseだった);
            } else if(Unary.Method is not null){
                I.Call(Unary.Method);
            } else{
                I.M_Cne();
            }
        }
        protected override void IsTrue(UnaryExpression Unary){
            var Unary_Operand=Unary.Operand;
            var Unary_Operand_Type=Unary_Operand.Type;
            var I=this.I!;
            //Debug.Assert(I is not null);
            this.Traverse(Unary_Operand);
            if(Unary_Operand_Type.IsNullable()){
                var Nullable変数=I.M_DeclareLocal_Stloc(Unary_Operand_Type);
                I.Ldloca(Nullable変数);
                I.Call(Unary_Operand_Type.GetProperty(nameof(Nullable<int>.HasValue))!.GetMethod);
                var HasValueがfalseだった=I.DefineLabel();
                var HasValueがtrueだった=I.DefineLabel();
                I.Brtrue_S(HasValueがtrueだった);
                I.Ldloc(Nullable変数);
                I.Br_S(HasValueがfalseだった);
                I.MarkLabel(HasValueがtrueだった);
                I.Ldloca(Nullable変数);
                I.Call(Unary_Operand_Type.GetMethod(nameof(Nullable<int>.GetValueOrDefault),Type.EmptyTypes)!);
                if(Unary.Method is not null)
                    I.Call(Unary.Method);
                I.Newobj(Unary.Type.GetConstructors()[0]);
                I.MarkLabel(HasValueがfalseだった);
            }else if(Unary.Method is not null){
                I.Call(Unary.Method);
            }
        }
        protected override void Not(UnaryExpression Unary) => this.共通UnaryExpression(
            Unary,
            I => {
                if(Unary.Type==typeof(bool)) {
                    I.M_Cne();
                } else {
                    I.Not();
                }
            }
        );
        protected override void Throw(UnaryExpression Unary){
            Debug.Assert(Unary.Method is null);
            this.Traverse(Unary.Operand);
            this.I!.Throw();
            this.Default(Unary.Type);
        }
        private static readonly Action<ILGenerator> I_Empty=_=>{};
        protected override void UnaryPlus(UnaryExpression Unary)=>this.共通UnaryExpression(Unary,I_Empty);
        protected override void Unbox(UnaryExpression Unary){
            Debug.Assert(Unary.Method is null);
            this.Traverse(Unary.Operand);
            this.I!.Unbox_Any(Unary.Type);
        }
        protected override void Coalesce(BinaryExpression Binary){
            var Binary_Left=Binary.Left;
            this.PointerTraverseNulllable(Binary_Left);
            var Binary_Left_Type=Binary_Left.Type;
            var I=this.I!;
            //Debug.Assert(I is not null);
            var HasValueがfalseだった =I.DefineLabel();
            if(Binary_Left_Type.IsValueType){
                var Nullable変数=I.M_DeclareLocal_Stloc(Binary_Left_Type.MakeByRefType());
                I.Ldloc(Nullable変数);
                I.Call(Binary_Left_Type.GetProperty(nameof(Nullable<int>.HasValue))!.GetMethod);
                var HasValueがtrueだった=I.DefineLabel();
                I.Brtrue(HasValueがtrueだった);
                Debug.Assert(Binary_Left_Type.IsNullable());
                this.Traverse(Binary.Right);
                I.Br_S(HasValueがfalseだった);
                I.MarkLabel(HasValueがtrueだった);
                I.Ldloc(Nullable変数);
                I.Call(Binary_Left_Type.GetMethod(nameof(Nullable<int>.GetValueOrDefault),Type.EmptyTypes)!);
            } else{
                var Class変数=I.M_DeclareLocal_Stloc(Binary_Left_Type);
                I.Ldloc(Class変数);
                var 値があった時=I.DefineLabel();
                I.Brtrue(値があった時);
                this.Traverse(Binary.Right);
                I.Br_S(HasValueがfalseだった);
                I.MarkLabel(値があった時);
                I.Ldloc(Class変数);
            }
            I.MarkLabel(HasValueがfalseだった);
        }
        protected override void Conditional(ConditionalExpression Conditional){
            this.Traverse(Conditional.Test);
            var I = this.I!;
            //Debug.Assert(I is not null);
            var EndIf = I.DefineLabel();
            var IfFalse = I.DefineLabel();
            I.Brfalse(IfFalse);
            if(Conditional.Type==typeof(void)){
                this.VoidTraverse(Conditional.IfTrue);
                //Brするとスタックが0になる。変数に代入すべき
                I.Br(EndIf);
                I.MarkLabel(IfFalse);
                this.VoidTraverse(Conditional.IfFalse);
            } else{
                Debug.Assert(Conditional.IfTrue.Type!=typeof(void)&Conditional.IfFalse.Type!=typeof(void));
                this.Traverse(Conditional.IfTrue);
                I.Br(EndIf);
                I.MarkLabel(IfFalse);
                this.Traverse(Conditional.IfFalse);
            }
            I.MarkLabel(EndIf);
        }
        protected override void Dynamic(DynamicExpression Dynamic){
            var I=this.I;
            Debug.Assert(I is not null);
            var Member=this.DictionaryDynamic[Dynamic].Member;
            this.MemberAccess(Member);
            var Member_Type=Member.Type;
            var CallSite=I.M_DeclareLocal_Stloc_Ldloc(Member_Type);
            var Target=Member_Type.GetField("Target");
            Debug.Assert(Target is not null);
            I.Ldfld(Target);
            I.Ldloc(CallSite);
            this.TraverseExpressions(Dynamic.Arguments);
            I.Callvirt(Target.FieldType.GetMethod("Invoke")!);
        }
        /// <summary>
        /// 戻り値の必要ないTraverse
        /// </summary>
        /// <param name="e"></param>
        protected void VoidTraverse(Expression e){
            // ReSharper disable once SwitchStatementMissingSomeCases
            switch(e.NodeType){
                case ExpressionType.Assign:
                    this.VoidAssign((BinaryExpression)e);
                    break;
                case ExpressionType.PreDecrementAssign:
                    this.VoidPreDecrementAssign((UnaryExpression)e);
                    break;
                case ExpressionType.PreIncrementAssign:
                    this.VoidPreIncrementAssign((UnaryExpression)e);
                    break;
                case ExpressionType.Block:
                    this.VoidBlock((BlockExpression)e);
                    break;
                default:{
                    this.Traverse(e);
                    if(e.Type!=typeof(void))
                        this.I!.Pop();
                    break;
                }
            }
        }
        private void VoidAssign(BinaryExpression Binary) {
            var Binary_Left = Binary.Left;
            var Binary_Right = Binary.Right;
            if(Binary_Right.NodeType==ExpressionType.Label) {
                this.Traverse(Binary_Right);
                var I = this.I!;
                var Right値 = I.M_DeclareLocal_Stloc(Binary_Right.Type);
                this.格納先設定(Binary_Left);
                I.Ldloc(Right値);
            } else if(Binary_Right.NodeType==ExpressionType.Try) {
                var Try値 = this.PrivateTry値を代入した変数((TryExpression)Binary_Right);
                this.格納先設定(Binary_Left);
                this.I!.Ldloc(Try値);
            } else {
                this.格納先設定(Binary_Left);
                this.Traverse(Binary_Right);
            }
            this.Void格納先に格納(Binary_Left);
        }
        /// <summary>
        /// ++a,--a,a++,a--
        /// </summary>
        /// <param name="Unary"></param>
        private void VoidPreDecrementAssign(UnaryExpression Unary) {
            this.格納先設定IncrementDecrement(Unary,OpCodes.Sub);
            this.Void格納先に格納(Unary.Operand);
        }
        /// <summary>
        /// ++a,--a,a++,a--
        /// </summary>
        /// <param name="Unary"></param>
        private void VoidPreIncrementAssign(UnaryExpression Unary) {
            this.格納先設定IncrementDecrement(Unary,OpCodes.Add);
            this.Void格納先に格納(Unary.Operand);
        }
        private void VoidBlock(BlockExpression Block) {
            var Dictionary_Parameter_LocalBuilder = this.Dictionary_Parameter_LocalBuilder;
            Debug.Assert(Dictionary_Parameter_LocalBuilder is not null);
            foreach(var Block_Variable in Block.Variables) 
                //Debug.Assert(this.I is not null);
                Dictionary_Parameter_LocalBuilder.Add(Block_Variable,this.I!.DeclareLocal(Block_Variable.Type));
            foreach(var Block_Expression in Block.Expressions)
                this.VoidTraverse(Block_Expression);
            foreach(var Block_Variable in Block.Variables)
                Dictionary_Parameter_LocalBuilder.Remove(Block_Variable);
        }
        protected void Clear(){
            this.番号=0;
            this.Dictionary_Parameter_LocalBuilder.Clear();
        }
        private void PointerParameterNulllable(ParameterExpression Parameter) {
            var I = this.I!;
            var index = this.Parameters!.IndexOf(Parameter);
            if(index>=0) {
                if(this.インスタンスメソッドか)index++;
                if(Parameter.Type.IsValueType) {
                    if(Parameter.IsByRef)I.Ldarg((ushort)index);
                    else                 I.Ldarga((ushort)index);
                } else {
                    I.Ldarg((ushort)index);
                    if(Parameter.IsByRef)I.Ldobj(Parameter.Type);
                }
            } else{
                if(this.Dictionaryラムダ跨ぎParameter.TryGetValue(Parameter,out var Disp_Member)){
                    this.PointerTraverseNulllable(Disp_Member.Member);
                }else if(this.DispParameter==Parameter){
                    Debug.Assert(!Parameter.Type.IsValueType&&!Parameter.IsByRef||Parameter.Type.IsValueType&&Parameter.IsByRef);
                    I.Ldarg_0();
                }else{
                    Debug.Assert(this.Dictionary_Parameter_LocalBuilder is not null);
                    Debug.Assert(this.Dictionary_Parameter_LocalBuilder.ContainsKey(Parameter));
                    var Local = this.Dictionary_Parameter_LocalBuilder[Parameter];
                    if(Parameter.Type.IsValueType)I.Ldloca(Local);
                    else                          I.Ldloc(Local);
                }
            }
        }
    }
}
//2448 20220521
//2690 20220520
//2492 20220516
//3277 20220516
//3073
