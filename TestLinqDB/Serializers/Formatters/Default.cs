using Expressions = System.Linq.Expressions;
namespace Serializers.Formatters;
public class Default : 共通
{
    [Fact]
    public void Serialize()
    {
        //if(writer.TryWriteNil(value)) return;
        this.MemoryMessageJson_Assert(new { a = default(Expressions.DefaultExpression) });
        var input = Expressions.Expression.Default(typeof(void));
        this.MemoryMessageJson_Assert(
            new
            {
                a = input,
                b = (Expressions.Expression)input
            }
        );
    }
}
