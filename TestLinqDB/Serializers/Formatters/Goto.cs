using Expressions = System.Linq.Expressions;
namespace TestLinqDB.Serializers.Formatters;
public class Goto:共通{
    [Fact]
    public void Serialize(){
        this.MemoryMessageJson_T_Assert全パターン(new{a=default(Expressions.GotoExpression)});
        var target=Expressions.Expression.Label(typeof(int),"target");
        var input=Expressions.Expression.MakeGoto(
            Expressions.GotoExpressionKind.Return,
            target,
            Expressions.Expression.Constant(5),
            typeof(byte)
        );
        this.MemoryMessageJson_T_Assert全パターン(input);
    }
}
