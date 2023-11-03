//using System.Linq.Expressions;

namespace TestLinqDB.Serializers.Formatters.Expressions;
using System.Linq.Expressions;
public class Conditional : 共通
{
    [Fact]
    public void Serialize()
    {
        //if(writer.TryWriteNil(value)) return;
        this.ExpressionシリアライズAssertEqual(
            Expression.Condition(
                Expression.Constant(true),
                Expression.Constant(true),
                Expression.Constant(true)
            )
        );
    }
}
