using System.Linq.Expressions;

namespace TestLinqDB.Serializers.Formatters.Expressions;
public class ExpressionT : 共通
{
    [Fact]
    public void Serialize()
    {
        //if(writer.TryWriteNil(value)) return;
        var input = Expression.Lambda<Action>(Expression.Default(typeof(void)));
        this.ExpressionシリアライズAssertEqual(input);
    }
}
