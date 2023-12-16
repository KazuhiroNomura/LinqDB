using System.Linq.Expressions;

namespace TestLinqDB.Serializers.Formatters.Expressions;
public class Switch : 共通
{
    [Fact]
    public void Serialize()
    {
        var input = Expression.Switch(
            Expression.Constant(123),
            Expression.Constant(0m),
            Expression.SwitchCase(
                Expression.Constant(64m),
                Expression.Constant(124)
            )
        );
        this.ExpressionシリアライズAssertEqual(input);
    }
}
