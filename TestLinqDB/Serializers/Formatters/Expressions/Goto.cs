using System.Linq.Expressions;

namespace TestLinqDB.Serializers.Formatters.Expressions;
public class Goto : 共通
{
    [Fact]
    public void Serialize()
    {
        var target = Expression.Label(typeof(int), "target");
        var input = Expression.MakeGoto(
            GotoExpressionKind.Return,
            target,
            Expression.Constant(5),
            typeof(byte)
        );
        this.ExpressionシリアライズAssertEqual(input);
    }
}
