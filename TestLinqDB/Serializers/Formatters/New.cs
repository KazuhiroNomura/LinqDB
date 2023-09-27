using Expressions = System.Linq.Expressions;
namespace Serializers.Formatters;
public class New : 共通
{
    [Fact]
    public void Serialize()
    {
        var input = Expressions.Expression.New(
            typeof(ValueTuple<int>).GetConstructors()[0],
            Expressions.Expression.Constant(1)
        );
        this.MemoryMessageJson_Assert(new { a = default(Expressions.NewExpression) });
        this.MemoryMessageJson_Assert(
            new
            {
                a = input,
                b = (Expressions.Expression)input
            }
        );
    }
}
