using System.Linq.Expressions;

namespace TestLinqDB.Serializers.Formatters.Expressions;
public class Default : 共通
{
    [Fact]
    public void Serialize()
    {
        //if(writer.TryWriteNil(value)) return;
        this.ExpressionシリアライズAssertEqual(Expression.Default(typeof(void)));
    }
}
