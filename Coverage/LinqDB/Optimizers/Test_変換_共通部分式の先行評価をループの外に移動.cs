using System.Linq.Expressions;
using LinqDB.Sets;
using static LinqDB.Sets.ExtensionSet;
using Microsoft.VisualStudio.TestTools.UnitTesting;
// ReSharper disable ArrangeRedundantParentheses
// ReSharper disable PossibleNullReferenceException
// ReSharper disable RedundantCast
namespace CoverageCS.LinqDB.Optimizers;

[TestClass]
public class Test_変換_共通部分式の先行評価をループの外に移動 : ATest
{
    [TestMethod]
    public void Lambda共通後処理()
    {
        var A = 0;
        var b = 1;
        var c = 2;
        //if(今回ラムダ間Assigns_Count==0&&今回ループ内Assigns_Count==0) {
        this.Execute2(() => 0);
        //} else {
        //    for(var a = 今回ラムダ間Assigns_Count-1;a>=0;a--) {
        //        for(var b = a-1;b>=0;b--) {
        //            if(変数.Equals(今回ラムダ間Assign1.Right,今回ラムダ間Assign2.Right)) {
        this.Execute2(() => Lambda(Default => new
        {
            A = b,
            A2 = b,
            Default
        }));
        //            }
        this.Execute2(() => A.Let(a => new
        {
            A = b,
            C = c,
            a
        }));
        //        }
        this.Execute2(() => A.Let(a => c));
        //    }
        this.Execute2(() => A.Let(a => Lambda(d => a)));
        //    for(var a = 今回ループ内Assigns_Count-1;a>=0;a--) {
        //        for(var b = a-1;b>=0;b--) {
        //            if(変数.Equals(今回ループ内Assign1.Right,今回ループ内Assign2.Right)) {
        this.Execute引数パターン(a => ArrN<int>(a).Select(b=> ArrN<int>(a).Select(c=>a+b+c)));
        //                break;
        //            }
        //        }
        this.Execute引数パターン(a => ArrN<int>(a).Select(p => _StaticString));
        //    }
    }
    [TestMethod]
    public void Lambda()
    {
        var A = 0;
        var B = 1;
        var c = 1;
        //for(var a = 今回ラムダ間Assigns.Count-1;a>=0;a--) {
        //    if(変数_判定_指定Parametersが存在しない.実行(今回ラムダ間Assign,Lambda0_Parameters)) {
        this.Execute2(() => Lambda(p => Lambda(r => c)));
        //    }else{
        this.Execute2(() => A.Let(a => B.Let(b => a)));
        //    }
        //}
        //for(var a = 今回ループ内Assigns.Count-1;a>=0;a--) {
        //    if(変数_判定_指定Parametersが存在しない.実行(今回ループ内Assign,Lambda0_Parameters)) {
        this.Execute2(() => ArrN<int>(10).Let(b => b.Average(p => p * 2m)));
        //    } else {
        this.Execute引数パターン(a => Lambda(r => ArrN<int>(a).Select(q => q + r)));
        //    }
        //}
    }
    [TestMethod]
    public void Lambda1()
    {
        var d = System.Array.Empty<int>();
        this.Execute2(() =>
            d.SelectMany((Func<int, IEnumerable<int>>)(a =>
                    d.SelectMany((Func<int, IEnumerable<int>>)(b =>
                            d.Select(c => a * a)
                        ))
                ))
        );
    }
    [TestMethod]
    public void Call()
    {
        //if(this._ループ展開するか&&ExtendedSet.ループ展開可能なEnumerableSetに属するGenericMethodDefinitionか(MethodCall0_Method)) {
        //    if(Reflection.ExtendSet1.Aggregate_seed_func_resultSelector.MethodEquals(MethodCall0_Method)||Reflection.ExtendEnumerable.Aggregate_seed_func_resultSelector.MethodEquals(MethodCall0_Method)) {
        //        resultSelector==null
        this.Execute引数パターン(a => SetN<int>(a).Aggregate(0, (x, y) => x + y, (Func<int, int>)(z => z * 2)));
        this.Execute引数パターン(a => ArrN<int>(a).Aggregate(0, (x, y) => x + y, (Func<int, int>)(z => z * 2)));
        //        resultSelector!=null
        this.Execute引数パターン(a => SetN<int>(a).Aggregate(0, (x, y) => x + y, z => z * 2));
        this.Execute引数パターン(a => ArrN<int>(a).Aggregate(0, (x, y) => x + y, z => z * 2));
        //    }else{
        this.Execute引数パターン(a => ArrN<int>(a).Select(z => z * 2));
        //    }
        //    if(ExpressionsEquals(MethodCall0_Arguments,MethodCall1_Arguments)) return MethodCall0;
        // ReSharper disable once ConvertToLocalFunction
        Func<int, int> selector = x => x * 3;
        this.Execute引数パターン(a => ArrN<int>(a).Select(selector));
        this.Execute引数パターン(a => ArrN<int>(a).Select(x => x * 3));
        //}else{
        //    for(var x=0;x<MethodCall0_Arguments_Count;x++) {
        this.Execute引数パターン(a => ArrN<int>(a).Select(p => $"A{p}B"));
        //    }
        //    if(MethodCall0_Object==MethodCall1_Object&&ExpressionsEquals(MethodCall0_Arguments,MethodCall1_Arguments)) return MethodCall0;
        this.Execute引数パターン標準ラムダループ((a, b) => ArrN<int>(a).Select(c => ArrN<int>(b).Aggregate(0, (x, y) => x + y, z => z * 2)));
        this.Execute引数パターン(a => ArrN<int>(a).Aggregate(0, (x, y) => x + y, z => z * 2).ToString());
        //}


    }
    [TestMethod]
    public void インラインループ()
    {
        var B = 1;
        //if(Lambda==null)return this.Traverse(e);
        this.Execute引数パターン(a => ArrN<int>(a).Select(x => x * 2));
        this.Execute引数パターン(a => ArrN<int>(a).Select(x => x * 2));
        //for(var _Field = 今回ラムダ間Assigns.Count-1;_Field>=0;_Field--) {
        //    if(変数_判定_指定Parametersが存在しない.実行(今回ラムダ間Assign,Lambda_Parameters)) {
        this.Execute引数パターン(a => ArrN<int>(a).Select(x => B.Let(y => Lambda(z => Lambda(d => z)))));
        //    }else{
        this.Execute引数パターン(a => ArrN<int>(a).Select(x => B.Let(y => Lambda(z => y + x))));
        //    }
        //}
        //for(var _Field = 今回ループ内Assigns.Count-1;_Field>=0;_Field--) {
        //    if(変数_判定_指定Parametersが存在しない.実行(今回ループ内Assign,Lambda_Parameters)) {
        this.Execute引数パターン(a => ArrN<int>(a).Select(x => ArrN<int>(a).Select(y => ArrN<int>(a).Select(z => y))));
        //    }else{
        this.Execute引数パターン(a => ArrN<int>(a).Select(x => ArrN<int>(a).Select(y => ArrN<int>(a).Select(z => y + x))));
        //    }
        //}
    }
    [TestMethod]
    public void 先行評価式からBlock作成の複数ループラムダの先行評価式()
    {
        var A = 2;
        var B = 3;
        this.Execute2(() =>
            A.Let(a =>
                Inline(() => a)
                +
                B.Let(b => a)
            )
        );
        this.Execute2(() =>
            A.Let(a =>
                B.Let(b => a)
                +
                Inline(() => a)
            )
        );
        this.Execute2(() =>
            A.Let(a =>
                Inline(() => a + a)
                +
                B.Let(b => a + a)
            )
        );
        this.Execute2(() =>
            A.Let(a =>
                B.Let(b => a + a)
                +
                Inline(() => a + a)
            )
        );
        this.Execute2(() =>
            A.Let(a =>
                Inline(() => (a + a) - (a + a))
                +
                B.Let(b => (a + a) - (b + b))
            )
        );
        this.Execute2(() =>
            A.Let(a =>
                B.Let(b => (a + a) - (b + b))
                +
                Inline(() => (a + a) - (a + a))
            )
        );
        this.Execute2(() =>
            A.Let(a =>
                Inline(() => a - a)
                +
                B.Let(b => (a + a) - (b + b))
            )
        );
        this.Execute2(() =>
            A.Let(a =>
                B.Let(b => (a + a) - (b + b))
                +
                Inline(() => a - a)
            )
        );
    }
    [TestMethod]
    public void 先行評価式からBlock作成の複数ループの先行評価式0()
    {
        var A = 0;
        var B = 1;
        this.Execute2(() =>
            A.Let(a =>
                Inline(() => a)
                +
                a
            )
        );
    }
    [TestMethod]
    public void 先行評価式からBlock作成の複数ループの先行評価式1()
    {
        var A = 0;
        var B = 1;
        this.Execute2(() =>
            A.Let(a =>
                Inline(() => a)
                +
                Inline(() => a)
            )
        );
    }
    [TestMethod]
    public void 先行評価式からBlock作成の複数ループの先行評価式2()
    {
        var A = 0;
        var B = 1;
        this.Execute2(() =>
            A.Let(a =>
                Inline(() => a + a)
                +
                Inline(() => a + a)
            )
        );
    }
    [TestMethod]
    public void 先行評価式からBlock作成の複数ループの先行評価式3()
    {
        var A = 0;
        var B = 1;
        this.Execute2(() =>
            A.Let(a =>
                Inline(() => (a + a) - (B + B))
                +
                Inline(() => (a + a) - (B + B))
            )
        );
    }
    [TestMethod]
    public void 先行評価式からBlock作成の複数ループの先行評価式()
    {
        var A = 0;
        var B = 1;
        //IL生成を書き換えたらAssign参照が間違っているらしく結果が一致しなかった。
        this.Execute2(() =>
            A.Let(a =>
                Inline(() => a - a)
                +
                Inline(() => (a + a) - (a + a))
            )
        );
    }
    [TestMethod]
    public void 先行評価式からBlock作成の複数ラムダの先行評価式()
    {
        var A = 0;
        var B = 1;
        this.Execute2(() =>
            B.Let(b => A)
            +
            B.Let(b => A + A)
        );
        this.Execute2(() =>
            A.Let(a =>
                B.Let(b => a)
                +
                B.Let(b => a)
            )
        );
        this.Execute2(() =>
            A.Let(a =>
                B.Let(b => a + a)
                +
                B.Let(b => a + a)
            )
        );
        this.Execute2(() =>
            A.Let(a =>
                B.Let(b => (a + a) - (b + b))
                +
                B.Let(b => (a + a) - (b + b))
            )
        );
        this.Execute2(() =>
            A.Let(a =>
                B.Let(b => a - b)
                +
                B.Let(b => (a + a) - (b + b))
            )
        );
    }
    [TestMethod]
    public void 先行評価式からBlock作成の単一ラムダの先行評価式()
    {
        var A = 0;
        var B = 1;
        this.Execute2(() =>
            A.Let(a =>
                B.Let(b => (a + a))
            )
        );
        this.Execute2(() =>
            A.Let(a =>
                B.Let(b => (a + a) - (b + b))
            )
        );
        this.Execute2(() =>
            A.Let(a =>
                B.Let(b => (a + a) * (a + a) - (b + b) / (b + b))
            )
        );
        this.Execute2(() =>
            A.Let(a =>
                Inline(() => a + a)
            )
        );
        this.Execute2(() =>
            Inline(() =>
                Inline(() => A + A)
                +
                Inline(() => A* A)
            )
        );
        this.Execute2(() =>
            A.Let(a =>
                Inline(() => a + a)
                +
                Inline(() => a * a)
            )
        );
    }
    [TestMethod]
    public void 先行評価式からBlock作成()
    {
        var A = 0;
        var B = 1;
        var C = 2;
        //foreach(var 共通部分式 in Listループで初期化したい共通部分式) {
        this.Execute引数パターン(a => Inline(() => C + Inline(() => a + C + B)));
        //}
        //foreach(var 共通部分式 in Listラムダで初期化したい共通部分式) {
        this.Execute2(() => A.Let(a => B.Let(b => a)));
        //}
        //for(var a=List初期化したい共通部分式_Count-1;a>=0;a--){
        //    if(上位List初期化したい共通部分式.Contains(共通部分式,変数_ExpressionEqualityComparer)){
        this.Execute2(() => Inline(() => C + Inline(() => C + B)));
        //    }
        this.Execute2(() => A.Let(a => B.Let(b => a)));
        //}
    }
    [TestMethod]
    public void 先行評価式からBlock作成1()
    {
        var A1 = 1;
        var B2 = 2;
        var C3 = 3;
        this.Execute2(() =>
            B2.Let(a =>
                Inline(() =>
                    C3 + B2
                ) + a.Let(d =>
                    C3 + B2 + d
                )
            )
        );
        //for(var a = 0;a<Listループで初期化したい共通部分式_Count;a++) {
        //    for(var b = a+1;b<Listループで初期化したい共通部分式_Count;b++) {
        this.Execute標準ラムダループ(() =>
            Inline(() =>
                C3 + Inline(() =>
                    C3 + B2
                )
            )
        );
        //    }
        this.Execute2(() => Inline(() => Inline(() => A1)));
        //    for(var b = 0;b<Listラムダで初期化したい共通部分式_Count;b++) {
        this.Execute2(() =>
            B2.Let(a =>
                Inline(() =>
                    a + C3 + B2
                ) + a.Let(d =>
                    d + C3 + B2
                )
            )
        );
        //    }
        this.Execute2(() => A1.Let(a => B2.Let(b => a)));
        //}
        this.Execute2(() => Inline(() => C3 + Inline(() => C3 + B2)));
        //for(var a = 0;a<Listラムダで初期化したい共通部分式_Count;a++) {
        //    for(var b = a+1;b<Listラムダで初期化したい共通部分式_Count;b++) {
        this.Execute2(() => A1.Let(a => new { A = B2, C = C3, a }));
        //    }
        this.Execute2(() => A1.Let(a => B2.Let(b => a)));
        //}
        this.Execute2(() => Inline(() => Inline(() => C3 + B2)));
        //if(Variables!=null){
        this.Execute2(() => Inline(() => Inline(() =>C3 + B2)));
        //} else{
        //    if(Listループで初期化したい共通部分式.Count>0){
        this.Execute2(() => A1.Let(a => B2.Let(b => a)));
        //    }
        this.Execute2(() => Inline(() => C3 + Inline(() => C3 + B2)));
        //}
    }
    [TestMethod]
    public void Assert1段1幅外出し()
    {
        var p = Expression.Parameter(typeof(int));
        var Call0 = Expression.Call(
            typeof(global::LinqDB.Sets.Helpers).GetMethod(nameof(global::LinqDB.Sets.Helpers.Let))!.MakeGenericMethod(typeof(int), typeof(decimal)),
            Expression.Constant(1),
            Expression.Lambda<Func<int, decimal>>(
                Expression.Constant(1m),
                p
            )
        );
        var Call1 = Expression.Call(
            typeof(global::LinqDB.Sets.Helpers).GetMethod(nameof(global::LinqDB.Sets.Helpers.Let))!.MakeGenericMethod(typeof(int), typeof(decimal)),
            Expression.Constant(1),
            Expression.Lambda<Func<int, decimal>>(
                Expression.Add(
                    Expression.Constant(1m),
                    Expression.Constant(1m)
                ),
                p
            )
        );
        //".Lambda LラムダR<Func`1[Decimal]>() {",
        //"    .Block() {",
        //"        $Cラムダ跨ぎ0 = 1M;",
        //"        .Call Helpers.Let(",
        //"            1,",
        //"            .Lambda Lラムダ0<Func`2[Int32,Decimal]>(Int32 $Pラムダ引数1) {",
        //"                $Cラムダ跨ぎ0",
        //"            })",
        //"    }",
        //"}"
        this.Execute2(
            Expression.Lambda<Func<decimal>>(
                Call0
            )
        );
        //".Lambda LラムダR<Func`1[Decimal]>() {",
        //"    .Block(Decimal $Cラムダ局所0) {",
        //"        .Block() {",
        //"            $Cラムダ跨ぎ0 = ($Cラムダ局所0 = 1M);",
        //"            $Cラムダ跨ぎ1 = $Cラムダ局所0 + $Cラムダ局所0;",
        //"            .Call Helpers.Let(",
        //"                1,",
        //"                .Lambda Lラムダ0<Func`2[Int32,Decimal]>(Int32 $Pラムダ引数1) {",
        //"                    $Cラムダ跨ぎ0",
        //"                }) + .Call Helpers.Let(",
        //"                1,",
        //"                .Lambda Lラムダ1<Func`2[Int32,Decimal]>(Int32 $Pラムダ引数2) {",
        //"                    $Cラムダ跨ぎ1",
        //"                })",
        //"        }",
        //"    }",
        //"}"
        this.Execute2(
            Expression.Lambda<Func<decimal>>(
                Expression.Add(
                    Call0,
                    Call1
                )
            )
        );
        //".Lambda LラムダR<Func`1[Decimal]>() {",
        //"    .Block(Decimal $Cラムダ局所0) {",
        //"        .Block() {",
        //"            $Cラムダ跨ぎ0 = ($Cラムダ局所0 = 1M) + $Cラムダ局所0;",
        //"            $Cラムダ跨ぎ1 = $Cラムダ局所0;",
        //"            .Call Helpers.Let(",
        //"                1,",
        //"                .Lambda Lラムダ0<Func`2[Int32,Decimal]>(Int32 $Pラムダ引数1) {",
        //"                    $Cラムダ跨ぎ0",
        //"                }) + .Call Helpers.Let(",
        //"                1,",
        //"                .Lambda Lラムダ1<Func`2[Int32,Decimal]>(Int32 $Pラムダ引数2) {",
        //"                    $Cラムダ跨ぎ1",
        //"                })",
        //"        }",
        //"    }",
        //"}"
        this.Execute2(
            Expression.Lambda<Func<decimal>>(
                Expression.Add(
                    Call1,
                    Call0
                )
            )
        );
        //".Lambda LラムダR<Func`1[Decimal]>() {",
        //"    .Block(",
        //"        Decimal $Cラムダ局所0,",
        //"        Decimal $Cラムダ局所1) {",
        //"        .Block() {",
        //"            $Cラムダ跨ぎ0 = ($Cラムダ局所0 = 1M) + $Cラムダ局所0;",
        //"            ($Cラムダ局所1 = .Call Helpers.Let(",
        //"                1,",
        //"                .Lambda Lラムダ1<Func`2[Int32,Decimal]>(Int32 $Pラムダ引数2) {",
        //"                    $Cラムダ跨ぎ0",
        //"                })) + $Cラムダ局所1",
        //"        }",
        //"    }",
        //"}"
        this.Execute2(
            Expression.Lambda<Func<decimal>>(
                Expression.Add(
                    Call1,
                    Call1
                )
            )
        );
    }
    [TestMethod]
    public void Assert2段1幅外出し()
    {
        var p0 = Expression.Parameter(typeof(int));
        var p1 = Expression.Parameter(typeof(int));
        //".Lambda LラムダR<Func`1[Decimal]>() {",
        //"    .Block() {",
        //"        $Cラムダ跨ぎ0 = 1M;",
        //"        $Cラムダ跨ぎ1 = .Call Helpers.Let(",
        //"            1,",
        //"            .Lambda Lラムダ1<Func`2[Int32,Decimal]>(Int32 $Pラムダ引数2) {",
        //"                $Cラムダ跨ぎ0",
        //"            });",
        //"        .Call Helpers.Let(",
        //"            1,",
        //"            .Lambda Lラムダ0<Func`2[Int32,Decimal]>(Int32 $Pラムダ引数1) {",
        //"                $Cラムダ跨ぎ1",
        //"            })",
        //"    }",
        //"}"
        this.Execute2(
            Expression.Lambda<Func<decimal>>(
                Expression.Call(
                    typeof(global::LinqDB.Sets.Helpers).GetMethod(nameof(global::LinqDB.Sets.Helpers.Let))!.MakeGenericMethod(typeof(int), typeof(decimal)),
                    Expression.Constant(1),
                    Expression.Lambda<Func<int, decimal>>(
                        Expression.Call(
                            typeof(global::LinqDB.Sets.Helpers).GetMethod(nameof(global::LinqDB.Sets.Helpers.Let))!.MakeGenericMethod(typeof(int), typeof(decimal)),
                            Expression.Constant(1),
                            Expression.Lambda<Func<int, decimal>>(
                                Expression.Constant(1m),
                                p1
                            )
                        ),
                        p0
                    )
                )
            )
        );
    }
    [TestMethod]
    public void Assert3段1幅外出し()
    {
        var p0 = Expression.Parameter(typeof(int));
        var p1 = Expression.Parameter(typeof(int));
        var p2 = Expression.Parameter(typeof(int));
        //".Lambda LラムダR<Func`1[Decimal]>() {",
        //"    .Block() {",
        //"        $Cラムダ跨ぎ0 = 1M;",
        //"        $Cラムダ跨ぎ1 = .Call Helpers.Let(",
        //"            1,",
        //"            .Lambda Lラムダ2<Func`2[Int32,Decimal]>(Int32 $Pラムダ引数3) {",
        //"                $Cラムダ跨ぎ0",
        //"            });",
        //"        $Cラムダ跨ぎ2 = .Call Helpers.Let(",
        //"            1,",
        //"            .Lambda Lラムダ1<Func`2[Int32,Decimal]>(Int32 $Pラムダ引数2) {",
        //"                $Cラムダ跨ぎ1",
        //"            });",
        //"        .Call Helpers.Let(",
        //"            1,",
        //"            .Lambda Lラムダ0<Func`2[Int32,Decimal]>(Int32 $Pラムダ引数1) {",
        //"                $Cラムダ跨ぎ2",
        //"            })",
        //"    }",
        //"}"
        this.Execute2(
            Expression.Lambda<Func<decimal>>(
                Expression.Call(
                    typeof(global::LinqDB.Sets.Helpers).GetMethod(nameof(global::LinqDB.Sets.Helpers.Let))!.MakeGenericMethod(typeof(int), typeof(decimal)),
                    Expression.Constant(1),
                    Expression.Lambda<Func<int, decimal>>(
                        Expression.Call(
                            typeof(global::LinqDB.Sets.Helpers).GetMethod(nameof(global::LinqDB.Sets.Helpers.Let))!.MakeGenericMethod(typeof(int), typeof(decimal)),
                            Expression.Constant(1),
                            Expression.Lambda<Func<int, decimal>>(
                                Expression.Call(
                                    typeof(global::LinqDB.Sets.Helpers).GetMethod(nameof(global::LinqDB.Sets.Helpers.Let))!.MakeGenericMethod(typeof(int), typeof(decimal)),
                                    Expression.Constant(1),
                                    Expression.Lambda<Func<int, decimal>>(
                                        Expression.Constant(1m),
                                        p2
                                    )
                                ),
                                p1
                            )
                        ),
                        p0
                    )
                )
            )
        );
    }
}