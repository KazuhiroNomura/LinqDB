using System.Reflection;
using MemoryPack;
using MessagePack;
using Expression = System.Linq.Expressions.Expression;
namespace TestLinqDB.Optimizers.ReturnExpressionTraverser;
[MemoryPackable,MessagePackObject(true)]
public partial class 時間計測{
    public bool BooleanMethod(int x){
        //Thread.Sleep(100);
        return true;
    }
    public int Int32Method(int x)=>x;
    public override int GetHashCode()=>0;
    public override bool Equals(object? obj)=>true;
}
public class 変換_Stopwatchに埋め込む : 共通{
    //protected override テストオプション テストオプション=>テストオプション.ローカル実行|テストオプション.アセンブリ保存|テストオプション.プロファイラ;
    //protected override テストオプション テストオプション=>テストオプション.ローカル実行|テストオプション.アセンブリ保存;
    private static readonly MethodInfo BooleanMethod=typeof(時間計測).GetMethod(nameof(時間計測.BooleanMethod))!;
    private static readonly MethodInfo Int32Method=typeof(時間計測).GetMethod(nameof(時間計測.Int32Method))!;

    [Fact]public void Condition0(){
        var p=Expression.Constant(new 時間計測());
        var Loop=Expression.Condition(
            Expression.Call(p,BooleanMethod,Expression.Constant(0)),
            Expression.Call(p,BooleanMethod,Expression.Constant(1)),
            Expression.Call(p,BooleanMethod,Expression.Constant(2))
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
        _=typeof(時間計測).GetMethod(nameof(時間計測.BooleanMethod))!;
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
        var Loop=Expression.Loop(
            Expression.Block(
                Expression.Break(Break,p),
                Expression.Condition(
                    Expression.Call(p,BooleanMethod,Expression.Constant(0)),
                    Expression.Call(p,BooleanMethod,Expression.Constant(1)),
                    Expression.Call(p,BooleanMethod,Expression.Constant(2))
                )
            ),
            Break
        );
        this.Expression実行AssertEqual(Expression.Lambda<Func<時間計測>>(Loop));
    }
    [Fact]public void Condition41(){
        var p=Expression.Constant(new 時間計測());
        var Break=Expression.Label(typeof(時間計測));
        _=typeof(時間計測).GetMethod(nameof(時間計測.BooleanMethod))!;
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
        _=typeof(時間計測).GetMethod(nameof(時間計測.BooleanMethod))!;
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
        var c=Expression.Constant(new 時間計測());
        var Break=Expression.Label(typeof(int));
        var e=Expression.Block(
            new[]{i},
            Expression.Assign(i,Expression.Constant(ループ回数)),
            Expression.Loop(
                Expression.Block(
                    Expression.IfThen(
                        Expression.LessThan(
                            Expression.PostDecrementAssign(i),
                            Expression.Constant(0)
                        ),
                        Expression.Break(
                            Break,
                            Expression.Call(c,Int32Method,Expression.Constant(0))
                        )
                    ),
                    Expression.Condition(
                        Expression.Call(c,BooleanMethod,Expression.Constant(0)),
                        Expression.Call(c,BooleanMethod,Expression.Constant(1)),
                        Expression.Call(c,BooleanMethod,Expression.Constant(2))
                    )
                ),
                Break
            )
        );
        this.Expression実行AssertEqual(Expression.Lambda<Func<int>>(e));
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
        Expression.Label();
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
    private const int ループ回数=10000000;
    [Fact]public void ContinueBreakを組み合わせてLoop1(){
        var i=Expression.Parameter(typeof(int),"i");
        var p=Expression.Parameter(typeof(int),"p");
        var Continue=Expression.Label("Continue");
        var Break=Expression.Label(typeof(int),"Break");
        var Loop=Expression.Block(
            new[]{i},
            Expression.Assign(i,Expression.Constant(ループ回数)),
            Expression.Label(Continue),
            Expression.IfThen(
                Expression.LessThan(
                    Expression.PostDecrementAssign(i),
                    Expression.Constant(0)
                ),
                Expression.Break(Break,p)
            ),
            Expression.Continue(Continue),
            Expression.Label(Break,Expression.Default(Break.Type))
        );
        this.Expression実行AssertEqual(Expression.Lambda<Func<int,int>>(Loop,p));
    }
    [Fact]public void ContinueBreakを組み合わせてLoop10(){
        var i=Expression.Parameter(typeof(int));
        var p=Expression.Parameter(typeof(int),"p");
        var Continue=Expression.Label("Continue");
        var Break=Expression.Label(typeof(int),"Break");
        var Loop=Expression.Block(
            new[]{i},
            Expression.Assign(i,Expression.Constant(ループ回数)),
            Expression.Label(Continue),
            Expression.IfThenElse(
                Expression.LessThan(
                    Expression.PostDecrementAssign(i),
                    Expression.Constant(0)
                ),
                Expression.Break(Break,p),
                i
            ),
            Expression.Continue(Continue),
            Expression.Label(Break,Expression.Default(Break.Type))
        );
        this.Expression実行AssertEqual(Expression.Lambda<Func<int,int>>(Loop,p));
    }
    [Fact]public void Conditional6(){
        var i=Expression.Parameter(typeof(int));
        var p=Expression.Parameter(typeof(int),"p");
        var Loop=Expression.Block(
            new[]{i},
            Expression.Assign(i,Expression.Constant(ループ回数)),
            Expression.IfThenElse(
                Expression.LessThan(
                    Expression.PostDecrementAssign(i),
                    Expression.Constant(0)
                ),
                p,
                p
            ),
            i
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
