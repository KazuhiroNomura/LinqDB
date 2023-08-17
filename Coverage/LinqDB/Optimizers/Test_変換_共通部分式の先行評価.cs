using System.Diagnostics;
using System.Linq.Expressions;
using LinqDB.Optimizers;
using LinqDB.Sets;
using Microsoft.VisualStudio.TestTools.UnitTesting;
// ReSharper disable ConditionalTernaryEqualBranch
namespace CoverageCS.LinqDB.Optimizers;

[TestClass]
public class Test_変換_共通部分式の先行評価 : ATest
{
    private static int Staticメソッド() => 1;
    [TestMethod]
    public void 特定パターン()
    {
        //            this.AssertExecute(() => Lambda(b => b * 3 + b * 3 == 0 ? b * 3 + b * 3 : b * 3 + b * 3 + 3));
        this.Execute引数パターン(a => ArrN<int>(a).OrderBy(p => p).ThenBy(p => p, Comparer<int>.Default));
        this.Execute引数パターン(a => a == 0 ? a : a);
        this.Execute引数パターン(a => a == 0 ? a * 3 : a / 3);
        this.Execute引数パターン(a => a * a + a * a);
    }
    [TestMethod]
    public void MakeAssigng()
    {
        decimal a = 1;
        //this.AssertExecute(
        //    ()=>a+b,
        //    ".Lambda LラムダR<System.Func`1[System.Decimal]>() {",
        //    "    .Block(カバレッジCS.Lite.Optimizers.変換_共通部分式の先行評価+<>c__DisplayClass1_0 $Cラムダ局所0) {",
        //    "        ($Cラムダ局所0 = .Constant<カバレッジCS.Lite.Optimizers.変換_共通部分式の先行評価+<>c__DisplayClass1_0>(カバレッジCS.Lite.Optimizers.変換_共通部分式の先行評価+<>c__DisplayClass1_0)).a +",
        //    "        $Cラムダ局所0.b",
        //    "    }",
        //    "}"
        //);
        this.Execute標準ラムダループ(
            () =>
                "".Let(p =>
                    a+a+a.NoEarlyEvaluation()
                )
        );
        //".Lambda LラムダR<Func`1[Decimal]>() {",
        //"    .Block(Decimal $Cラムダ局所0) {",
        //"        .Block() {",
        //"            $Cラムダ跨ぎ0 = ($Cラムダ局所0 = .Constant<カバレッジCS.Lite.Optimizers.変換_共通部分式の先行評価+<>c__DisplayClass1_0>(カバレッジCS.Lite.Optimizers.変換_共通部分式の先行評価+<>c__DisplayClass1_0).a)",
        //"            + $Cラムダ局所0;",
        //"            .Call Helpers.Let(",
        //"                \"\",",
        //"                .Lambda Lラムダ0<Func`2[String,Decimal]>(String $Pラムダ引数1p) {",
        //"                    $Cラムダ跨ぎ0 + .Call Helpers.NoLoopUnrolling(.Constant<カバレッジCS.Lite.Optimizers.変換_共通部分式の先行評価+<>c__DisplayClass1_0>(カバレッジCS.Lite.Optimizers.変換_共通部分式の先行評価+<>c__DisplayClass1_0).a)",
        //"                })",
        //"        }",
        //"    }",
        //"}"
        this.Execute2(
            () =>
                "".Let(p =>
                    a + a + a.NoLoopUnrolling()
                )
        );
        //".Lambda LラムダR<Func`1[Decimal]>() {",
        //"    .Block(Decimal $Cラムダ局所0) {",
        //"        ($Cラムダ局所0 = .Constant<カバレッジCS.Lite.Optimizers.変換_共通部分式の先行評価+<>c__DisplayClass1_0>(カバレッジCS.Lite.Optimizers.変換_共通部分式の先行評価+<>c__DisplayClass1_0).a)",
        //"        + $Cラムダ局所0 + .Call Helpers.NoLoopUnrolling($Cラムダ局所0)",
        //"    }",
        //"}"
        this.Execute2(
            () => a + a + a.NoLoopUnrolling()
        );
        //".Lambda LラムダR<Func`1[Decimal]>() {",
        //"    .Block(Decimal $Cラムダ局所0) {",
        //"        ($Cラムダ局所0 = .Constant<カバレッジCS.Lite.Optimizers.変換_共通部分式の先行評価+<>c__DisplayClass1_0>(カバレッジCS.Lite.Optimizers.変換_共通部分式の先行評価+<>c__DisplayClass1_0).a)",
        //"        + $Cラムダ局所0 + .Call Helpers.NoEarlyEvaluation(.Constant<カバレッジCS.Lite.Optimizers.変換_共通部分式の先行評価+<>c__DisplayClass1_0>(カバレッジCS.Lite.Optimizers.変換_共通部分式の先行評価+<>c__DisplayClass1_0).a)",
        //"    }",
        //"}"
        this.Execute2(
            () => a + a + a.NoEarlyEvaluation()
        );
    }

