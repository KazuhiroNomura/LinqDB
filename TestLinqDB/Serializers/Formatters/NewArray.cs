using Expressions = System.Linq.Expressions;
namespace TestLinqDB.Serializers.Formatters;
public class NewArray : 共通
{
    [Fact]
    public void Serialize()
    {
        this.MemoryMessageJson_Assert(new { a = default(Expressions.NewArrayExpression) });
        var NewArrayBounds = Expressions.Expression.NewArrayBounds(
                typeof(int),
                Expressions.Expression.Constant(0),
                Expressions.Expression.Constant(1)
            );
        var NewArrayInit = Expressions.Expression.NewArrayInit(
            typeof(int),
            Expressions.Expression.Constant(2),
            Expressions.Expression.Constant(1)
        );
        this.MemoryMessageJson_Assert(
            new
            {
                NewArrayBounds,
                NewArrayBoundsExpression = (Expressions.Expression)NewArrayBounds,
                NewArrayBoundsObject = (object)NewArrayBounds,
                NewArrayInit,
                NewArrayInitExpression = (Expressions.Expression)NewArrayInit,
                NewArrayInitObject = (object)NewArrayInit,
            }
        );
    }
}
