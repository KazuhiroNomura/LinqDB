namespace CoverageFS
open Microsoft.VisualStudio.TestTools.UnitTesting
open System
open System.Linq;
open Lite.Sets
[<TestClass>]
type 変換_KeySelectorの匿名型をValueTupleに書き換え()=
    inherit CoverageCS.Lite.ATest() 
    member this.SetInt32変数 = Set<Int32>[|3; 4; 5; 6; 7; 8; 9; 10|];
    member this.ArrInt32変数 = [|3; 4; 5; 6; 7; 8; 9; 10|];
    [<TestMethod>]
    member this.クエリ0() =
        this.AssertExecute(<@ Set<int32>([||]).Select(fun p->p+1) @>)
    [<TestMethod>]
    member this.クエリ1() =
        this.AssertExecute(<@ Set<int32>([||]).Join(Set<int32>([||]),(fun o->o),(fun i->i),(fun a b->a,b)) @>)
    [<TestMethod>]
    member this.クエリ2() =
        this.AssertExecute(<@ Set<int32>([||]).Select(fun p->p,p) @>)
    member this.クエリ3() =
        this.AssertExecute(<@ Set<int32>([||]).Intersect(Set<int32>([||])) @>)
    [<TestMethod>]
    member this.Call0() =
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