    [TestMethod]
    public void MakeAssign()
    {
        var array = new int[5];
        var i = Expression.Parameter(typeof(int));
        this.Execute2(
            Expression.Lambda<Func<int>>(
                Expression.Block(
                    new[] { i },
                    Expression.Assign(
                        i,
                        Expression.Constant(1)
                    ),
                    Expression.Assign(
                        Expression.ArrayAccess(
                            Expression.Constant(array),
                            Expression.Block(
                                Expression.Add(
                                    Expression.Add(
                                        i,
                                        i
                                    ),
                                    Expression.Add(
                                        i,
                                        i
                                    )
                                )
                            )
                        ),
                        Expression.Constant(3)
                    )
                )
            )
        );
        var d = Expression.Parameter(typeof(decimal));
        this.Execute2(
            Expression.Lambda<Func<decimal>>(
                Expression.Block(
                    new[] { d },
                    Expression.Assign(
                        d,
                        Expression.Multiply(
                            Expression.Constant(3m),
                            Expression.Constant(3m)
                        )
                    )
                )
            )
        );
        var p = Expression.Parameter(typeof(double));
        this.Execute2(
            Expression.Lambda<Func<double>>(
                Expression.Block(
                    new[] { p },
                    Expression.Assign(
                        p,
                        Expression.Constant(3.0)
                    )
                )
            )
        );
    }
    [TestMethod]
    public void Traverse()
    {
        //if(e0==null) return null;
        this.Execute2(() => Staticメソッド());
        //case ExpressionType.Default:return e0;
        this.Execute2(
            Expression.Lambda<Func<int>>(
                Expression.Default(typeof(int))
            )
        );
        //case ExpressionType.Label
        var Label1 = Expression.Label(typeof(int));
        var Label2 = Expression.Label(typeof(int));
        this.Execute2(
            Expression.Lambda<Func<int>>(
                Expression.Add(
                    Expression.Block(
                        Expression.Goto(Label1, Expression.Constant(1)),
                        Expression.Label(Label1, Expression.Constant(11))
                    ),
                    Expression.Block(
                        Expression.Goto(Label2, Expression.Constant(2)),
                        Expression.Label(Label2, Expression.Constant(22))
                    )
                )
            )
        );
        //case ExpressionType.Parameter:return e0;
        this.Execute2(() => Lambda(p => p));
        //case ExpressionType.Constant:
        //    if(e0.Type.Is定数をILで直接埋め込めるTypeか()) return e0;
        this.Execute2(() => "ABC");
        this.Execute2(() => 1m);
        //case ExpressionType.Assign:
        this.Execute2(
            Expression.Lambda<Func<double>>(
                Expression.Block(
                    new[] { d0 },
                    Expression.Assign(d0, Expression.Constant(3.0))
                )
            )
        );
        //case ExpressionType.PostDecrementAssign:
        //case ExpressionType.PostIncrementAssign:
        //case ExpressionType.PreIncrementAssign:
        //case ExpressionType.PreDecrementAssign:
        this.Execute2(
            Expression.Lambda<Func<double>>(
                Expression.Block(
                    new[] { d0 },
                    Expression.Assign(d0, Expression.Constant(3d)),
                    Expression.PostDecrementAssign(d0)
                )
            )
        );
        //case ExpressionType.Lambda:{
        //    if(this._HashSet一度出現したExpression.Add(e0)) {
        this.Execute2(() => Lambda(p => p + 1));
        //    }else{
        this.Execute2(() => Lambda(p => p + 2) + (1).Let(p => p + 2));
        //    }
        //}
        //case ExpressionType.Conditional:{
        //    if(this._変数_判定_ラムダ間ループ内指定Parameters以外のParameterが存在する.実行(e0,this._指定Parameters)) {
        // ReSharper disable once ConvertToLocalFunction
        Func<int, int> UpdateSelector = p => p;
        this.Execute引数パターン(a => SetN<int>(a).Update(p => p == 0, UpdateSelector));
        //    }else{
        //        if(HashSet一度出現したExpression.Add(e0)) {
        this.Execute2(() => Lambda(a => a == 0 ? a + 1 : a + 2));
        //        }else{
        this.Execute2(() => Lambda(a => (a == 0 ? a + 1 : a + 2) + (a == 0 ? a + 1 : a + 2)));
        //        }
        //    }
        //    foreach(var ifTrueで二度出現したExpression in HashSet_ifTrueで二度出現したExpression) {
        //        if(HashSet_ifFalseで二度出現したExpression.Contains(ifTrueで二度出現したExpression)) {
        this.Execute2(() => Lambda(a => a * 2 + a * 2 == 0 ? a * 2 + a * 2 : a * 2 + a * 2 + 2));
        //        }else{
        this.Execute2(() => Lambda(a => a == 0 ? a * 2 + a * 2 : a + 2));
        //        }
        //    }
        //    foreach(var ifTrueで一度出現したExpression in HashSet_ifTrueで一度出現したExpression) {
        //        if(HashSet_ifFalseで一度出現したExpression.Contains(ifTrueで一度出現したExpression)) {
        //            if(HashSet一度出現したExpression.Contains(ifTrueで一度出現したExpression)) {
        this.Execute2(() => Lambda(a => a * 2 == 0 ? a * 2 : a * 2 + 1));
        //            } else {
        this.Execute2(() => Lambda(a => a == 0 ? a * 2 : a * 2 + 1));
        //            }
        //        }else{
        this.Execute2(() => Lambda(a => a == 0 ? a * 2 : a));
        //        }
        //    }
        //    foreach(var 二度出現したExpression in HashSet二度出現したExpression) {
        this.Execute2(() => Lambda(a => a * 2 == 0 ? a * 2 : a * 2 + 2));
        //    }
        //}
        //case ExpressionType.MemberAccess:{
        //    if(Member0_Member.GetCustomAttribute(typeof(NoOptimizeAttribute))!=null) {
        this.Execute2(() => Lambda(a => _Static_class_演算子オーバーロード1._最適化されないメンバー));
        //    }else{
        this.Execute2(() => Lambda(a => _Static_class_演算子オーバーロード1.Int32フィールド));
        //    }
        //}
        //case ExpressionType.Call:{
        //    if(MethodCall_Method.GetCustomAttribute(typeof(NoOptimizeAttribute))!=null)return
        this.Execute2(() => Lambda(a => _Static_class_演算子オーバーロード1.最適化されないメソッド()));
        this.Execute2(() => Lambda(a => _Static_class_演算子オーバーロード1.メソッド()));
        //    if(ExtendedSet.ループ展開可能なEnumerableSetに属するGenericMethodDefinitionか(MethodCall0_GenericMethodDefinition)) {
        //        if(Reflection.ExtendSet1.Aggregate_seed_func_resultSelector==MethodCall0_GenericMethodDefinition||Reflection.ExtendEnumerable.Aggregate_seed_func_resultSelector==MethodCall0_GenericMethodDefinition) {
        //            if(resultSelctor0!=null) {
        this.Execute引数パターン(a => SetN<int>(a).Aggregate(1, (x, y) => x + y, c => c + 1));
        this.Execute引数パターン(a => EnuN<int>(a).Aggregate(1, (x, y) => x + y, c => c + 1));
        //            }else{
        this.Execute引数パターン(a => SetN<int>(a).Aggregate(1, (x, y) => x + y, c => c + 1));
        this.Execute引数パターン(a => EnuN<int>(a).Aggregate(1, (x, y) => x + y, c => c + 1));
        //            }
        //        }else{
        //            for(var _Field = 0;_Field<MethodCall0_Arguments_Count;_Field++) {
        //                if(MethodCall0_Lambda!=null) {
        this.Execute引数パターン(a => EnuN<int>(a).Select(p => p + 1));
        //                } else {
        //↑と同じ
        //                }
        //            }
        //        }
        //    }
        //}
        //case ExpressionType.Loop:{
        {
            var Break = Expression.Label();
            var 作業 = Expression.Variable(typeof(decimal));
            this.Execute2(
                Expression.Lambda<Func<decimal>>(
                    Expression.Block(
                        new[] { 作業 },
                        Expression.Loop(
                            Expression.Block(
                                Expression.Assign(
                                    作業,
                                    Expression.Add(
                                        Expression.Constant(1m),
                                        Expression.Constant(1m)
                                    )
                                ),
                                Expression.Break(Break)
                            ),
                            Break
                        ),
                        作業
                    )
                )
            );
        }
        //}
        //if(this._変数_判定_ラムダ間ループ内指定Parameters以外のParameterが存在する.実行(e0,this._指定Parameters)) {
        var 外部パラメータ = 3;
        this.Execute引数パターン標準ラムダループ((a, b) => ArrN<int>(a).Join(ArrN<int>(b), o => o, i => i, (o, i) => new { o, i }).Select(oi => 外部パラメータ));
        //Execute(()=>Lambda(_Field=>Lambda(b=>_Field)));
        //} else{
        //    if(this._HashSet一度出現したExpression.Add(e0)) {
        this.Execute2(() => _Int32);
        //    } else {
        this.Execute2(() => _Int32 + _Int32);
        //    }
        //}
    }
    private static Optimizer p = new();
    //private static global::LinqDB.Optimizers.OptimizeLevels[]OptimizeLevels = new[]{
    //    global::LinqDB.Optimizers.OptimizeLevels.None,
    //    global::LinqDB.Optimizers.OptimizeLevels.Anonymousをnewしてメンバーを参照している式の省略,
    //    global::LinqDB.Optimizers.OptimizeLevels.KeySelectorがAnonymousの場合ValueTupleに変換,
    //    global::LinqDB.Optimizers.OptimizeLevels.WhereからDictionary,
    //    global::LinqDB.Optimizers.OptimizeLevels.Whereから葉に移動する,
    //    global::LinqDB.Optimizers.OptimizeLevels.プロファイル,
    //    global::LinqDB.Optimizers.OptimizeLevels.ループ融合,
    //    global::LinqDB.Optimizers.OptimizeLevels.局所Parameterの先行評価,
    //    global::LinqDB.Optimizers.OptimizeLevels.独自のILGenerator
    //};
    private static void 出力(Expression e) {
        Debug.WriteLine(Optimizer.インラインラムダテキスト(e));
        //foreach(var OptimizeLevel0 in OptimizeLevels) {
        //    foreach(var OptimizeLevel1 in OptimizeLevels) {
        //        p.OptimizeLevel=OptimizeLevel0|OptimizeLevel1;
        //        Debug.WriteLine(Optimizer.インラインラムダテキスト(p.CreateExpression(e)));
        //    }
        //}
    }
    [TestMethod]
    public void Assignの先行評価()
    {
        var p0 = Expression.Parameter(typeof(bool), "000000");
        var p1 = Expression.Parameter(typeof(bool), "000001");
        var c = Expression.Constant(false);
        var And = Expression.And(
            p0,
            p0
        );
        出力(
            Expression.Lambda<Func<bool, bool, bool>>(
                Expression.And(
                    Expression.Assign(
                        p1,
                        Expression.Assign(
                            p0,
                            And
                        )
                    ),
                    And
                ), p0, p1
            )
        );
        出力(
            Expression.Lambda<Func<bool, bool, bool>>(
                Expression.Assign(
                    p1,
                    Expression.Assign(
                        p0,
                        And
                    )
                ), p0, p1
            )
        );
        出力(
            Expression.Lambda<Func<bool, bool, bool>>(
                Expression.Assign(
                    p1,
                    Expression.Assign(
                        p0,
                        c
                    )
                ), p0, p1
            )
        );
    }
    [TestMethod]
    public void 非Block内部の先行評価()
    {
        var p0 = Expression.Parameter(typeof(bool), "000000");
        var And = Expression.And(
            Expression.And(
                p0,
                p0
            ),
            Expression.And(
                p0,
                p0
            )
        );
        出力(
            Expression.Lambda<Func<bool, bool>>(
                Expression.Condition(
                    And,
                    And,
                    And
                ),
                p0
            )
        );
        出力(
            Expression.Lambda<Func<bool, bool>>(
                Expression.Condition(
                    And,
                    And,
                    p0
                ),
                p0
            )
        );
        出力(
            Expression.Lambda<Func<bool, bool>>(
                Expression.Condition(
                    And,
                    p0,
                    And
                ),
                p0
            )
        );
        出力(
            Expression.Lambda<Func<bool, bool>>(
                Expression.Condition(
                    And,
                    p0,
                    p0
                ),
                p0
            )
        );
        出力(
            Expression.Lambda<Func<bool, bool>>(
                Expression.Condition(
                    p0,
                    And,
                    And
                ),
                p0
            )
        );
        出力(
            Expression.Lambda<Func<bool, bool>>(
                Expression.Condition(
                    p0,
                    And,
                    p0
                ),
                p0
            )
        );
        出力(
            Expression.Lambda<Func<bool, bool>>(
                Expression.Condition(
                    p0,
                    p0,
                    And
                ),
                p0
            )
        );
        出力(
            Expression.Lambda<Func<bool, bool>>(
                Expression.Condition(
                    p0,
                    p0,
                    p0
                ),
                p0
            )
        );
    }

