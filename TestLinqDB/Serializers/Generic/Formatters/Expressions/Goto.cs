using System.Linq.Expressions;
namespace TestLinqDB.Serializers.Generic.Formatters.Expressions;
public abstract class Goto:共通{
    protected Goto(テストオプション テストオプション):base(テストオプション){}
    [Fact]
    public void Serialize(){
        var target=Expression.Label(typeof(int),"target");
        var input=Expression.MakeGoto(
            GotoExpressionKind.Return,
            target,
            Expression.Constant(5),
            typeof(byte)
        );
        this.AssertEqual(input);
    }
}
