using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using LinqDB.Helpers;
// ReSharper disable AssignNullToNotNullAttribute
// ReSharper disable PossibleNullReferenceException
namespace LinqDB.Optimizers;

partial class Optimizer {
    /// <summary>
    /// プロファイル出来るように式木に計測を埋め込む
    /// </summary>
    private sealed class 変換_Stopwatchに埋め込む:ReturnExpressionTraverser{
        public 変換_Stopwatchに埋め込む(作業配列 作業配列) : base(作業配列){
        }
        private static void TypeString(StringBuilder sb,Type e) {
            if(e.IsAnonymous()){
                sb.Append('{');
                foreach(var Parameter in e.GetConstructors()[0].GetParameters()){
                    TypeString(sb,Parameter.ParameterType);
                    sb.Append(Parameter.Name).Append(',');
                }
                sb[^1]='}';
            } else {
                if(!e.IsGenericType){
                    sb.Append(e.FullName);
                    return;
                }
                var Name = e.FullName!;
                //Debug.Assert(Name is not null);
                var index1 =Name.IndexOf('`');
                sb.Append(
                    index1>=0
                        ?Name[..index1]
                        :Name).Append('<');
                foreach(var GenericArgument in e.GetGenericArguments()) {
                    TypeString(sb,GenericArgument);
                    sb.Append(',');
                }
                sb[^1]='>';
            }
        }
        private static void ParameterString(StringBuilder sb,MethodBase Method){
            sb.Append('(');
            foreach(var Parameter in Method.GetParameters()){
                TypeString(sb,Parameter.ParameterType);
                sb.Append(' ');
                sb.Append(Parameter.Name);
                sb.Append(',');
            }
            sb[^1]=')';
        }
        private static void 結果出力(List<A計測> プロファイルArray){
            var プロファイル=プロファイルArray[0];
            プロファイル.Consoleに出力();
            プロファイル.CreateXDocument().Save("XDocument.xml");
        }
        private static readonly MethodInfo 結果出力Method=typeof(変換_Stopwatchに埋め込む).GetMethod(nameof(結果出力),Static_NonPublic)!;
        private ConstantExpression? ConstantList計測;
        private List<A計測>? List計測;
        public Expression 実行(Expression e,List<A計測> List計測,ConstantExpression ConstantList計測){
            var 計測しない=this.計測しない;
            計測しない.Clear();
            this.計測=計測しない;
            this.List計測=List計測;
            this.ConstantList計測=ConstantList計測;
            var Lambda0=(LambdaExpression)e;
            var Console_Out = Console.Out;
            try {
                var Lambda1=(LambdaExpression)this.Traverse(Lambda0);
                var Lambda1_Body=Lambda1.Body;
                var Expression結果出力=Expression.Call(
                    結果出力Method,
                    ConstantList計測
                );
                LambdaExpression Lambda2;
                if(Lambda1_Body.Type==typeof(void)){
                    Lambda2=Expression.Lambda(
                        Lambda0.Type,
                        Expression.Block(
                            Lambda1_Body,
                            Expression結果出力
                        ),
                        Lambda0.Parameters
                    );
                } else {
                    var 作業配列 = this._作業配列;
                    var 戻り値 = Expression.Parameter(
                        Lambda1_Body.Type,
                        $"戻り値{List計測.Count}"
                    );
                    Lambda2=Expression.Lambda(
                        Lambda0.Type,
                        Expression.Block(
                            作業配列.Parameters設定(戻り値),
                            作業配列.Expressions設定(
                                Expression.Assign(
                                    戻り値,
                                    Lambda1_Body
                                ),
                                Expression結果出力,
                                戻り値
                            )
                        ),
                        Lambda0.Parameters
                    );
                }
                return Lambda2;
            } finally{
                Console.SetOut(Console_Out);
            }
        }
        protected override Expression Block(BlockExpression Block0){
            var Block0_Expressions = Block0.Expressions;
            var Block0_Expressions_Count = Block0_Expressions.Count;
            var Block1_Expressions = new Expression[Block0_Expressions_Count];
            var Block0_Expressions_Count_1 = Block0_Expressions_Count- 1;
            if(Block0_Expressions_Count_1 >=0){
                for(var a = 0;a<Block0_Expressions_Count_1;a++) {
                    Block1_Expressions[a]=this.Traverse(Block0_Expressions[a]);
                }
                Block1_Expressions[Block0_Expressions_Count_1]=this.Traverse(Block0_Expressions[Block0_Expressions_Count_1]);
            }
            return Expression.Block(
                Block0.Variables,
                Block1_Expressions
            );
        }
        private (A計測 親計測, int List計測Index) プロファイル前処理(ParameterExpression Parameter,string Name,string? Value = null) {
            var List計測 = this.List計測!;
            var 子計測 = new 計測する(Parameter,Name,Value,List計測.Count);
            var 親計測 = this.計測!;
            //子計測.Name=Name;
            //子計測.Value=Value;
            //Debug.Assert(List計測 is not null);
            //子計測.番号=List計測.Count;
            //Debug.Assert(this.計測 is not null);
            this.計測!.Add末子(子計測);
            this.計測=子計測;
            var List計測_Count = List計測.Count;
            List計測.Add(子計測);
            //Debug.Assert(親計測 is not null);
            return (親計測, List計測_Count);
        }
        private (A計測 親計測, int List計測Index) プロファイル前処理(string Name,string? Value = null) {
            var List計測 = this.List計測!;
            var 子計測 =new 計測する(Name,Value,List計測.Count);
            var 親計測 = this.計測!;
            //子計測.Name=Name;
            //子計測.Value=Value;
            //Debug.Assert(List計測 is not null);
            //子計測.番号=List計測.Count;
            //Debug.Assert(this.計測 is not null);
            this.計測!.Add末子(子計測);
            this.計測=子計測;
            var List計測_Count = List計測.Count;
            List計測.Add(子計測);
            //Debug.Assert(親計測 is not null);
            return (親計測, List計測_Count);
        }
        private A計測 プロファイル前処理計測しない(ParameterExpression Parameter,string Name,string? Value = null) {
            var List計測 = this.List計測!;
            var 子計測 = new 計測しない(Parameter,Name,Value,List計測.Count);
            var 親計測 = this.計測!;
            //子計測.Name=Name;
            //子計測.Value=Value;
            //Debug.Assert(List計測 is not null);
            //子計測.番号=List計測.Count;
            //Debug.Assert(this.計測 is not null);
            this.計測!.Add末子(子計測);
            this.計測=子計測;
            List計測.Add(子計測);
            //Debug.Assert(親計測 is not null);
            return 親計測;
        }
        private A計測 プロファイル前処理計測しない(string Name,string? Value = null) {
            var List計測 = this.List計測!;
            var 子計測 =new 計測しない(Name,Value,List計測.Count);
            var 親計測 = this.計測!;
            //子計測.Name=Name;
            //子計測.Value=Value;
            //Debug.Assert(List計測 is not null);
            //子計測.番号=List計測.Count;
            //Debug.Assert(this.計測 is not null);
            this.計測!.Add末子(子計測);
            this.計測=子計測;
            List計測.Add(子計測);
            //Debug.Assert(親計測 is not null);
            return 親計測;
        }
        private Expression プロファイル後処理2(int List計測Index,Expression Body) {
            var 計測Expression=Expression.Call(
                this.ConstantList計測,
                List計測する_Item,
                Expression.Constant(List計測Index)
            );
            return Expression.Call(
                Expression.Call(
                    計測Expression,
                    A計測.Reflection.Start
                ),
                this._作業配列.MakeGenericMethod(A計測.Reflection.StopReturn,Body.Type),
                Body
            );
        }
        private Expression プロファイル後処理1((A計測 親計測, int List計測Index) 前データ,Expression Expression1){
            this.計測=前データ.親計測;
            if(Expression1.Type==typeof(void)){
                var 計測Expression = Expression.Call(
                    this.ConstantList計測,
                    List計測する_Item,
                    Expression.Constant(前データ.List計測Index)
                );
                var Start = Expression.Call(
                    計測Expression,
                    A計測.Reflection.Start
                );
                var Stop = Expression.Call(
                    計測Expression,
                    A計測.Reflection.Stop
                );
                return Expression.Block(
                    Start,
                    Expression1,
                    Stop
                );
            } else {
                return this.プロファイル後処理2(
                    前データ.List計測Index,
                    Expression1
                );
            }
        }
        private readonly 計測しない 計測しない=new("ダミー");
        private A計測? 計測;
        protected override Expression Call(MethodCallExpression MethodCall0){
            Debug.Assert(MethodCall0.Method is DynamicMethod||MethodCall0.Method.DeclaringType is not null,"MethodCall0_Method.DeclaringType != null");
            var MethodCall0_Method = MethodCall0.Method;
            (A計測 親計測,int List計測Index) 前処理データ;
            if(ループ展開可能メソッドか(GetGenericMethodDefinition(MethodCall0_Method))) {
                前処理データ=this.プロファイル前処理(MethodCall0_Method.Name);
            } else {
                var sb=new StringBuilder();
                if(MethodCall0_Method.DeclaringType is not null) {
                    TypeString(sb,MethodCall0_Method.DeclaringType);
                    sb.Append('.');
                }
                sb.Append(MethodCall0_Method.Name);
                ParameterString(sb,MethodCall0_Method);
                前処理データ=this.プロファイル前処理(
                    nameof(ExpressionType.Call),
                    sb.ToString()
                );
            }
            MethodCallExpression MethodCall1;
            if(MethodCall0_Method.IsStatic){
                MethodCall1=Expression.Call(
                    MethodCall0_Method,
                    this.TraverseExpressions(MethodCall0.Arguments)
                );
            } else if(MethodCall0.Arguments.Count==0){
                MethodCall1=Expression.Call(
                    this.Traverse(MethodCall0.Object),
                    MethodCall0_Method
                );
            } else{
                MethodCall1=Expression.Call(
                    this.Traverse(MethodCall0.Object),
                    MethodCall0_Method,
                    this.TraverseExpressions(MethodCall0.Arguments)
                );
            }
            return this.プロファイル後処理1(前処理データ,MethodCall1);
        }
        private Expression 共通Binary(BinaryExpression Binary0) => this.プロファイル後処理1(
            Binary0.Method is not null 
                ? this.プロファイル前処理(Binary0.NodeType.ToString(),Binary0.Method.Name)
                : this.プロファイル前処理(Binary0.NodeType.ToString()),
            Expression.MakeBinary(
                Binary0.NodeType,
                this.Traverse(Binary0.Left),
                this.Traverse(Binary0.Right),
                Binary0.IsLiftedToNull,
                Binary0.Method,
                this.TraverseNullable(Binary0.Conversion) as LambdaExpression
            )
        );
        private Expression 共通Unary(UnaryExpression Unary0) {
            A計測 親計測;
            int List計測Index;
            if(Unary0.Method is not null) {
                if(Unary0.Type!=Unary0.Operand.Type) {
                    (親計測, List計測Index)=this.プロファイル前処理(Unary0.NodeType.ToString(),$"{Unary0.Type.FullName} {Unary0.Method.Name}");
                } else {
                    (親計測, List計測Index)=this.プロファイル前処理(Unary0.NodeType.ToString(),Unary0.Method.Name);
                }
            } else if(Unary0.Type!=Unary0.Operand.Type) {
                (親計測, List計測Index)=this.プロファイル前処理(Unary0.NodeType.ToString(),Unary0.Type.FullName);
            } else {
                (親計測, List計測Index)=this.プロファイル前処理(Unary0.NodeType.ToString());
            }
            return this.プロファイル後処理1(
                (親計測, List計測Index),
                Expression.MakeUnary(
                    Unary0.NodeType,
                    this.Traverse(Unary0.Operand),
                    Unary0.Type
                )
            );
        }
        private Expression 共通TypeBinary(TypeBinaryExpression TypeBinary0,string op) {
            var TypeOperand = TypeBinary0.TypeOperand;
            return this.プロファイル後処理1(
                this.プロファイル前処理(op,TypeOperand.Name),
                Expression.TypeIs(
                    this.Traverse(TypeBinary0.Expression),
                    TypeOperand
                )
            );
        }
        private Expression? PointerTraverseNullable(Expression? Expression0) {
            if(Expression0 is null) {
                return null;
            }
            var NodeType = Expression0.NodeType;
            if(NodeType==ExpressionType.Parameter) {
                if(Expression0.Type.IsValueType) {
                    var List計測 = this.List計測!;
                    //Debug.Assert(List計測 is not null);
                    var Parameter0 = (ParameterExpression)Expression0;
                    var 子計測1 = new 計測しない(
                        Parameter0,
                        nameof(ExpressionType.Parameter),
                        Parameter0.Name,
                        List計測.Count
                    );
                    //Debug.Assert(this.計測 is not null);
                    this.計測!.Add末子(子計測1);
                    List計測.Add(子計測1);
                    return Expression0;
                }
                return Expression0;
            }
            if(NodeType==ExpressionType.MemberAccess) {
                var Member0 = (MemberExpression)Expression0;
                return Expression.MakeMemberAccess(
                    Expression0.Type.IsValueType
                        ? this.PointerTraverseNullable(Member0.Expression)
                        : this.TraverseNullable(Member0.Expression),
                    Member0.Member
                );
            }
            return this.Traverse(Expression0);
        }
        private static readonly MethodInfo List計測する_Item= typeof(List<A計測>).GetProperty("Item",Instance_NonPublic_Public)!.GetMethod!;
        protected override Expression Assign(BinaryExpression Assign0){
            Debug.Assert(
                Assign0.Left.NodeType==ExpressionType.Parameter||
                Assign0.Left.NodeType==ExpressionType.MemberAccess||
                Assign0.Left.NodeType==ExpressionType.ArrayIndex||
                Assign0.Left.NodeType==ExpressionType.Index
            );
            var Binary0_Left = Assign0.Left;
            var Binary0_Left_NodeType = Binary0_Left.NodeType;
            var 前データ=this.プロファイル前処理(nameof(ExpressionType.Assign));
            var Binary1_Left =Binary0_Left;
            switch(Binary0_Left_NodeType){
                case ExpressionType.Parameter: {
                    //.NETのバージョンによっては変数の参照も計測できるかもしれない。たとえば
                    //a=1↓
                    var Parameter0 = (ParameterExpression)Binary0_Left;
                    var List計測 = this.List計測!;
                    var 子計測 =new 計測しない(
                        Parameter0,
                        nameof(ExpressionType.Parameter),
                        Parameter0.Name,
                        List計測.Count
                    );
                    //Debug.Assert(List計測 is not null);
                    //子計測.番号=List計測.Count;
                    //Debug.Assert(this.計測 is not null);
                    this.計測!.Add末子(子計測);
                    List計測.Add(子計測);
                    break;
                }
                case ExpressionType.MemberAccess:{
                    var Member0 = (MemberExpression)Binary0_Left;
                    var 親計測 = this.プロファイル前処理計測しない(Member0.Member.Name);
                    Binary1_Left=Expression.MakeMemberAccess(
                        this.PointerTraverseNullable(Member0.Expression),
                        Member0.Member
                    );
                    this.計測=親計測;
                    break;
                }
                case ExpressionType.Index:{
                    //Int32[]
                    //ArrayAccess
                    var Index0 = (IndexExpression)Binary0_Left;
                    var 親計測=this.プロファイル前処理計測しない(nameof(ExpressionType.Index));
                    Binary1_Left =Expression.MakeIndex(
                        this.Traverse(Index0.Object),
                        Index0.Indexer,
                        this.TraverseExpressions(Index0.Arguments)
                    );
                    this.計測=親計測;
                    break;
                }
            }
            return this.プロファイル後処理1(
                前データ,
                Expression.Assign(
                    Binary1_Left,
                    this.Traverse(Assign0.Right)
                )
            );
        }
        protected override Expression Add(BinaryExpression Binary0) => this.共通Binary(Binary0);
        protected override Expression AddChecked(BinaryExpression Binary0) => this.共通Binary(Binary0);
        protected override Expression ArrayIndex(BinaryExpression Binary0) => this.共通Binary(Binary0);
        protected override Expression Subtract(BinaryExpression Binary0) => this.共通Binary(Binary0);
        protected override Expression SubtractChecked(BinaryExpression Binary0) => this.共通Binary(Binary0);
        protected override Expression Multiply(BinaryExpression Binary0) => this.共通Binary(Binary0);
        protected override Expression MultiplyChecked(BinaryExpression Binary0) => this.共通Binary(Binary0);
        protected override Expression Divide(BinaryExpression Binary0) => this.共通Binary(Binary0);
        protected override Expression Modulo(BinaryExpression Binary0) => this.共通Binary(Binary0);
        protected override Expression And(BinaryExpression Binary) => this.共通Binary(Binary);
        protected override Expression Or(BinaryExpression Binary) => this.共通Binary(Binary);
        protected override Expression ExclusiveOr(BinaryExpression Binary0) => this.共通Binary(Binary0);
        protected override Expression Equal(BinaryExpression Binary0) => this.共通Binary(Binary0);
        protected override Expression NotEqual(BinaryExpression Binary0) => this.共通Binary(Binary0);
        protected override Expression GreaterThan(BinaryExpression Binary0) => this.共通Binary(Binary0);
        protected override Expression LessThan(BinaryExpression Binary0) => this.共通Binary(Binary0);
        protected override Expression GreaterThanOrEqual(BinaryExpression Binary0) => this.共通Binary(Binary0);
        protected override Expression LessThanOrEqual(BinaryExpression Binary0) => this.共通Binary(Binary0);
        protected override Expression LeftShift(BinaryExpression Binary0) => this.共通Binary(Binary0);
        protected override Expression RightShift(BinaryExpression Binary0) => this.共通Binary(Binary0);
        protected override Expression AndAlso(BinaryExpression Binary) => this.共通Binary(Binary);
        protected override Expression OrElse(BinaryExpression Binary) => this.共通Binary(Binary);
        protected override Expression Power(BinaryExpression Binary0) => this.共通Binary(Binary0);
        protected override Expression TypeAs(UnaryExpression Unary) => this.共通Unary(Unary);
        protected override Expression TypeEqual(TypeBinaryExpression TypeBinary0) => this.共通TypeBinary(TypeBinary0,"type_equal");
        protected override Expression TypeIs(TypeBinaryExpression TypeBinary0)=> this.共通TypeBinary(TypeBinary0,"is");
        protected override Expression OnesComplement(UnaryExpression Unary0) => this.共通Unary(Unary0);
        protected override Expression ArrayLength(UnaryExpression Unary0) => this.共通Unary(Unary0);
        protected override Expression Convert(UnaryExpression Unary0) => this.共通Unary(Unary0);
        protected override Expression ConvertChecked(UnaryExpression Unary0) => this.共通Unary(Unary0);
        protected override Expression Increment(UnaryExpression Unary) => this.共通Unary(Unary);
        protected override Expression Decrement(UnaryExpression Unary0) => this.共通Unary(Unary0);
        protected override Expression PostIncrementAssign(UnaryExpression Unary0) => this.共通Unary(Unary0);
        protected override Expression PostDecrementAssign(UnaryExpression Unary0) => this.共通Unary(Unary0);
        protected override Expression PreIncrementAssign(UnaryExpression Unary0) => this.共通Unary(Unary0);
        protected override Expression PreDecrementAssign(UnaryExpression Unary0) => this.共通Unary(Unary0);
        protected override Expression Negate(UnaryExpression Unary0) => this.共通Unary(Unary0);
        protected override Expression NegateChecked(UnaryExpression Unary) => this.共通Unary(Unary);
        protected override Expression New(NewExpression New0){
            var sb = new StringBuilder();
            if(New0.Type.IsAnonymous()){
                sb.Append('{');
                foreach(var Parameter in New0.Type.GetConstructors()[0].GetParameters()){
                    TypeString(sb,Parameter.ParameterType);
                    sb.Append(' ').Append(Parameter.Name).Append(',');
                }
                sb[^1]='}';
                return this.プロファイル後処理1(
                    this.プロファイル前処理(nameof(ExpressionType.New),sb.ToString()),
                    Expression.New(
                        New0.Constructor,
                        this.TraverseExpressions(New0.Arguments),
                        New0.Members
                    )
                );
            } else {
                var Constructor = New0.Constructor;
                if(Constructor is not null) {
                    TypeString(sb,New0.Type);
                    ParameterString(sb,Constructor);
                    return this.プロファイル後処理1(
                        this.プロファイル前処理(nameof(ExpressionType.New),sb.ToString()),
                        New0.Members is not null
                            ? Expression.New(
                                Constructor,
                                this.TraverseExpressions(New0.Arguments),
                                New0.Members
                            )
                            : Expression.New(
                                Constructor,
                                this.TraverseExpressions(New0.Arguments)
                            )
                    );
                } else {
                    return this.プロファイル後処理1(
                        this.プロファイル前処理(nameof(ExpressionType.New),sb.ToString()),
                        Expression.New(New0.Type)
                    );
                }
            }
        }
        protected override Expression NewArrayBounds(NewArrayExpression NewArray0) {
            var NewArray0_Expressions = NewArray0.Expressions;
            var ElementType=NewArray0.Type.GetElementType()!;
            return this.プロファイル後処理1(
                this.プロファイル前処理(nameof(ExpressionType.NewArrayBounds),$"{ElementType.Name}[{NewArray0_Expressions.Count}]"),
                Expression.NewArrayInit(
                    ElementType,
                    this.TraverseExpressions(NewArray0_Expressions)
                )
            );
        }
        protected override Expression NewArrayInit(NewArrayExpression NewArray0) {
            Debug.Assert(NewArray0.Type.IsArray);
            var NewArray0_Expressions = NewArray0.Expressions;
            var ElementType = NewArray0.Type.GetElementType()!;
            return this.プロファイル後処理1(
                this.プロファイル前処理(nameof(ExpressionType.NewArrayInit),$"{ElementType.Name}[{NewArray0_Expressions.Count}]"),
                Expression.NewArrayInit(
                    ElementType,
                    this.TraverseExpressions(NewArray0_Expressions)
                )
            );
        }
        protected override Expression Conditional(ConditionalExpression Conditional0) => this.プロファイル後処理1(
            this.プロファイル前処理(nameof(ExpressionType.Conditional)),
            Expression.Condition(
                this.Traverse(Conditional0.Test),
                this.Traverse(Conditional0.IfTrue),
                this.Traverse(Conditional0.IfFalse),
                Conditional0.Type
            )
        );
        protected override Expression Constant(ConstantExpression Constant0){
            string Name;
            if(Constant0.Value is null){
                Name="null";
            } else {
                var Constant_Type = Constant0.Type;
                if(Constant_Type==typeof(string)){
                    Name="\"" + Constant0.Value + "\"";
                }else if(Constant_Type==typeof(decimal)){
                    Name=Constant0.Value + "M";
                }else {
                    if(Constant_Type.IsEnum){
                        Constant_Type=Enum.GetUnderlyingType(Constant_Type);
                    }
                    if(Constant_Type==typeof(sbyte)){
                        Name="(SByte)" + Constant0.Value;
                    }else if(Constant_Type==typeof(short)){
                        Name="(Int16)" + Constant0.Value;
                    }else if(Constant_Type==typeof(int)){
                        Name=Constant0.Value.ToString()!;
                    }else if(Constant_Type==typeof(long)){
                        Name="(Int64)" + Constant0.Value;
                    }else if(Constant_Type==typeof(IntPtr)){
                        Name="(IntPtr)" + Constant0.Value;
                    }else if(Constant_Type==typeof(byte)){
                        Name="(Byte)" + Constant0.Value;
                    }else if(Constant_Type==typeof(ushort)){
                        Name="(UInt16)" + Constant0.Value;
                    }else if(Constant_Type==typeof(uint)){
                        Name=Constant0.Value + "u";
                    }else if(Constant_Type==typeof(ulong)){
                        Name=Constant0.Value + "ul";
                    }else if(Constant_Type==typeof(UIntPtr)){
                        Name="(UIntPtr)" + Constant0.Value;
                    }else if(Constant_Type==typeof(bool)){
                        Name=(bool)Constant0.Value ? "true" : "false";
                    }else if(Constant_Type==typeof(char)){
                        Name="'" + Constant0.Value + '"';
                    }else if(Constant_Type==typeof(float)){
                        Name=Constant0.Value + "f";
                    }else if(Constant_Type==typeof(double)){
                        Name=Constant0.Value + "d";
                    } else{
                        Name="[Object]";
                    }
                }
            }
            return this.プロファイル後処理1(
                this.プロファイル前処理(nameof(ExpressionType.Constant),Name),
                Constant0
            );
        }

