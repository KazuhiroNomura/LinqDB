namespace CoverageFS
open Microsoft.VisualStudio.TestTools.UnitTesting
open Microsoft.FSharp.Quotations
open System
open System.Collections.Generic
open System.Linq
open Lite.Sets
[<TestClass>]
type Enumerable_Set1() =
    inherit CoverageCS.Lite.ATest() 
    [<TestMethod>]
    member this.型無しExpr()= 
        let 型無しExpr:Expr = 
            <@@ 1+2 @@>
        ()
    [<TestMethod>]
    member this.Aggregate()= 
        this.AssertExecute(<@ Set<Int32>([|3; 4; 5; 6; 7; 8; 9; 10|]).Aggregate(fun a b-> a + b) @>)|>ignore
        this.AssertExecute(<@ [|3; 4; 5; 6; 7; 8; 9; 10|].Aggregate(fun a b-> a + b) @>)|>ignore
    [<TestMethod>]
    member this.Aggregate_seed_func()= 
        this.AssertExecute(<@ Set<Int32>([|3; 4; 5; 6; 7; 8; 9; 10|]).Aggregate(1,fun a b-> a + b) @>)|>ignore
        this.AssertExecute(<@ [|3; 4; 5; 6; 7; 8; 9; 10|].Aggregate(1,fun a b-> a + b) @>)
    [<TestMethod>]
    member this.Aggregate_seed_func_resultSelector()= 
        this.AssertExecute(<@ Set<Int32>([|3; 4; 5; 6; 7; 8; 9; 10|]).Aggregate(1,(fun a b-> a + b),(fun accumulate -> accumulate * 2)) @>)|>ignore
        this.AssertExecute(<@ [|3; 4; 5; 6; 7; 8; 9; 10|].Aggregate(1,(fun a b-> a + b),(fun accumulate -> accumulate * 2)) @>)
    [<TestMethod>]
    member this.All()= 
        this.AssertExecute(<@ Set<Int32>([|3; 4; 5; 6; 7; 8; 9; 10|]).All(fun p->p=0) @>)|>ignore
        this.AssertExecute(<@ [|3; 4; 5; 6; 7; 8; 9; 10|].All(fun p->p=0) @>)
    [<TestMethod>]
    member this.AsEnumerable()= 
        this.AssertExecute(<@ Set<Int32>([|3; 4; 5; 6; 7; 8; 9; 10|]).AsEnumerable() @>)|>ignore
        this.AssertExecute(<@ [|3; 4; 5; 6; 7; 8; 9; 10|].AsEnumerable() @>)
    [<TestMethod>]
    member this.AvedevDouble_selector()= 
        this.AssertExecute(<@ Set<Double>([|3.0; 4.0; 5.0|]).Avedev(fun p->p+1.0) @>)
    [<TestMethod>]
    member this.SelectMany0()= 
        let x=seq {for i in 1 .. 3 -> i}
        x
    [<TestMethod>]
    member this.SelectMany1()= 
        let x=[|3; 4; 5|]
        x
    [<TestMethod>]
    member this.SelectMany2()= 
        let x=[3; 4; 5]
        x
    [<TestMethod>]
    member this.SelectMany_collectionSelector()= 
        this.AssertExecute(
            <@ 
                Set<Int32>([|3; 4; 5|]).SelectMany(
                    (fun source->Set<Int32>([|3; 4; 5|]):>ASet<Int32>),
                    (fun source collection->source+collection)
                ) 
            @>
        )
        let items2=[|3; 4; 5|].AsEnumerable();
        this.AssertExecute(
            <@ 
                items2.SelectMany(
                    (fun source->items2),
                    (fun source collection->source+collection)
                ) 
            @>
        )
    [<TestMethod>]
    member this.Cast()= 
        this.AssertExecute(<@ Set<Int32>([|3; 4; 5|]).Cast<Object>() @>)
        this.AssertExecute(<@ [|3; 4; 5|].Cast<Object>() @>)
