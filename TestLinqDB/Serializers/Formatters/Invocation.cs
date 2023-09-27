using Expressions = System.Linq.Expressions;
namespace Serializers.Formatters;
public class Invocation : 共通
{
    [Fact]
    public void Serialize()
    {
        var @string = Expressions.Expression.Parameter(typeof(string));
        var input = Expressions.Expression.Invoke(
            Expressions.Expression.Lambda(@string, @string),
            Expressions.Expression.Constant("B")
        );
        this.MemoryMessageJson_Assert(new { a = default(Expressions.InvocationExpression) });
        this.MemoryMessageJson_Assert(
            new
            {
                a = input,
                b = (Expressions.Expression)input
            }
        );
    }
}
