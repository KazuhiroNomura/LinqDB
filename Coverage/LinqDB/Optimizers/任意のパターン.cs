﻿//using System.Collections.Generic;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Reflection;
using System.Reflection.Emit;
using LinqDB.Helpers;
using LinqDB.Optimizers;
using LinqDB.Sets;
using Microsoft.VisualStudio.TestTools.UnitTesting;
// ReSharper disable AssignNullToNotNullAttribute
namespace CoverageCS.LinqDB.Optimizers;

[TestClass]
public class 任意のパターン : ATest{
    private void Create<TResult>(Expression<Func<TResult>> Lambda) {
        var Optimizer = this.Optimizer;
        var M= Optimizer.CreateDelegate(Lambda);
    }
    [TestMethod]
    public void JoinにComparerがつくと例外が発生する(){
        for(var a=0;a<1000;a++){
            var Tuple=new ClassTuple<int>(3);
            var Impl_DynamicMethod=new DynamicMethod("",typeof(int),new Type[]{typeof(ClassTuple<int>),typeof(int)},typeof(任意のパターン),true){
                InitLocals=false
            };
            var I=Impl_DynamicMethod.GetILGenerator();
            I.Ldarg_0();
            I.Ldfld(typeof(ClassTuple<int>).GetField(nameof(ClassTuple<int>.Item1))!);
            I.Ldarg_1();
            I.Add();
            I.Ret();
            var Delegate=(Func<int,int>)Impl_DynamicMethod.CreateDelegate(typeof(Func<int,int>),Tuple);
            var v=Delegate(4);
        }
        //this.AssertExecute((a,b) => ArrN<int>(a).Join(ArrN<int>(b),o => o,i => i,(o,i) => o+i).Any());
        //this.AssertExecute((a,b) => EnuN<int>(a).Join(EnuN<int>(b),o => o,i => i,(o,i) => o+i).Any());
        //this.AssertExecute((a,b) => SetN<int>(a).Join(SetN<int>(b),o => o,i => i,(o,i) => o+i).Any());
        //this.AssertExecute((a,b) => ArrN<int>(a).Join(ArrN<int>(b),o => o,i => i,(o,i) => o+i).Any());
        //this.AssertExecute((a,b) => EnuN<int>(a).Join(EnuN<int>(b),o => o,i => i,(o,i) => o+i).Any());
        //this.AssertExecute((a,b) => SetN<int>(a).Join(SetN<int>(b),o => o,i => i,(o,i) => o+i).Any());
        //this.AssertExecute((a,b) => ArrN<int>(a).Join(ArrN<int>(b),o => o,i => i,(o,i) => o+i,EqualityComparer<int>.Default).Any());
        //this.AssertExecute((a,b) => EnuN<int>(a).Join(EnuN<int>(b),o => o,i => i,(o,i) => o+i,EqualityComparer<int>.Default).Any());
        var s0=new int[1];
        var s1=new int[1];
        var v0=3;
        var v1=3;
        var Default=EqualityComparer<int>.Default;
        //for(var a=0;a<1000;a++){
        //    this.Create(()=>(1).Let(o=>s1.Select(i=>o).Where(i=>Default.Equals(i,i))));
        //    Trace.WriteLine(a.ToString());
        //}
        //for(var a=0;a<1000;a++){
        //    this.Execute(()=>(1).Let(o=>s1.Select(i=>Default.Equals(i,i))));
        //    Trace.WriteLine(a.ToString());
        //}
        for(var a=0;a<1000;a++){
            this.Execute2(()=>(1).Let(o=>s1.Select(i=>o).Where(i=>Default.Equals(i,i))));
            Trace.WriteLine(a.ToString());
        }
        for(var a=0;a<1000;a++){
            this.Execute2(()=>s0.Select(o=>s1.Select(i=>o).Where(i=>Default.Equals(i,i))));
            Trace.WriteLine(a.ToString());
        }
        for(var a=0;a<1000;a++){
            this.Execute2(()=>s0.Select(o=>s1.Where(i=>Default.Equals(i,i)).Select(i=>o)));
            Trace.WriteLine(a.ToString());
        }
        for(var a=0;a<1000;a++){
            this.Execute2(()=>s0.SelectMany(o=>s1.Where(i=>Default.Equals(i,i)).Select(i=>o)));
            Trace.WriteLine(a.ToString());
        }
        for(var a=0;a<1000;a++){
            this.Execute2(()=>s0.SelectMany(o=>s1.Where(i=>EqualityComparer<int>.Default.Equals(o,i)).Select(i=>o+i)));
            Trace.WriteLine(a.ToString());
        }
        for(var a=0;a<1000;a++){
            this.Execute2(()=>s0.Join(s1,o=>o,i=>i,(o,i)=>o+i,EqualityComparer<int>.Default));
            //this.Execute(()=>EqualityComparer<int>.Default.Equals(v0,v1));
        }
        for(var a=0;a<1000;a++){
            //2
            this.Execute2(()=>s0.Join(s1,o=>o,i=>i,(o,i)=>o+i));
        }
        for(var a=0;a<1000;a++){
            //2
            this.Execute引数パターン標準ラムダループ((a,b)=>s0.Join(s1,o=>o,i=>i,(o,i)=>o+i,EqualityComparer<int>.Default));
        }

        this.Execute引数パターン標準ラムダループ((a,b) => s0.Join(s1,o => o,i => i,(o,i) => o+i,EqualityComparer<int>.Default).Any());//
        this.Execute標準ラムダループ(() => s0.Join(s1,o => o,i => i,(o,i) => o+i,EqualityComparer<int>.Default));
        this.Execute引数パターン標準ラムダループ((a,b) => SetN<int>(a).Join(SetN<int>(b),o => o,i => i,(o,i) => o+i,EqualityComparer<int>.Default));
        //this.AssertExecute((a,b) => ArrN<int>(a).AsEnumerable().Join(ArrN<int>(b),o => o,i => i,(o,i) => o+i).Any());
        //this.AssertExecute((a,b) => EnuN<int>(a).AsEnumerable().Join(EnuN<int>(b),o => o,i => i,(o,i) => o+i).Any());
        this.Execute引数パターン標準ラムダループ((a,b) => SetN<int>(a).AsEnumerable().Join(SetN<int>(b),o => o,i => i,(o,i) => o+i).Any());//
    }