//        [<TestMethod>]
//      member this.Dictionary_Equal()= 
//        this.AssertExecute(<@ Lambda(fun a->Set<Int32>([|3; 4; 5|])) @>)
    [<TestMethod>]
    member this.Distinct()= 
        this.AssertExecute(<@ [|3; 4; 5|].Distinct() @>)
    [<TestMethod>]
    member this.Except()= 
        this.AssertExecute(<@ Set<Int32>([|3; 4; 5|]).Except(Set<Int32>([|3; 4; 5|])) @>)
        this.AssertExecute(<@ [|3; 4; 5|].Except([|3; 4; 5|]) @>)
    [<TestMethod>]
    member this.Except_Any()= 
        let Arr=[|3; 4; 5|]
        let Set=Set<Int32>(Arr)
        this.AssertExecute(<@ Set.Except(Set).Any() @>)|>ignore
        this.AssertExecute(<@ Arr.Except(Set).Any() @>)
    [<TestMethod>]
    member this.GroupBy()= 
        let Arr=[|3; 4; 5|]
        let Set=Set<Int32>(Arr)
        this.AssertExecute(<@ Set.GroupBy(fun p->p.ToString()+"Key") @>)|>ignore
        this.AssertExecute(<@ Arr.GroupBy(fun p->p.ToString()+"Key") @>)|>ignore
        this.AssertExecute(<@ Arr.GroupBy(fun p->p.ToString()+"Key",EqualityComparer<String>.Default) @>)|>ignore
        this.AssertExecute(<@ Set.GroupBy(fun p->p.ToString()+"Key",fun p->p.ToString()+"Element") @>)|>ignore
        this.AssertExecute(<@ Arr.GroupBy(fun p->p.ToString()+"Key",fun p->p.ToString()+"Element") @>)|>ignore
        this.AssertExecute(<@ Arr.GroupBy(fun p->p.ToString()+"Key",fun p->p.ToString()+"Element",EqualityComparer<String>.Default) @>)|>ignore
        this.AssertExecute(<@ Arr.GroupBy(fun p->p.ToString()+"Key",fun v e->(v,e)) @>)|>ignore
        this.AssertExecute(<@ Arr.GroupBy(fun p->p.ToString()+"Key",fun v e->(v,e),EqualityComparer<String>.Default) @>)|>ignore
        this.AssertExecute(<@ Arr.GroupBy(fun p->p.ToString()+"Key",fun p->p.ToString()+"Element",fun v e->(v,e)) @>)|>ignore
        this.AssertExecute(<@ Arr.GroupBy(fun p->p.ToString()+"Key",fun p->p.ToString()+"Element",fun v e->(v,e),EqualityComparer<String>.Default) @>)
    [<TestMethod>]
    member this.Intersect()= 
        let Arr=[|3; 4; 5|]
        let Set=Set<Int32>(Arr)
        this.AssertExecute(<@ Arr.Select(fun p->p+1).Intersect(Arr.Select(fun p->p+2)) @>)|>ignore
        this.AssertExecute(<@ Arr.Select(fun p->p+1).Intersect(Arr) @>)|>ignore
        this.AssertExecute(<@ Arr.Intersect(Arr.Select(fun p->p+2)) @>)|>ignore
        this.AssertExecute(<@ Arr.Intersect(Arr) @>)|>ignore
        this.AssertExecute(<@ Set.Select(fun p->p+1).Intersect(Set.Select(fun p->p+2)) @>)|>ignore
        this.AssertExecute(<@ Set.Select(fun p->p+1).Intersect(Set) @>)|>ignore
        this.AssertExecute(<@ Set.Intersect(Set.Select(fun p->p+2)) @>)|>ignore
        this.AssertExecute(<@ Set.Intersect(Set) @>)
    [<TestMethod>]
    member this.Intersect_Any()= 
        let Arr=[|3; 4; 5|]
        let Set=Set<Int32>(Arr)
        this.AssertExecute(<@ Arr.Select(fun p->p+1).Intersect(Arr).Any() @>)|>ignore
        this.AssertExecute(<@ Set.Select(fun p->p+1).Intersect(Set).Any() @>)|>ignore
        this.AssertExecute(<@ Set.Intersect(Set).Any() @>)|>ignore
        this.AssertExecute(<@ Set.Intersect(Set.Select(fun p->p+2)).Any() @>)|>ignore
        this.AssertExecute(<@ Arr.Select(fun p->p+1).Intersect(Arr.Select(fun p->p+2)).Any() @>)|>ignore
        this.AssertExecute(<@ Arr.Intersect(Arr.Select(fun p->p+2)).Any() @>)|>ignore
        this.AssertExecute(<@ Arr.Intersect(Arr).Any() @>)|>ignore
        this.AssertExecute(<@ Set.Select(fun p->p+1).Intersect(Set.Select(fun p->p+2)).Any() @>)|>ignore
    [<TestMethod>]
    member this.Join()= 
        let Arr=[|3; 4; 5|]
        let Set=Set<Int32>(Arr)
        this.AssertExecute(<@ Arr.Join(Arr,(fun o->o),(fun i->i),(fun o i->o+i)) @>)
        this.AssertExecute(<@ Arr.Join(Arr,(fun o->o),(fun i->i),(fun o i->o+i),EqualityComparer<Int32>.Default) @>)
        this.AssertExecute(<@ Set.Join(Set,(fun o->o),(fun i->i),(fun o i->o+i)) @>)
    [<TestMethod>]
    member this.Join_Any()= 
        let Arr=[|3; 4; 5|]
        let Set=Set<Int32>(Arr)
        this.AssertExecute(<@ Arr.Join(Arr,(fun o->o),(fun i->i),(fun o i->o+i)).Any() @>)
        this.AssertExecute(<@ Arr.Join(Arr,(fun o->o),(fun i->i),(fun o i->o+i),EqualityComparer<Int32>.Default).Any() @>)
        this.AssertExecute(<@ Set.Join(Set,(fun o->o),(fun i->i),(fun o i->o+i)).Any() @>)
    [<TestMethod>]
    member this.Range()= 
        this.AssertExecute(<@ Enumerable.Range(5,10) @>)
    [<TestMethod>]
    member this.Repeat()= 
        this.AssertExecute(<@ Enumerable.Repeat(3,10) @>)
    [<TestMethod>]
    member this.Select_selector()= 
        let Arr=[|3; 4; 5|]
        let Set=Set<Int32>(Arr)
        this.AssertExecute(<@ Arr.Select(fun p->p+1) @>)
        this.AssertExecute(<@ Set.Select(fun p->p+1) @>)
    [<TestMethod>]
    member this.Select_indexSelector()= 
        let Arr=[|3; 4; 5|]
        this.AssertExecute(<@ Arr.Select(fun p i->p+1) @>)
    //[<TestMethod>]
    //member this.SelectMany_selector()= 
    //    let Arr=[|3; 4; 5|]
    //    let Set=Set<Int32>(Arr)
    //    this.AssertExecute(<@ Arr.SelectMany((fun i->Arr)) @>)
    //    this.AssertExecute(<@ Arr.SelectMany((fun i->Arr):?>Func<Int32,Int32[]>) @>)
    //    this.AssertExecute(<@ Set.SelectMany(fun i->Set) @>)
    //    this.AssertExecute(<@ Set.SelectMany((fun i->Set):?>Func<Int32,Int32[]>) @>)
    [<TestMethod>]
    member this.SequenceEqual()= 
        let Arr=[|3; 4; 5|]
        this.AssertExecute(<@ Arr.SequenceEqual(Arr) @>)
        this.AssertExecute(<@ Arr.SequenceEqual(Arr,EqualityComparer<Int32>.Default) @>)
    [<TestMethod>]
    member this.Set_Union()= 
        let Set=Set<Int32>([|3; 4; 5|])
        this.AssertExecute(<@ Set.Union(Set) @>)
    [<TestMethod>]
    member this.Single()= 
        let Arr=[|5|]
        let Set=Set<Int32>(Arr)
        this.AssertExecute(<@ Arr.Single() @>)
        this.AssertExecute(<@ Set.Single() @>)
    [<TestMethod>]
    member this.SingleOrDefault()= 
        let Arr=[|5|]
        let Set=Set<Int32>(Arr)
        this.AssertExecute(<@ Arr.SingleOrDefault() @>)
        this.AssertExecute(<@ Set.SingleOrDefault() @>)
    [<TestMethod>]
    member this.Enumerable_Union0()= 
        let Arr=[|5|]
        this.AssertExecute(<@ Arr.Union(Arr) @>)
    [<TestMethod>]
    member this.Enumerable_DUnion()= 
        let Set=Set<Int32>([||]:Int32[])
        this.AssertExecute(<@ Set.DUnion(Set) @>)
    //[<TestMethod>]
    //member this.AverageNullable()= 
    //    let Set=Set<Int32>([||]:Int32[])
    //    this.AssertExecute(<@ Set<Decimal?>([|1,2,3|]).Average() @>)
    //    this.AssertExecute(<@ Set<Double?>([|1,2,3|]).Average() @>)
