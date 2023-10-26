using Expressions = System.Linq.Expressions;
namespace TestLinqDB.Serializers.Formatters;
public class ExpressionT : 共通
{
    [Fact]
    public void Serialize()
    {
        //if(writer.TryWriteNil(value)) return;
        var input = Expressions.Expression.Lambda<Action>(Expressions.Expression.Default(typeof(void)));
        this.MemoryMessageJson_T_Assert全パターン(input);
    }
}