    [TestMethod]
    public void Block内部の手動Parameter1()
    {
        var a = Expression.Parameter(typeof(int), "Pワーク変数a");
        var b = Expression.Parameter(typeof(int), "Pワーク変数b");
        出力(
            Expression.Lambda<Func<int>>(
                Expression.Block(
                    new[]{
                        a,b
                    },
                    Expression.Assign(
                        a,
                        Expression.Constant(1)
                    ),
                    Expression.Assign(
                        b,
                        Expression.Constant(2)
                    ),
                    Expression.Assign(
                        a,
                        Expression.Add(
                            a,
                            b
                        )
                    ),
                    Expression.Assign(
                        b,
                        Expression.Add(
                            a,
                            b
                        )
                    )
                )
            )
        );
    }
    [TestMethod]
    public void Block内部の手動Parameter2()
    {
        var a = Expression.Parameter(typeof(int), "Pワーク変数a");
        var b = Expression.Parameter(typeof(int), "Pワーク変数b");
        出力(
            Expression.Lambda<Func<int>>(
                Expression.Block(
                    new[]{
                        a,b
                    },
                    Expression.Assign(
                        a,
                        Expression.Constant(1)
                    ),
                    Expression.Assign(
                        b,
                        Expression.Constant(2)
                    ),
                    Expression.Assign(
                        a,
                        Expression.Add(
                            Expression.Add(
                                a,
                                b
                            ),
                            Expression.Add(
                                a,
                                b
                            )
                        )
                    ),
                    Expression.Assign(
                        b,
                        Expression.Add(
                            a,
                            b
                        )
                    )
                )
            )
        );
    }

