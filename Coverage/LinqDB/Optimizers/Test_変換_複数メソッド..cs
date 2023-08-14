using System;
using System.Linq;
using LinqDB.Sets;
using Microsoft.VisualStudio.TestTools.UnitTesting;
// ReSharper disable SimilarAnonymousTypeNearby
// ReSharper disable RedundantExplicitArrayCreation
// ReSharper disable RedundantCast
namespace CoverageCS.LinqDB.Optimizers;

[TestClass]
public class Test_変換_複数メソッド:ATest {
    [TestMethod]
    public void Call() {
        //if(MethodCall0_Arguments.Count==0) {
        this.Execute標準ラムダループ(
            () => Enumerable.Empty<int>());
        //}
        //if(MethodCall1_MethodCall!=null) {
        //    if(Reflection.ExtendSet.Any == MethodCall0_GenericMethodDefinition){
        //        if(Reflection.ExtendSet.GroupJoin0==MethodCall1_MethodCall_GenericMethodDefinition)
        this.Execute標準ラムダループ(
            () => new Set<int>().GroupJoin(new Set<int>(),o => o,i => i,(o,i) => new { o,i }).Any());
        this.Execute標準ラムダループ(
            () => new Set<int>().GroupJoin(new Set<int> { 1,2,3 },o => o,i => i,(o,i) => new { o,i }).Any());
        this.Execute2(() => new Set<int> { 1,2,3 }.GroupJoin(new Set<int>(),o => o,i => i,(o,i) => new { o,i }).Any());
        this.Execute2(() => new Set<int> { 1,2,3 }.GroupJoin(new Set<int> { 1,2,3 },o => o,i => i,(o,i) => new { o,i }).Any());
        //    } else if(Reflection.ExtendEnumerable.Any==MethodCall0_GenericMethodDefinition) {
        //        if(Reflection.ExtendEnumerable.GroupJoin0==MethodCall1_MethodCall_GenericMethodDefinition)
        this.Execute2(() => System.Array.Empty<int>().GroupJoin(System.Array.Empty<int>(),o => o,i => i,(o,i) => new { o,i }).Any());
        this.Execute2(() => System.Array.Empty<int>().GroupJoin(new int[] { 1,2,3 },o => o,i => i,(o,i) => new { o,i }).Any());
        this.Execute2(() => new int[] { 1,2,3 }.GroupJoin(System.Array.Empty<int>(),o => o,i => i,(o,i) => new { o,i }).Any());
        this.Execute2(() => new int[] { 1,2,3 }.GroupJoin(new int[] { 1,2,3 },o => o,i => i,(o,i) => new { o,i }).Any());
        //    if(Reflection.ExtendEnumerable.Select_selector==MethodCall0_GenericMethodDefinition) {
        //        if(Reflection.ExtendEnumerable.Join0==MethodCall1_MethodCall_GenericMethodDefinition)return this.Join0Select1(MethodCall0,MethodCall1_MethodCall,Reflection.ExtendEnumerable.Join0);
        this.Execute引数パターン標準ラムダループ((a,b) => ArrN<int>(a).Join(ArrN<int>(b),o => o,i => i,(o,i) => new { o,i }).Select(p => new { p.i,p.o }));
        //        if(Reflection.ExtendEnumerable.Select_selector==MethodCall1_MethodCall_GenericMethodDefinition)return this.Select1Select1(MethodCall0,MethodCall1_MethodCall,Reflection.ExtendEnumerable.Select_selector);
        this.Execute引数パターン標準ラムダループ((a,b) => ArrN<int>(a).Select(p => new { o = p*2,i = p*3 }).Select(p => new { p.i,p.o }));
        //    } else if(Reflection.ExtendEnumerable.Where0==MethodCall0_GenericMethodDefinition) {
        //        if(Reflection.ExtendEnumerable.Join0==MethodCall1_MethodCall_GenericMethodDefinition) {
        //            if(MethodCall1_MethodCall_resultSelector!=null&&MethodCall0_predicate!=null) {
        this.Execute引数パターン標準ラムダループ((a,b) => ArrN<int>(a).Join(ArrN<int>(b),o => o,i => i,(o,i) => new { o,i }).Where(p => p.o==1));
        //                if(葉OuterPredicate!=null) {
        this.Execute引数パターン標準ラムダループ((a,b) => ArrN<int>(a).Join(ArrN<int>(b),o => o,i => i,(o,i) => new { o,i }).Where(p => p.o==0));
        //                }
        this.Execute引数パターン標準ラムダループ((a,b) => ArrN<int>(a).Join(ArrN<int>(b),o => o,i => i,(o,i) => new { o,i }).Where(p => true));
        //                if(葉InnerPredicate!=null) {
        this.Execute引数パターン標準ラムダループ((a,b) => ArrN<int>(a).Join(ArrN<int>(b),o => o,i => i,(o,i) => new { o,i }).Where(p => p.i==0));
        //                }
        this.Execute引数パターン標準ラムダループ((a,b) => ArrN<int>(a).Join(ArrN<int>(b),o => o,i => i,(o,i) => new { o,i }).Where(p => false));
        //                if(根predicate==null) return Join;
        this.Execute引数パターン標準ラムダループ((a,b) => ArrN<int>(a).Join(ArrN<int>(b),o => o,i => i,(o,i) => new { o,i }).Where(p => 3==DateTimeOffset.Now.Year));
        this.Execute引数パターン標準ラムダループ((a,b) => ArrN<int>(a).Join(ArrN<int>(b),o => o,i => i,(o,i) => new { o,i }).Where(p => p.o==p.i));
        //            }
        this.Execute引数パターン標準ラムダループ((a,b) => ArrN<int>(a).Join(ArrN<int>(b),o => o,i => i,this.Func((int o,int i) => new { o,i })).Where(p => p.o==0));
        this.Execute引数パターン標準ラムダループ((a,b) => ArrN<int>(a).Join(ArrN<int>(b),o => o,i => i,(o,i) => o+i).Where(Func((int p) => p==0)));
        this.Execute引数パターン標準ラムダループ((a,b) => ArrN<int>(a).Join(ArrN<int>(b),o => o,i => i,this.Func((int o,int i) => o+1)).Where(Func((int p) => p==0)));
        //        } else if(Reflection.ExtendEnumerable.Where0==MethodCall1_MethodCall_GenericMethodDefinition) {
        this.Execute引数パターン標準ラムダループ((a,b) => ArrN<int>(a).Where(p => p==1).Where(p => p==2));
        //        }
        //    }else  if(Reflection.ExtendSet1.Select_selector==MethodCall0_GenericMethodDefinition) {
        //        }else if(Reflection.ExtendSet1.Join==MethodCall1_MethodCall_GenericMethodDefinition){
        this.Execute引数パターン標準ラムダループ((a,b) => SetN<int>(a).Join(SetN<int>(b),o => o,i => i,(o,i) => new { o,i }).Select(p => new { p.i,p.o }));
        //        }else if(Reflection.ExtendSet1.Select_selector==MethodCall1_MethodCall_GenericMethodDefinition){
        this.Execute引数パターン標準ラムダループ((a,b) => SetN<int>(a).Select(p => new { o = p*2,i = p*3 }).Select(p => new { p.i,p.o }));
        //        }
        this.Execute引数パターン標準ラムダループ((a,b) => SetN<int>(a).OfType<object>().Select(p => p is int));
        //    } else if(Reflection.ExtendSet1.Where==MethodCall0_GenericMethodDefinition) {
        //        if(Reflection.ExtendSet1.InternalJoin12==MethodCall1_MethodCall_GenericMethodDefinition) {
        //            if(MethodCall1_MethodCall_resultSelector!=null&&MethodCall0_predicate!=null) {
        //            }
        //        } else if(Reflection.ExtendSet2.InternalJoin21==MethodCall1_MethodCall_GenericMethodDefinition) {
        //            if(MethodCall1_MethodCall_resultSelector!=null&&MethodCall0_predicate!=null) {
        //            }
        //        } else if(Reflection.ExtendSet1.Join==MethodCall1_MethodCall_GenericMethodDefinition) {
        //            if(MethodCall1_MethodCall_resultSelector!=null&&MethodCall0_predicate!=null) {
        this.Execute引数パターン標準ラムダループ((a,b) => SetN<int>(a).Join(SetN<int>(b),o => o,i => i,(o,i) => new { o,i }).Where(p => p.o==1));
        //                if(葉OuterPredicate!=null) {
        this.Execute引数パターン標準ラムダループ((a,b) => SetN<int>(a).Join(SetN<int>(b),o => o,i => i,(o,i) => new { o,i }).Where(p => p.o==0));
        //                }
        this.Execute引数パターン標準ラムダループ((a,b) => SetN<int>(a).Join(SetN<int>(b),o => o,i => i,(o,i) => new { o,i }).Where(p => true));
        //                if(葉InnerPredicate!=null) {
        this.Execute引数パターン標準ラムダループ((a,b) => SetN<int>(a).Join(SetN<int>(b),o => o,i => i,(o,i) => new { o,i }).Where(p => p.i==0));
        //                }
        this.Execute引数パターン標準ラムダループ((a,b) => SetN<int>(a).Join(SetN<int>(b),o => o,i => i,(o,i) => new { o,i }).Where(p => false));
        //                if(根predicate==null) return Join;
        this.Execute引数パターン標準ラムダループ((a,b) => SetN<int>(a).Join(SetN<int>(b),o => o,i => i,(o,i) => new { o,i }).Where(p => 3==DateTimeOffset.Now.Year));
        this.Execute引数パターン標準ラムダループ((a,b) => SetN<int>(a).Join(SetN<int>(b),o => o,i => i,(o,i) => new { o,i }).Where(p => p.o==p.i));
        //            }
        this.Execute引数パターン標準ラムダループ((a,b) => SetN<int>(a).Join(SetN<int>(b),o => o,i => i,this.Func((int o,int i) => new { o,i })).Where(p => p.o==0));
        this.Execute引数パターン標準ラムダループ((a,b) => SetN<int>(a).Join(SetN<int>(b),o => o,i => i,(o,i) => o+i).Where(Func((int p) => p==0)));
        this.Execute引数パターン標準ラムダループ((a,b) => SetN<int>(a).Join(SetN<int>(b),o => o,i => i,this.Func((int o,int i) => o+1)).Where(Func((int p) => p==0)));
        //        } else if(Reflection.ExtendSet1.Where==MethodCall1_MethodCall_GenericMethodDefinition) {
        this.Execute引数パターン標準ラムダループ((a,b) => SetN<int>(a).Where(p => p%3==0).Where(p => p%2==0));
        //        }
        this.Execute引数パターン標準ラムダループ((a,b) => SetN<int>(a).Where(p => p%3==0));
        //    }
        //}
        this.Execute引数パターン標準ラムダループ((a,b) => SetN<int>(a).Union(SetN<int>(b)));
    }
    [TestMethod]
    public void Union_Where() {
        this.Execute引数パターン標準ラムダループ((a,b) => ArrN<int>(a).Union(ArrN<int>(b)).Where(p => p%3==0));
        this.Execute引数パターン標準ラムダループ((a,b) => ArrN<int>(a).Union(SetN<int>(b)).Where(p => p%3==0));
        this.Execute引数パターン標準ラムダループ((a,b) => SetN<int>(a).Union(ArrN<int>(b)).Where(p => p%3==0));
        this.Execute引数パターン標準ラムダループ((a,b) => SetN<int>(a).Union(SetN<int>(b)).Where(p => p%3==0));
    }
    [TestMethod]
    public void Intersect_Where() {
        this.Execute引数パターン標準ラムダループ((a,b) => ArrN<int>(a).Intersect(ArrN<int>(b)).Where(p => p%3==0));
        this.Execute引数パターン標準ラムダループ((a,b) => ArrN<int>(a).Intersect(SetN<int>(b)).Where(p => p%3==0));
        this.Execute引数パターン標準ラムダループ((a,b) => SetN<int>(a).Intersect(ArrN<int>(b)).Where(p => p%3==0));
        this.Execute引数パターン標準ラムダループ((a,b) => SetN<int>(a).Intersect(SetN<int>(b)).Where(p => p%3==0));
    }
    [TestMethod]
    public void Except_Where() {
        this.Execute引数パターン標準ラムダループ((a,b) => ArrN<int>(a).Except(ArrN<int>(b)).Where(p => p%3==0));
        this.Execute引数パターン標準ラムダループ((a,b) => ArrN<int>(a).Except(SetN<int>(b)).Where(p => p%3==0));
        this.Execute引数パターン標準ラムダループ((a,b) => SetN<int>(a).Except(ArrN<int>(b)).Where(p => p%3==0));
        this.Execute引数パターン標準ラムダループ((a,b) => SetN<int>(a).Except(SetN<int>(b)).Where(p => p%3==0));
    }
    private Func<T0,T1,TResult> Func<T0, T1, TResult>(Func<T0,T1,TResult> func) => func;
    [TestMethod]
    public void Join_Select() {
        this.Execute引数パターン標準ラムダループ((a,b) => ArrN<int>(a).Join(ArrN<int>(b),o => o,i => i,(o,i) => o+i).Select(q => q-1));
        this.Execute引数パターン標準ラムダループ((a,b) => ArrN<int>(a).Join(ArrN<int>(b),o => o,i => i,(o,i) => o+i).Select((Func<int,int>)(q => q-1)));
        this.Execute引数パターン標準ラムダループ((a,b) => ArrN<int>(a).Join(ArrN<int>(b),o => o,i => i,(Func<int,int,int>)((o,i) => o+i)).Select(q => q-1));
        this.Execute引数パターン標準ラムダループ((a,b) => ArrN<int>(a).Join(ArrN<int>(b),o => o,i => i,(Func<int,int,int>)((o,i) => o+i)).Select((Func<int,int>)(q => q-1)));
        this.Execute引数パターン標準ラムダループ((a,b) => SetN<int>(a).Join(SetN<int>(b),o => o,i => i,(o,i) => o+i).Select(q => q-1));
        this.Execute引数パターン標準ラムダループ((a,b) => SetN<int>(a).Join(SetN<int>(b),o => o,i => i,(o,i) => o+i).Select((Func<int,int>)(q => q-1)));
        this.Execute引数パターン標準ラムダループ((a,b) => SetN<int>(a).Join(SetN<int>(b),o => o,i => i,(Func<int,int,int>)((o,i) => o+i)).Select(q => q-1));
        this.Execute引数パターン標準ラムダループ((a,b) => SetN<int>(a).Join(SetN<int>(b),o => o,i => i,(Func<int,int,int>)((o,i) => o+i)).Select((Func<int,int>)(q => q-1)));
    }
    [TestMethod]
    public void Select_Select() {
        this.Execute引数パターン(a => ArrN<int>(a).Select(p => p+1).Select(q => q-1));
        this.Execute引数パターン(a => ArrN<int>(a).Select(p => p+1).Select((Func<int,int>)(q => q-1)));
        this.Execute引数パターン(a => ArrN<int>(a).Select((Func<int,int>)(p => p+1)).Select(q => q-1));
        this.Execute引数パターン(a => ArrN<int>(a).Select((Func<int,int>)(p => p+1)).Select((Func<int,int>)(q => q-1)));
        this.Execute引数パターン(a => SetN<int>(a).Select(p => p+1).Select(q => q-1));
        this.Execute引数パターン(a => SetN<int>(a).Select(p => p+1).Select((Func<int,int>)(q => q-1)));
        this.Execute引数パターン(a => SetN<int>(a).Select((Func<int,int>)(p => p+1)).Select(q => q-1));
        this.Execute引数パターン(a => SetN<int>(a).Select((Func<int,int>)(p => p+1)).Select((Func<int,int>)(q => q-1)));
    }
    [TestMethod]
    public void selector_selector() {
        //if(selector1 is LambdaExpression Lambda1) {
        //    if(selector0 is LambdaExpression Lambda0) {
        this.Execute引数パターン(a => 
            ArrN<int>(a).Select(oi=>
                new {
                    o = oi+1,
                    i = oi-1
                }
            ).Select(oi=>
                new {
                    oi,
                    oi.o,
                    oi.i
                }
            )
        );
        this.Execute引数パターン標準ラムダループ((a,b) => 
            ArrN<int>(a).Join(ArrN<int>(b),o=>o,i=>i,(o,i)=>
                new {
                    o,
                    i
                }
            ).Select(oi => 
                new {
                    oi,
                    oi.o,
                    oi.i
                }
            )
        );
        //    } else {
        static Func<TSource,TResult> SelectSourceDelegate<TSource, TResult>(TSource source,Func<TSource,TResult> Lambda) => Lambda;
        static Func<int,int,TResult> JoinDelegate<TResult>(Func<int,int,TResult> Lambda) => Lambda;
        {
            var resultSelector1 = JoinDelegate((o,i) => new { o,i });
            var selector0 = SelectSourceDelegate(resultSelector1(0,0),oi =>
                new {
                    oi,
                    oi.o,
                    oi.i
                }
            );
            this.Execute引数パターン(a => 
                ArrN<int>(a).Select(oi =>
                    new {
                        o = oi+1,
                        i = oi-1
                    }
                ).Select(selector0)
            );
            this.Execute引数パターン標準ラムダループ((a,b) => 
                ArrN<int>(a).Join(ArrN<int>(b),o => o,i => i,(o,i) => 
                    new {
                        o,
                        i
                    }
                ).Select(selector0)
            );
        }
        //    }
        //} else {
        //    if(selector0 is LambdaExpression Lambda0) {
        static Func<int,TResult> SelectDelegate<TResult>(Func<int,TResult> Lambda) => Lambda;
        {
            var selector1 = SelectDelegate(p=>new { o = p+1,i = p-1 });
            this.Execute引数パターン(a => 
                ArrN<int>(a).Select(selector1).Select(oi=>
                    new {
                        oi,
                        oi.o,
                        oi.i
                    }
                )
            );
            var resultSelector1 = JoinDelegate((o,i) => new { o,i });
            this.Execute引数パターン標準ラムダループ((a,b) => 
                ArrN<int>(a).Join(ArrN<int>(b),o => o,i => i,resultSelector1).Select(oi =>
                    new {
                        oi,
                        oi.o,
                        oi.i
                    }
                )
            );
        }
        //    } else {
        {
            //                static Func<TSource,TResult> SelectDelegate<TSource, TResult>(TSource source,Func<TSource,TResult> Lambda) => Lambda;
            //              static Func<Int32,Int32,TResult> JoinDelegate<TResult>(Func<Int32,Int32,TResult> Lambda) => Lambda;
            var selector1 = SelectDelegate(p => new { o = p+1,i = p-1 });
            var selector0 = SelectSourceDelegate(selector1(0),oi =>
                new {
                    oi,
                    oi.o,
                    oi.i
                }
            );
            this.Execute引数パターン(a =>
                ArrN<int>(a).Select(selector1).Select(selector0)
            );
            var resultSelector1 = JoinDelegate((o,i) => new { o,i });
            this.Execute引数パターン標準ラムダループ((a,b) =>
                ArrN<int>(a).Join(ArrN<int>(b),o => o,i => i,resultSelector1).Select(selector0)
            );
        }
        //    }
        //}
    }
    [TestMethod]
    public void Where_Where() {
        //if(MethodCall0_predicate!=null) {
        //    if(MethodCall0_MethodCall_predicate!=null) {
        this.Execute引数パターン(a => ArrN<int>(a).Where(p => p==1).Where(p => p==2));
        //    } else {
        this.Execute引数パターン(a => ArrN<int>(a).Where((Func<int,bool>)(p => p==1)).Where(p => p==2));
        //    }
        //} else {
        //    if(MethodCall0_MethodCall_predicate!=null) {
        this.Execute引数パターン(a => ArrN<int>(a).Where(p => p==1).Where((Func<int,bool>)(p => p==2)));
        //    } else {
        //    .Lambda LラムダR<Func`1[IEnumerable`1[Int32]]>() {
        //    .Block(
        //        Func`2[Int32,Boolean] $Cループ跨ぎ0,
        //    Func`2[Int32,Boolean] $Cループ跨ぎ1) {
        //        $Cループ跨ぎ0 = (Func`2[Int32,Boolean]).Lambda Lラムダ1<Func`2[Int32,Boolean]>(Int32 $Pラムダ引数1p) {
        //            $Pラムダ引数1p == 1
        //        };
        //        $Cループ跨ぎ1 = (Func`2[Int32,Boolean]).Lambda Lラムダ2<Func`2[Int32,Boolean]>(Int32 $Pラムダ引数2p) {
        //            $Pラムダ引数2p == 2
        //        };
        //        .Call Linq.Enumerable.Where(
        //            .Call カバレッジCS.Lite.ATest.Arr変数(),
        //            .Lambda Lループ0<Func`2[Int32,Boolean]>(Int32 $Pラムダ引数0) {
        //            .Invoke $Cループ跨ぎ0($Pラムダ引数0) && .Invoke $Cループ跨ぎ1($Pラムダ引数0)
        //        })
        //    }
        //}
        this.Execute引数パターン(a => ArrN<int>(a).Where((Func<int,bool>)(p => p==1)).Where((Func<int,bool>)(p => p==2)));
        //    }
        //}
    }
    [TestMethod]
    public void Not() {
        //if(Unary1_Operand.NodeType==ExpressionType.Not)
        this.Execute引数パターン(a => ArrN<int>(a).Where(p => !(!(p==1))));
        this.Execute引数パターン(a => ArrN<int>(a).Where(p => !(p==1)));
    }
    [TestMethod]
    public void SelectMany_Where0() {
        this.Execute引数パターン標準ラムダループ((a,b) =>
            SetN<int>(a).SelectMany(
                o0 => SetN<int>(b),
                (o1,i1) => new { o1,i1 }
            ).Where(
                oi=>oi.o1==0&&1==oi.o1&&oi.i1==2&&3==oi.i1&&oi.o1==oi.i1&&oi.i1==oi.o1
            )
        );
        this.Execute引数パターン標準ラムダループ((a,b) =>
            SetN<int>(a).SelectMany(
                o0 => SetN<int>(b),
                (Func<int,int,int>)((o1,i1) => o1+i1)
            )
        );
        this.Execute引数パターン標準ラムダループ((a,b) =>
            SetN<int>(a).SelectMany(
                (Func<int,Set<int>>)(o0 => SetN<int>(b)),
                (o1,i1) => o1+i1
            )
        );
        this.Execute引数パターン標準ラムダループ((a,b) =>
            SetN<int>(a).SelectMany(
                (Func<int,Set<int>>)(o0 => SetN<int>(b)),
                (Func<int,int,int>)((o1,i1) => o1+i1)
            )
        );
    }
}