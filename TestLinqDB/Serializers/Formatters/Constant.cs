using Expressions = System.Linq.Expressions;
namespace Serializers.Formatters;
public class Constant : 共通
{
    [Fact]
    public void Serialize()
    {
        //if(writer.TryWriteNil(value)) return;
        this.MemoryMessageJson_Assert(new { a = default(Expressions.ConstantExpression) });
        this.MemoryMessageJson_Assert(
            new
            {
                a = Expressions.Expression.Constant(true)
            }
        );
    }
}
