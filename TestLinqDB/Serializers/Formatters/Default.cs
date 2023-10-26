using Expressions = System.Linq.Expressions;
namespace TestLinqDB.Serializers.Formatters;
public class Default : 共通
{
    [Fact]
    public void Serialize()
    {
        //if(writer.TryWriteNil(value)) return;
        this.MemoryMessageJson_Expression_Assert全パターン(Expressions.Expression.Default(typeof(void)));
    }
}
