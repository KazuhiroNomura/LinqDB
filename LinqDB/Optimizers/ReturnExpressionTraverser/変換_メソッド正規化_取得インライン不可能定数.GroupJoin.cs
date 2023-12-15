using System;
using Generic=System.Collections.Generic;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Reflection;
using LinqDB.Helpers;
using LinqDB.Sets;
using Linq=System.Linq;
namespace LinqDB.Optimizers.ReturnExpressionTraverser;
using static Common;
partial class 変換_メソッド正規化_取得インライン不可能定数{
    protected Expression GroupJoin(MethodCallExpression MethodCall0) {
        var MethodCall0_Method = MethodCall0.Method;
        //var MethodCall0_Arguments = MethodCall0.Arguments;
        var MethodCall1_Arguments = this.TraverseExpressions(MethodCall0.Arguments);
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
        var GenericArguments = MethodCall0_Method.GetGenericArguments();
        MethodCall1_Arguments=this.KeySelectorの匿名型をValueTuple(GenericArguments,MethodCall1_Arguments);
        var MethodCall1_Arguments_0 = MethodCall1_Arguments[0];
        var MethodCall1_Arguments_1 = MethodCall1_Arguments[1];
        var MethodCall1_Arguments_2 = MethodCall1_Arguments[2];
        var MethodCall1_Arguments_3 = MethodCall1_Arguments[3];
        var MethodCall1_Arguments_4 = MethodCall1_Arguments[4];
        Expression? MethodCall1_Arguments_5;
        if(MethodCall1_Arguments.Count==6)
            MethodCall1_Arguments_5=MethodCall1_Arguments[5];
        else
            MethodCall1_Arguments_5=null;
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
        } else {
            MethodCall1_Arguments_5_Type = 作業配列.MakeGenericType(typeof(Generic.EqualityComparer<>),TKey);
            MethodCall1_Arguments_5=Expression.Call(MethodCall1_Arguments_5_Type.GetProperty(nameof(Generic.EqualityComparer<int>.Default))!.GetMethod);
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
}
