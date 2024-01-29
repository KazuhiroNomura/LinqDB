using System;
using Generic=System.Collections.Generic;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Reflection;
using LinqDB.Helpers;
using Linq=System.Linq;
namespace LinqDB.Optimizers.ReturnExpressionTraverser;
using static Common;
partial class 変換_メソッド正規化_取得インライン不可能定数{
    protected Expression Join(MethodCallExpression MethodCall0) {
        var MethodCall0_Method = MethodCall0.Method;
        //var MethodCall0_Arguments = MethodCall0.Arguments;
        var MethodCall1_Arguments = this.TraverseExpressions(MethodCall0.Arguments);
        var MethodCall0_GenericMethodDefinition = GetGenericMethodDefinition(MethodCall0_Method);
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
        var TOuter = GenericArguments[0];
        var TInner = GenericArguments[1];
        var TKey = GenericArguments[2];
        var TResult = GenericArguments[3];
        Expression Equals_this;
        ParameterExpression o;
        LambdaExpression selector;
        Expression Equals_Argument;
        Generic.IEnumerable<ParameterExpression> predicate_Parameters;
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
                        変換_旧Parameterを新Expression1.実行(
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
                        変換_旧Parameterを新Expression1.実行(
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
                        変換_旧Parameterを新Expression1.実行(
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
        //Type MethodCall1_Arguments_5_Type;
        Expression predicate_Body;
        if(MethodCall1_Arguments_5 is not null) {
            //引数5にはComparerがあるのでそれで比較する。
            //O.Group     (I,o=>o,i=>i,???)
            //O.SelectMany(o=>I.Where(i=>EqualityComparer.Equal(i,o).???)
            //nullの比較の仕方をEqualityComparerに任せる
            var MethodCall1_Arguments_5_Type = MethodCall1_Arguments_5.Type;
            Debug.Assert(MethodCall1_Arguments_5_Type.GetInterface(CommonLibrary.IEqualityComparer_FullName)!.GetGenericArguments()[0]==TKey);
            predicate_Body=Expression.Call(
                MethodCall1_Arguments_5,
                作業配列.GetMethod(MethodCall1_Arguments_5_Type,nameof(Generic.EqualityComparer<int>.Equals),TKey,TKey),
                Equals_this,
                Equals_Argument
            );
        } else {
            //MethodCall1_Arguments_5_Type = 作業配列.MakeGenericType(typeof(Generic.EqualityComparer<>),TKey);
            //MethodCall1_Arguments_5=Expression.Call(MethodCall1_Arguments_5_Type.GetProperty(nameof(Generic.EqualityComparer<int>.Default))!.GetMethod);


            if(TKey.IsNullable()){
                Debug.Assert(Equals_Argument.Type.IsNullable());
                //nullの比較の仕方をDBのnullの比較と同様にする。
                //null==nullはC#だとtrueだがDBと同様にfalseにする
                var HasValue = Equals_this.Type.GetProperty("HasValue");
                var Equals_this_GetValueOrDefault=Equals_this.GetValueOrDefault();
                var ElementTKey=Equals_this_GetValueOrDefault.Type;
                predicate_Body=Expression.AndAlso(
                    Expression.Property(Equals_this,HasValue),
                    Expression.AndAlso(
                        Expression.Property(Equals_Argument,HasValue),
                        Expression.Call(
                            Equals_this_GetValueOrDefault,
                            作業配列.GetMethod(ElementTKey,nameof(object.Equals),ElementTKey),
                            Equals_Argument.GetValueOrDefault()
                        )
                    )
                );
            } else{
                predicate_Body=Expression.Call(
                    Equals_this,
                    作業配列.GetMethod(Equals_this.Type,nameof(object.Equals),TKey),
                    Equals_Argument
                );
            }
        }
        //var predicate_Body=Expression.Call(
        //    MethodCall1_Arguments_5,
        //    作業配列.GetMethod(MethodCall1_Arguments_5_Type,nameof(Generic.EqualityComparer<int>.Equals),TKey,TKey),
        //    Equals_this,
        //    Equals_Argument
        //);
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
}
