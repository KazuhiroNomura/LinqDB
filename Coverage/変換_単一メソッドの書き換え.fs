namespace CoverageFS
open Microsoft.VisualStudio.TestTools.UnitTesting
open System
open System.Linq;
open Lite.Sets
[<TestClass>]
type 変換_単一メソッドの書き換え()=
    inherit CoverageCS.Lite.ATest() 
    member this.SetInt32変数 = Set<Int32>[|1; 2|];
    member this.ArrInt32変数 = [|3; 4; 5; 6; 7; 8; 9; 10|];
    [<TestMethod>]
    member this.Add() =
        let Expr0 = <@@ 13 @@>
        let Expr1= <@ (fun x -> x * %%Expr0) @>
        this.AssertExecute(
            <@ 
                Set<int32>().Select(fun p->p+1) 
            @>
        )
    [<TestMethod>]
    member this.Join() =
        this.AssertExecute(
            <@ 
                Set<int32>().Join(
                    Set<int32>(),
                    (fun o->o),
                    (fun i->i),
                    (fun a b->a,b)
                ) 
            @>
        )
    [<TestMethod>]
    member this.Select() =
        this.AssertExecute(
            <@ 
                Set<int32>().Select(fun p->p,p) 
            @>
        )
    member this.Intersect() =
        this.AssertExecute(
            <@ 
                Set<int32>().Intersect(Set<int32>()) 
            @>
        )
    [<TestMethod>]
    member this.Join_Join() =
        this.AssertExecute(
            <@ 
                this.SetInt32変数.Join(
                    this.SetInt32変数.Join(
                        this.SetInt32変数,
                        (fun o->o),
                        (fun i->i),
                        (fun o i->o+i)
                    ),
                    (fun o->o),
                    (fun i->i),
                    (fun o i->o,i)
                )
            @>
        )
    [<TestMethod>]
    member this.Call1() =
        this.AssertExecute(
            <@ 
                this.SetInt32変数.Join(
                    this.SetInt32変数,
                    (fun o->o),
                    (fun i->i),
                    (fun o i->o,i)
                )
            @>
        )
    [<TestMethod>]
    member this.Call2() =
        this.AssertExecute(
            <@ 
                this.ArrInt32変数.Join(
                    this.ArrInt32変数,
                    (fun o->o),
                    (fun i->i),
                    (fun o i->o,i)
                )
            @>
        )
    [<TestMethod>]
    member this.Call3() =
        this.AssertExecute(
            <@ 
                this.SetInt32変数.Join(
                    this.SetInt32変数,
                    (fun o->o,o+1),
                    (fun i->i,i+1),
                    (fun o i->o,i)
                )
            @>
        )
    [<TestMethod>]
    member this.Call4() =
        this.AssertExecute(
            <@ 
                this.ArrInt32変数.Join(
                    this.ArrInt32変数,
                    (fun o->o,o+1),
                    (fun i->i,i+1),
                    (fun o i->o,i)
                )
            @>
        )