        protected override Expression Default(DefaultExpression Default0)=>this.プロファイル後処理1(
            this.プロファイル前処理(nameof(ExpressionType.Default),Default0.Type.Name),
            Default0
        );

        protected override Expression Parameter(ParameterExpression Parameter0)=>this.プロファイル後処理1(
            this.プロファイル前処理(nameof(ExpressionType.Parameter),Parameter0.Name),
            Parameter0
        );
        protected override Expression Index(IndexExpression Index0) => this.プロファイル後処理1(
            this.プロファイル前処理(nameof(ExpressionType.Index)),
            Expression.MakeIndex(
                this.Traverse(Index0.Object),
                Index0.Indexer,
                this.TraverseExpressions(Index0.Arguments)
            )
        );
        protected override Expression Invoke(InvocationExpression Invocation0) => this.プロファイル後処理1(
            this.プロファイル前処理(nameof(ExpressionType.Invoke)),
            Expression.Invoke(
                this.Traverse(Invocation0.Expression),
                this.TraverseExpressions(Invocation0.Arguments)
            )
        );
        protected override Expression MemberAccess(MemberExpression Member0){
            Debug.Assert(Member0.Member.DeclaringType is not null,"Member0_Member.DeclaringType != null");
            var Member0_Member =Member0.Member;
            return this.プロファイル後処理1(
                this.プロファイル前処理(nameof(ExpressionType.MemberAccess),Member0_Member.Name),
                Expression.MakeMemberAccess(
                    Member0.Expression is not null?this.Traverse(Member0.Expression):null,
                    Member0_Member
                )
            );
        }
        protected override IList<MemberBinding> Bindings(ReadOnlyCollection<MemberBinding> Bindings0) {
            var Bindings0_Count = Bindings0.Count;
            var Bindings1 = new MemberBinding[Bindings0_Count];
            var 変化したか = false;
            for(var a = 0;a < Bindings0_Count;a++) {
                var Binding0 = Bindings0[a];
                switch(Binding0.BindingType) {
                    case MemberBindingType.Assignment:{
                        var MemberAssignment=(MemberAssignment)Binding0;
                        var Binding0_Expression = MemberAssignment.Expression;
                        var 前処理データ = this.プロファイル前処理(MemberAssignment.Member.Name);
                        var Binding1_Expression = this.Traverse(Binding0_Expression);
                        Bindings1[a]=Expression.Bind(
                            Binding0.Member,
                            this.プロファイル後処理1(
                                前処理データ,
                                Binding1_Expression
                            )
                        );
                        変化したか=true;
                        break;
                    }
                    case MemberBindingType.MemberBinding: {
                        var MemberMemberBinding = (MemberMemberBinding)Binding0;
                        var 親計測=this.プロファイル前処理計測しない(MemberMemberBinding.Member.Name);
                        var Binding0_Bindings = MemberMemberBinding.Bindings;
                        var Binding1_Bindings = this.Bindings(Binding0_Bindings);
                        if(Binding0_Bindings==Binding1_Bindings){
                            Bindings1[a]=Binding0;
                        } else {
                            Bindings1[a]=Expression.MemberBind(
                                Binding0.Member,
                                Binding1_Bindings
                            );
                            変化したか=true;
                        }
                        this.計測=親計測;
                        break;
                    }
                    case MemberBindingType.ListBinding: {
                        var MemberListBinding0 = (MemberListBinding)Binding0;
                        var MemberListBinding0_Initializers = MemberListBinding0.Initializers;
                        var MemberListBinding0_Initializers_Count = MemberListBinding0_Initializers.Count;
                        var MemberListBinding1_Initializers = new ElementInit[MemberListBinding0_Initializers_Count];
                        var 親計測=this.プロファイル前処理計測しない(MemberListBinding0.Member.Name);
                        var 変化したか1 = false;
                        for(var b = 0;b < MemberListBinding0_Initializers_Count;b++) {
                            var MemberListBinding0_Initializer = MemberListBinding0_Initializers[b];
                            var 親計測2=this.プロファイル前処理計測しない(MemberListBinding0_Initializer.AddMethod.ToString());
                            var MemberListBinding0_Initializer_Arguments=MemberListBinding0_Initializer.Arguments;
                            var MemberListBinding1_Initializer_Arguments=this.TraverseExpressions(MemberListBinding0_Initializer_Arguments);
                            if(ReferenceEquals(MemberListBinding0_Initializer_Arguments,MemberListBinding1_Initializer_Arguments)) {
                                MemberListBinding1_Initializers[b]=MemberListBinding0_Initializer;
                            } else {
                                MemberListBinding1_Initializers[b]=Expression.ElementInit(
                                    MemberListBinding0_Initializer.AddMethod,
                                    MemberListBinding1_Initializer_Arguments
                                );
                                変化したか1=true;
                            }
                            //var MemberListBinding1_Initializer_Arguments=this.TraverseExpressionsNullable(MemberListBinding0_Initializer.Arguments);
                            //if(MemberListBinding1_Initializer_Arguments is null) {
                            //    MemberListBinding1_Initializers[b]=MemberListBinding0_Initializer;
                            //} else {
                            //    MemberListBinding1_Initializers[b]=Expression.ElementInit(
                            //        MemberListBinding0_Initializer.AddMethod,
                            //        MemberListBinding1_Initializer_Arguments
                            //    );
                            //    変化したか1=true;
                            //}
                            this.計測=親計測2;
                        }
                        if(変化したか1) {
                            Bindings1[a]=Expression.ListBind(
                                Binding0.Member,
                                MemberListBinding1_Initializers
                            );
                            変化したか=true;
                        } else{
                            Bindings1[a]=MemberListBinding0;
                        }
                        this.計測=親計測;
                        break;
                    }
                    default:
                        throw new NotSupportedException($"{Binding0.BindingType}はサポートされていない");
                }
            }
            return 変化したか ? (IList<MemberBinding>)Bindings1 : Bindings0;
        }

