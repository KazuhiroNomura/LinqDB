using Expressions = System.Linq.Expressions;
namespace Serializers.Formatters;
public class TypeBinary : 共通
{
    [Fact]
    public void Serialize()
    {
        //if(writer.TryWriteNil(value)) return;
        this.MemoryMessageJson_Assert(new { a = default(Expressions.TypeBinaryExpression) });
        var TypeIs = Expressions.Expression.TypeIs(
                Expressions.Expression.Constant(1m),
                typeof(decimal)
            );
        var TypeEqual = Expressions.Expression.TypeEqual(
            Expressions.Expression.Constant(1m),
            typeof(decimal)
        );
        this.MemoryMessageJson_Assert(
            new
            {
                TypeIs,
                TypeIsExpression = (Expressions.Expression)TypeIs,
                TypeIsObject = (object)TypeIs,
                TypeEqual,
                TypeEqualExpression = (Expressions.Expression)TypeIs,
                TypeEqualObject = (object)TypeIs,
            }
        );
    }
}
