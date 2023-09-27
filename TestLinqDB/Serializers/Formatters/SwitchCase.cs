using Expressions = System.Linq.Expressions;
namespace Serializers.Formatters;
public class SwitchCase : 共通
{
    [Fact]
    public void Serialize()
    {
        var input = Expressions.Expression.SwitchCase(
            Expressions.Expression.Constant(64m),
            Expressions.Expression.Constant(124)
        );
        this.MemoryMessageJson_Assert(new { a = default(Expressions.SwitchCase) });
        this.MemoryMessageJson_Assert(
            new
            {
                a = input
            }
        );
        this.MemoryMessageJson_Assert(
            new
            {
                a = (object)input
            }
        );
    }
}