//            this.AssertExecute(<@ ([|1m,2m,3m|]:?>Decimal?[]).Average() @>)
//          this.AssertExecute(<@ [|1m,2,3|]:Double?[].Average() @>)
    [<TestMethod>]
    member this.Average()= 
        this.AssertExecute(<@ Set<Double>([|1.0;2.0|]).Average() @>)
        this.AssertExecute(<@ Set<Single>([|1.0f;2.0f|]).Average() @>)
        this.AssertExecute(<@ Set<Int64>([|1L;2L|]).Average() @>)
        this.AssertExecute(<@ Set<Int32>([|1;2|]).Average() @>)
        this.AssertExecute(<@ [|1m;2m|].Average() @>)
        this.AssertExecute(<@ [|1.0;2.0|].Average() @>)
        this.AssertExecute(<@ Set<Int32>([|1;2|]).Where((fun p->p%2=0)).Average() @>)
        this.AssertExecute(<@ Set<Int32>([|1;2|]).Select((fun p->p+1)).Average() @>)
        this.AssertExecute(<@ [|1.0;2.0|].Let((fun p->p.Average())) @>)
        this.AssertExecute(<@ Set<Int32>([|1;2|]).Let((fun p->p.Average())) @>)
    [<TestMethod>]
    member this.Average_selector()= 
        this.AssertExecute(<@ Set<Double>([|1.0;2.0|]).Average((fun p->p+1.0)) @>)
        this.AssertExecute(<@ [|1m;2m|].Average((fun p->p+ 1m)) @>)
        this.AssertExecute(<@ [|1.0;2.0|].Average((fun p->p+1.0)) @>)
        this.AssertExecute(<@ [|1.0f;2.0f|].Average((fun p->p+1.0f)) @>)
        this.AssertExecute(<@ [|1L;2L|].Average((fun p->p+1L)) @>)
        this.AssertExecute(<@ [|1;2|].Average((fun p->p+1)) @>)
        this.AssertExecute(<@ Set<Int32>([|1;2|]).Where((fun p->p%2=0)).Average((fun p->p+1)) @>)
        this.AssertExecute(<@ Set<Int32>([|1;2|]).Select((fun p->p+1)).Average((fun p->p+1)) @>)
    [<TestMethod>]
    member this.GeomeanDouble_selector()= 
        let S=Set<Double>([|1.0;2.0|])
        this.AssertExecute(<@ S.Geomean((fun p->p*p)) @>)
        this.AssertExecute(<@ S.Where((fun p->p<>0.0)).Geomean((fun p->p*p)) @>)
        this.AssertExecute(<@ S.Select((fun p->p+1.0)).Geomean((fun p->p*p)) @>)
        this.AssertExecute(<@ S.Let((fun q->q.Geomean((fun p->p*p)))) @>)
        this.AssertExecute(<@ S.Let((fun q->q)).Geomean((fun p->p*p)) @>)

