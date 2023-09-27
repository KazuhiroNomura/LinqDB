using Expressions = System.Linq.Expressions;
namespace Serializers.Formatters;
public class Switch : 共通
{
    [Fact]
    public void Serialize()
    {
        var input = Expressions.Expression.Switch(
            Expressions.Expression.Constant(123),
            Expressions.Expression.Constant(0m),
            Expressions.Expression.SwitchCase(
                Expressions.Expression.Constant(64m),
                Expressions.Expression.Constant(124)
            )
        );
        this.MemoryMessageJson_Assert(new { a = default(Expressions.SwitchExpression) });
        this.MemoryMessageJson_Assert(
            new
            {
                a = input
            }
        );
        this.MemoryMessageJson_Assert(
            new
            {
                a = (Expressions.Expression)input
            }
        );
    }
}
