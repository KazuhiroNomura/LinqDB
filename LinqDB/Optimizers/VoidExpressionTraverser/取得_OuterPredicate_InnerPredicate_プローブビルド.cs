using LinqDB.Helpers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;

// ReSharper disable AssignNullToNotNullAttribute
namespace LinqDB.Optimizers.VoidExpressionTraverser;
using static Common;

internal sealed class 取得_OuterPredicate_InnerPredicate_プローブビルド
{
    //:ReturnExpressionTraverser_Quoteを処理しない{
    private readonly 判定_指定Parameter無_他Parameter有 判定_指定Parameter無_他Parameter有;
    private sealed class 判定_指定Parameterが存在する : VoidExpressionTraverser_Quoteを処理しない
    {
        private bool Parameterが存在する;
        private ParameterExpression? 指定Parameter;
        public bool 実行(Expression e, ParameterExpression 指定Parameter)
        {
            this.指定Parameter=指定Parameter;
            this.Parameterが存在する=false;
            this.Traverse(e);
            return this.Parameterが存在する;
        }
        protected override void Parameter(ParameterExpression Parameter)
        {
            Debug.Assert(this.指定Parameter is not null);
            if (this.指定Parameter==Parameter) this.Parameterが存在する=true;
        }
    }
    private readonly 判定_指定Parameterが存在する _判定_指定Parameterが存在する = new();
    private readonly 作業配列 作業配列;
    private readonly 判定_指定Parameter有_他Parameter無_Lambda内部走査 判定_指定Parameter有_他Parameter無_Lambda内部走査;
    private readonly List<(Expression プローブ, Expression ビルド)> Listプローブビルド = new();
    private readonly ブローブビルドExpressionEqualityComparer ブローブビルドExpressionEqualityComparer;
    public 取得_OuterPredicate_InnerPredicate_プローブビルド(作業配列 作業配列, 判定_指定Parameter無_他Parameter有 判定_指定Parameter無_他Parameter有, 判定_指定Parameter有_他Parameter無_Lambda内部走査 判定_指定Parameter有_他Parameter無_Lambda内部走査,
        ブローブビルドExpressionEqualityComparer ブローブビルドExpressionEqualityComparer)
    {
        //:base(作業配列){
        this.作業配列=作業配列;
        this.判定_指定Parameter無_他Parameter有=判定_指定Parameter無_他Parameter有;
        this.判定_指定Parameter有_他Parameter無_Lambda内部走査=判定_指定Parameter有_他Parameter無_Lambda内部走査;
        this.ブローブビルドExpressionEqualityComparer=ブローブビルドExpressionEqualityComparer;
    }
    private IList<ParameterExpression>? 外側Parameters;
    private ParameterExpression? 内側Parameter;
    private Expression? OuterPredicate;
    //条件式CNFに分解して取得しその式のラムダの深さをListに入れる
    //ソートしてつまりラムダの浅い順に入れる
    //そのリストを走査し
    public (Expression? OuterPredicate, Expression? InnerPredicate, List<(Expression プローブ, Expression ビルド)> Listプローブビルド) 実行(Expression predicate_Body, IList<ParameterExpression> 外側Parameters, ParameterExpression 内側Parameter)
    {
        this.外側Parameters=外側Parameters;
        this.内側Parameter=内側Parameter;
        this.OuterPredicate=null;
        var Listプローブビルド = this.Listプローブビルド;
        Listプローブビルド.Clear();
        var InnerPredicate = this.PrivateTraverseNullable(predicate_Body);
        return (this.OuterPredicate, InnerPredicate, Listプローブビルド);
    }
    //public (Expression? OuterPredicate, Expression? InnerPredicate, List<(Expression プローブ, Expression ビルド)> Listプローブビルド) TSQLから実行(Expression predicate_Body,IList<ParameterExpression> 外側Parameters,ParameterExpression 内側Parameter) {
    //    this.外側Parameters=外側Parameters;
    //    this.内側Parameter=内側Parameter;
    //    this.OuterPredicate=null;
    //    this.Comparer=null;
    //    var Listプローブビルド = this.Listプローブビルド;
    //    Listプローブビルド.Clear();
    //    var InnerPredicate = this.PrivateTraverseNullable(predicate_Body);
    //    Debug.Assert(this.Comparer is null);
    //    return (this.OuterPredicate, InnerPredicate,Listプローブビルド);
    //}
    private Expression? 等号が出現した時にDictionaryHashとEqualに分離(Expression e, Expression Left, Expression Right)
    {
        Debug.Assert(this.外側Parameters is not null);
        if (HashEqualを設定(Right, Left)||HashEqualを設定(Left, Right)) return null;
        Debug.Assert(this.内側Parameter is not null);
        if (this.判定_指定Parameter有_他Parameter無_Lambda内部走査.実行(e, this.内側Parameter!))
        {
            this.OuterPredicate=AndAlsoで繋げる(this.OuterPredicate, e);
            return null;
        }
        return e;
        bool HashEqualを設定(Expression プローブ, Expression ビルド)
        {
            //↓のb=>の式が
            //Int32変数.Let(a=>
            //    a.Let(b=>
            //    )
            //)
            //c.Where(c=>c+1==a+1)○
            //c.Where(c=>c+1==2)  ×
            //c.Where(c=>c+a==a+1)×
            //c.Where(c=>c+a==a+c)×
            //c.Where(c=>c+a==c+1)×
            Debug.Assert(this.内側Parameter is not null);
            if (this._判定_指定Parameterが存在する.実行(ビルド, this.内側Parameter))
            {
                if (this.判定_指定Parameter無_他Parameter有.実行(プローブ, this.内側Parameter))
                {
                    //_ListParameters[0]={}
                    //_ListParameters[1]={a}
                    //_ListParameters[2]={b}
                    //_ListParameters[3]={c}
                    //↑の例だと{b},{c}が含まれていたらDictionary出来ない。
                    if (!this.Listプローブビルド.Contains((プローブ, ビルド), this.ブローブビルドExpressionEqualityComparer))
                    {
                        this.Listプローブビルド.Add((プローブ, ビルド));
                        return true;
                    }
                }
            }
            return false;
        }
    }
    //protected override Expression Traverse(Expression Expression0){
    //    var Expression1=this.PrivateTraverseNullable(Expression0)!;
    //    return Expression1;
    //}
    private Expression? PrivateTraverseNullable(Expression Expression0)
    {
        switch (Expression0.NodeType)
        {
            case ExpressionType.OrElse:
                return Expression0;
            case ExpressionType.AndAlso:
                {
                    var Binary0 = (BinaryExpression)Expression0;
                    var Binary0_Left = Binary0.Left;
                    var Binary0_Right = Binary0.Right;
                    var Binary1_Left = this.PrivateTraverseNullable(Binary0_Left);
                    var Binary1_Right = this.PrivateTraverseNullable(Binary0_Right);
                    if (Binary1_Left is null) return Binary1_Right;
                    if (Binary1_Right is null) return Binary1_Left;
                    return Expression.AndAlso(Binary1_Left, Binary1_Right);
                }
            case ExpressionType.Equal:
                {
                    var Binary0 = (BinaryExpression)Expression0;
                    var Binary_Left = Binary0.Left;
                    if (Binary_Left.Type.IsPrimitive) return this.等号が出現した時にDictionaryHashとEqualに分離(Binary0, Binary_Left, Binary0.Right);
                    var Binary_Left_Type = Binary_Left.Type;
                    var IEquatableType = this.作業配列.MakeGenericType(typeof(IEquatable<>), Binary_Left_Type);
                    return IEquatableType.IsAssignableFrom(Binary_Left_Type) ? this.等号が出現した時にDictionaryHashとEqualに分離(Binary0, Binary_Left, Binary0.Right) : Expression0;
                }
            case ExpressionType.Call:
                {
                    var MethodCall = (MethodCallExpression)Expression0;
                    if (nameof(Equals)==MethodCall.Method.Name)
                    {
                        if (Reflection.Object.Equals_==MethodCall.Method)
                        {
                            return this.等号が出現した時にDictionaryHashとEqualに分離(
                                Expression0,
                                MethodCall.Object,
                                MethodCall.Arguments[0]
                            );
                        }
                        //if(Reflection.Helpers.EqualityComparer_Equals==GetGenericMethodDefinition(MethodCall.Method)) {
                        //    //Reflection.Helpers.EqualityComparer_Equals==MethodCall.Method.GetGenericMethodDefinition()) {
                        //    this.Comparer=MethodCall.Object;
                        //    var MethodCall_Arguments = MethodCall.Arguments;
                        //    return this.等号が出現した時にDictionaryHashとEqualに分離(
                        //        Expression0,
                        //        MethodCall_Arguments[0],
                        //        MethodCall_Arguments[1]
                        //    );
                        //}
                        //var T=typeof(IEqualityComparer<>).GetGenericArguments()[0];
                        //var xx=typeof(IEqualityComparer<>).GetMethod("Equals",new []{T,T});
                        //if(MethodCall.Object!.Type.GetGenericArguments(typeof(IEqualityComparer<>),out var _)){
                        //    this.Comparer=MethodCall.Object;
                        //    var MethodCall_Arguments = MethodCall.Arguments;
                        //    return this.等号が出現した時にDictionaryHashとEqualに分離(
                        //        Expression0,
                        //        MethodCall_Arguments[0],
                        //        MethodCall_Arguments[1]
                        //    );
                        //}
                        var IEquatable = MethodCall.Arguments[0].Type.GetInterface(CommonLibrary.IEquatable_FullName);
                        if (IEquatable is not null) return this.等号が出現した時にDictionaryHashとEqualに分離(Expression0, MethodCall.Object, MethodCall.Arguments[0]);
                    }
                    break;
                }
        }
        Debug.Assert(this.内側Parameter is not null);
        if (this.判定_指定Parameter有_他Parameter無_Lambda内部走査.実行(Expression0, this.内側Parameter!))
        {
            this.OuterPredicate=AndAlsoで繋げる(this.OuterPredicate, Expression0);
            return null;
        }
        return Expression0;
    }
}
