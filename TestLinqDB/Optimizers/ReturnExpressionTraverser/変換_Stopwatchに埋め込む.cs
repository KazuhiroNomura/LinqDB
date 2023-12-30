using Expression = System.Linq.Expressions.Expression;
namespace TestLinqDB.Optimizers.ReturnExpressionTraverser;
public class 変換_Stopwatchに埋め込む : 共通{
    protected override テストオプション テストオプション=>テストオプション.ローカル実行|テストオプション.アセンブリ保存|テストオプション.プロファイラ;
    //protected override テストオプション テストオプション=>テストオプション.ローカル実行|テストオプション.アセンブリ保存;
    private class 時間計測{
        public bool Method(int x){
            Thread.Sleep(100);
            return true;
        }
    }
    [Fact]public void Condition0(){
        var p=Expression.Constant(new 時間計測());
        var Method=typeof(時間計測).GetMethod(nameof(時間計測.Method))!;
        var Loop=Expression.Condition(
            Expression.Call(p,Method,Expression.Constant(0)),
            Expression.Call(p,Method,Expression.Constant(1)),
            Expression.Call(p,Method,Expression.Constant(2))
        );
        this.Expression実行AssertEqual(
            Expression.Lambda<Func<bool>>(
                Loop
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
    [Fact]public void Condition40(){
        var p=Expression.Constant(new 時間計測());
        var Break=Expression.Label(typeof(時間計測));
        var Method=typeof(時間計測).GetMethod(nameof(時間計測.Method))!;
        var Loop=Expression.Loop(
            Expression.Block(
                Expression.Break(Break,p),
                Expression.Condition(
                    Expression.Call(p,Method,Expression.Constant(0)),
                    Expression.Call(p,Method,Expression.Constant(1)),
                    Expression.Call(p,Method,Expression.Constant(2))
                )
            ),
            Break
        );
        this.Expression実行AssertEqual(Expression.Lambda<Func<時間計測>>(Loop));
    }
    [Fact]public void Condition41(){
        var p=Expression.Constant(new 時間計測());
        var Break=Expression.Label(typeof(時間計測));
        var Method=typeof(時間計測).GetMethod(nameof(時間計測.Method))!;
        var Loop=Expression.Loop(
            Expression.Block(
                Expression.Break(Break,p)
            ),
            Break
        );
        this.Expression実行AssertEqual(Expression.Lambda<Func<時間計測>>(Loop));
    }
    [Fact]public void Condition42(){
        var p=Expression.Constant(new 時間計測());
        var Break=Expression.Label(typeof(時間計測));
        var Method=typeof(時間計測).GetMethod(nameof(時間計測.Method))!;
        var Loop=Expression.Loop(
            Expression.Block(
                Expression.Break(Break,p),
                p
            ),
            Break
        );
        this.Expression実行AssertEqual(Expression.Lambda<Func<時間計測>>(Loop));
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
    [Fact]public void Goto0(){
        //ok
        var p=Expression.Constant(1);
        var Continue=Expression.Label();
        var Break=Expression.Label(typeof(int));
        var Loop=Expression.Block(
            Expression.Label(Continue),
            Expression.Break(Break,p),
            p,
            Expression.Goto(Continue),
            Expression.Label(Break,p)
        );
        this.Expression実行AssertEqual(Expression.Lambda<Func<int>>(Loop));
    }
    [Fact]public void Goto1(){
        //ok
        var p=Expression.Constant(1);
        var Continue=Expression.Label();
        var Break=Expression.Label(typeof(int));
        var Loop=Expression.Block(
            Expression.Break(Break,p),
            Expression.Label(Break,p)
        );
        this.Expression実行AssertEqual(Expression.Lambda<Func<int>>(Loop));
    }
    [Fact]public void ContinueBreakを組み合わせてLoop0(){
        //ok
        var p=Expression.Parameter(typeof(int),"p");
        var Continue=Expression.Label();
        var Break=Expression.Label(typeof(int));
        var Loop=Expression.Block(
            Expression.Label(Continue),
            Expression.Break(Break,p),
            Expression.Goto(Continue),
            Expression.Label(Break,Expression.Default(Break.Type))
        );
        this.Expression実行AssertEqual(Expression.Lambda<Func<int,int>>(Loop,p));
    }
    [Fact]public void ContinueBreakを組み合わせてLoop01(){
        //ok
        var p=Expression.Parameter(typeof(int),"p");
        var Continue=Expression.Label("Continue");
        var Break=Expression.Label(typeof(int),"Break");
        var Loop=Expression.Block(
            Expression.Label(Continue),
            Expression.Break(Break,p),
            Expression.Goto(Continue),
            Expression.Label(Break,p)
        );
        this.Expression実行AssertEqual(Expression.Lambda<Func<int,int>>(Loop,p));
    }
    [Fact]public void ContinueBreakを組み合わせてLoop1(){
        var p=Expression.Parameter(typeof(int),"p");
        var Continue=Expression.Label("Continue");
        var Break=Expression.Label(typeof(int),"Break");
        var Loop=Expression.Block(
            Expression.Label(Continue),
            Expression.Break(Break,p),
            p,
            Expression.Continue(Continue),
            Expression.Label(Break,Expression.Default(Break.Type))
        );
        this.Expression実行AssertEqual(Expression.Lambda<Func<int,int>>(Loop,p));
    }
    [Fact]public void ContinueBreakを組み合わせてLoop2(){
        var p=Expression.Parameter(typeof(int),"p");
        var Continue=Expression.Label();
        var Break=Expression.Label();
        var Loop=Expression.Block(
            Expression.Label(Continue),
            Expression.Block(
                Expression.Break(Break),
                p
            ),
            Expression.Goto(Continue),
            Expression.Label(Break),
            p
        );
        this.Expression実行AssertEqual(Expression.Lambda<Func<int,int>>(Loop,p));
    }
    [Fact]public void Loop0(){
        //ok
        var p=Expression.Parameter(typeof(int),"p");
        var Break=Expression.Label(typeof(int));
        var Loop=Expression.Loop(
            Expression.Block(
                Expression.Break(Break,p)
            ),
            Break
        );
        this.Expression実行AssertEqual(Expression.Lambda<Func<int,int>>(Loop,p));
    }
    [Fact]public void Loop1(){
        //error
        var p=Expression.Parameter(typeof(int),"p");
        var Break=Expression.Label(typeof(int),"Break");
        var Loop=Expression.Loop(
            Expression.Block(
                Expression.Break(Break,p)
                ,p
            ),
            Break
        );
        this.Expression実行AssertEqual(Expression.Lambda<Func<int,int>>(Loop,p));
    }
}
