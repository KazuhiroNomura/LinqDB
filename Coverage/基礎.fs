namespace CoverageFS
module Module1=begin
    open Microsoft.VisualStudio.TestTools.UnitTesting
    open Microsoft.FSharp.Quotations
    open System
    open System.Collections.Generic;
    open Microsoft.FSharp.Linq.RuntimeHelpers
    [<TestClass>]
    type 基礎() =
        inherit CoverageCS.Lite.ATest() 
        //member this.Execute fs_lambda =
        //    let Optimizer=Optimizers.Optimizer();
        //    let cs_lambda=Expression.Lambda (LeafExpressionConverter.QuotationToExpression fs_lambda);
        //    Optimizer.AssertExecute cs_lambda;
        //    ();
                
        [<TestMethod>]
        member this.printfn()=printfn "test"
        [<TestMethod>]
        member this.ループ0()= 
            this.AssertExecute(
                <@ 
                    let r = new List<Int32>()
                    for i = 1 to 10 do
                        r.Add i
                    r
                @>
            )
            ()
        [<TestMethod>]
        member this.ループ1()= 
            this.AssertExecute(
                <@ 
                    let r = ResizeArray()
                    for i = 1 to 10 do
                        r.Add i
                    r
                @>
            )
            ()
        [<TestMethod>]
        member this.ループ2()= 
            this.AssertExecute(
                <@ 
                    seq {
                        for i = 1 to 10 do
                        yield i
                    }
                @>
            )
            ()
        [<TestMethod>]
        member this.ループ3()= 
            this.AssertExecute(
                <@ 
                    seq {
                        for i in 1 .. 10 do
                        yield i
                    }
                @>
            )
            ()
        [<TestMethod>]
        member this.ループ4()= 
            this.AssertExecute(
                <@ 
                    seq {
                        for i in 1 .. 10->i
                    }
                @>
            )
            ()
        [<TestMethod>]
        member this.ループ5()= 
            this.AssertExecute(
                <@ 
                    seq {
                        1..10
                    }
                @>
            )
            ()
        [<TestMethod>]
        member this.ループ6()= 
            this.AssertExecute(
                <@ 
                    [| for i = 1 to 10 do yield i |]
                @>
            )
            ()
        [<TestMethod>]
        member this.ループ7()= 
            this.AssertExecute(
                <@ 
                    [| for i in 1 .. 10 do yield i |]
                @>
            )
            ()
        [<TestMethod>]
        member this.ループ8()= 
            this.AssertExecute(
                <@ 
                    [| for i in 1 .. 10 -> i |]
                @>
            )
            ()
        [<TestMethod>]
        member this.ループ9()= 
            this.AssertExecute(
                <@ 
                    [| 1 .. 10 |]
                @>
            )
            ()
        [<TestMethod>]
        member this.Expr_Value()=
            let 引数=Expr.Value(13) 
            ()
        [<TestMethod>]
        member this.Expr() =
            let 引数 = Expr.Value(13)
            let Expr1= <@ (fun x -> x * %%引数) @>
            ()
        [<TestMethod>]
        member this.ExprからExpression() =
            let 引数 = Expr.Value(13)
            let Expr1= <@ (fun x -> x * %%引数) @>
            let Expression0=LeafExpressionConverter.QuotationToExpression(Expr1)
            ()
        [<TestMethod>]
        member this.型無しExpr()= 
            let 型無しExpr:Expr = <@@ 1+2 @@>
            ()
        [<TestMethod>]
        member this.型付きExpr()= 
            let 型付き引数:Expr<int32> = <@ 1+2 @>
            this.AssertExecute(型付き引数)
        [<TestMethod>]
        member this.Add()= 
            let 引数0 = <@ 3 @>
            let 引数1 = <@ 5 @>
            this.AssertExecute(<@ (fun x -> %引数0+ %引数1)() @>)
        [<TestMethod>]
        member this.And()= 
            let 引数0 = <@ 3 @>
            let 引数1 = <@ 5 @>
            this.AssertExecute(<@ (fun x -> %引数0&&& %引数1)() @>)
        //[<TestMethod>]
        //member this.AndAssign()= 
        //    let 引数0 = <@ 3 @>
        //    let 引数1 = <@ 5 @>
        //    this.AssertExecute(<@ (fun x -> x&&& %引数0) %引数1 @>)
        [<TestMethod>]
        member this.ArrayLength()= 
            this.AssertExecute(<@ (fun x -> [| 1 .. 10 |].Length)() @>)
        [<TestMethod>]
        member this.Let()= 
            let 引数 = <@ 13 @>
            this.AssertExecute(<@ (fun x -> 
                let y=x
                y*y
            ) %引数 @>)
        [<TestMethod>]
        member this.Block()= 
            let 引数 = <@ 13 @>
            this.AssertExecute(<@ (fun x -> 
                基礎.Callされるメソッド x,
                基礎.Callされるメソッド x
            ) %引数 
            @>)
        static member Callされるメソッド(x)=x 
        [<TestMethod>]
        member this.Call()= 
            let 引数 = <@ 13 @>
            this.AssertExecute(<@ (fun x -> 基礎.Callされるメソッド x) %引数 @>)
        [<TestMethod>]
        member this.Conditional()= 
            let 引数 = <@ 13 @>
            this.AssertExecute(<@ (fun x -> if x>=0 then 1 else -1) %引数 @>)
        [<TestMethod>]
        member this.Constant()= 
            this.AssertExecute(<@ (fun x -> x) 1 @>)
        [<TestMethod>]
        member this.Convert()= 
            let 引数 = <@ 13 @>
            this.AssertExecute(<@ (fun x -> double x) %引数 @>)
        [<TestMethod>]
        member this.Divide()= 
            let 引数 = <@ 13 @>
            this.AssertExecute(<@ (fun x -> x/2) %引数 @>)
        [<TestMethod>]
        member this.DivideAssign()= 
            let 引数 = <@ 13 @>
            this.AssertExecute(<@ (fun x ->
                let y=x/2
                y
            ) %引数 @>)
        [<TestMethod>]
        member this.Equal()= 
            let 引数 = <@ 13 @>
            this.AssertExecute(<@ (fun x -> x=0) %引数 @>)
        [<TestMethod>]
        member this.ExclusiveOr()= 
            let 引数 = <@ 13 @>
            this.AssertExecute(<@ (fun x -> x^^^0) %引数 @>)
        [<TestMethod>]
        member this.ExclusiveOrAssign()= 
            let 引数 = <@ 13 @>
            this.AssertExecute(<@ (fun x ->
                let y=x^^^0
                y
            ) %引数 @>)
        [<TestMethod>]
        member this.LessThan()= 
            let 引数 = <@ 13 @>
            this.AssertExecute(<@ (fun x -> x<0) %引数 @>)
        [<TestMethod>]
        member this.LessThanOrEqual()= 
            let 引数 = <@ 13 @>
            this.AssertExecute(<@ (fun x -> x<=0) %引数 @>)
        [<TestMethod>]
        member this.GreaterThan()= 
            let 引数 = <@ 13 @>
            this.AssertExecute(<@ (fun x -> x>0) %引数 @>)
        [<TestMethod>]
        member this.GreaterThanOrEqual()= 
            let 引数 = <@ 13 @>
            this.AssertExecute(<@ (fun x -> x>=0) %引数 @>)
        [<TestMethod>]
        member this.Index()= 
            let 引数 = <@ 1 @>
            this.AssertExecute(<@ (fun x -> [| 1 .. 10 |].[x]) %引数 @>)
        [<TestMethod>]
        member this.Invoke()= 
            let 引数 = <@ 13 @>
            this.AssertExecute(<@ (fun x -> x * %引数)3 @>)
        [<TestMethod>]
        member this.IsFalse()= 
            let 引数 = <@ new CoverageCS.Lite.ATest.class_演算子オーバーロード() @>
            this.AssertExecute(<@ (fun x -> %引数 &&& %引数)() @>)
        [<TestMethod>]
        member this.IsTrue()= 
            let 引数 = <@ new CoverageCS.Lite.ATest.class_演算子オーバーロード() @>
            this.AssertExecute(<@ (fun x -> %引数 ||| %引数)() @>)
        [<TestMethod>]
        member this.MemberInit()= 
            let 引数0 = <@ 3 @>
            let 引数1 = <@ 5 @>
            this.AssertExecute(<@ (fun x -> new CoverageCS.Lite.ATest.class_演算子オーバーロード(Int32フィールド=3))() @>)
        [<TestMethod>]
        member this.Modulo()= 
            let 引数0 = <@ 3 @>
            let 引数1 = <@ 5 @>
            this.AssertExecute(<@ (fun x -> %引数0% %引数1)() @>)
        [<TestMethod>]
        member this.Multiply()= 
            let 引数0 = <@ 3 @>
            let 引数1 = <@ 5 @>
            this.AssertExecute(<@ (fun x -> %引数0* %引数1)() @>)
        [<TestMethod>]
        member this.Negate()= 
            let 引数0 = <@ 3 @>
            this.AssertExecute(<@ (fun x -> - %引数0)() @>)
        [<TestMethod>]
        member this.New()= 
            this.AssertExecute(<@ (fun x -> new CoverageCS.Lite.ATest.class_演算子オーバーロード())() @>)
        [<TestMethod>]
        member this.Not()= 
            let 引数0 = <@ true @>
            this.AssertExecute(<@ (fun x -> not %引数0)() @>)
        [<TestMethod>]
        member this.NotEqual()= 
            let 引数0 = <@ 3 @>
            let 引数1 = <@ 5 @>
            this.AssertExecute(<@ (fun x -> %引数0<> %引数1)()  @>)
        //[<TestMethod>]
        //member this.OnesComplement()= 
        //    let 引数0 = <@ 3 @>
        //    let 引数1 = <@ 5 @>
        //    this.AssertExecute(<@ (fun x -> ~ %引数0)()  @>)
        [<TestMethod>]
        member this.Subtract()= 
            let 引数0 = <@ 3 @>
            let 引数1 = <@ 5 @>
            this.AssertExecute(<@ (fun x -> %引数0- %引数1)() @>)
        [<TestMethod>]
        member this.Or()= 
            let 引数0 = <@ 3 @>
            let 引数1 = <@ 5 @>
            this.AssertExecute(<@ (fun x -> %引数0||| %引数1)() @>)
end
