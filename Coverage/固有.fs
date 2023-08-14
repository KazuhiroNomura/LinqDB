namespace CoverageFS
open Microsoft.VisualStudio.TestTools.UnitTesting
open System
open System.Linq;
[<TestClass>]
type 固有() =
    inherit CoverageCS.Lite.ATest() 
    [<TestMethod>]
    member this.TestHoge() =
        printfn "test"
    [<TestMethod>]
    member this.TestList() =
        let list = [1;2;3]
        Assert.AreEqual([1;2;3], list)
    [<TestMethod>]
    member this.Tuple()=
        let b=query{
            for i in [1;2] do
            select (i.ToString(),i)
        }
        let Count=b.Count()
        Assert.AreEqual(2,Count)
    [<TestMethod>]
    member this.Select()=
        let b=query{
            for i in [1;2] do
            select i
        }
        let Count=b.Count()
        Assert.AreEqual(2,Count)
    [<TestMethod>]
    member this.Where()=
        let b=query{
            for i in [1;2] do
            where (i=2)
            select i
        }
        Assert.AreEqual(1,b.Count())
    member this.FSharpのExprを作る()=
        <@ fun ()-> 1 @>
    [<TestMethod>]
    member this.Linq呼び出し()=
        let data=[|1;2;3|]
        let ex=
                                <@ 
                                    query{
                                        for i in data do
                                        where (i=2)
                                        select i
                                    }
                                @>
        let b=this.AssertExecute(
                                <@ 
                                    query{
                                        for i in data do
                                        where (i=2)
                                        select i
                                    }
                                @>
        )
        Assert.AreEqual(1,ex)
        Assert.AreEqual(1,b)
        
    [<TestMethod>]
    member this.ArrayAccess() =
        let array=[| 1; 2; 3 |]
        let list = [1;2;3]
        let ex=
                                <@fun() -> 
                                    list.[0]
                                @>
        let b=this.AssertExecute(
                                <@fun() -> 
                                    list.[0]
                                @>
        )
        let array2=[ [ 1; 0]; [0; 1] ]
        let b=this.AssertExecute(
                                <@
                                    array2.[0] 
                                @>
        )
        Assert.AreEqual([1;2;3], list)
