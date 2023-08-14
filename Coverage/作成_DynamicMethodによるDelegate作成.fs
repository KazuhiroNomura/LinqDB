namespace CoverageFS
open Microsoft.VisualStudio.TestTools.UnitTesting
open System.Linq;
open Lite.Sets
[<TestClass>]
type 作成_DynamicMethodによるDelegate作成()=
    inherit CoverageCS.Lite.ATest() 
    static member Int32_1=1;
    static member Int32_2=2;
    [<TestMethod>]
    member this.Add() =
        let Int32_1=1
        let Int32_2=2
        this.AssertExecute(<@ Int32_1+Int32_2@>)
    [<TestMethod>]
    member this.Constant() =
        this.AssertExecute(<@ "S"+ string 1m @>)
    [<TestMethod>]
    member this.クエリ0() =
        this.AssertExecute(<@ Set<int32>([|3; 4; 5; 6; 7; 8; 9; 10|]).Select(fun p->p+1) @>)
    [<TestMethod>]
    member this.クエリ1() =
        this.AssertExecute(<@ [|3; 4; 5; 6; 7; 8; 9; 10|] |> Seq.map (fun p->p+1) @>)
    [<TestMethod>]
    member this.クエリ2() =
        this.AssertExecute(<@ [|3; 4; 5; 6; 7; 8; 9; 10|].Select(fun p->p+1) @>)
[<Struct>]
type Struct1 =
    val a : int
    new(a) = {a = a}

type Struct2 =
    { a : int }