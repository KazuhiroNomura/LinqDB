using Expression = System.Linq.Expressions.Expression;
namespace TestLinqDB.Optimizers.ReturnExpressionTraverser;
public class 変換_Stopwatchに埋め込む : 共通{
    protected override テストオプション テストオプション=>テストオプション.ローカル実行|テストオプション.アセンブリ保存;//|テストオプション.プロファイラ;
    private class 時間計測{
        public bool Method(int x){
            Thread.Sleep(100);
            return true;
        }
    }
    [Fact]public void Condition0(){
        var i=Expression.Parameter(typeof(int));
        var p=Expression.Constant(new 時間計測());
        var Break=Expression.Label(typeof(時間計測));
        var Method=typeof(時間計測).GetMethod(nameof(時間計測.Method))!;
        var Loop=Expression.Loop(
            Expression.Block(
                Expression.Condition(
                    Expression.Call(p,Method,Expression.Constant(0)),
                    Expression.Call(p,Method,Expression.Constant(1)),
                    Expression.Call(p,Method,Expression.Constant(2))
                )
            ),
            Break
        );
        this.Expression実行AssertEqual(
            Expression.Lambda<Func<時間計測>>(
                Expression.Block(
                    new[]{i},
                    Expression.Assign(i,Expression.Constant(1)),
                    Loop
                )
            )
        );
    }
    [Fact]public void Condition3(){
        var i=Expression.Parameter(typeof(int));
        var p=Expression.Constant(new 時間計測());
        var Break=Expression.Label(typeof(時間計測));
        var Method=typeof(時間計測).GetMethod(nameof(時間計測.Method))!;
        var Loop=Expression.Loop(
            Expression.Block(
                Expression.IfThen(
                    Expression.LessThan(
                        Expression.PostDecrementAssign(i),
                        Expression.Constant(0)
                    ),
                    Expression.Break(Break,p)
                //),
                //Expression.Condition(
                //    Expression.Call(p,Method,Expression.Constant(0)),
                //    Expression.Call(p,Method,Expression.Constant(1)),
                //    Expression.Call(p,Method,Expression.Constant(2))
                )
            ),
            Break
        );
        this.Expression実行AssertEqual(
            Expression.Lambda<Func<時間計測>>(
                Expression.Block(
                    new[]{i},
                    Expression.Assign(i,Expression.Constant(1)),
                    Loop
                )
            )
        );
    }
    [Fact]public void Condition5(){
        var i=Expression.Parameter(typeof(int));
        var p=Expression.Constant(new 時間計測());
        var Break=Expression.Label(typeof(時間計測));
        var Method=typeof(時間計測).GetMethod(nameof(時間計測.Method))!;
        var Loop=Expression.Loop(
            Expression.Block(
                Expression.IfThen(
                    Expression.LessThan(
                        Expression.PostDecrementAssign(i),
                        Expression.Constant(0)
                    ),
                    Expression.Break(Break,p)
                ),
                Expression.Condition(
                    Expression.Call(p,Method,Expression.Constant(0)),
                    Expression.Call(p,Method,Expression.Constant(1)),
                    Expression.Call(p,Method,Expression.Constant(2))
                )
            ),
            Break
        );
        this.Expression実行AssertEqual(
            Expression.Lambda<Func<時間計測>>(
                Expression.Block(
                    new[]{i},
                    Expression.Assign(i,Expression.Constant(1)),
                    Loop
                )
            )
        );
    }
}
