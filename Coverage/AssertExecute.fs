namespace CoverageFS
open Microsoft.VisualStudio.TestTools.UnitTesting
[<TestClass>]
type AssertExecute() =
    inherit CoverageCS.Lite.ATest() 
    [<TestMethod>]
    member this.Block0()= 
        this.AssertExecute(
            <@ 
                let a=1 
                a 
            @>
        )
    [<TestMethod>]
    member this.Block1()= 
        this.AssertExecute(
            <@ 
                let a=1
                let b=2
                a+b
            @>
        )
    [<TestMethod>]
    member this.Block2()= 
        this.AssertExecute(
            <@ 
                let a=1
                let b=2
                let c=3
                a+b+c
            @>
        )