    [TestMethod]
    public void Block内部の先行評価()
    {
        var p0 = Expression.Parameter(typeof(int), "Pワーク変数");
        {
            var t = Expression.Subtract(
                Expression.Multiply(
                    p0,
                    p0
                ),
                Expression.Multiply(
                    p0,
                    p0
                )
            );
            出力(
                Expression.Lambda<Func<int>>(
                    Expression.Add(
                        Expression.Add(
                            t,
                            t
                        ),
                        t
                    )
                )
            );
        }
        出力(
            Expression.Lambda<Func<int>>(
                Expression.And(
                    Expression.And(
                        p0,
                        p0
                    ),
                    Expression.And(
                        p0,
                        p0
                    )
                )
            )
        );
        出力(
            Expression.Lambda<Func<int>>(
                Expression.And(
                    Expression.And(
                        p0,
                        p0
                    ),
                    Expression.And(
                        p0,
                        Expression.And(
                            p0,
                            p0
                        )
                    )
                )
            )
        );
        出力(
            Expression.Lambda<Func<int>>(
                Expression.And(
                    Expression.And(
                        p0,
                        p0
                    ),
                    Expression.And(
                        Expression.And(
                            p0,
                            p0
                        ),
                        p0
                    )
                )
            )
        );
        出力(
            Expression.Lambda<Func<int>>(
                Expression.And(
                    Expression.And(
                        p0,
                        p0
                    ),
                    Expression.And(
                        Expression.And(
                            p0,
                            p0
                        ),
                        Expression.And(
                            p0,
                            p0
                        )
                    )
                )
            )
        );
        出力(
            Expression.Lambda<Func<int>>(
                Expression.And(
                    Expression.And(
                        p0,
                        Expression.And(
                            p0,
                            p0
                        )
                    ),
                    Expression.And(
                        p0,
                        p0
                    )
                )
            )
        );
        出力(
            Expression.Lambda<Func<int>>(
                Expression.And(
                    Expression.And(
                        p0,
                        Expression.And(
                            p0,
                            p0
                        )
                    ),
                    Expression.And(
                        p0,
                        Expression.And(
                            p0,
                            p0
                        )
                    )
                )
            )
        );
        出力(
            Expression.Lambda<Func<int>>(
                Expression.And(
                    Expression.And(
                        p0,
                        Expression.And(
                            p0,
                            p0
                        )
                    ),
                    Expression.And(
                        Expression.And(
                            p0,
                            p0
                        ),
                        p0
                    )
                )
            )
        );
        出力(
            Expression.Lambda<Func<int>>(
                Expression.And(
                    Expression.And(
                        p0,
                        Expression.And(
                            p0,
                            p0
                        )
                    ),
                    Expression.And(
                        Expression.And(
                            p0,
                            p0
                        ),
                        Expression.And(
                            p0,
                            p0
                        )
                    )
                )
            )
        );
        出力(
            Expression.Lambda<Func<int>>(
                Expression.And(
                    Expression.And(
                        Expression.And(
                            p0,
                            p0
                        ),
                        p0
                    ),
                    Expression.And(
                        p0,
                        p0
                    )
                )
            )
        );
        出力(
            Expression.Lambda<Func<int>>(
                Expression.And(
                    Expression.And(
                        Expression.And(
                            p0,
                            p0
                        ),
                        p0
                    ),
                    Expression.And(
                        p0,
                        Expression.And(
                            p0,
                            p0
                        )
                    )
                )
            )
        );
        出力(
            Expression.Lambda<Func<int>>(
                Expression.And(
                    Expression.And(
                        Expression.And(
                            p0,
                            p0
                        ),
                        p0
                    ),
                    Expression.And(
                        Expression.And(
                            p0,
                            p0
                        ),
                        p0
                    )
                )
            )
        );
        出力(
            Expression.Lambda<Func<int>>(
                Expression.And(
                    Expression.And(
                        Expression.And(
                            p0,
                            p0
                        ),
                        p0
                    ),
                    Expression.And(
                        Expression.And(
                            p0,
                            p0
                        ),
                        Expression.And(
                            p0,
                            p0
                        )
                    )
                )
            )
        );
        出力(
            Expression.Lambda<Func<int>>(
                Expression.And(
                    Expression.And(
                        Expression.And(
                            p0,
                            p0
                        ),
                        Expression.And(
                            p0,
                            p0
                        )
                    ),
                    Expression.And(
                        p0,
                        p0
                    )
                )
            )
        );
        出力(
            Expression.Lambda<Func<int>>(
                Expression.And(
                    Expression.And(
                        Expression.And(
                            p0,
                            p0
                        ),
                        Expression.And(
                            p0,
                            p0
                        )
                    ),
                    Expression.And(
                        p0,
                        Expression.And(
                            p0,
                            p0
                        )
                    )
                )
            )
        );
        出力(
            Expression.Lambda<Func<int>>(
                Expression.And(
                    Expression.And(
                        Expression.And(
                            p0,
                            p0
                        ),
                        Expression.And(
                            p0,
                            p0
                        )
                    ),
                    Expression.And(
                        Expression.And(
                            p0,
                            p0
                        ),
                        p0
                    )
                )
            )
        );
        出力(
            Expression.Lambda<Func<int>>(
                Expression.And(
                    Expression.And(
                        Expression.And(
                            p0,
                            p0
                        ),
                        Expression.And(
                            p0,
                            p0
                        )
                    ),
                    Expression.And(
                        Expression.And(
                            p0,
                            p0
                        ),
                        Expression.And(
                            p0,
                            p0
                        )
                    )
                )
            )
        );
    }
    [TestMethod]
    public void Conditional内部の先行評価()
    {
        var p0 = Expression.Parameter(typeof(bool), "Pワーク変数");
        var And = new[]{
            Expression.And(
                p0,
                p0
            ),
            Expression.And(
                p0,
                Expression.And(
                    p0,
                    p0
                )
            ),
            Expression.And(
                Expression.And(
                    p0,
                    p0
                ),
                p0
            ),
            Expression.And(
                Expression.And(
                    p0,
                    p0
                ),
                Expression.And(
                    p0,
                    p0
                )
            )
        };
        foreach (var Test in And)
        foreach (var IfTrue in And)
        foreach (var IfFalse in And)
            出力(
                Expression.Lambda<Func<bool>>(
                    Expression.Condition(
                        Test,
                        IfTrue,
                        IfFalse
                    )
                )
            );
    }
    [TestMethod]
    public void AndAlso内部の先行評価0()
    {
        var p0 = Expression.Parameter(typeof(bool), "Pワーク変数");
        出力(
            Expression.Lambda<Func<bool>>(
                Expression.AndAlso(
                    Expression.And(
                        p0,
                        p0
                    ),
                    Expression.And(
                        p0,
                        p0
                    )
                )
            )
        );
    }
    [TestMethod]
    public void AndAlso内部の先行評価1()
    {
        var p0 = Expression.Parameter(typeof(bool), "Pワーク変数");
        出力(
            Expression.Lambda<Func<bool>>(
                Expression.AndAlso(
                    Expression.And(
                        Expression.And(
                            p0,
                            p0
                        ),
                        Expression.And(
                            p0,
                            p0
                        )
                    ),
                    Expression.And(
                        p0,
                        p0
                    )
                )
            )
        );
    }
    [TestMethod]
    public void OrElse内部の先行評価0()
    {
        var p0 = Expression.Parameter(typeof(bool), "Pワーク変数");
        出力(
            Expression.Lambda<Func<bool>>(
                Expression.OrElse(
                    Expression.And(
                        p0,
                        p0
                    ),
                    Expression.And(
                        p0,
                        p0
                    )
                )
            )
        );
    }
    [TestMethod]
    public void OrElse内部の先行評価1()
    {
        var p0 = Expression.Parameter(typeof(bool), "Pワーク変数");
        出力(
            Expression.Lambda<Func<bool>>(
                Expression.OrElse(
                    Expression.And(
                        p0,
                        p0
                    ),
                    Expression.And(
                        p0,
                        p0
                    )
                )
            )
        );
    }
    [TestMethod]
    public void OrElse内部の先行評価2()
    {
        var p0 = Expression.Parameter(typeof(bool), "Pワーク変数");
        出力(
            Expression.Lambda<Func<bool>>(
                Expression.OrElse(
                    Expression.And(
                        Expression.And(
                            p0,
                            p0
                        ),
                        Expression.And(
                            p0,
                            p0
                        )
                    ),
                    Expression.And(
                        p0,
                        p0
                    )
                )
            )
        );
    }
    [TestMethod]
    public void Switch内部の先行評価() {
        var p0 = Expression.Parameter(typeof(decimal),"Pワーク変数");
        var And = new Expression[]{
            p0,
            Expression.Add(
                p0,
                p0
            )
        };
        var 回数 = 0;
        foreach(var Body0 in And) {
            foreach(var TestValue00 in And) {
                foreach(var TestValue01 in And) {
                    var SwitchCase0 = Expression.SwitchCase(
                        Body0,
                        TestValue00,TestValue01
                    );
                    foreach(var Body1 in And) {
                        foreach(var TestValue10 in And) {
                            foreach(var TestValue11 in And) {
                                var SwitchCase1=Expression.SwitchCase(
                                    Body1,
                                    TestValue10,TestValue11
                                );
                                foreach(var SwitchValue in And) {
                                    foreach(var DefaultBody in And) {
                                        Debug.WriteLine(回数++.ToString());
                                        出力(
                                            Expression.Lambda<Func<decimal>>(
                                                Expression.Switch(
                                                    SwitchValue,
                                                    DefaultBody,
                                                    SwitchCase0,
                                                    SwitchCase1
                                                )
                                            )
                                        );
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }
    [TestMethod]
    public void Gotoの先行評価()
    {
        var p0 = Expression.Constant(1m);
        var GotoLabel = Expression.Label(typeof(decimal));
        出力(
            Expression.Lambda<Func<decimal>>(
                Expression.Block(
                    Expression.Goto(
                        GotoLabel,
                        Expression.Multiply(
                            p0,
                            p0
                        )
                    ),
                    Expression.Label(
                        GotoLabel,
                        Expression.Subtract(
                            p0,
                            p0
                        )
                    )
                )
            )
        );
    }
}