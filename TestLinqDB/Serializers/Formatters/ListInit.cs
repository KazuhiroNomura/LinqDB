using Expressions = System.Linq.Expressions;
namespace Serializers.Formatters;
public class ListInit : 共通
{
    [Fact]
    public void Serialize()
    {
        //if(writer.TryWriteNil(value)) return;
        this.MemoryMessageJson_Assert(new { a = default(Expressions.ListInitExpression) });
        var input = Expressions.Expression.ListInit(
            Expressions.Expression.New(typeof(List<int>)),
            Expressions.Expression.ElementInit(typeof(List<int>).GetMethod("Add")!, Expressions.Expression.Constant(1))
        );
        this.MemoryMessageJson_Assert(
            new
            {
                a = input,
                b = (Expressions.Expression)input
            }
        );
    }
}
