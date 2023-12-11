/*
 * a.Union(a)→a
 * a.Except(a)→Empty
 */
using System;
using Generic=System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Reflection;
using LinqDB.Sets;
//using Microsoft.CSharp.RuntimeBinder;
//using Binder = Microsoft.CSharp.RuntimeBinder.Binder;
using LinqDB.Helpers;
using LinqDB.Optimizers.VoidExpressionTraverser;
//using CatchBlock = System.Linq.Expressions.CatchBlock;
//using Expression = System.Linq.Expressions.Expression;
using Type = System.Type;
//using System.Runtime.Remoting.Messaging;
using Linq=System.Linq;
// ReSharper disable MemberHidesStaticFromOuterClass
namespace LinqDB.Optimizers.ReturnExpressionTraverser;
using static Common;
using static System.Net.Mime.MediaTypeNames;

/// <summary>
/// インライン不可能定数とは1mとか単純にIL命令で書けないもの。
/// a+=bなどの合体演算子をa=a+bにする。
/// A.SelectMany(a=>B,(a,b)=>new{a,b})→A.SelectMany(a=>B.Select(b=>=>new{a,b}))にする。
/// decimal.Parse("1111")→1111mに変換する
/// </summary>
internal sealed class 変換_メソッド正規化_取得インライン不可能定数:ReturnExpressionTraverser_Quoteを処理しない {
    private sealed class 取得_Parameter_OuterPredicate_InnerPredicate {
        private sealed class 判定_Parameter_葉に移動したいPredicate:VoidExpressionTraverser_Quoteを処理しない {
            private ParameterExpression? 許可するParameter;
            private bool 移動出来る;
            public bool 実行(Expression e,ParameterExpression? 許可するParameter) {
                this.許可するParameter=許可するParameter;
                this.移動出来る=true;
                this.Traverse(e);
                return this.移動出来る;
            }
            protected override void Parameter(ParameterExpression Parameter) {
                if(Parameter!=this.許可するParameter) {
                    this.移動出来る=false;
                }
            }
        }
        private readonly 判定_Parameter_葉に移動したいPredicate _判定_Parameter_葉に移動したいPredicate=new();
        private Expression? OuterPredicate , OtherPredicate;
        private ParameterExpression? Outer;
        public (Expression? OuterPredicate, Expression? OtherPredicate) 実行(Expression e,ParameterExpression Outer) {
            this.Outer=Outer;
            this.OuterPredicate=this.OtherPredicate=null;
            this.Traverse(e);
            return (this.OuterPredicate, this.OtherPredicate);
        }
        private void Traverse(Expression e) {
            var 判定_Parameter_葉に移動したいPredicate = this._判定_Parameter_葉に移動したいPredicate;
            if(e.NodeType==ExpressionType.AndAlso) {
                var Binary = (BinaryExpression)e;
                var Binary_Left = Binary.Left;
                var Binary_Right = Binary.Right;
                var Left葉Outerに移動する = 判定_Parameter_葉に移動したいPredicate.実行(Binary_Left,this.Outer!);
                var Right葉Outerに移動する = 判定_Parameter_葉に移動したいPredicate.実行(Binary_Right,this.Outer!);
                if(Left葉Outerに移動する) {
                    if(Right葉Outerに移動する) {
                        this.OuterPredicate=AndAlsoで繋げる(this.OuterPredicate,Binary);
                    } else {
                        this.OuterPredicate=AndAlsoで繋げる(this.OuterPredicate,Binary_Left);
                        this.Traverse(Binary_Right);
                    }
                } else if(Right葉Outerに移動する) {
                    this.OuterPredicate=AndAlsoで繋げる(this.OuterPredicate,Binary_Right);
                    this.Traverse(Binary_Left);
                } else {
                    this.Traverse(Binary_Left);
                    this.Traverse(Binary_Right);
                }
            } else if(this._判定_Parameter_葉に移動したいPredicate.実行(e,this.Outer!)) {
                this.OuterPredicate=AndAlsoで繋げる(this.OuterPredicate,e);
            } else {
                this.OtherPredicate=AndAlsoで繋げる(this.OtherPredicate,e);
            }
        }
    }
    private readonly 取得_Parameter_OuterPredicate_InnerPredicate _取得_Parameter_OuterPredicate_InnerPredicate=new();
    private readonly 変換_旧Expressionを新Expression1 変換_旧Expressionを新Expression1;
    private readonly 変換_旧Parameterを新Expression1 変換_旧Parameterを新Expression1;
    private readonly 変換_旧Parameterを新Expression2 変換_旧Parameterを新Expression2;
    public 変換_メソッド正規化_取得インライン不可能定数(作業配列 作業配列,変換_旧Parameterを新Expression1 変換_旧Parameterを新Expression1,
        変換_旧Parameterを新Expression2 変換_旧Parameterを新Expression2,
        変換_旧Expressionを新Expression1 変換_旧Expressionを新Expression1) : base(作業配列) {
        this.変換_旧Parameterを新Expression1=変換_旧Parameterを新Expression1;
        this.変換_旧Parameterを新Expression2=変換_旧Parameterを新Expression2;
        //this._取得_New_OuterPredicate_InnerPredicate_OtherPredicate=new 取得_New_OuterPredicate_InnerPredicate_OtherPredicate(判定_New_葉に移動したいPredicate);
        this.変換_旧Expressionを新Expression1=変換_旧Expressionを新Expression1;
    }
    private int 番号;
    //internal Information? Information;
    public Expression 実行(Expression e) {
        this.番号=0;
        this.DictionaryConstant.Clear();
        return this.Traverse(e);
    }
    internal Generic.Dictionary<ConstantExpression,(FieldInfo Disp,MemberExpression Member)> DictionaryConstant=default!;
    #if true
    private Expression AndAlso_OrElse(ParameterExpression p,Expression test,Expression ifTrue,Expression ifFalse) {
        if(test.Type==typeof(bool)) {
            return Expression.Block(
                this.作業配列.Parameters設定(p),
                Expression.Condition(
                    test,
                    ifTrue,
                    ifFalse
                )                
            );
        } else {
            return Expression.Block(
                this.作業配列.Parameters設定(p),
                Expression.Condition(
                    Expression.Call(
                        test.Type.GetMethod(op_True)!,
                        test
                    ),
                    ifTrue,
                    ifFalse
                )
            );
        }
    }
    //protected override Expression AndAlso(BinaryExpression Binary0) {
    //    var Binary1_Left = this.Traverse(Binary0.Left);
    //    var And = Expression.And(
    //        Binary1_Left,
    //        this.Traverse(Binary0.Right)
    //    );
    //    return this.AndAlso_OrElse(Binary1_Left,And,Binary1_Left);
    //}
    /// <summary>
    /// a&amp;&amp;b→operator true(a)?a&amp;b:a
    /// </summary>
    /// <param name="Binary0"></param>
    /// <returns></returns>
    protected override Expression AndAlso(BinaryExpression Binary0) {
        var Binary1_Left = this.Traverse(Binary0.Left);
        var Binary1_Right=this.Traverse(Binary0.Right);
        if(Binary1_Right.NodeType is ExpressionType.Constant or ExpressionType.Parameter) return Expression.And(Binary1_Left,Binary1_Right);
        var p=Expression.Parameter(Binary1_Left.Type,"AndAlso");
        return this.AndAlso_OrElse(
            p,
            Expression.Assign(p,Binary1_Left),
            Expression.And(p,Binary1_Right),
            p
        );
    }
    //protected override Expression OrElse(BinaryExpression Binary0) {
    //    var Binary1_Left = this.Traverse(Binary0.Left);
    //    var Or = Expression.Or(
    //        Binary1_Left,
    //        this.Traverse(Binary0.Right)
    //    );
    //    return this.AndAlso_OrElse(Binary1_Left,Binary1_Left,Or);
    //}
    /// <summary>
    /// a||b→operator false(a)?a|b:a
    /// </summary>
    /// <param name="Binary0"></param>
    /// <returns></returns>
    protected override Expression OrElse(BinaryExpression Binary0) {
        var Binary1_Left =this.Traverse(Binary0.Left);
        var Binary1_Right=this.Traverse(Binary0.Right);
        if(Binary1_Right.NodeType is ExpressionType.Constant or ExpressionType.Parameter) return Expression.Or(Binary1_Left,Binary1_Right);
        var p=Expression.Parameter(Binary1_Left.Type,"AndAlso");
        return this.AndAlso_OrElse(
            p,
            Expression.Assign(p,Binary1_Left),
            p,
            Expression.Or(p,Binary1_Right)
        );
    }
    #endif
    protected override Expression Constant(ConstantExpression Constant0) {
        if(!ILで直接埋め込めるか(Constant0.Type))
            this.DictionaryConstant[Constant0]=default!;
            //this.DictionaryConstant.TryAdd(Constant0,default!);
        return Constant0;
    }
    protected override Expression Quote(UnaryExpression Unary0) {
        var Constant=Expression.Constant(Unary0.Operand);
        this.DictionaryConstant[Constant]=default!;
        //this.DictionaryConstant.TryAdd(Constant,default!);
        return Constant;
    }

    private Expression 共通BinaryAssign(BinaryExpression Binary0, ExpressionType NodeType) {
        var Binary1_Left=this.Traverse(Binary0.Left);
        var Binary1_Right=this.Traverse(Binary0.Right);
        Expression Binary2_Right=Expression.MakeBinary(
            NodeType,
            Binary1_Left,
            Binary1_Right,
            Binary0.IsLiftedToNull,
            Binary0.Method,
            null
        );
        if(Binary0.Conversion is not null){
            var Binary0_Conversion=Binary0.Conversion;
            var Binary1_Conversion_Body=this.Traverse(Binary0_Conversion.Body);
            Binary2_Right=this.変換_旧Expressionを新Expression1.実行(Binary1_Conversion_Body,Binary0_Conversion.Parameters[0],Binary2_Right);
        }
        return Expression.Assign(
            Binary1_Left,
            Binary2_Right
        );
    }
    protected override Expression AddAssign(BinaryExpression Binary0)=> this.共通BinaryAssign(Binary0, ExpressionType.Add);
    protected override Expression AddAssignChecked(BinaryExpression Binary0)=> this.共通BinaryAssign(Binary0, ExpressionType.AddChecked);
    protected override Expression AndAssign(BinaryExpression Binary0) => this.共通BinaryAssign(Binary0, ExpressionType.And);
    protected override Expression DivideAssign(BinaryExpression Binary0)=> this.共通BinaryAssign(Binary0, ExpressionType.Divide);
    protected override Expression ExclusiveOrAssign(BinaryExpression Binary0)=> this.共通BinaryAssign(Binary0, ExpressionType.ExclusiveOr);
    protected override Expression LeftShiftAssign(BinaryExpression Binary0)=> this.共通BinaryAssign(Binary0, ExpressionType.LeftShift);
    protected override Expression ModuloAssign(BinaryExpression Binary0)=> this.共通BinaryAssign(Binary0, ExpressionType.Modulo);
    protected override Expression MultiplyAssign(BinaryExpression Binary0)=> this.共通BinaryAssign(Binary0, ExpressionType.Multiply);
    protected override Expression MultiplyAssignChecked(BinaryExpression Binary0)=> this.共通BinaryAssign(Binary0, ExpressionType.MultiplyChecked);
    protected override Expression OrAssign(BinaryExpression Binary0)=> this.共通BinaryAssign(Binary0, ExpressionType.Or);
    protected override Expression PowerAssign(BinaryExpression Binary0)=> this.共通BinaryAssign(Binary0, ExpressionType.Power);
    protected override Expression RightShiftAssign(BinaryExpression Binary0)=> this.共通BinaryAssign(Binary0, ExpressionType.RightShift);
    protected override Expression SubtractAssign(BinaryExpression Binary0)=> this.共通BinaryAssign(Binary0, ExpressionType.Subtract);
    protected override Expression SubtractAssignChecked(BinaryExpression Binary0)=> this.共通BinaryAssign(Binary0, ExpressionType.SubtractChecked);
    protected override Expression Try(TryExpression Try0){
        Debug.Assert(!(Try0.Finally is not null&&Try0.Fault is not null));
        var Try0_Handlers=Try0.Handlers;
        var Try0_Handlers_Count=Try0_Handlers.Count;
        var Try1_Handlers=new CatchBlock[Try0_Handlers_Count];
        var 変化したか=false;
        var Try0_Body=Try0.Body;
        var Try1_Body=this.Traverse(Try0_Body);
        if(Try0_Body!=Try1_Body)
            変化したか=true;
        var Try0_Finally=Try0.Finally;
        var Try1_Finally=this.TraverseNullable(Try0_Finally);
        if(Try0_Finally!=Try1_Finally)
            変化したか=true;
        for(var a=0;a<Try0_Handlers_Count;a++) {
            var Try0_Handler=Try0_Handlers[a];
            Debug.Assert(Try0_Handler!=null,nameof(Try0_Handler)+" != null");
            var Try0_Handler_Variable=Try0_Handler.Variable;
            CatchBlock Try1_Handler;
            if(Try0_Handler_Variable is not null) {
                var Try1_Handler_Body=this.Traverse(Try0_Handler.Body);
                var Try1_Handler_Filter=this.TraverseNullable(Try0_Handler.Filter);
                //Debug.Assert(Try1_Handler_Filter!=null,nameof(Try1_Handler_Filter)+" != null");
                if(Try0_Handler.Body!=Try1_Handler_Body||Try0_Handler.Filter!=Try1_Handler_Filter) {
                    変化したか=true;
                    Try1_Handler=Expression.Catch(Try0_Handler_Variable,Try1_Handler_Body,Try1_Handler_Filter);
                } else {
                    Try1_Handler=Try0_Handler;
                }
            } else {
                var Try1_Handler_Body=this.Traverse(Try0_Handler.Body);
                var Try1_Handler_Filter=this.TraverseNullable(Try0_Handler.Filter);
                if(Try0_Handler.Body!=Try1_Handler_Body||Try0_Handler.Filter!=Try1_Handler_Filter) {
                    変化したか=true;
                    Try1_Handler=Expression.Catch(Try0_Handler.Test,Try1_Handler_Body,Try1_Handler_Filter);
                } else {
                    Try1_Handler=Try0_Handler;
                }
            }
            Try1_Handlers[a]=Try1_Handler;
        }
        if(Try0.Fault is not null){
            Debug.Assert(Try0_Finally is null);
            var Try0_Fault=Try0.Fault;
            var Try1_Fault=this.Traverse(Try0_Fault);
            if(Try0_Fault!=Try1_Fault)変化したか=true;
            return 変化したか
                ? Expression.TryFault(Try1_Body,Try1_Fault)
                :Try0;
        } else{
            return 変化したか
                ? Expression.TryCatchFinally(Try1_Body,Try1_Finally,Try1_Handlers)
                :Try0;
        }
    }
    private Expression 共通Post(UnaryExpression Unary0, ExpressionType NodeType) {
        var Unary1_Operand=this.Traverse(Unary0.Operand);
        var 変数=Expression.Parameter(Unary0.Operand.Type);
        var 作業配列=this.作業配列;
        return Expression.Block(
            作業配列.Parameters設定(変数),
            作業配列.Expressions設定(
                Expression.Assign(変数,Unary1_Operand),
                Expression.Assign(
                    Unary1_Operand,
                    Expression.MakeUnary(NodeType,Unary1_Operand,Unary0.Type,Unary0.Method)
                ),
                変数
            )
        );
    }
    protected override Expression PostDecrementAssign(UnaryExpression Unary0)=> this.共通Post(Unary0, ExpressionType.Decrement);
    protected override Expression PostIncrementAssign(UnaryExpression Unary0)=> this.共通Post(Unary0, ExpressionType.Increment);
    private Expression 共通Pre(UnaryExpression Unary0, ExpressionType NodeType) {
        var Unary1_Operand=this.Traverse(Unary0.Operand);
        return Expression.Assign(
            Unary1_Operand,
            Expression.MakeUnary(NodeType,Unary1_Operand,Unary0.Type,Unary0.Method)
        );
    }
    protected override Expression PreDecrementAssign(UnaryExpression Unary0)=> this.共通Pre(Unary0, ExpressionType.Decrement);
    protected override Expression PreIncrementAssign(UnaryExpression Unary0)=> this.共通Pre(Unary0, ExpressionType.Increment);
    protected override Expression Lambda(LambdaExpression Lambda0) {
        var Lambda0_Parameters=Lambda0.Parameters;
        var Lambda1_Body=this.Traverse(Lambda0.Body);
        return Expression.Lambda(Lambda0.Type,Lambda1_Body,Lambda0.Name,Lambda0.TailCall,Lambda0_Parameters);
    }
    /// <summary>
    /// !(!(a))→a
    /// </summary>
    /// <param name="Unary0"></param>
    /// <returns></returns>
    protected override Expression Not(UnaryExpression Unary0) {
        var Unary0_Operand = Unary0.Operand;
        var Unary1_Operand = this.Traverse(Unary0_Operand);
        if(Unary1_Operand.NodeType==ExpressionType.Not)return ((UnaryExpression)Unary1_Operand).Operand;
        if(Unary0_Operand==Unary1_Operand)return Unary0;
        return Expression.Not(Unary1_Operand);
    }
    private Expression 共通ConvertConvertChecked(UnaryExpression Unary0, ExpressionType NodeType) {
        Debug.Assert(NodeType==ExpressionType.Convert||NodeType==ExpressionType.ConvertChecked);
        var Unary0_Type =Unary0.Type;
        var Unary0_Operand=Unary0.Operand;
        var Unary0_Operand_Type=Unary0_Operand.Type;
        //(Int32)3はキャストを除く
        //(Func<Int32>)(()=>3)は除かない
        if(Unary0_Type==Unary0_Operand_Type&&Unary0_Operand.NodeType!=ExpressionType.Lambda)return this.Traverse(Unary0_Operand);
        if(Unary0.Method is null)return base.Convert(Unary0);
        Debug.Assert(Unary0.Method.GetParameters().Length==1);
        //if(
        //    Unary0_Type==typeof(nint) && (Unary0_Operand_Type==typeof(int) ||Unary0_Operand_Type==typeof(long)) ||
        //    Unary0_Type==typeof(nuint) && (Unary0_Operand_Type==typeof(uint) ||Unary0_Operand_Type==typeof(ulong)) ||
        //    (Unary0_Type==typeof(int) || Unary0_Type==typeof(long)) && Unary0_Operand_Type==typeof(nint) ||
        //    (Unary0_Type==typeof(uint) || Unary0_Type==typeof(ulong)) && Unary0_Operand_Type==typeof(nuint)
        //)return Expression.MakeUnary(NodeType,this.Traverse(Unary0_Operand),Unary0_Type);
        var Unary1_Operand=this.Traverse(Unary0_Operand);
        if(Unary0_Operand==Unary1_Operand) return Unary0;
        return Expression.MakeUnary(NodeType,Unary1_Operand,Unary0_Type,Unary0.Method);
    }
    protected override Expression Convert(UnaryExpression Unary0)=> this.共通ConvertConvertChecked(Unary0, ExpressionType.Convert);
    //protected override Expression Convert(UnaryExpression Unary0) {
    //    var Unary0_Type = Unary0.Type;
    //    var Unary0_Operand = Unary0.Operand;
    //    if(Unary0_Operand is ConstantExpression Constant&&Unary0_Operand.Type==typeof(string)) {
    //        var Constant_Value=Constant.Value!;
    //        if(Unary0_Type==typeof(sbyte         ))return Expression.Constant(sbyte         .Parse((string)Constant_Value));
    //        if(Unary0_Type==typeof(short         ))return Expression.Constant(short         .Parse((string)Constant_Value));
    //        if(Unary0_Type==typeof(int           ))return Expression.Constant(int           .Parse((string)Constant_Value));
    //        if(Unary0_Type==typeof(long          ))return Expression.Constant(long          .Parse((string)Constant_Value));
    //        if(Unary0_Type==typeof(byte          ))return Expression.Constant(byte          .Parse((string)Constant_Value));
    //        if(Unary0_Type==typeof(ushort        ))return Expression.Constant(ushort        .Parse((string)Constant_Value));
    //        if(Unary0_Type==typeof(uint          ))return Expression.Constant(uint          .Parse((string)Constant_Value));
    //        if(Unary0_Type==typeof(ulong         ))return Expression.Constant(ulong         .Parse((string)Constant_Value));
    //        if(Unary0_Type==typeof(DateTime      ))return Expression.Constant(DateTime      .Parse((string)Constant_Value));
    //        if(Unary0_Type==typeof(DateTimeOffset))return Expression.Constant(DateTimeOffset.Parse((string)Constant_Value));
    //        if(Unary0_Type==typeof(Guid          ))return Expression.Constant(Guid          .Parse((string)Constant_Value));
    //        if(Unary0_Type==typeof(object))return Constant;
    //        Debug.Fail("ありえない");
    //    }
    //    return this.共通ConvertConvertChecked(Unary0,ExpressionType.Convert);
    //}
    protected override Expression ConvertChecked(UnaryExpression Unary0)=> this.共通ConvertConvertChecked(Unary0, ExpressionType.ConvertChecked);