        protected override Expression MemberInit(MemberInitExpression MemberInit0){
            var 前処理データ=this.プロファイル前処理(nameof(ExpressionType.MemberInit));
            var MemberInit0_NewExpression = MemberInit0.NewExpression;
            var 親計測1=this.プロファイル前処理計測しない(MemberInit0_NewExpression.Type.Name);
            var MemberInit1_NewExpression = (NewExpression)base.New(MemberInit0_NewExpression);
            this.計測=親計測1;
            var MemberInit0_Bindings = MemberInit0.Bindings;
            var MemberInit1_Bindings = this.Bindings(MemberInit0_Bindings);
            return this.プロファイル後処理1(
                前処理データ,
                Expression.MemberInit(
                    MemberInit1_NewExpression,
                    MemberInit1_Bindings
                )
            );
        }

        protected override Expression Quote(UnaryExpression Unary0) {
            var (親計測, _)=this.プロファイル前処理(nameof(ExpressionType.Quote));
            var Unqry1_Operand=this.Traverse(Unary0.Operand);
            this.計測=親計測;
            return Expression.Quote(
                Unqry1_Operand
            );
        }
        protected override Expression Lambda(LambdaExpression Lambda0) {
            var sb = new StringBuilder();
            var Lambda_Parameters = Lambda0.Parameters;
            var Lambda_Parameters_Count = Lambda_Parameters.Count;
            if(Lambda_Parameters_Count > 0) {
                sb.Append(Lambda_Parameters[0].Name);
                for(var a = 1;a < Lambda_Parameters_Count;a++) {
                    sb.Append(' ');
                    sb.Append(Lambda_Parameters[a].Name);
                }
            }
            var 親計測 = this.プロファイル前処理計測しない(nameof(ExpressionType.Lambda),sb.ToString());
            var Lambda1_Body = this.Traverse(Lambda0.Body);
            this.計測=親計測;
            return Expression.Lambda(
                Lambda0.Type,
                Lambda1_Body,
                Lambda0.Parameters
            );
        }
        protected override Expression ListInit(ListInitExpression ListInit0){
            var 前処理データ = this.プロファイル前処理(nameof(ExpressionType.ListInit));
            var ListInit0_NewExpression = ListInit0.NewExpression;
            var 親計測1=this.プロファイル前処理計測しない(ListInit0_NewExpression.Type.Name);
            var ListInit1_NewExpression = (NewExpression)base.New(ListInit0_NewExpression);
            this.計測=親計測1;
            var ListInit0_Initializers = ListInit0.Initializers;
            var ListInit0_Initializers_Count = ListInit0_Initializers.Count;
            var ListInit1_Initializers = new ElementInit[ListInit0_Initializers_Count];
            for(var a = 0;a<ListInit0_Initializers_Count;a++) {
                var ListInit0_Initializer = ListInit0_Initializers[a];
                var 親計測2=this.プロファイル前処理計測しない(ListInit0_Initializer.AddMethod.ToString());
                ListInit1_Initializers[a]=Expression.ElementInit(
                    ListInit0_Initializer.AddMethod,
                    this.TraverseExpressions(ListInit0_Initializer.Arguments)
                );
                this.計測=親計測2;
            }
            return this.プロファイル後処理1(
                前処理データ,
                Expression.ListInit(
                    ListInit1_NewExpression,
                    ListInit1_Initializers
                )
            );
        }
        protected override Expression Loop(LoopExpression Loop0) {
            var 前処理データ = this.プロファイル前処理(nameof(ExpressionType.Loop));
            return this.プロファイル後処理1(
                前処理データ,
                Expression.Loop(
                    this.Traverse(Loop0.Body),
                    Loop0.BreakLabel,
                    Loop0.ContinueLabel
                )
            );
        }
    }
    public static void 結果出力(List<A計測> プロファイルArray) {
        var プロファイル = プロファイルArray[0];
        プロファイル.Consoleに出力();
        プロファイル.CreateXDocument().Save("XDocument.xml");
    }
}