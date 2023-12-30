using System.Diagnostics;
using System.Linq.Expressions;
// ReSharper disable All
namespace TestLinqDB.仕様;
public class Expressions:共通{
    protected override テストオプション テストオプション=>テストオプション.MemoryPack_MessagePack_Utf8Json;
    [Fact]
    public void GotoはVoidになる(){
        var Label=Expression.Label(typeof(decimal),"L0");
        var Goto=Expression.Goto(Label,Expression.Constant(3m));
        Assert.Equal(typeof(void),Goto.Type);
    }
    [Fact]
    public void LoopはVoidになる(){
        var Loop=Expression.Loop(Expression.Constant(3m));
        Assert.Equal(typeof(void),Loop.Type);
    }
    [Fact]
    public void Loopはオペランドに使える(){
        const decimal input=2;
        const decimal expected=input+input;
        var Break=Expression.Label(typeof(decimal),"L0");
        var Loop=Expression.Loop(
            Expression.Break(Break,Expression.Constant(input)),
            Break
        );
        var Lambda=Expression.Lambda<Func<decimal>>(
            Expression.Add(Loop,Loop)
        );
        {
            var actual=Lambda.Compile()();
            Assert.Equal(expected,actual);
        }
        {
            var actual=this.Optimizer.Execute(Lambda);
            Assert.Equal(expected,actual);
        }
    }
    [Fact]
    public void Labelはオペランドに使えない() {
        const decimal input=2;
        var Goto = Expression.Label(typeof(decimal),"L0");
        var Lambda=Expression.Lambda<Func<decimal>>(
            Expression.Block(
                Expression.Goto(Goto,Expression.Constant(input)),
                Expression.Add(
                    Expression.Constant(input),
                    Expression.Label(Goto,Expression.Constant(0m))
                )
            )
        );
        //'Cannot jump to non-local label 'L0' with a value. Only jumps to labels defined in outer blocks can pass values.'
        {
            Assert.Throws<InvalidOperationException>(()=>Lambda.Compile());
        }
        //'Common Language Runtime detected an invalid program.'
        {
            var Delegate=this.Optimizer.CreateDelegate(Lambda);
            Assert.Throws<InvalidProgramException>(()=>Delegate());
        }
    }
    [Fact]public void Labelをオペランドに使う方法() {
        const decimal input=2;
        const decimal expected=input+input;
        var Goto = Expression.Label(typeof(decimal),"L0");
        var Lambda=Expression.Lambda<Func<decimal>>(
            Expression.Add(
                Expression.Constant(input),
                Expression.Block(
                    Expression.Goto(Goto,Expression.Constant(input)),
                    Expression.Label(Goto,Expression.Constant(0m))
                )
            )
        );
        {
            var actual=Lambda.Compile()();
            Assert.Equal(expected,actual);
        }
        {
            var actual=this.Optimizer.Execute(Lambda);
            Assert.Equal(expected,actual);
        }
    }
    private static ref int Private左辺値を返すメソッド(ref int input){
        Trace.WriteLine(input.ToString());
        input+=1;
        return ref input;
    }
    [Fact]public void 左辺値を返すメソッド(){
        //const int input=1;
        //const int expected=2;
        //var p=Expression.Parameter(typeof(int));
        //var Call=Expression.Call(
        //    typeof(Expressions).GetMethod(nameof(Private左辺値を返すメソッド),BindingFlags.NonPublic|BindingFlags.Static)!,
        //    p
        //);
        //var Lambda=Expression.Lambda<Func<int,int>>(
        //    Expression.Assign(
        //        Call,
        //        p
        //    ),p
        //);
        //var actual=Lambda.Compile()(input);
        //Assert.Equal(expected,actual);
    }
}