    private static void Let(Action func)=>func();
    [TestMethod]
    public void LambdaとBlockのネストの外だし0(){
        //()=>{
        //    decimal DealerDiscount
        //    {
        //        DealerDiscount=.6m	
        //        "".Let(()=>DealerDiscount.HasValue)
        //    }
        //}
        var DealerDiscount=Expression.Parameter(typeof(decimal?),"DealerDiscount");
        var Lambda = Expression.Lambda<Action>(
            Expression.Block(
                new[]{DealerDiscount},
                Expression.Assign(DealerDiscount,Expression.Constant(.6m,typeof(decimal?))),
                Expression.Call(
                    typeof(任意のパターン).GetMethod("Let",BindingFlags.NonPublic|BindingFlags.Static)!,
                    Expression.Lambda<Action>(
                        Expression.Property(DealerDiscount,"HasValue")
                    )
                )
            )
        );
        this.CreateDelegate(Lambda);
    }
    [TestMethod]
    public void LambdaとBlockのネストの外だし1(){
        //()=>{
        //    decimal DealerDiscount
        //    {
        //        DealerDiscount=.6m	
        //        "".Let(()=>DealerDiscount.HasValue)
        //    }
        //}
        var p=Expression.Parameter(typeof(int),"p");
        var q=Expression.Parameter(typeof(int),"q");
        var DealerDiscount=Expression.Parameter(typeof(decimal?),"DealerDiscount");
        var a=Expression.Call(
                global::LinqDB.Reflection.ExtensionSet.Select_selector.MakeGenericMethod(typeof(int),typeof(int)),
                Expression.Constant(new Set<int>()),
                Expression.Lambda<Func<int,int>>(
                    Expression.Condition(
                        Expression.Property(DealerDiscount,"HasValue"),
                        q,
                        q
                    ),
                    q
                )
            )
            ;
        var Lambda = Expression.Lambda<Action>(
            Expression.Block(
                new[]{DealerDiscount},
                Expression.Block(
                    Expression.Assign(DealerDiscount,Expression.Constant(.6m,typeof(decimal?))),
                    Expression.Call(
                        global::LinqDB.Reflection.ExtensionSet.SelectMany_selector.MakeGenericMethod(typeof(int),typeof(int)),
                        Expression.Constant(new Set<int>()),
                        Expression.Lambda<Func<int,ImmutableSet<int>>>(
                            Expression.Call(
                                global::LinqDB.Reflection.ExtensionSet.Select_selector.MakeGenericMethod(typeof(int),typeof(int)),
                                Expression.Constant(new Set<int>()),
                                Expression.Lambda<Func<int,int>>(
                                    Expression.Condition(
                                        Expression.Property(DealerDiscount,"HasValue"),
                                        q,
                                        q
                                    ),
                                    q
                                )
                            )
                            ,p
                        )
                    )
                )
            )
        );
        this.CreateDelegate(Lambda);
    }
    [TestMethod]
    public void 定数0(){
        var data = new[]{
            1,2,3
        };
        this.Execute2(() =>
            3.Let(p => 1m)
        );
    }
    [TestMethod]
    public void 定数1(){
        var data = new[]{
            1,2,3
        };
        this.Execute2(() =>
            3.Let(p => data.Select(q => 1m))
        );
    }
    [TestMethod]
    public void Dictionaryの共通部分式を外だし()
    {
        var data = new[]{
            1,2,3
        };
        this.Execute2(() =>
            3.Let(p => data.Where(q => p == q))
        );
    }
    [TestMethod]
    public void Parameterが内部ラムダで読み取りされたらフィールドにより橋渡し0(){
        this.Execute2(() =>
            3.Let(p => 3.Let(q =>p+q ))
        );
    }
    [TestMethod]
    public void Parameterが内部ラムダで読み取りされたらフィールドにより橋渡し1(){
        var a=Expression.Parameter(typeof(int),"a");
        var b=Expression.Parameter(typeof(int),"b");
        //()=>
        //    Func<Int32,Func<Int32,Int32>>a=>
        //        Func<Int32,Int32>b=>a+b
        this.CreateDelegate(
            Expression.Lambda<Func<int,Func<int,int>>>(
                Expression.Lambda<Func<int,int>>(
                    Expression.Add(a,b),
                    b
                ),
                a
            )
        );
    }
    [TestMethod]
    public void Parameterが内部ラムダで読み取りされたらフィールドにより橋渡し2(){
        var a=Expression.Parameter(typeof(int),"a");
        var b=Expression.Parameter(typeof(int),"b");
        //()=>
        //    Method0(
        //        1,
        //        Func<Int32,Func<Int32,Int32>>a=>
        //            Method1(
        //                2,
        //                Func<Int32,Int32>b=>a+b
        //            )
        //        )
        //    )
        //()=>
        //    Method0(
        //        1,
        //        Func<Int32,Func<Int32,Int32>>a=>{
        //            this.a=a
        //            Method1(
        //                2,
        //                Func<Int32,Int32>b=>this.a+b
        //            )
        //        })
        //    )
        var Method0 = typeof(任意のパターン).GetMethod("Method0",BindingFlags.NonPublic|BindingFlags.Static);
        var Method1 = typeof(任意のパターン).GetMethod("Method1",BindingFlags.NonPublic|BindingFlags.Static);
        var Lambda = Expression.Lambda<Func<int>>(
            Expression.Call(
                Method0,
                Expression.Constant(1),
                Expression.Lambda<Func<int,int>>(
                    Expression.Call(
                        Method1,
                        Expression.Constant(2),
                        Expression.Lambda<Func<int,int>>(
                            Expression.Add(a,b),
                            b
                        )
                    ),
                    a
                )
            )
        );
        this.CreateDelegate(Lambda);
    }
    class キャプチャ {
        public int a;
    }
    private static int Method0(int a,Func<int,int>Func)=>Func(a);
    private static int Method1(int b,Func<int,int>Func)=>Func(b);
    private delegate int input_reference(int input,ref int reference);
    [TestMethod]
    public void Parameterが内部ラムダで書き込みされたらフィールドにより橋渡し2(){
        var a=Expression.Parameter(typeof(int),"a");
        var b=Expression.Parameter(typeof(int),"b");
        //var キャプチャ=new キャプチャ();
        //var @this=Expression.Constant(キャプチャ);
        //var this_a=Expression.Parameter(typeof(Int32).MakeByRefType(),"this_a");
        //()=>
        //    Method0(
        //        1,
        //        Func<Int32,Func<Int32,Int32>>a=>
        //            Method1(
        //                2,
        //                Func<Int32,Int32>b=>a=b
        //            )
        //        )
        //    )
        //()=>
        //    Method0(
        //        1,
        //        Func<Int32,Func<Int32,Int32>>a=>{
        //            Method1(
        //                2,
        //                Func<Int32,Int32>b=>this.a=b
        //            )
        //            a=this.a
        //        })
        //    )
        var Method0 = typeof(任意のパターン).GetMethod("Method0",BindingFlags.NonPublic|BindingFlags.Static)!;
        var Method1 = typeof(任意のパターン).GetMethod("Method1",BindingFlags.NonPublic|BindingFlags.Static)!;
        var Lambda = Expression.Lambda<Func<int>>(
            Expression.Call(
                Method0,
                Expression.Constant(1),
                Expression.Lambda<Func<int,int>>(
                    Expression.Call(
                        Method1,
                        Expression.Constant(2),
                        Expression.Lambda<Func<int,int>>(
                            Expression.Assign(a,b),
                            b
                        )
                    ),
                    a
                )
            )
        );
        this.CreateDelegate(Lambda);
    }
    [TestMethod]
    public void Parameterが内部ラムダで読み書きされたらフィールドにより橋渡し2(){
        var a=Expression.Parameter(typeof(int),"a");
        var b=Expression.Parameter(typeof(int),"b");
        //var キャプチャ=new キャプチャ();
        //var @this=Expression.Constant(キャプチャ);
        //var this_a=Expression.Parameter(typeof(Int32).MakeByRefType(),"this_a");
        //()=>
        //    Method0(
        //        1,
        //        Func<Int32,Func<Int32,Int32>>a=>
        //            Method1(
        //                2,
        //                Func<Int32,Int32>b=>a=a+b
        //            )
        //        )
        //    )
        //()=>
        //    Method0(
        //        1,
        //        Func<Int32,Func<Int32,Int32>>a=>{
        //            this.a=a
        //            Method1(
        //                2,
        //                Func<Int32,Int32>b=>this.a=this.a+b
        //            )
        //            a=this.a
        //        })
        //    )
        var Method0 = typeof(任意のパターン).GetMethod("Method0",BindingFlags.NonPublic|BindingFlags.Static);
        var Method1 = typeof(任意のパターン).GetMethod("Method1",BindingFlags.NonPublic|BindingFlags.Static);
        var Lambda = Expression.Lambda<Func<int>>(
            Expression.Call(
                Method0,
                Expression.Constant(1),
                Expression.Lambda<Func<int,int>>(
                    Expression.Call(
                        Method1,
                        Expression.Constant(2),
                        Expression.Lambda<Func<int,int>>(
                            Expression.Assign(a,Expression.Add(a,b)),
                            b
                        )
                    ),
                    a
                )
            )
        );
        this.CreateDelegate(Lambda);
    }
    [TestMethod]
    public void Parameterが内部ラムダで読み書きされたらフィールドにより橋渡し3(){
        var a=Expression.Parameter(typeof(int),"a");
        var b=Expression.Parameter(typeof(int),"b");
        var c=Expression.Parameter(typeof(int),"c");
        //var キャプチャ=new キャプチャ();
        //var @this=Expression.Constant(キャプチャ);
        //var this_a=Expression.Parameter(typeof(Int32).MakeByRefType(),"this_a");
        //()=>
        //    Method0(
        //        1,
        //        Func<Int32,Func<Int32,Int32>>a=>{
        //            Int32 b=3
        //            return a+b
        //        })
        //    )
        var Method0 = typeof(任意のパターン).GetMethod("Method0",BindingFlags.NonPublic|BindingFlags.Static);
        var Method1 = typeof(任意のパターン).GetMethod("Method1",BindingFlags.NonPublic|BindingFlags.Static);
        var Lambda = Expression.Lambda<Func<int>>(
            Expression.Call(
                Method0,
                Expression.Constant(1),
                Expression.Lambda<Func<int,int>>(
                    Expression.Block(
                        new[] {b },
                        Expression.Call(
                            Method1,
                            Expression.Constant(2),
                            Expression.Lambda<Func<int,int>>(
                                Expression.Assign(b,Expression.Constant(3)),
                                c
                            )
                        ),
                        Expression.Call(
                            Method1,
                            Expression.Constant(4),
                            Expression.Lambda<Func<int,int>>(
                                b,
                                c
                            )
                        )
                    ),
                    a
                )
            )
        );
        this.CreateDelegate(Lambda);
    }
}
