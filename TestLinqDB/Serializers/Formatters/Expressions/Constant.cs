using System.Linq.Expressions;

namespace TestLinqDB.Serializers.Formatters.Expressions;
public class Constant : 共通
{
    [Fact]
    public void Serialize()
    {
        //if(writer.TryWriteNil(value)) return;
        this.AssertEqual(Expression.Constant(true));
    }
}
