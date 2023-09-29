using Expressions = System.Linq.Expressions;
namespace TestLinqDB.Serializers.Formatters;
public class Try : 共通
{
    [Fact]
    public void Serialize()
    {
        //if(writer.TryWriteNil(value)) return;
        this.MemoryMessageJson_Assert(new { a = default(Expressions.TryExpression) });
        var input = Expressions.Expression.TryCatch(
            Expressions.Expression.Constant(0),
            Expressions.Expression.Catch(
                typeof(Exception),
                Expressions.Expression.Constant(0)
            )
        );
        this.MemoryMessageJson_Assert(
            new
            {
                a = input,
                b = (Expressions.Expression)input,
                c = (object)input
            }
        );
    }
}