    /// <summary>
    /// 末尾最適化できる部分多いが煩雑になるので素直にthis.Call再帰する。
    /// </summary>
    /// <param name="MethodCall0"></param>
    /// <returns></returns>
    protected override Expression Call(MethodCallExpression MethodCall0) {
        var MethodCall0_Method = MethodCall0.Method;
        //var MethodCall0_Arguments = MethodCall0.Arguments;
        var MethodCall1_Arguments = this.TraverseExpressions(MethodCall0.Arguments);
        if(MethodCall0_Method.IsStatic) {
            var MethodCall0_GenericMethodDefinition = GetGenericMethodDefinition(MethodCall0_Method);
            if(ループ展開可能メソッドか(MethodCall0_GenericMethodDefinition)) {
                //内部のSelectManyのにSelectManyのsource,selectorに分離する。
                //任意のメソッド
                //    SelectMany<TSource,TResult>
                //        O
                //        o=>I
                //    X
                //SelectMany<TSource,TResult>
                //    Where
                //        O
                //        o=>o==0
                //    o=>Where
                //        I
                //        i=>o==i&&i==1
                var 作業配列 = this.作業配列;
                switch(MethodCall0_Method.Name) {
                    case nameof(Linq.Enumerable.Average): {
                        //set.Average()は重複を除いて平均
                        //set.Average(p=>p)は重複ありで平均
                        //s.Average()→s.Average(p=>p)
                        if(Reflection.ExtensionEnumerable.AverageDecimal==MethodCall0_GenericMethodDefinition)
                            return 集約を集約_selectorに変換TSource(MethodCall0,Reflection.ExtensionEnumerable.AverageDecimal_selector);
                        if(Reflection.ExtensionEnumerable.AverageDouble==MethodCall0_GenericMethodDefinition)
                            return 集約を集約_selectorに変換TSource(MethodCall0,Reflection.ExtensionEnumerable.AverageDouble_selector);
                        if(Reflection.ExtensionEnumerable.AverageSingle==MethodCall0_GenericMethodDefinition)
                            return 集約を集約_selectorに変換TSource(MethodCall0,Reflection.ExtensionEnumerable.AverageSingle_selector);
                        if(Reflection.ExtensionEnumerable.AverageInt64==MethodCall0_GenericMethodDefinition)
                            return 集約を集約_selectorに変換TSource(MethodCall0,Reflection.ExtensionEnumerable.AverageInt64_selector);
                        if(Reflection.ExtensionEnumerable.AverageInt32==MethodCall0_GenericMethodDefinition)
                            return 集約を集約_selectorに変換TSource(MethodCall0,Reflection.ExtensionEnumerable.AverageInt32_selector);
                        if(Reflection.ExtensionEnumerable.AverageNullableDecimal==MethodCall0_GenericMethodDefinition)
                            return 集約を集約_selectorに変換TSource(MethodCall0,Reflection.ExtensionEnumerable.AverageNullableDecimal_selector);
                        if(Reflection.ExtensionEnumerable.AverageNullableDouble==MethodCall0_GenericMethodDefinition)
                            return 集約を集約_selectorに変換TSource(MethodCall0,Reflection.ExtensionEnumerable.AverageNullableDouble_selector);
                        if(Reflection.ExtensionEnumerable.AverageNullableSingle==MethodCall0_GenericMethodDefinition)
                            return 集約を集約_selectorに変換TSource(MethodCall0,Reflection.ExtensionEnumerable.AverageNullableSingle_selector);
                        if(Reflection.ExtensionEnumerable.AverageNullableInt64==MethodCall0_GenericMethodDefinition)
                            return 集約を集約_selectorに変換TSource(MethodCall0,Reflection.ExtensionEnumerable.AverageNullableInt64_selector);
                        if(Reflection.ExtensionEnumerable.AverageNullableInt32==MethodCall0_GenericMethodDefinition)
                            return 集約を集約_selectorに変換TSource(MethodCall0,Reflection.ExtensionEnumerable.AverageNullableInt32_selector);
                        break;
                        MethodCallExpression 集約を集約_selectorに変換TSource(MethodCallExpression MethodCall00,MethodInfo 集約_selector) {
                            var 作業配列0 = this.作業配列;
                            var SourceType=MethodCall0_Method.GetParameters()[0].ParameterType.GetGenericArguments()[0];
                            var p = Expression.Parameter(SourceType,$"Averageﾟ{this.番号++}");
                            var MethodCall01_Arguments_0 = this.Traverse(MethodCall00.Arguments[0]);
                            return Expression.Call(
                                作業配列0.MakeGenericMethod(
                                    集約_selector,
                                    SourceType
                                ),
                                MethodCall01_Arguments_0,
                                Expression.Lambda(
                                    p,
                                    作業配列0.Parameters設定(p)
                                )
                            );
                        }
                    }
                    case nameof(Linq.Enumerable.Any): {
                        //s.Select().Any()→s.Any()
                        //s.GroupJoin().Any()→s.Any()
                        var MethodCall1_Arguments_0=MethodCall1_Arguments[0];
                        while(MethodCall1_Arguments_0 is MethodCallExpression MethodCall){
                            if(ループ展開可能メソッドか(MethodCall)){
                                if(MethodCall.Method.Name is nameof(Linq.Enumerable.GroupJoin) or nameof(Linq.Enumerable.Select)){
                                    MethodCall1_Arguments_0=MethodCall.Arguments[0];
                                } else
                                    break;
                            } else
                                break;
                        }
                        var GenericArguments = this.作業配列.GetGenericArguments(MethodCall1_Arguments_0.Type);
                        MethodCallExpression MethodCall1;
                        if(Reflection.ExtensionEnumerable.Any_predicate==MethodCall0_GenericMethodDefinition) {
                            //s.Any(p)→s.Where(p).Any()
                            MethodCall1=Expression.Call(
                                Reflection.ExtensionEnumerable.Any.MakeGenericMethod(GenericArguments),
                                Expression.Call(
                                    Reflection.ExtensionEnumerable.Where.MakeGenericMethod(GenericArguments),
                                    MethodCall1_Arguments_0,
                                    MethodCall1_Arguments[1]
                                )
                            );
                        } else {
                            Debug.Assert(
                                Reflection.ExtensionSet.Any==MethodCall0_GenericMethodDefinition||
                                Reflection.ExtensionEnumerable.Any==MethodCall0_GenericMethodDefinition
                            );
                            MethodCall1=Expression.Call(
                                MethodCall0_GenericMethodDefinition.MakeGenericMethod(GenericArguments),
                                MethodCall1_Arguments_0
                            );
                        }
                        return MethodCall1;
                    }
                    case nameof(Linq.Enumerable.Contains): {
                        //s.Contains(x)→s.Where(p=>p.Equals(x)).Any()
                        var MethodCall1_Arguments_0 = MethodCall1_Arguments[0];
                        var MethodCall1_Arguments_0_Type = MethodCall1_Arguments_0.Type;
                        Type[] GenericArguments;
                        Type GenericArgument;
                        if(MethodCall1_Arguments_0_Type.IsArray) {
                            GenericArgument=MethodCall1_Arguments_0_Type.GetElementType()!;
                            GenericArguments=作業配列.Types設定(GenericArgument);
                        } else {
                            GenericArguments=IEnumerable1(MethodCall1_Arguments_0_Type).GetGenericArguments();
                            //while(true) {
                            //    var GenericTypeDefinition = Set1;
                            //    if(GenericTypeDefinition.IsGenericType) {
                            //        GenericTypeDefinition=Set1.GetGenericTypeDefinition();
                            //    }
                            //    ////Set.Containsはインラインではない
                            //    //if(GenericTypeDefinition==typeof(ImmutableSet<>)) {
                            //    //    GenericArguments=Set1.GetGenericArguments();
                            //    //    break;
                            //    //}
                            //    Set1=Set1.BaseType;
                            //    if(Set1 is null) {
                            //        GenericArguments=IEnumerable1(MethodCall1_Arguments_0_Type).GetGenericArguments();
                            //        break;
                            //    }
                            //}
                            GenericArgument=GenericArguments[0];
                        }
                        Debug.Assert(GenericArgument is not null,"GenericArgument != null");
                        var p = Expression.Parameter(
                            GenericArgument,
                            $"Containsﾟ{this.番号++}"
                        );
                        var (Where, Any)=MethodCall0_GenericMethodDefinition.DeclaringType==typeof(ExtensionSet)
                            ? (Reflection.ExtensionSet.Where, Reflection.ExtensionSet.Any)
                            : (Reflection.ExtensionEnumerable.Where, Reflection.ExtensionEnumerable.Any);
                        Any=Any.MakeGenericMethod(GenericArguments);
                        Where=Where.MakeGenericMethod(MethodCall0_Method.GetGenericArguments());
                        var q = MethodCall1_Arguments[1];
                        Expression EqualExpression;
                        var p_Type = p.Type;
                        var q_Type = q.Type;
                        //Object a;Int32 b
                        //aは上位クラスでもいい。
                        Debug.Assert(p_Type.IsAssignableFrom(q.Type));
                        if(p_Type.IsPrimitive) {
                            //Contains(primitive)
                            EqualExpression=Expression.Equal(
                                p,
                                q
                            );
                        } else {
                            var IEquatableType = typeof(IEquatable<>).MakeGenericType(GenericArguments);
                            if(IEquatableType.IsAssignableFrom(p_Type)) {
                                //Contains(decimal)
                                var InterfaceMap = p_Type.GetInterfaceMap(IEquatableType);
                                Debug.Assert(InterfaceMap.InterfaceMethods[0]==
                                             IEquatableType.GetMethod(nameof(IEquatable<int>.Equals)));
                                EqualExpression=Expression.Call(
                                    p,
                                    InterfaceMap.TargetMethods[0],
                                    作業配列.Expressions設定(q)
                                );
                            } else {
                                //if(q_Type.IsValueType) {
                                //    q=Expression.Convert(
                                //        q,
                                //        typeof(object)
                                //    );
                                //}
                                EqualExpression=Expression.Call(
                                    p,
                                    Reflection.Object.Equals_,
                                    作業配列.Expressions設定(q)
                                );
                            }
                        }
                        return Expression.Call(
                            Any,
                            Expression.Call(
                                Where,
                                MethodCall1_Arguments_0,
                                Expression.Lambda(
                                    EqualExpression,
                                    作業配列.Parameters設定(p)
                                )
                            )
                        );
                    }
                    case nameof(Linq.Enumerable.GroupBy):{
                        //GroupBy(keySelector,resultSelector)→GroupBy(keySelector,(key,g)=>resultSelector(g))
                        if(Reflection.ExtensionEnumerable.GroupBy_keySelector_resultSelector==MethodCall0_GenericMethodDefinition) {
                            return GroupBy_keySelector_resultSelector(Reflection.ExtensionEnumerable.GroupBy_keySelector_elementSelector,Reflection.ExtensionEnumerable.Select_selector,typeof(Linq.IGrouping<,>));
                        } else if(Reflection.ExtensionEnumerable.GroupBy_keySelector_resultSelector_comparer==MethodCall0_GenericMethodDefinition) {
                            return GroupBy_keySelector_resultSelector(Reflection.ExtensionEnumerable.GroupBy_keySelector_elementSelector_comparer,Reflection.ExtensionEnumerable.Select_selector,typeof(Linq.IGrouping<,>));
                        } else if(Reflection.ExtensionSet.GroupBy_keySelector_resultSelector==MethodCall0_GenericMethodDefinition) {
                            return GroupBy_keySelector_resultSelector(Reflection.ExtensionSet.GroupBy_keySelector_elementSelector,Reflection.ExtensionSet.Select_selector,typeof(IGrouping<,>));
                        }
                        if(Reflection.ExtensionEnumerable.GroupBy_keySelector_elementSelector_resultSelector==MethodCall0_GenericMethodDefinition) {
                            return GroupBy_keySelector_elementSelector_resultSelector(Reflection.ExtensionEnumerable.GroupBy_keySelector_elementSelector,Reflection.ExtensionEnumerable.Select_selector,typeof(Linq.IGrouping<,>));
                        } else if(Reflection.ExtensionEnumerable.GroupBy_keySelector_elementSelector_resultSelector_comparer==MethodCall0_GenericMethodDefinition) {
                            return GroupBy_keySelector_elementSelector_resultSelector(Reflection.ExtensionEnumerable.GroupBy_keySelector_elementSelector_comparer,Reflection.ExtensionEnumerable.Select_selector,typeof(Linq.IGrouping<,>));
                        } else if(Reflection.ExtensionSet.GroupBy_keySelector_elementSelector_resultSelector==MethodCall0_GenericMethodDefinition) {
                            return GroupBy_keySelector_elementSelector_resultSelector(Reflection.ExtensionSet.GroupBy_keySelector_elementSelector,Reflection.ExtensionSet.Select_selector,typeof(IGrouping<,>));
                        }
                        if(Reflection.ExtensionEnumerable.GroupBy_keySelector==MethodCall0_GenericMethodDefinition) {
                            return GroupBy_keySelector(Reflection.ExtensionEnumerable.GroupBy_keySelector_elementSelector);
                        } else if(Reflection.ExtensionEnumerable.GroupBy_keySelector_comparer==MethodCall0_GenericMethodDefinition) {
                            return GroupBy_keySelector(Reflection.ExtensionEnumerable.GroupBy_keySelector_elementSelector_comparer);
                        } else if(Reflection.ExtensionSet.GroupBy_keySelector==MethodCall0_GenericMethodDefinition) {
                            return GroupBy_keySelector(Reflection.ExtensionSet.GroupBy_keySelector_elementSelector);
                        }
                        break;
                        Expression GroupBy_keySelector_resultSelector(MethodInfo Method,MethodInfo Select_selector,Type IGrouping) {
                            //source.GroupBy(x => x.Id).Select(g =>new{Id=g.Key,Count=g.Count()})
                            //source.GroupBy(x => x.Id,   (Key,g)=>new{Id=  Key,Count=g.Count()})
                            var MethodCall1_Arguments_0 = MethodCall1_Arguments[0];
                            var MethodCall1_Arguments_1 = MethodCall1_Arguments[1];
                            var MethodCall1_Arguments_2 = MethodCall1_Arguments[2];
                            var GenericArguments = MethodCall0_Method.GetGenericArguments();
                            var TSource = GenericArguments[0];
                            var TKey = GenericArguments[1];
                            var TResult = GenericArguments[2];
                            Method=作業配列.MakeGenericMethod(
                                Method,
                                TSource,
                                TKey,
                                TSource
                            );
                            var e = Expression.Parameter(TSource,"e");
                            var elementSelector = Expression.Lambda(
                                e,
                                作業配列.Parameters設定(e)
                            );
                            MethodCallExpression GroupBy;
                            if(MethodCall1_Arguments.Count==3) {
                                //compareなし
                                GroupBy=Expression.Call(
                                    Method,
                                    MethodCall1_Arguments_0,
                                    MethodCall1_Arguments_1,
                                    elementSelector
                                );
                            } else {
                                GroupBy=Expression.Call(
                                    Method,
                                    MethodCall1_Arguments_0,
                                    MethodCall1_Arguments_1,
                                    elementSelector,
                                    MethodCall1_Arguments[3]
                                );
                            }
                            //var TGrouping = Method.ReturnType.GetGenericArguments()[0];
                            //var T=Method.ReturnType.GetGenericArguments()[0];
                            var TGrouping=Method.ReturnType.GetGenericArguments()[0].GetInterface(IGrouping);
                            var p = Expression.Parameter(TGrouping,"p");
                            var Property=IGrouping==typeof(Linq.IGrouping<,>)
                                ?TGrouping.GetProperty(nameof(Linq.IGrouping<int,int>.Key))
                                :typeof(Linq.IGrouping<,>).MakeGenericType(TGrouping.GetGenericArguments()).GetProperty(nameof(Linq.IGrouping<int,int>.Key));
                            var p_Key = Expression.Property(p,Property);
                            //var p_Source = p;
                            Expression selector_Body;
                            if(MethodCall1_Arguments_2 is LambdaExpression resultSelector) {
                                //O.GroupBy<TSource,TKey,TResult>(MethodCall1_Arguments_1,                        (TKey Key,TSource Source)=>new{Id=         Key,Count=Source  .Count()})
                                //O.GroupBy<TSource,TKey>        (MethodCall1_Arguments_1).Select<TResult>((IGrouping<TKey,TSource>Grouping=>new{Id=Grouping.Key,Count=Grouping.Count()})
                                var resultSelector_Parameters = resultSelector.Parameters;
                                selector_Body=this.変換_旧Parameterを新Expression2.実行(
                                    resultSelector.Body,
                                    resultSelector_Parameters[0],
                                    p_Key,
                                    resultSelector_Parameters[1],
                                    p
                                );
                            } else {
                                //O.GroupBy<TSource,TKey,TResult>(MethodCall1_Arguments_1,                     resultSelector         )
                                //O.GroupBy<TSource,TKey>        (MethodCall1_Arguments_1).Select<TResult>(g =>resultSelector(g.Key,g))
                                selector_Body=Expression.Invoke(
                                    MethodCall1_Arguments_2,
                                    作業配列.Expressions設定(
                                        p_Key,
                                        p
                                    )
                                );
                            }
                            return Expression.Call(
                                作業配列.MakeGenericMethod(
                                    Select_selector,
                                    TGrouping,
                                    TResult
                                ),
                                GroupBy,
                                Expression.Lambda(
                                    selector_Body,
                                    作業配列.Parameters設定(p)
                                )
                            );
                        }
                        Expression GroupBy_keySelector_elementSelector_resultSelector(MethodInfo GroupBy_keySelector_elementSelector,MethodInfo Select_selector,Type IGrouping) {
                            //source.GroupBy(x => x.Id).Select(g =>new{Id=g.Key,Count=g.Count()})
                            //source.GroupBy(x => x.Id,   (Key,g)=>new{Id=  Key,Count=g.Count()})
                            //source.GroupBy<TSource,TKey,TElemnt,TResult>(keySelector,elementSelector,resultSelector)
                            //source.GroupBy<TSource,TKey,TElemnt>(keySelector,elementSelector).Select<TGropuing,TResult>(IGrouping=>resultSelect(IGrouping.Key,IGrouping))
                            var MethodCall1_Arguments_0 = MethodCall1_Arguments[0];
                            var MethodCall1_Arguments_1 = MethodCall1_Arguments[1];
                            var MethodCall1_Arguments_2 = MethodCall1_Arguments[2];
                            var MethodCall1_Arguments_3 = MethodCall1_Arguments[3];
                            var GenericArguments = MethodCall0_Method.GetGenericArguments();
                            var TSource = GenericArguments[0];
                            var TKey = GenericArguments[1];
                            var TElement = GenericArguments[2];
                            var TResult = GenericArguments[3];
                            var Method = 作業配列.MakeGenericMethod(
                                GroupBy_keySelector_elementSelector,
                                TSource,
                                TKey,
                                TElement
                            );
                            MethodCallExpression GroupBy;
                            if(MethodCall1_Arguments.Count==4) {
                                GroupBy=Expression.Call(
                                    Method,
                                    MethodCall1_Arguments_0,
                                    MethodCall1_Arguments_1,
                                    MethodCall1_Arguments_2
                                );
                            } else {
                                GroupBy=Expression.Call(
                                    Method,
                                    MethodCall1_Arguments_0,
                                    MethodCall1_Arguments_1,
                                    MethodCall1_Arguments_2,
                                    MethodCall1_Arguments[4]
                                );
                            }
                            var TGrouping = Method.ReturnType.GetGenericArguments()[0];
                            var p = Expression.Parameter(TGrouping,"p");
                            var Property=IGrouping==typeof(Linq.IGrouping<,>)
                                ?TGrouping.GetProperty(nameof(Linq.IGrouping<int,int>.Key))
                                :typeof(Linq.IGrouping<,>).MakeGenericType(TGrouping.GetGenericArguments()).GetProperty(nameof(Linq.IGrouping<int,int>.Key));
                            var p_Key = Expression.Property(p,Property);
                            Expression selector_Body;
                            if(MethodCall1_Arguments_3 is LambdaExpression resultSelector) {
                                //O.GroupBy<TSource,TKey,TResult>(MethodCall1_Arguments_1,                         (TKey Key,TSource Source)=>new{Id=         Key,Count=Source  .Count()})
                                //O.GroupBy<TSource,TKey>        (MethodCall1_Arguments_1).Select<TResult>((IGrouping<TKey,TSource>Grouping)=>new{Id=Grouping.Key,Count=Grouping.Count()})
                                var resultSelector_Parameters = resultSelector.Parameters;
                                selector_Body=this.変換_旧Parameterを新Expression2.実行(
                                    resultSelector.Body,
                                    resultSelector_Parameters[0],
                                    p_Key,
                                    resultSelector_Parameters[1],
                                    p
                                );
                            } else {
                                //O.GroupBy<TSource,TKey,TResult>(MethodCall1_Arguments_1,                     resultSelector         )
                                //O.GroupBy<TSource,TKey>        (MethodCall1_Arguments_1).Select<TResult>(g =>resultSelector(g.Key,g))
                                selector_Body=Expression.Invoke(
                                    MethodCall1_Arguments_3,
                                    作業配列.Expressions設定(
                                        p_Key,
                                        p
                                    )
                                );
                            }
                            return Expression.Call(
                                作業配列.MakeGenericMethod(
                                    Select_selector,
                                    TGrouping,
                                    TResult
                                ),
                                GroupBy,
                                Expression.Lambda(
                                    selector_Body,
                                    作業配列.Parameters設定(p)
                                )
                            );
                        }
                        Expression GroupBy_keySelector(MethodInfo GroupBy_keySelector_elementSelector) {
                            //source.GroupBy<TSource,TKey,TElemnt,TResult>(keySelector,elementSelector,resultSelector)
                            //source.GroupBy<TSource,TKey,TElemnt>(keySelector,elementSelector).Select<TGropuing,TResult>(IGrouping=>resultSelect(IGrouping.Key,IGrouping))
                            var MethodCall1_Arguments_0 = MethodCall1_Arguments[0];
                            var MethodCall1_Arguments_1 = MethodCall1_Arguments[1];
                            var GenericArguments = MethodCall0_Method.GetGenericArguments();
                            var TSource = GenericArguments[0];
                            var TKey = GenericArguments[1];
                            GroupBy_keySelector_elementSelector=作業配列.MakeGenericMethod(
                                GroupBy_keySelector_elementSelector,
                                TSource,
                                TKey,
                                TSource
                            );
                            var e = Expression.Parameter(TSource,"e");
                            var elementSelector = Expression.Lambda(
                                e,
                                作業配列.Parameters設定(e)
                            );
                            if(MethodCall1_Arguments.Count==2) {
                                return Expression.Call(
                                    GroupBy_keySelector_elementSelector,
                                    MethodCall1_Arguments_0,
                                    MethodCall1_Arguments_1,
                                    elementSelector
                                );
                            } else {
                                return Expression.Call(
                                    GroupBy_keySelector_elementSelector,
                                    MethodCall1_Arguments_0,
                                    MethodCall1_Arguments_1,
                                    elementSelector,
                                    MethodCall1_Arguments[2]
                                );
                            }
                        }
                    }
                    case nameof(Linq.Enumerable.GroupJoin): {
                        var MethodCall1_Arguments_0 = MethodCall1_Arguments[0];
                        var MethodCall1_Arguments_1 = MethodCall1_Arguments[1];
                        var MethodCall1_Arguments_2 = MethodCall1_Arguments[2];
                        var MethodCall1_Arguments_3 = MethodCall1_Arguments[3];
                        var MethodCall1_Arguments_4 = MethodCall1_Arguments[4];
                        var MethodCall1_Arguments_5=MethodCall1_Arguments.Count==6//引数5にはComparerがあるのでそれで比較する。
                            ?MethodCall1_Arguments[5]
                            :null;
                        var GenericArguments = MethodCall0_Method.GetGenericArguments();
                        var TOuter = GenericArguments[0];
                        var TInner = GenericArguments[1];
                        var TKey = GenericArguments[2];
                        var TResult = GenericArguments[3];
                        Expression Equals_this;
                        ParameterExpression o;
                        if(MethodCall1_Arguments_2 is LambdaExpression outerKeySelector) {
                            //O.GroupJoin<TOuter,TInner,TKey,TResult>(I,o=>o,i=>i,(o,i)=>o,i)
                            //O.Select   <TOuter,            TResult>(o=>I.Where(i=>o==i)   )
                            o=outerKeySelector.Parameters[0];
                            Equals_this=outerKeySelector.Body;
                        } else {
                            //O.GroupJoin<TOuter,TInner,TKey,TResult>(   I,outerKeySelector,i=>i,(o,i)=>o,i)
                            //O.Select   <TOuter,            TResult>(o=>I.Where(i=>outerKeySelector(o).Equals(i)))
                            o=Expression.Parameter(TOuter,"o");
                            Equals_this=Expression.Invoke(
                                MethodCall1_Arguments_2,
                                o
                            );
                        }
                        Expression Equals_Argument;
                        ParameterExpression i;
                        if(MethodCall1_Arguments_3 is LambdaExpression innerKeySelector) {
                            //O.GroupJoin<TOuter,TInner,TKey,TResult>(I,o=>o,i=>i,(o,i)=>o,i)
                            //O.Select   <TOuter,            TResult>(o=>I.Where(i=>o==i)   )
                            i=innerKeySelector.Parameters[0];
                            Equals_Argument=innerKeySelector.Body;
                        } else {
                            //O.GroupJoin<TOuter,TInner,TKey,TResult>(   I,o=>      o,innerKeySelector,(o,i)=>o,i)
                            //O.Select   <TOuter,            TResult>(o=>I.Where(i=>o.Equals(innerKeySelector(i))))
                            i=Expression.Parameter(TInner,"i");
                            Equals_Argument=Expression.Invoke(
                                MethodCall1_Arguments_3,
                                i
                            );
                        }
                        MethodInfo Where_predicate;
                        MethodInfo Select_selector;
                        if(typeof(Linq.Enumerable)==MethodCall0_Method.DeclaringType) {
                            Where_predicate=Reflection.ExtensionEnumerable.Where;
                            Select_selector=Reflection.ExtensionEnumerable.Select_selector;
                        } else {
                            Debug.Assert(typeof(ExtensionSet)==MethodCall0_Method.DeclaringType);
                            Where_predicate=Reflection.ExtensionSet.Where;
                            Select_selector=Reflection.ExtensionSet.Select_selector;
                        }
                        Type MethodCall1_Arguments_5_Type;
                        if(MethodCall1_Arguments_5 is not null) {
                            //引数5にはComparerがあるのでそれで比較する。
                            //O.GroupJoin(I,o=>o,i=>i,???)
                            //O.Select(o=>I.Where(i=>EqualityComparer.Equal(i,o).???)
                            MethodCall1_Arguments_5_Type = MethodCall1_Arguments_5.Type;
                            Debug.Assert(MethodCall1_Arguments_5_Type.GetInterface(CommonLibrary.IEqualityComparer_FullName)!.GetGenericArguments()[0]==TKey);
                            //var T = MethodCall1_Arguments_5_Type.GetInterface(CommonLibrary.IEqualityComparer_FullName)!.GetGenericArguments()[0];
                            //predicate_Body=Expression.Call(
                            //    MethodCall1_Arguments_5,
                            //    作業配列.GetMethod(MethodCall1_Arguments_5_Type,nameof(Generic.IEqualityComparer<int>.Equals),TKey,TKey),
                            //    Equals_this,
                            //    Equals_Argument
                            //);
                        } else {
                            MethodCall1_Arguments_5_Type = 作業配列.MakeGenericType(typeof(Generic.EqualityComparer<>),TKey);
                            MethodCall1_Arguments_5=Expression.Call(MethodCall1_Arguments_5_Type.GetProperty(nameof(Generic.EqualityComparer<int>.Default))!.GetMethod);
                            //predicate_Body=Expression.Call(
                            //    作業配列.MakeGenericType(
                            //        typeof(Generic.EqualityComparer<>),
                            //        TKey
                            //    ).GetProperty(nameof(Generic.EqualityComparer<int>.Default))!.GetMethod,
                            //    //作業配列.MakeGenericMethod(
                            //    //    Reflection.Helpers.EqualityComparer_Equals,
                            //    //    TKey
                            //    //),
                            //    Equals_this,
                            //    Equals_Argument
                            //);
                        }
                        var predicate_Body=Expression.Call(
                            MethodCall1_Arguments_5,
                            作業配列.GetMethod(MethodCall1_Arguments_5_Type,nameof(Generic.IEqualityComparer<int>.Equals),TKey,TKey),
                            Equals_this,
                            Equals_Argument
                        );
                        var Where = Expression.Call(
                            作業配列.MakeGenericMethod(
                                Where_predicate,
                                TInner
                            ),
                            MethodCall1_Arguments_1,
                            Expression.Lambda(
                                predicate_Body,
                                作業配列.Parameters設定(i)
                            )
                        );
                        Expression selector_Body;
                        if(MethodCall1_Arguments_4 is LambdaExpression resultSelector) {
                            //O.GroupJoin<TOuter,TInner,TKey,TResult>(I,o=>o,i=>i,(o,i)=>o,i)
                            //O.Select   <TOuter,            TResult>(o=>o,I.Where(i=>o.Equals(i)))
                            var resultSelector_Parameters = resultSelector.Parameters;
                            selector_Body=this.変換_旧Parameterを新Expression2.実行(
                                resultSelector.Body,
                                resultSelector_Parameters[0],
                                o,
                                resultSelector_Parameters[1],
                                Where
                            );
                        } else {
                            //O.GroupJoin<TOuter,TInner,TKey,TResult>(I,o=>o,i=>i,resultSelector)
                            //O.Select   <TOuter,            TResult>(o=>resultSelector(o,I.Where(i=>o.Equals(i))))
                            selector_Body=Expression.Invoke(
                                MethodCall1_Arguments_4,
                                作業配列.Expressions設定(
                                    o,
                                    Where
                                )
                            );
                        }
                        var Select=Expression.Call(
                            作業配列.MakeGenericMethod(
                                Select_selector,
                                TOuter,
                                TResult
                            ),
                            MethodCall1_Arguments_0,
                            Expression.Lambda(
                                selector_Body,
                                作業配列.Parameters設定(o)
                            )
                        );
                        return this.Call(Select);//Selectを作ったのでそれの形を最適化する。
                    }
                    case nameof(Linq.Enumerable.Intersect): {
                        var MethodCall1_Arguments_0 = MethodCall1_Arguments[0];
                        var MethodCall1_Arguments_1 = MethodCall1_Arguments[1];
                        //Intersect
                        //    SelectMany
                        //        MethodCall1_MethodCall.Arguments[0]
                        //        o=>x
                        //    MethodCall1_Arguments_1
                        //SelectMany
                        //    MethodCall1_MethodCall.Arguments[0]
                        //    o=>Intersect
                        //        x
                        //        MethodCall1_Arguments_1
                        if(typeof(ExtensionSet)==MethodCall0_Method.DeclaringType) {
                            //O.SelectMany(o=>I).Intersect(X)→O.SelectMany(o=>I.Intersect(X))
                            //O.SelectMany(selector).Intersect(X)→O.SelectMany(o=>selector(o).Intersect(X))
                            var SelectMany = this.条件が合えば内部SelectManyのselector_Bodyに外部メソッドを入れる(
                                MethodCall0_Method,
                                MethodCall1_Arguments_0,
                                MethodCall1_Arguments_1
                            );
                            if(SelectMany is not null) return SelectMany;
                        }
                        if(ループ展開可能なSetのCall(MethodCall1_Arguments_1) is null) {
                            //A.Intersect(B).Where(predicate)のWhere
                            (MethodCall1_Arguments_0,MethodCall1_Arguments_1)=(MethodCall1_Arguments_1,MethodCall1_Arguments_0);
                        }
                        if(MethodCall1_Arguments.Count==3) {
                            Debug.Assert(Reflection.ExtensionEnumerable.Intersect_comparer==MethodCall0_GenericMethodDefinition);
                            return Expression.Call(
                                MethodCall0_Method,
                                MethodCall1_Arguments_0,
                                MethodCall1_Arguments_1,
                                MethodCall1_Arguments[2]
                            );
                        } else {
                            Debug.Assert(Reflection.ExtensionSet.Intersect==MethodCall0_GenericMethodDefinition||Reflection.ExtensionEnumerable.Intersect==MethodCall0_GenericMethodDefinition);
                            return Expression.Call(
                                MethodCall0_Method,
                                MethodCall1_Arguments_0,
                                MethodCall1_Arguments_1
                            );
                        }
                    }
                    case nameof(Linq.Enumerable.Join): {
                        var MethodCall1_Arguments_0 = MethodCall1_Arguments[0];
                        var MethodCall1_Arguments_1 = MethodCall1_Arguments[1];
                        var MethodCall1_Arguments_2 = MethodCall1_Arguments[2];
                        var MethodCall1_Arguments_3 = MethodCall1_Arguments[3];
                        var MethodCall1_Arguments_4 = MethodCall1_Arguments[4];
                        var MethodCall1_Arguments_5=MethodCall1_Arguments.Count==6//引数5にはComparerがあるのでそれで比較する。
                            ?MethodCall1_Arguments[5]
                            :null;
                        MethodInfo SelectMany_selector, Select_selector, Where_predicate;
                        //Join,SelectManyのresultSelectorが(o,i)=>new { o,i }でなければそれに置換する
                        if(Reflection.ExtensionSet.Join==MethodCall0_GenericMethodDefinition) {
                            SelectMany_selector=Reflection.ExtensionSet.SelectMany_selector;
                            Select_selector=Reflection.ExtensionSet.Select_selector;
                            Where_predicate=Reflection.ExtensionSet.Where;
                        } else {
                            Debug.Assert(typeof(Linq.Enumerable)==MethodCall0_GenericMethodDefinition.DeclaringType);
                            SelectMany_selector=Reflection.ExtensionEnumerable.SelectMany_selector;
                            Select_selector=Reflection.ExtensionEnumerable.Select_selector;
                            Where_predicate=Reflection.ExtensionEnumerable.Where;
                        }
                        var MethodCall0_Method_GetGenericArguments = MethodCall0_Method.GetGenericArguments();
                        var TOuter = MethodCall0_Method_GetGenericArguments[0];
                        var TInner = MethodCall0_Method_GetGenericArguments[1];
                        var TKey = MethodCall0_Method_GetGenericArguments[2];
                        var TResult = MethodCall0_Method_GetGenericArguments[3];
                        LambdaExpression selector;
                        Expression Equals_this;
                        Expression Equals_Argument;
                        Generic.IEnumerable<ParameterExpression> predicate_Parameters;
                        ParameterExpression o;
                        if(MethodCall1_Arguments_2 is LambdaExpression outerKeySelector) {
                            o=outerKeySelector.Parameters[0];
                            if(MethodCall1_Arguments_3 is LambdaExpression innerKeySelector) {
                                Equals_Argument=innerKeySelector.Body;
                                var innerKeySelector_Parameters = innerKeySelector.Parameters;
                                predicate_Parameters=innerKeySelector_Parameters;
                                if(MethodCall1_Arguments_4 is LambdaExpression resultSelector) {
                                    //O.Join      <TOuter,TInner,TKey,TResult>(I,o=>o*o,i=>i*o,(o,i)=>new{o,i})
                                    //O.SelectMany<TOuter,            TResult>(o=>I.Where<TInner>(i=>(o*o).Equals(i*i).Select<TInner,TResult>(i=>new{o,i})
                                    selector=Expression.Lambda(
                                        this.変換_旧Parameterを新Expression1.実行(
                                            resultSelector.Body,
                                            resultSelector.Parameters[0],
                                            o
                                        ),
                                        作業配列.Parameters設定(resultSelector.Parameters[1])
                                    );
                                } else {
                                    //O.Join      <TOuter,TInner,TKey,TResult>(I,o=>o*o,i=>i*o,resultSelector)
                                    //O.SelectMany<TOuter,            TResult>(o=>I.Where<TInner>(i=>(o*o).Equals(i*i).Select<TInner,TResult>(i=>resultSelector(o,i))
                                    selector=Expression.Lambda(
                                        Expression.Invoke(
                                            MethodCall1_Arguments_4,
                                            作業配列.Expressions設定(
                                                o,
                                                innerKeySelector_Parameters[0]
                                            )
                                        ),
                                        innerKeySelector_Parameters
                                    );
                                }
                            } else {
                                ParameterExpression i;
                                if(MethodCall1_Arguments_4 is LambdaExpression resultSelector) {
                                    //O.Join      <TOuter,TInner,TKey,TResult>(I,o=>o*o,innerKeySelector,(o,i)=>new{o,i})
                                    //O.SelectMany<TOuter,            TResult>(o=>I.Where<TInner>(i=>(o*o).Equals(innerKeySelector(i)).Select<TInner,TResult>(i=>new{o,i})
                                    var resultSelector_Parameters = resultSelector.Parameters;
                                    i=resultSelector_Parameters[1];
                                    selector=Expression.Lambda(
                                        this.変換_旧Parameterを新Expression1.実行(
                                            resultSelector.Body,
                                            resultSelector_Parameters[0],
                                            o
                                        ),
                                        作業配列.Parameters設定(i)
                                    );
                                } else {
                                    //O.Join      <TOuter,TInner,TKey,TResult>(I,o=>o*o,innerKeySelector,resultSelector)
                                    //O.SelectMany<TOuter,            TResult>(o=>I.Where<TInner>(i=>(o*o).Equals(innerKeySelector(i)).Select<TInner,TResult>(i=>resultSelector(o,i))
                                    i=Expression.Parameter(TInner,"i2");
                                    selector=Expression.Lambda(
                                        Expression.Invoke(
                                            MethodCall1_Arguments_4,
                                            作業配列.Expressions設定(
                                                o,
                                                i
                                            )
                                        ),
                                        作業配列.Parameters設定(i)
                                    );
                                }
                                Equals_Argument=Expression.Invoke(
                                    MethodCall1_Arguments_3,
                                    i
                                );
                                predicate_Parameters=作業配列.Parameters設定(i);
                            }
                            Equals_this=outerKeySelector.Body;
                        } else {
                            if(MethodCall1_Arguments_3 is LambdaExpression innerKeySelector) {
                                var innerKeySelector_Parameters = innerKeySelector.Parameters;
                                predicate_Parameters=innerKeySelector.Parameters;
                                var innerKeySelector_Body = innerKeySelector.Body;
                                if(MethodCall1_Arguments_4 is LambdaExpression resultSelector) {
                                    //O.Join      <TOuter,TInner,TKey,TResult>(I,outerKeySelector,i0=>i0*i0,(o,i)=>{o,i)
                                    //O.SelectMany<TOuter,            TResult>(o=>I.Where<TInner>(i=>outerKeySelector(o).Equals(i*i).Select<TInner,TResult>(i=>(o,i)=>{o,i})
                                    o=resultSelector.Parameters[0];
                                    selector=Expression.Lambda(
                                        this.変換_旧Parameterを新Expression1.実行(
                                            resultSelector.Body,
                                            resultSelector.Parameters[0],
                                            o
                                        ),
                                        作業配列.Parameters設定(resultSelector.Parameters[1])
                                    );
                                } else {
                                    //O.Join(I,outerKeySelector,i0=>i0*i0,resultSelector)
                                    //O.SelectMany(o=>I.Where(i=>outerKeySelector(o).Equals(i*i).Select<TInner,TResult>(i=>resultSelector(o,i))
                                    o=Expression.Parameter(TOuter,"o2");
                                    selector=Expression.Lambda(
                                        Expression.Invoke(
                                            MethodCall1_Arguments_4,
                                            作業配列.Expressions設定(
                                                o,
                                                innerKeySelector_Parameters[0]
                                            )
                                        ),
                                        innerKeySelector_Parameters
                                    );
                                }
                                Equals_Argument=innerKeySelector_Body;
                            } else {
                                var Parameters1 = 作業配列.Parameters1;
                                predicate_Parameters=Parameters1;
                                ParameterExpression i;
                                if(MethodCall1_Arguments_4 is LambdaExpression resultSelector) {
                                    //O.Join(I,outerKeySelector,innerKeySelector,(o,i)=>{o,i})
                                    //O.SelectMany(o=>I.Where(i=>outerKeySelector(o).Equals(innerKeySelector(i)).Select<TInner,TResult>(i=>(o,i)=>{o,i})
                                    var resultSelector_Parameters = resultSelector.Parameters;
                                    o=resultSelector_Parameters[0];
                                    i=resultSelector_Parameters[1];
                                    selector=Expression.Lambda(
                                        resultSelector.Body,
                                        作業配列.Parameters設定(i)
                                    );
                                } else {
                                    //O.Join(I,outerKeySelector,innerKeySelector,resultSelector)
                                    //O.SelectMany(o=>I.Where(i=>outerKeySelector(o).Equals(innerKeySelector(i)).Select<TInner,TResult>(i=>resultSelector(o,i))
                                    o=Expression.Parameter(TOuter,"o2");
                                    i=Expression.Parameter(TInner,"i2");
                                    selector=Expression.Lambda(
                                        Expression.Invoke(
                                            MethodCall1_Arguments_4,
                                            作業配列.Expressions設定(
                                                o,
                                                i
                                            )
                                        ),
                                        作業配列.Parameters設定(i)
                                    );
                                }
                                Parameters1[0]=i;
                                Equals_Argument=Expression.Invoke(
                                    MethodCall1_Arguments_3,
                                    i
                                );
                            }
                            Equals_this=Expression.Invoke(
                                MethodCall1_Arguments_2,
                                o
                            );
                        }
                        Type MethodCall1_Arguments_5_Type;
                        if(MethodCall1_Arguments_5 is not null) {
                            //引数5にはComparerがあるのでそれで比較する。
                            //O.Group     (I,o=>o,i=>i,???)
                            //O.SelectMany(o=>I.Where(i=>EqualityComparer.Equal(i,o).???)
                            MethodCall1_Arguments_5_Type = MethodCall1_Arguments_5.Type;
                            Debug.Assert(MethodCall1_Arguments_5_Type.GetInterface(CommonLibrary.IEqualityComparer_FullName)!.GetGenericArguments()[0]==TKey);
                            //var T = MethodCall1_Arguments_5_Type.GetInterface(CommonLibrary.IEqualityComparer_FullName)!.GetGenericArguments()[0];
                            //predicate_Body=Expression.Call(
                            //    MethodCall1_Arguments_5,
                            //    作業配列.GetMethod(MethodCall1_Arguments_5_Type,nameof(Generic.IEqualityComparer<int>.Equals),TKey,TKey),
                            //    Equals_this,
                            //    Equals_Argument
                            //);
                        } else {
                            //predicate_Body=Expression.Call(
                            //    作業配列.MakeGenericMethod(
                            //        Reflection.Helpers.EqualityComparer_Equals,
                            //        TKey
                            //    ),
                            //    Equals_this,
                            //    Equals_Argument
                            //);
                            MethodCall1_Arguments_5_Type = 作業配列.MakeGenericType(typeof(Generic.EqualityComparer<>),TKey);
                            MethodCall1_Arguments_5=Expression.Call(MethodCall1_Arguments_5_Type.GetProperty(nameof(Generic.EqualityComparer<int>.Default))!.GetMethod);
                        }
                        var predicate_Body=Expression.Call(
                            MethodCall1_Arguments_5,
                            作業配列.GetMethod(MethodCall1_Arguments_5_Type,nameof(Generic.EqualityComparer<int>.Equals),TKey,TKey),
                            Equals_this,
                            Equals_Argument
                        );
                        var Where = Expression.Call(
                            作業配列.MakeGenericMethod(
                                Where_predicate,
                                TInner
                            ),
                            MethodCall1_Arguments_1,
                            Expression.Lambda(
                                predicate_Body,
                                predicate_Parameters
                            )
                        );
                        var Select = Expression.Call(
                            作業配列.MakeGenericMethod(
                                Select_selector,
                                TInner,
                                TResult
                            ),
                            Where,
                            selector
                        );
                        var SelectMany = Expression.Call(
                            作業配列.MakeGenericMethod(
                                SelectMany_selector,
                                TOuter,
                                TResult
                            ),
                            MethodCall1_Arguments_0,
                            Expression.Lambda(
                                Select,
                                作業配列.Parameters設定(o)
                            )
                        );
                        //SelectManyにgotoするのと本質的に同じだと思う。;
                        Debug.Assert(Reflection.ExtensionEnumerable.SelectMany_selector==SelectMany.Method.GetGenericMethodDefinition()||Reflection.ExtensionEnumerable.SelectMany_indexSelector==SelectMany.Method.GetGenericMethodDefinition()||Reflection.ExtensionSet.SelectMany_selector==SelectMany.Method.GetGenericMethodDefinition());
                        return this.Call(SelectMany);//SelectManyを作ったのでそれの形を最適化する。
                    }
                    case nameof(Linq.Enumerable.OfType): {
                        var MethodCall1_Arguments_0 = MethodCall1_Arguments[0];
                        var SelectMany = this.条件が合えば内部SelectManyのselector_Bodyに外部メソッドを入れる(
                            MethodCall0_Method,
                            MethodCall1_Arguments_0
                        );
                        if(SelectMany is not null) return SelectMany;
                        //Strings.OfType<Object>()
                        //Strings
                        if(MethodCall0.Type.IsAssignableFrom(MethodCall1_Arguments_0.Type)) return MethodCall1_Arguments_0;
                        return Expression.Call(
                            MethodCall0_Method,
                            MethodCall1_Arguments_0
                        );
                    }
                    case nameof(Linq.Enumerable.Select):{
                        if(Reflection.ExtensionEnumerable.Select_indexSelector==MethodCall0_GenericMethodDefinition) break;
                        var MethodCall1_Arguments_0 = MethodCall1_Arguments[0];
                        var MethodCall1_Arguments_1 = MethodCall1_Arguments[1];
                        if(ループ展開可能メソッドか(MethodCall1_Arguments_0,out var MethodCall1_MethodCall)) {
                            var MethodCall1_MethodCall_Method = MethodCall1_MethodCall.Method;
                            var MethodCall1_MethodCall_GenericMethodDefinition = GetGenericMethodDefinition(MethodCall1_MethodCall_Method);
                            Debug.Assert(nameof(Linq.Enumerable.Join)!=MethodCall1_MethodCall_Method.Name);
                            switch(MethodCall1_MethodCall_Method.Name) {
                                case nameof(Linq.Enumerable.SelectMany): {
                                    if(Reflection.ExtensionEnumerable.SelectMany_indexSelector!=MethodCall1_MethodCall.Method.GetGenericMethodDefinition()){
                                        var SelectMany=this.内部SelectManyのselector_Bodyに外部メソッドを入れる(
                                            MethodCall0_Method,
                                            MethodCall1_MethodCall,
                                            MethodCall1_Arguments_1
                                        );
                                        return this.Call(SelectMany);
                                    }
                                    break;
                                }
                                case nameof(Linq.Enumerable.Select): {
                                    Debug.Assert(
                                        Reflection.ExtensionEnumerable.Select_selector==MethodCall0_GenericMethodDefinition||
                                        Reflection.ExtensionSet.Select_selector==MethodCall0_GenericMethodDefinition
                                    );
                                    //A.Select(selector1).Select(selector0)
                                    //A.Select(selector0(selector1))
                                    var MethodCall1_MethodCall_Arguments_1 = MethodCall1_MethodCall.Arguments[1];
                                    LambdaExpression Lambda;
                                    if(MethodCall1_MethodCall_Arguments_1 is LambdaExpression selector1) {
                                        if(MethodCall1_Arguments_1 is LambdaExpression selector0) {
                                            //O.Select_selector(p1=>p1+p1).Select_selector(p0=>p0*p0)
                                            //O.Select_selector(p1=>(p1+p1)*(p1+p1))
                                            Lambda=Expression.Lambda(
                                                this.変換_旧Parameterを新Expression1.実行(
                                                    selector0.Body,
                                                    selector0.Parameters[0],
                                                    selector1.Body
                                                ),
                                                selector1.Parameters
                                            );
                                        } else {
                                            //O.Select_selector(p1=>p1+p1).Select_selector(MethodCall1_Arguments_1)
                                            //O.Select_selector(p1=>MethodCall1_Arguments_1(p1+p1))
                                            Lambda=Expression.Lambda(
                                                Expression.Invoke(
                                                    MethodCall1_Arguments_1,
                                                    selector1.Body
                                                ),
                                                selector1.Parameters
                                            );
                                        }
                                    } else {
                                        var p1 = Expression.Parameter(MethodCall1_MethodCall_Method.GetGenericArguments()[0],"p1");
                                        if(MethodCall1_Arguments_1 is LambdaExpression selector0) {
                                            //O.Select_selector(MethodCall1_MethodCall_Arguments_1).Select_selector(p0=>p0+p0)
                                            //O.Select_selector(p1=>MethodCall1_MethodCall_Arguments_1(p1)+MethodCall1_MethodCall_Arguments_1(p1)))
                                            Lambda=Expression.Lambda(
                                                this.変換_旧Parameterを新Expression1.実行(
                                                    selector0.Body,
                                                    selector0.Parameters[0],
                                                    Expression.Invoke(
                                                        MethodCall1_MethodCall_Arguments_1,
                                                        作業配列.Expressions設定(p1)
                                                    )
                                                ),
                                                作業配列.Parameters設定(p1)
                                            );
                                        } else {
                                            //O.Select_selector(selector1).Select_selector(selector0)
                                            //O.Select_selector(p1=>selector0(selector1(p1)))
                                            Lambda=Expression.Lambda(
                                                Expression.Invoke(
                                                    MethodCall1_Arguments_1,
                                                    Expression.Invoke(
                                                        MethodCall1_MethodCall_Arguments_1,
                                                        作業配列.Expressions設定(p1)
                                                    )
                                                ),
                                                作業配列.Parameters設定(p1)
                                            );
                                        }
                                    }
                                    return Expression.Call(
                                        作業配列.MakeGenericMethod(
                                            MethodCall1_MethodCall_GenericMethodDefinition,
                                            MethodCall1_MethodCall_Method.GetGenericArguments()[0],
                                            MethodCall0_Method.GetGenericArguments()[1]
                                        ),
                                        MethodCall1_MethodCall.Arguments[0],
                                        Lambda
                                    );
                                }
                            }
                        }
                        //A.Select(a=>a)
                        //A
                        Debug.Assert(Reflection.ExtensionSet.Select_selector==MethodCall0_GenericMethodDefinition||Reflection.ExtensionEnumerable.Select_selector==MethodCall0_GenericMethodDefinition||Reflection.ExtensionEnumerable.Select_indexSelector==MethodCall0_GenericMethodDefinition);
                        if(MethodCall1_Arguments_1 is LambdaExpression MethodCall1_selector&&MethodCall1_selector.Parameters[0]==MethodCall1_selector.Body) {
                            return MethodCall1_Arguments_0;
                        }
                        return Expression.Call(
                            MethodCall0_Method,
                            MethodCall1_Arguments_0,
                            MethodCall1_Arguments_1
                        );
                    }
                    case nameof(Linq.Enumerable.Single): {
                        if(Reflection.ExtensionEnumerable.Single_predicate==MethodCall0_GenericMethodDefinition) {
                            //O.Single(predicate)→O.Where(predicate).Single()
                            var MethodCall1_Arguments_0 = MethodCall1_Arguments[0];
                            var MethodCall1_Arguments_1 = MethodCall1_Arguments[1];
                            return Expression.Call(
                                Reflection.ExtensionEnumerable.Single.MakeGenericMethod(MethodCall0_Method.GetGenericArguments()),
                                Expression.Call(
                                    作業配列.ElementTypeからMakeGenericMethod(
                                        Reflection.ExtensionEnumerable.Where,
                                        MethodCall1_Arguments_0.Type
                                    ),
                                    MethodCall1_Arguments_0,
                                    MethodCall1_Arguments_1
                                )
                            );
                        }
                        break;
                    }
                    case nameof(Linq.Enumerable.SingleOrDefault): {
                        if(Reflection.ExtensionEnumerable.SingleOrDefault_predicate==MethodCall0_GenericMethodDefinition) {
                            var MethodCall1_Arguments_0 = MethodCall1_Arguments[0];
                            var MethodCall1_Arguments_1 = MethodCall1_Arguments[1];
                            //A.SingleOrDefault(predicate)
                            //A.Where(predicate).SingleOrDefault()
                            return Expression.Call(
                                Reflection.ExtensionEnumerable.SingleOrDefault.MakeGenericMethod(MethodCall0_Method.GetGenericArguments()),
                                Expression.Call(
                                    作業配列.ElementTypeからMakeGenericMethod(
                                        Reflection.ExtensionEnumerable.Where,
                                        MethodCall1_Arguments_0.Type
                                    ),
                                    MethodCall1_Arguments_0,
                                    MethodCall1_Arguments_1
                                )
                            );
                            //A.SingleOrDefault(predicate,defaultValue)は無視する
                        }else if(Reflection.ExtensionEnumerable.SingleOrDefault_predicate_defaultValue==MethodCall0_GenericMethodDefinition){
                            //A.SingleOrDefault(predicate)
                            //A.Where(predicate).SingleOrDefault()
                            var MethodCall1_Arguments_0=MethodCall1_Arguments[0];
                            return Expression.Call(
                                Reflection.ExtensionEnumerable.SingleOrDefault_defaultValue.MakeGenericMethod(MethodCall0_Method.GetGenericArguments()),
                                Expression.Call(
                                    作業配列.ElementTypeからMakeGenericMethod(
                                        Reflection.ExtensionEnumerable.Where,
                                        MethodCall1_Arguments_0.Type
                                    ),
                                    MethodCall1_Arguments_0,
                                    MethodCall1_Arguments[1]
                                ),
                                MethodCall1_Arguments[2]
                            );
                        }
                        break;
                    }
                    //case nameof(Linq.Enumerable.ToArray): {
                    //    Debug.Assert(Reflection.ExtensionEnumerable.ToArray==MethodCall0_GenericMethodDefinition);
                    //    var MethodCall1_Arguments_0 = MethodCall1_Arguments[0];
                    //    if(MethodCall1_Arguments_0.Type.IsArray)return MethodCall1_Arguments_0;
                    //    return Expression.Call(
                    //        MethodCall0_Method,
                    //        MethodCall1_Arguments_0
                    //    );
                    //}
                    case nameof(Linq.Enumerable.Except):
                    case nameof(Linq.Enumerable.Union): {
                        if(MethodCall1_Arguments.Count==3) {
                            Debug.Assert(
                                Reflection.ExtensionEnumerable.Except_comparer==MethodCall0_GenericMethodDefinition||
                                Reflection.ExtensionEnumerable.Union_comparer==MethodCall0_GenericMethodDefinition
                            );
                            var MethodCall1_Arguments_0=MethodCall1_Arguments[0];
                            var MethodCall1_Arguments_1=MethodCall1_Arguments[1];
                            var MethodCall1_Arguments_2=MethodCall1_Arguments[2];
                            //O.SelectMany(o=>I).Union(x,comparer)→O.SelectMany(o=>I.Union(x,comparer))
                            //O.SelectMany(selector).comparer(x,comparer)→O.SelectMany(o=>selector(o).Union(x,comparer))
                            var SelectMany = this.条件が合えば内部SelectManyのselector_Bodyに外部メソッドを入れる(
                                MethodCall0_Method,
                                MethodCall1_Arguments_0,
                                MethodCall1_Arguments_1,
                                MethodCall1_Arguments_2
                            );
                            if(SelectMany is not null) return SelectMany;
                            return Expression.Call(MethodCall0_Method,MethodCall1_Arguments_0,MethodCall1_Arguments_1,MethodCall1_Arguments_2);
                        } else {
                            Debug.Assert(
                                Reflection.ExtensionEnumerable.Except==MethodCall0_GenericMethodDefinition||
                                Reflection.ExtensionEnumerable.Union==MethodCall0_GenericMethodDefinition||
                                Reflection.ExtensionSet.Except==MethodCall0_GenericMethodDefinition||
                                Reflection.ExtensionSet.Union==MethodCall0_GenericMethodDefinition
                            );
                            var MethodCall1_Arguments_0=MethodCall1_Arguments[0];
                            var MethodCall1_Arguments_1=MethodCall1_Arguments[1];
                            //O.SelectMany(o=>I).Union(x)→O.SelectMany(o=>I.Union(x))
                            //O.SelectMany(selector).Union(x)→O.SelectMany(o=>selector(o).Union(x))
                            var SelectMany = this.条件が合えば内部SelectManyのselector_Bodyに外部メソッドを入れる(
                                MethodCall0_Method,
                                MethodCall1_Arguments_0,
                                MethodCall1_Arguments_1
                            );
                            if(SelectMany is not null) return SelectMany;
                            return Expression.Call(MethodCall0_Method,MethodCall1_Arguments_0,MethodCall1_Arguments_1);
                        }
                    }
                    case nameof(Linq.Enumerable.SelectMany): {
                        if(MethodCall1_Arguments.Count==2) {
                            //SelectManyの内部のWhereをSelectManyのsource,selectorに分離する。
                            //SelectMany<TSource,TResult>
                            //    O
                            //    o=>Where
                            //        I
                            //        i=>o==i&&o==0&&i==1
                            //SelectMany<TSource,TResult>
                            //    Where
                            //        O
                            //        o=>o==0
                            //    o=>Where
                            //        I
                            //        i=>o==i&&i==1
                            var MethodCall1_Arguments_0 = MethodCall1_Arguments[0];
                            var MethodCall1_Arguments_1 = MethodCall1_Arguments[1];
                            var SelectMany = this.条件が合えば内部SelectManyのselector_Bodyに外部メソッドを入れる(
                                MethodCall0_Method,
                                MethodCall1_Arguments_0,
                                MethodCall1_Arguments_1
                            );
                            if(SelectMany is not null)return SelectMany;
                            if(MethodCall1_Arguments_1 is LambdaExpression selector&&ループ展開可能メソッドか(selector.Body,out var MethodCall1)) {
                                var selector_Parameters = selector.Parameters;
                                var(Body,OuterPredicate)=共通(selector_Parameters,MethodCall1);
                                if(OuterPredicate is not null) {
                                    MethodCall1_Arguments_0=this.Outer又はInnerにWhereを付ける(
                                        MethodCall1_Arguments_0,
                                        MethodCall1_Arguments_0.Type.IsInheritInterface(typeof(IEnumerable<>))
                                            ? Reflection.ExtensionSet.Where
                                            : Reflection.ExtensionEnumerable.Where,
                                        selector_Parameters[0].Type,
                                        selector_Parameters,
                                        OuterPredicate
                                    );
                                }
                                MethodCall1_Arguments_1=Expression.Lambda(
                                    Body,
                                    selector_Parameters
                                );
                            }
                            return Expression.Call(
                                MethodCall0_Method,//SelectMany_selector
                                MethodCall1_Arguments_0,
                                MethodCall1_Arguments_1
                            );
                            (Expression body,Expression? predicate) 共通(Generic.IList<ParameterExpression> selector_Parameters,MethodCallExpression MethodCall) {
                                var MethodCall_Method = MethodCall.Method;
                                switch(MethodCall_Method.Name) {
                                    case nameof(Linq.Enumerable.Where): {
                                        if(MethodCall.Arguments[1] is LambdaExpression predicate) {
                                            var o = selector_Parameters[0];
                                            var predicate_Parameters = predicate.Parameters;
                                            var (OuterPredicate, OtherPredicate)=this._取得_Parameter_OuterPredicate_InnerPredicate.実行(
                                                predicate.Body,
                                                o
                                            );
                                            if(OtherPredicate is not null) {
                                                var body=Expression.Call(
                                                    MethodCall_Method,//Where
                                                    MethodCall.Arguments[0],
                                                    Expression.Lambda(
                                                        OtherPredicate,
                                                        predicate_Parameters
                                                    )
                                                );
                                                return(body,OuterPredicate);
                                            }
                                            return (MethodCall.Arguments[0],OuterPredicate);
                                        }
                                        return (MethodCall,null);
                                    }
                                    default: {
                                        if(ループ展開可能メソッドか(MethodCall.Arguments[0],out var MethodCall2)){
                                            var(body,predicate)=共通(selector_Parameters,MethodCall2);
                                            var MethodCall_Arguments=MethodCall.Arguments;
                                            var MethodCall_Arguments_Count=MethodCall_Arguments.Count;
                                            var Expressions = new Expression[MethodCall_Arguments_Count];
                                            for(var a = 1;a<MethodCall_Arguments_Count;a++) {
                                                Expressions[0]=body;
                                                Expressions[a]=MethodCall_Arguments[a];
                                            }
                                            return (Expression.Call(MethodCall_Method,Expressions),predicate);
                                        } else{
                                            return (MethodCall,null);
                                        }
                                    }
                                }
                            }
                        } else {
                            Debug.Assert(MethodCall1_Arguments.Count==3);
                            //SelectMany<TSource,ICollection,TResult>
                            //    O
                            //    o=>I
                            //    o,i=>o+i
                            //SelectMany<TSource,TResult>
                            //    O
                            //    o=>Select<ICollection,TResult>
                            //        I
                            //        i=>o+i
                            var MethodCall1_Arguments_0 = MethodCall1_Arguments[0];
                            var MethodCall1_Arguments_1 = MethodCall1_Arguments[1];
                            var MethodCall1_Arguments_2 = MethodCall1_Arguments[2];
                            var SelectMany_GenericArguments = MethodCall0_Method.GetGenericArguments();
                            var TSource = SelectMany_GenericArguments[0];
                            var TCollection = SelectMany_GenericArguments[1];
                            var TResult = SelectMany_GenericArguments[2];
                            MethodInfo SelectMany, Select;
                            bool indexSelectorか;
                            if(Reflection.ExtensionEnumerable.SelectMany_collectionSelector_resultSelector==MethodCall0_GenericMethodDefinition) {
                                SelectMany=Reflection.ExtensionEnumerable.SelectMany_selector;
                                Select=Reflection.ExtensionEnumerable.Select_selector;
                                indexSelectorか=false;
                            } else if(Reflection.ExtensionEnumerable.SelectMany_indexCollectionSelector_resultSelector==MethodCall0_GenericMethodDefinition) {
                                SelectMany=Reflection.ExtensionEnumerable.SelectMany_indexSelector;
                                Select=Reflection.ExtensionEnumerable.Select_selector;
                                indexSelectorか=true;
                            } else {
                                Debug.Assert(Reflection.ExtensionSet.SelectMany_collectionSelector_resultSelector==MethodCall0_GenericMethodDefinition);
                                SelectMany=Reflection.ExtensionSet.SelectMany_selector;
                                Select=Reflection.ExtensionSet.Select_selector;
                                indexSelectorか=false;
                            }
                            SelectMany=作業配列.MakeGenericMethod(SelectMany,TSource,TResult);
                            Select=作業配列.MakeGenericMethod(Select,TCollection,TResult);
                            Generic.IEnumerable<ParameterExpression> SelectMany_Parameters;
                            Expression Select_source, Select_selector;
                            if(MethodCall1_Arguments_1 is LambdaExpression collectionSelector) {
                                var collectionSelector_Parameters = collectionSelector.Parameters;
                                var collectionSelector_o = collectionSelector_Parameters[0];
                                SelectMany_Parameters=collectionSelector_Parameters;
                                if(MethodCall1_Arguments_2 is LambdaExpression resultSelector) {
                                    var resultSelector_o = resultSelector.Parameters[0];
                                    var resultSelector_i = resultSelector.Parameters[1];
                                    //SelectMany<TSource,ICollection,TResult>
                                    //    O
                                    //    (o,index)=>I+index
                                    //    o,i=>o+i
                                    //SelectMany<TSource,TResult>
                                    //    O
                                    //    (o,index)=>Select<ICollection,TResult>Select_selector
                                    //        I+index
                                    //        i=>o+i
                                    Select_selector=Expression.Lambda(
                                        this.変換_旧Parameterを新Expression1.実行(
                                            resultSelector.Body,
                                            resultSelector_o,
                                            collectionSelector_o
                                        ),
                                        this.作業配列.Parameters設定(resultSelector_i)
                                    );
                                } else {
                                    var resultSelector_i = Expression.Parameter(TCollection,"i");
                                    //SelectMany<TSource,ICollection,TResult>
                                    //    O
                                    //    o=>I
                                    //    resultSelector
                                    //SelectMany<TSource,TResult>
                                    //    O
                                    //    o=>Select<ICollection,TResult>
                                    //        I
                                    //        i=>resultSelector(o,i)
                                    Select_selector=Expression.Lambda(
                                        Expression.Invoke(
                                            MethodCall1_Arguments_2,
                                            作業配列.Expressions設定(collectionSelector_o,resultSelector_i)
                                        ),
                                        作業配列.Parameters設定(resultSelector_i)
                                    );
                                }
                                Select_source=collectionSelector.Body;
                            } else {
                                ParameterExpression SelectMany_collectionSelector_o, SelectMany_selector_i;
                                if(MethodCall1_Arguments_2 is LambdaExpression resultSelector) {
                                    SelectMany_collectionSelector_o=resultSelector.Parameters[0];
                                    SelectMany_selector_i=resultSelector.Parameters[1];
                                    Select_selector=Expression.Lambda(
                                        resultSelector.Body,
                                        作業配列.Parameters設定(SelectMany_selector_i)
                                    );
                                } else {
                                    SelectMany_collectionSelector_o=Expression.Parameter(TSource,"o");
                                    SelectMany_selector_i=Expression.Parameter(TCollection,"i");
                                    Select_selector=Expression.Lambda(
                                        Expression.Invoke(
                                            MethodCall1_Arguments_2,
                                            作業配列.Expressions設定(SelectMany_collectionSelector_o,SelectMany_selector_i)
                                        ),
                                        作業配列.Parameters設定(SelectMany_selector_i)
                                    );
                                }
                                if(indexSelectorか) {
                                    //SelectMany<TSource,ICollection,TResult>
                                    //    O
                                    //    MethodCall1_Arguments_1
                                    //    MethodCall1_Arguments_2
                                    //SelectMany<TSource,TResult>
                                    //    O
                                    //    o,index=>Select<TCollection,TResult> SelectMany_collectionSelector_index
                                    //        MethodCall1_Arguments_1(o,index)
                                    //        i=>MethodCall1_Arguments_2(o,i)
                                    var SelectMany_collectionSelector_index = Expression.Parameter(typeof(int),"index");
                                    Select_source=Expression.Invoke(
                                        MethodCall1_Arguments_1,
                                        作業配列.Expressions設定(SelectMany_collectionSelector_o,SelectMany_collectionSelector_index)
                                    );
                                    SelectMany_Parameters=作業配列.Parameters設定(SelectMany_collectionSelector_o,SelectMany_collectionSelector_index);
                                } else {
                                    //SelectMany<TSource,ICollection,TResult>
                                    //    O
                                    //    collectionSelector
                                    //    resultSelector
                                    //SelectMany<TSource,TResult>
                                    //    O
                                    //    o=>Select<TCollection,TResult>Select_source
                                    //        MethodCall1_Arguments_1(o)
                                    //        i=>resultSelector(o,i)
                                    Select_source=Expression.Invoke(
                                        MethodCall1_Arguments_1,
                                        作業配列.Expressions設定(SelectMany_collectionSelector_o)
                                    );
                                    SelectMany_Parameters=作業配列.Parameters設定(SelectMany_collectionSelector_o);
                                }
                            }
                            return Expression.Call(
                                SelectMany,
                                MethodCall1_Arguments_0,
                                Expression.Lambda(
                                    Expression.Call(
                                        Select,
                                        Select_source,
                                        Select_selector
                                    ),
                                    SelectMany_Parameters
                                )
                            );
                        }
                    }
                    case nameof(Linq.Enumerable.Where):{
                        if(Reflection.ExtensionEnumerable.Where_index==MethodCall0_GenericMethodDefinition) break;
                        var MethodCall1_Arguments_0 = MethodCall1_Arguments[0];
                        var MethodCall1_Arguments_1 = MethodCall1_Arguments[1];
                        if(ループ展開可能メソッドか(MethodCall1_Arguments_0,out var MethodCall1_MethodCall)) {
                            //if(Reflection.ExtensionEnumerable.Where_index!=MethodCall0_GenericMethodDefinition&&ループ展開可能メソッドか(MethodCall1_Arguments_0,out var MethodCall1_MethodCall)) {
                            var MethodCall1_MethodCall_Method = MethodCall1_MethodCall.Method;
                            switch(MethodCall1_MethodCall_Method.Name) {
                                case nameof(ExtensionSet.Except):
                                case nameof(ExtensionSet.Intersect):
                                case nameof(ExtensionSet.Union): {
                                    //O               .Intersect(I).Where(p=>p==1)
                                    //O.Where(p=>p==1).Intersect(I.Where(p=>p==1))
                                    var MethodCall0_MethodCall1_Arguments = MethodCall1_MethodCall.Arguments;
                                    return Expression.Call(
                                        MethodCall1_MethodCall_Method,
                                        Expression.Call(MethodCall0_Method,MethodCall0_MethodCall1_Arguments[0],MethodCall1_Arguments_1),
                                        Expression.Call(MethodCall0_Method,MethodCall0_MethodCall1_Arguments[1],MethodCall1_Arguments_1)
                                    );
                                }
                                case nameof(ExtensionSet.Select): {
                                    if(MethodCall1_Arguments_1 is LambdaExpression predicate) {
                                        if(MethodCall1_MethodCall.Arguments[1] is LambdaExpression selector) {
                                            //A.Select(a=>new{a}).Where(w=>w.a==3)
                                            //A.Where(w=>w==3).Select(a=>new{a})
                                            var selector_Body = selector.Body;
                                            var selector_Parameters = selector.Parameters;
                                            var predicate_Parameters = predicate.Parameters;
                                            var Where0 = Expression.Call(
                                                this.作業配列.MakeGenericMethod(MethodCall0_GenericMethodDefinition,MethodCall1_MethodCall_Method.GetGenericArguments()[0]),
                                                MethodCall1_MethodCall.Arguments[0],
                                                Expression.Lambda(
                                                    this.Select_Where再帰で匿名型を走査(
                                                        selector_Body,
                                                        predicate_Parameters[0],
                                                        predicate.Body
                                                    ),
                                                    selector_Parameters
                                                )
                                            );
                                            var Where1 = this.Call(Where0);
                                            return Expression.Call(
                                                MethodCall1_MethodCall_Method,
                                                Where1,
                                                selector
                                            );
                                        }
                                    }
                                    break;
                                }
                                case nameof(ExtensionSet.SelectMany):{
                                    if(Reflection.ExtensionEnumerable.SelectMany_indexSelector==MethodCall1_MethodCall.Method.GetGenericMethodDefinition()) break;
                                    //Where
                                    //    SelectMany
                                    //        O
                                    //        o=>I
                                    var SelectMany = this.内部SelectManyのselector_Bodyに外部メソッドを入れる(
                                        MethodCall0_Method,
                                        MethodCall1_MethodCall,
                                        MethodCall1_Arguments_1
                                    );
                                    //todo goto SelectMany;
                                    Debug.Assert(Reflection.ExtensionEnumerable.SelectMany_selector==SelectMany.Method.GetGenericMethodDefinition()||Reflection.ExtensionEnumerable.SelectMany_indexSelector==SelectMany.Method.GetGenericMethodDefinition()||Reflection.ExtensionSet.SelectMany_selector==SelectMany.Method.GetGenericMethodDefinition());
                                    return this.Call(SelectMany);
                                }
                                case nameof(ExtensionSet.Where):{
                                    if(Reflection.ExtensionEnumerable.Where_index==MethodCall1_MethodCall_Method.GetGenericMethodDefinition()) break;
                                    var MethodCall1_MethodCall1_Arguments = MethodCall1_MethodCall.Arguments;
                                    if(MethodCall1_Arguments_1 is LambdaExpression predicate外) {
                                        var predicate外_Parameters = predicate外.Parameters;
                                        if(MethodCall1_MethodCall1_Arguments[1]is LambdaExpression predicate内) {
                                            //Where(p=>p==3).Where(q=>q==2)
                                            //Where(q=>p==3&&q==2)
                                            //Where(q=>q==3&&q==2)
                                            return Expression.Call(
                                                MethodCall0_Method,
                                                MethodCall1_MethodCall1_Arguments[0],
                                                Expression.Lambda(
                                                    predicate外.Type,
                                                    Expression.AndAlso(
                                                        predicate外.Body,
                                                        this.変換_旧Parameterを新Expression1.実行(
                                                            predicate内.Body,
                                                            predicate内.Parameters[0],
                                                            predicate外_Parameters[0]
                                                        )
                                                    ),
                                                    predicate外_Parameters
                                                )
                                            );
                                        } else {
                                            //Where(predicate).Where(q=>q==3)
                                            //Where(q=>predicate(q)&&q==3)
                                            return Expression.Call(
                                                MethodCall0_Method,
                                                MethodCall1_MethodCall1_Arguments[0],
                                                Expression.Lambda(
                                                    predicate外.Type,
                                                    Expression.AndAlso(
                                                        Expression.Invoke(
                                                            MethodCall1_MethodCall1_Arguments[1],
                                                            predicate外_Parameters[0]
                                                        ),
                                                        predicate外.Body
                                                    ),
                                                    predicate外_Parameters
                                                )
                                            );
                                        }
                                    } else {
                                        if(MethodCall1_MethodCall1_Arguments[1] is LambdaExpression predicate内) {
                                            //Where(p=>p==3).Where(predicate)
                                            //Where(p=>p==3&&predicate(p))
                                            var predicate内_Parameters = predicate内.Parameters;
                                            return Expression.Call(
                                                MethodCall0_Method,
                                                MethodCall1_MethodCall1_Arguments[0],
                                                Expression.Lambda(
                                                    MethodCall1_Arguments_1.Type,
                                                    Expression.AndAlso(
                                                        predicate内.Body,
                                                        Expression.Invoke(
                                                            MethodCall1_Arguments_1,
                                                            predicate内_Parameters[0]
                                                        )
                                                    ),
                                                    predicate内_Parameters
                                                )
                                            );
                                        } else {
                                            //Where(predicate0).Where(predicate1)
                                            //Where(p=>predicate0(p)&&predicate1(p))
                                            var MethodCall1_Arguments_1_Type = MethodCall1_Arguments_1.Type;
                                            var p = Expression.Parameter(
                                                MethodCall1_Arguments_1_Type.GetGenericArguments()[0],
                                                $"Whereﾟ{this.番号++}"
                                            );
                                            var Expressions = 作業配列.Expressions設定(p);
                                            var Left = Expression.Invoke(
                                                MethodCall1_MethodCall1_Arguments[1],
                                                Expressions
                                            );
                                            var Right = Expression.Invoke(
                                                MethodCall1_Arguments_1,
                                                Expressions
                                            );
                                            return Expression.Call(
                                                MethodCall0_Method,
                                                MethodCall1_MethodCall1_Arguments[0],
                                                Expression.Lambda(
                                                    MethodCall1_Arguments_1_Type,
                                                    Expression.AndAlso(
                                                        Left,
                                                        Right
                                                    ),
                                                    作業配列.Parameters設定(p)
                                                )
                                            );
                                        }
                                    }
                                }
                            }
                        }
                        break;
                    }
                }
            }
            return Expression.Call(MethodCall0_Method,MethodCall1_Arguments);
        }else{
            Debug.Assert(MethodCall0!=null);
            Debug.Assert(MethodCall0.Object!=null,"MethodCall0.Object != null");
            var MethodCall1_Object = this.Traverse(MethodCall0.Object);
            var MethodCall1_Object_Type = MethodCall1_Object.Type;
            if(MethodCall0_Method.Name==nameof(object.Equals))
                if(MethodCall1_Object_Type.IsAnonymous())
                    return this.共通AnonymousValueTuple(MethodCall1_Object,MethodCall0_Method,MethodCall0.Arguments[0]);
                else if(MethodCall1_Object_Type.IsValueTuple())
                    return this.共通AnonymousValueTuple(MethodCall1_Object,MethodCall0_Method,MethodCall0.Arguments[0]);
            /*
            var IsAnonymous = MethodCall1_Object_Type.IsAnonymous();
            var IsValueTuple = MethodCall1_Object_Type.IsValueTuple();
            if(IsAnonymous||IsValueTuple) {
                if(IsAnonymous) {
                    if(MethodCall0_Method.Name==nameof(object.Equals))
                        return this.共通AnonymousValueTuple(MethodCall1_Object,MethodCall0_Method,MethodCall0.Arguments[0]);
                } else {
                    Debug.Assert(IsValueTuple);
                    if(MethodCall0_Method.Name==nameof(ValueTuple<int>.Equals))
                        return this.共通AnonymousValueTuple(MethodCall1_Object,MethodCall0_Method,MethodCall0.Arguments[0]);
                }
            }
            */
            //インスタンスメソッドで、仮想メソッドの場合よりインスタンスの変数の型一致していてsealedの場合それを選ぶ。
            foreach(var ChildMethod in MethodCall1_Object_Type.GetMethods(BindingFlags.Instance|BindingFlags.NonPublic|BindingFlags.Public))
                if((MethodCall1_Object_Type.IsSealed||ChildMethod.IsFinal)&&ChildMethod.GetBaseDefinition()==MethodCall0_Method)
                    return Expression.Call(MethodCall1_Object,ChildMethod,MethodCall1_Arguments);
            if(MethodCall0.Object==MethodCall1_Object&&ReferenceEquals(MethodCall0.Arguments,MethodCall1_Arguments))return MethodCall0;
            return Expression.Call(MethodCall1_Object,MethodCall0_Method,MethodCall1_Arguments);
        }
    }
    private Expression 共通AnonymousValueTuple(Expression MethodCall1_Object,MethodInfo MethodCall0_Method,Expression MethodCall0_Arguments_0) {
        var MethodCall1_Arguments_0 = this.Traverse(MethodCall0_Arguments_0);
        if(MethodCall1_Object is NewExpression LNew&&MethodCall1_Arguments_0 is NewExpression RNew) {
            var 作業配列 = this.作業配列;
            var LNew_Arguments = LNew.Arguments;
            var RNew_Arguments = RNew.Arguments;
            var LNew_Arguments_0 = LNew_Arguments[0];
            var RNew_Arguments_0 = RNew_Arguments[0];
            var LNew_Arguments_0_Type = LNew_Arguments_0.Type;
            Expression Result = Expression.Call(LNew_Arguments_0,作業配列.GetMethod(LNew_Arguments_0_Type,nameof(Equals),LNew_Arguments_0_Type),RNew_Arguments_0);
            var LNew_Arguments_Count = LNew_Arguments.Count;
            for(var a = 1;a<LNew_Arguments_Count;a++) {
                var LNew_Arguments_a = LNew_Arguments[a];
                var LNew_Arguments_a_Type = LNew_Arguments_a.Type;
                Result=Expression.AndAlso(
                    Result,
                    Expression.Call(
                        LNew_Arguments_a,
                        作業配列.GetMethod(LNew_Arguments_a_Type,nameof(Equals),LNew_Arguments_a_Type),
                        RNew_Arguments[a]
                    )
                );
            }
            return Result;
        }
        return Expression.Call(MethodCall1_Object,MethodCall0_Method,this.作業配列.Expressions設定(MethodCall1_Arguments_0));
    }
    /// <summary>
    /// OfType()
    /// </summary>
    /// <param name="MethodCall0_Method"></param>
    /// <param name="MethodCall1_Arguments_0"></param>
    /// <returns></returns>
    private Expression? 条件が合えば内部SelectManyのselector_Bodyに外部メソッドを入れる(MethodInfo MethodCall0_Method,Expression MethodCall1_Arguments_0) {
        if(ループ展開可能メソッドか(MethodCall1_Arguments_0,out var MethodCall1_MethodCall)) {
            switch(MethodCall1_MethodCall.Method.Name) {
                case nameof(ExtensionSet.SelectMany): {
                    if(Reflection.ExtensionEnumerable.SelectMany_indexSelector!=MethodCall1_MethodCall.Method.GetGenericMethodDefinition()) {
                        var SelectMany = this.内部SelectManyのselector_Bodyに外部メソッドを入れる(
                            MethodCall0_Method,
                            MethodCall1_MethodCall
                        );
                        return this.Call(SelectMany);
                    }
                    break;
                }
            }
        }
        return null;
    }
    /// <summary>
    /// Delete(predicate),Intersect(second),Union(second),Except(second)
    /// </summary>
    /// <param name="MethodCall0_Method"></param>
    /// <param name="MethodCall1_Arguments_0"></param>
    /// <param name="MethodCall1_Arguments_1"></param>
    /// <returns></returns>
    private Expression? 条件が合えば内部SelectManyのselector_Bodyに外部メソッドを入れる(MethodInfo MethodCall0_Method,Expression MethodCall1_Arguments_0,Expression MethodCall1_Arguments_1) {
        if(ループ展開可能メソッドか(MethodCall1_Arguments_0,out var MethodCall1_MethodCall)){
            switch(MethodCall1_MethodCall.Method.Name) {
                case nameof(ExtensionSet.SelectMany): {
                    if(Reflection.ExtensionEnumerable.SelectMany_indexSelector!=MethodCall1_MethodCall.Method.GetGenericMethodDefinition()) {
                        var SelectMany = this.内部SelectManyのselector_Bodyに外部メソッドを入れる(
                            MethodCall0_Method,
                            MethodCall1_MethodCall,
                            MethodCall1_Arguments_1
                        );
                        return this.Call(SelectMany);
                    }
                    break;
                }
            }
        }
        return null;
    }
    /// <summary>
    /// Intersect(second,comparer),Union(second,comparer),Except(second,comparer)
    /// </summary>
    /// <param name="MethodCall0_Method"></param>
    /// <param name="MethodCall1_Arguments_0"></param>
    /// <param name="MethodCall1_Arguments_1"></param>
    /// <param name="MethodCall1_Arguments_2"></param>
    /// <returns></returns>
    private Expression? 条件が合えば内部SelectManyのselector_Bodyに外部メソッドを入れる(MethodInfo MethodCall0_Method,Expression MethodCall1_Arguments_0,Expression MethodCall1_Arguments_1,Expression MethodCall1_Arguments_2) {
        if(ループ展開可能メソッドか(MethodCall1_Arguments_0,out var MethodCall1_MethodCall)) {
            switch(MethodCall1_MethodCall.Method.Name) {
                case nameof(ExtensionSet.SelectMany): {
                    if(Reflection.ExtensionEnumerable.SelectMany_indexSelector!=MethodCall1_MethodCall.Method.GetGenericMethodDefinition()) {
                        var SelectMany = this.内部SelectManyのselector_Bodyに外部メソッドを入れる(
                            MethodCall0_Method,
                            MethodCall1_MethodCall,
                            MethodCall1_Arguments_1,
                            MethodCall1_Arguments_2
                        );
                        return this.Call(SelectMany);
                    }
                    break;
                }
            }
        }
        return null;
    }
    /// <summary>
    /// SelectMany.OfType
    /// </summary>
    /// <param name="MethodCall0_Method"></param>
    /// <param name="MethodCall1_MethodCall"></param>
    /// <returns></returns>
    private MethodCallExpression 内部SelectManyのselector_Bodyに外部メソッドを入れる(MethodInfo MethodCall0_Method,MethodCallExpression MethodCall1_MethodCall) {
        var MethodCall1_MethodCall_Arguments = MethodCall1_MethodCall.Arguments;
        Debug.Assert(MethodCall1_MethodCall_Arguments.Count==2);
        LambdaExpression selector1;
        Expression selector1_Body;
        if(MethodCall1_MethodCall_Arguments[1] is LambdaExpression selector0) {
            Debug.Assert(
                Reflection.ExtensionSet.SelectMany_selector==MethodCall1_MethodCall.Method.GetGenericMethodDefinition()||
                Reflection.ExtensionEnumerable.SelectMany_selector==MethodCall1_MethodCall.Method.GetGenericMethodDefinition()
            );
            //MethodCall0_Method                          OfType
            //    MethodCall1_MethodCall_Method           SelectMany
            //        MethodCall1_MethodCall_Arguments[0] O
            //        o=>                                 selector0.Parameters
            //            I                               selector0.Body
            //MethodCall1_MethodCall_Method               SelectMany
            //    MethodCall1_MethodCall_Arguments[0]     O
            //    o=>                                     selector0.Parameters
            //        MethodCall0_Method                  OfType
            //            I                               selector0.Body
            //O.SelectMany(o=>I).OfType<T>()→O.SelectMany(o=>I.OfType<T>())
            var selector0_Body = Expression.Call(
                MethodCall0_Method,
                selector0.Body
            );
            selector1_Body = this.Call(selector0_Body);
            selector1=Expression.Lambda(
                selector1_Body,
                selector0.Parameters
            );
        } else {
            //MethodCall0_Method                                        OfType
            //    MethodCall1_MethodCall_Method                         SelectMany
            //        MethodCall1_MethodCall_Arguments[0]               O
            //        MethodCall1_MethodCall_Arguments[1]               selector0
            //MethodCall1_MethodCall_Method                             SelectMany
            //    MethodCall1_MethodCall_Arguments[0]                   O
            //    o=>
            //        MethodCall0_Method                                OfType<T>
            //            MethodCall1_MethodCall_Arguments[1].Invoke(o) selector0
            //O.SelectMany(selector).OfType<T>()→O.SelectMany(o=>selector(O).OfType<T>())
            var 作業配列 = this.作業配列;
            var o = Expression.Parameter(MethodCall1_MethodCall.Method.GetGenericArguments()[0],"o");
            var Invoke=Expression.Invoke(
                MethodCall1_MethodCall_Arguments[1],
                作業配列.Expressions設定(o)
            );
            var selector0_Body = Expression.Call(
                MethodCall0_Method,
                Invoke
            );
            selector1_Body = this.Call(selector0_Body);
            selector1=Expression.Lambda(
                selector1_Body,
                作業配列.Parameters設定(o)
            );
        }
        return 共通後処理内部SelectManyのselectorBodyに外部メソッドを入れる(MethodCall1_MethodCall,selector1_Body,MethodCall1_MethodCall_Arguments,selector1);
    }
    /// <summary>
    /// SelectMany.Where
    /// SelectMany.Select
    /// </summary>
    /// <param name="MethodCall0_Method"></param>
    /// <param name="MethodCall1_MethodCall"></param>
    /// <param name="MethodCall1_Arguments_1"></param>
    /// <returns></returns>
    private MethodCallExpression 内部SelectManyのselector_Bodyに外部メソッドを入れる(MethodInfo MethodCall0_Method,MethodCallExpression MethodCall1_MethodCall,Expression MethodCall1_Arguments_1) {
        var MethodCall1_MethodCall_Arguments = MethodCall1_MethodCall.Arguments;
        Debug.Assert(MethodCall1_MethodCall_Arguments.Count==2);
        LambdaExpression selector1;
        Expression selector1_Body;
        if(MethodCall1_MethodCall_Arguments[1] is LambdaExpression selector0) {
            Debug.Assert(
                Reflection.ExtensionEnumerable.SelectMany_indexSelector==MethodCall1_MethodCall.Method.GetGenericMethodDefinition()||
                Reflection.ExtensionEnumerable.SelectMany_selector==MethodCall1_MethodCall.Method.GetGenericMethodDefinition()||
                Reflection.ExtensionSet.SelectMany_selector==MethodCall1_MethodCall.Method.GetGenericMethodDefinition()
            );
            //MethodCall0_Method                          Select
            //    MethodCall1_MethodCall_Method           SelectMany
            //        MethodCall1_MethodCall_Arguments[0] O
            //        o=>                                 SelectMany_selector0.Parameters
            //            I                               SelectMany_selector0.Body
            //    MethodCall1_Arguments_1                 Select_selector
            //MethodCall1_MethodCall_Method               SelectMany
            //    MethodCall1_MethodCall_Arguments[0]     O
            //    o=>                                     SelectMany_selector0.Parameters
            //        MethodCall0_Method                  Select
            //            I                               I
            //            MethodCall1_Arguments_1         Select_selector
            //O.SelectMany(o=>I).Select(Select_selector)→O.SelectMany(o=>I.Select(Select_selector))
            var selector0_Body = Expression.Call(
                MethodCall0_Method,
                selector0.Body,
                MethodCall1_Arguments_1
            );
            selector1_Body = this.Call(selector0_Body);
            selector1=Expression.Lambda(
                selector1_Body,
                selector0.Parameters
            );
        } else {
            //MethodCall0_Method
            //    MethodCall1_MethodCall_Method           SelectMany
            //        MethodCall1_MethodCall_Arguments[0]
            //        MethodCall1_MethodCall_Arguments[1]
            //    MethodCall1_Arguments[1]
            //MethodCall1_MethodCall_Method               SelectMany
            //    MethodCall1_MethodCall_Arguments[0]
            //    o=>
            //        MethodCall0_Method
            //            MethodCall1_MethodCall_Arguments[1].Invoke(o)
            //            MethodCall1_Arguments[1]
            var 作業配列 = this.作業配列;
            var o = Expression.Parameter(MethodCall1_MethodCall.Method.GetGenericArguments()[0],"o");
            var Invoke=Expression.Invoke(
                MethodCall1_MethodCall_Arguments[1],
                作業配列.Expressions設定(o)
            );
            var selector0_Body = Expression.Call(
                MethodCall0_Method,
                Invoke,
                MethodCall1_Arguments_1
            );
            selector1_Body = this.Call(selector0_Body);
            selector1=Expression.Lambda(
                selector1_Body,
                作業配列.Parameters設定(o)
            );
        }
        return 共通後処理内部SelectManyのselectorBodyに外部メソッドを入れる(MethodCall1_MethodCall,selector1_Body,MethodCall1_MethodCall_Arguments,selector1);
    }
    /// <summary>
    /// Except(second,comparer),Union(second,comparer)
    /// </summary>
    /// <param name="MethodCall0_Method"></param>
    /// <param name="MethodCall1_MethodCall"></param>
    /// <param name="MethodCall1_Arguments_1"></param>
    /// <param name="MethodCall1_Arguments_2"></param>
    /// <returns></returns>
    private MethodCallExpression 内部SelectManyのselector_Bodyに外部メソッドを入れる(MethodInfo MethodCall0_Method,MethodCallExpression MethodCall1_MethodCall,Expression MethodCall1_Arguments_1,Expression MethodCall1_Arguments_2) {
        var MethodCall1_MethodCall_Arguments = MethodCall1_MethodCall.Arguments;
        Debug.Assert(MethodCall1_MethodCall_Arguments.Count==2);
        LambdaExpression selector1;
        Expression selector1_Body;
        if(MethodCall1_MethodCall_Arguments[1] is LambdaExpression selector0) {
            Debug.Assert(
                Reflection.ExtensionSet.SelectMany_selector==MethodCall1_MethodCall.Method.GetGenericMethodDefinition()||
                Reflection.ExtensionEnumerable.SelectMany_selector==MethodCall1_MethodCall.Method.GetGenericMethodDefinition()
            );
            //MethodCall0_Method                          Except
            //    MethodCall1_MethodCall_Method           SelectMany
            //        MethodCall1_MethodCall_Arguments[0] O
            //        o=>                                 selector0.Parameters
            //            I                               selector0.Body
            //    MethodCall1_Arguments[1]                second
            //    MethodCall1_Arguments[2]                comparer
            //MethodCall1_MethodCall_Method               SelectMany
            //    MethodCall1_MethodCall_Arguments[0]     O
            //    o=>                                     selector0.Parameters
            //        MethodCall0_Method                  Except
            //            I                               selector0.Body
            //            MethodCall1_Arguments[1]        second
            //            MethodCall1_Arguments[2]        comparer
            var selector0_Body = Expression.Call(
                MethodCall0_Method,
                selector0.Body,
                MethodCall1_Arguments_1,
                MethodCall1_Arguments_2
            );
            selector1_Body=this.Call(selector0_Body);
            selector1=Expression.Lambda(
                selector1_Body,
                selector0.Parameters
            );
        } else {
            //MethodCall0_Method                                        Except
            //    MethodCall1_MethodCall_Method                         SelectMany
            //        MethodCall1_MethodCall_Arguments[0]               O
            //        MethodCall1_MethodCall_Arguments[1]               selector
            //    MethodCall1_Arguments[1]                              second
            //    MethodCall1_Arguments[2]                              comparer
            //MethodCall1_MethodCall_Method                             SelectMany
            //    MethodCall1_MethodCall_Arguments[0]                   O
            //    o=>
            //        MethodCall0_Method                                Except
            //            MethodCall1_MethodCall_Arguments[1].Invoke(o)
            //            MethodCall1_Arguments[1]                      second
            //            MethodCall1_Arguments[2]                      comparer
            var 作業配列 = this.作業配列;
            var o = Expression.Parameter(MethodCall1_MethodCall.Method.GetGenericArguments()[0],"o");
            var Invoke=Expression.Invoke(
                MethodCall1_MethodCall_Arguments[1],
                作業配列.Expressions設定(o)
            );
            var selector0_Body = Expression.Call(
                MethodCall0_Method,
                Invoke,
                MethodCall1_Arguments_1,
                MethodCall1_Arguments_2
            );
            selector1_Body=this.Call(selector0_Body);
            selector1=Expression.Lambda(
                selector1_Body,
                作業配列.Parameters設定(o)
            );
        }
        return 共通後処理内部SelectManyのselectorBodyに外部メソッドを入れる(MethodCall1_MethodCall,selector1_Body,MethodCall1_MethodCall_Arguments,selector1);
    }
    private static MethodCallExpression 共通後処理内部SelectManyのselectorBodyに外部メソッドを入れる(MethodCallExpression SelectMany,Expression selector1_Body,ReadOnlyCollection<Expression> MethodCall1_MethodCall_Arguments,LambdaExpression selector1){
        //Enumerable.Except(ExtensionSet.SelectMany(CreateSet(),o=>CreateSet()),CreateSet(),EqualityComparer<int>.Default));
        //Enumerable.SelectMany(CreateSet(),o=>Enumerable.Except(CreateSet(),CreateSet(),EqualityComparer<int>.Default));
        var SelectMany_Method=SelectMany.Method;
        var SelectMany_GenericMethodDefinition=SelectMany_Method.GetGenericMethodDefinition();
        //インターフェースのBaseType is nullは決まっている
        if(typeof(ExtensionSet)==SelectMany_GenericMethodDefinition.DeclaringType)
            if(SelectMany_GenericMethodDefinition==Reflection.ExtensionSet.SelectMany_selector)
                if(
                    (selector1_Body.Type.IsGenericType&&selector1_Body.Type.GetGenericTypeDefinition()==typeof(IEnumerable<>))||
                    selector1_Body.Type.IsInheritInterface(typeof(IEnumerable<>))){
                }else{
                    SelectMany_GenericMethodDefinition=Reflection.ExtensionEnumerable.SelectMany_selector;
                }
                //if(
                //    (selector1_Body.Type.IsGenericType&&selector1_Body.Type.GetGenericTypeDefinition()==typeof(Generic.IEnumerable<>))||
                //    !selector1_Body.Type.IsImplement(typeof(Sets.IEnumerable<>))
                //    //selector1_Body.Type.GetInterface(CommonLibrary.Sets_IEnumerable1_FullName)is null
                //)
                //    SelectMany_GenericMethodDefinition=Reflection.ExtensionEnumerable.SelectMany_selector;
        var MethodCall1_MethodCall_Arguments_0=MethodCall1_MethodCall_Arguments[0];
        var GenericArguments=SelectMany_Method.GetGenericArguments();
        GenericArguments[1]=IEnumerable1のT(selector1.ReturnType);
        return Expression.Call(
            SelectMany_GenericMethodDefinition.MakeGenericMethod(GenericArguments),
            MethodCall1_MethodCall_Arguments_0,
            selector1
        );
    }
    /// <summary>
    /// ローカル関数にするとビルドがフリーズする。
    /// </summary>
    /// <param name="outer又はinner"></param>
    /// <param name="Where"></param>
    /// <param name="Where_T"></param>
    /// <param name="Where_Parameters"></param>
    /// <param name="OuterPredicate又はInnerPredicate"></param>
    /// <returns></returns>
    private Expression Outer又はInnerにWhereを付ける(Expression outer又はinner,MethodInfo Where,Type Where_T,Generic.IEnumerable<ParameterExpression> Where_Parameters,Expression OuterPredicate又はInnerPredicate) => Expression.Call(
        this.作業配列.MakeGenericMethod(
            Where,
            Where_T
        ),
        outer又はinner,
        Expression.Lambda(
            OuterPredicate又はInnerPredicate,
            Where_Parameters
        )
    );
    /// <summary>
    /// ローカル関数にするとビルドがフリーズする。
    /// </summary>
    /// <param name="selector_Body"></param>
    /// <param name="Instance"></param>
    /// <param name="対象"></param>
    /// <returns></returns>
    private Expression Select_Where再帰で匿名型を走査(Expression selector_Body,Expression Instance,Expression 対象) {
        if(selector_Body is NewExpression New) {
            var New_Type = New.Type;
            var IsAnonymous = New_Type.IsAnonymous();
            var IsValueTuple = New_Type.IsValueTuple();
            if(IsAnonymous||IsValueTuple) {
                Debug.Assert(New.Constructor!=null);
                if(IsAnonymous) {
                    var New_Arguments = New.Arguments;
                    var New_Arguments_Count = New_Arguments.Count;
                    var New_Constructor_GetParameters = New.Constructor.GetParameters();
                    Debug.Assert(New_Constructor_GetParameters.Length==New_Arguments_Count);
                    for(var a = 0;a<New_Arguments_Count;a++) {
                        var NewExpression_Argument = New_Arguments[a];
                        対象=this.Select_Where再帰で匿名型を走査(
                            NewExpression_Argument,
                            Expression.Property(
                                Instance,
                                New_Constructor_GetParameters[a].Name
                            ),
                            対象
                        );
                    }
                } else {
                    Debug.Assert(IsValueTuple);
                    var Index = 0;
                    foreach(var New_Argument in New.Arguments){
                        対象=Index switch{
                            0=>this.Select_Where再帰で匿名型を走査(New_Argument,Expression.Field(Instance,nameof(ValueTuple<int,int,int,int,int,int,int,int>.Item1)),対象),
                            1=>this.Select_Where再帰で匿名型を走査(New_Argument,Expression.Field(Instance,nameof(ValueTuple<int,int,int,int,int,int,int,int>.Item2)),対象),
                            2=>this.Select_Where再帰で匿名型を走査(New_Argument,Expression.Field(Instance,nameof(ValueTuple<int,int,int,int,int,int,int,int>.Item3)),対象),
                            3=>this.Select_Where再帰で匿名型を走査(New_Argument,Expression.Field(Instance,nameof(ValueTuple<int,int,int,int,int,int,int,int>.Item4)),対象),
                            4=>this.Select_Where再帰で匿名型を走査(New_Argument,Expression.Field(Instance,nameof(ValueTuple<int,int,int,int,int,int,int,int>.Item5)),対象),
                            5=>this.Select_Where再帰で匿名型を走査(New_Argument,Expression.Field(Instance,nameof(ValueTuple<int,int,int,int,int,int,int,int>.Item6)),対象),
                            6=>this.Select_Where再帰で匿名型を走査(New_Argument,Expression.Field(Instance,nameof(ValueTuple<int,int,int,int,int,int,int,int>.Item7)),対象),
                            _=>this.Select_Where再帰で匿名型を走査(New_Argument,Expression.Field(Instance,nameof(ValueTuple<int,int,int,int,int,int,int,int>.Rest)),対象)
                        };
                        Index++;
                        if(Index==8) Index=0;
                    }
                }
            }
        }
        return this.変換_旧Expressionを新Expression1.実行(対象,Instance,selector_Body);
    }
}

//2022/05/30 2788
//2022/04/02 3123
//2022/03/23 3192
