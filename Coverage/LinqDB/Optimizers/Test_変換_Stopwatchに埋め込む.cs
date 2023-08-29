using System.Linq.Expressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace CoverageCS.LinqDB.Optimizers;

[TestClass]
public class Test_変換_Stopwatchに埋め込む : ATest{
    [TestMethod]public void Loop0(){
        var p = Expression.Parameter(typeof(bool),"p");
        var Break = Expression.Label("Break");
        var Continue = Expression.Label("Continue");
        this.実行結果が一致するか確認(
            Expression.Lambda<Action>(
                Expression.Block(
                    new[] { p },
                    Expression.Assign(p, Expression.Constant(false)),
                    Expression.Loop(
                        Expression.IfThenElse(
                            p,
                            Expression.Continue(Continue),
                            Expression.Break(Break)
                        ),
                        Break,
                        Continue
                    )
                )
            )
        );
    }
    [TestMethod]public void Loop1(){
        var p = Expression.Parameter(typeof(bool),"p");
        var Break = Expression.Label("Break");
        this.実行結果が一致するか確認(
            Expression.Lambda<Action>(
                Expression.Block(
                    new[] { p },
                    Expression.Assign(p, Expression.Constant(true)),
                    Expression.Loop(
                        Expression.IfThen(
                            p,
                            Expression.Break(Break)
                        ),
                        Break
                    )
                )
            )
        );
    }
    [TestMethod]public void Loop2(){
        var p = Expression.Parameter(typeof(bool),"p");
        var Break = Expression.Label("Break");
        var Continue = Expression.Label("Continue");
        this.実行結果が一致するか確認(
            Expression.Lambda<Action>(
                Expression.Block(
                    new[] { p },
                    Expression.Loop(
                        Expression.IfThenElse(
                            p,
                            Expression.Continue(Continue),
                            Expression.Break(Break)
                        )
                    ),
                    Expression.Label(Continue),
                    Expression.Label(Break)
                )
            )
        );
    }
}
