using Expressions = System.Linq.Expressions;
namespace Serializers.Formatters;
public class Label : 共通
{
    [Fact]
    public void Serialize()
    {
        var labelTarget = Expressions.Expression.Label();
        //this.MemoryMessageJson_Expression(Expressions.Expression.Label(labelTarget));
        //this.MemoryMessageJson_Expression(Expressions.Expression.Label(labelTarget,Expressions.Expression.Constant(1)));
        var input = Expressions.Expression.Label(labelTarget);
        this.MemoryMessageJson_Assert(new { a = default(Expressions.LabelExpression) });
        this.MemoryMessageJson_Assert(
            new
            {
                a = input,
                b = (Expressions.Expression)input
            }
        );
    }
}
