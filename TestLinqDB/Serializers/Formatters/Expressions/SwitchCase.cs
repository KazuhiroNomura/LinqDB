using System.Linq.Expressions;

namespace TestLinqDB.Serializers.Formatters.Expressions;
public class SwitchCase : 共通
{
    [Fact]
    public void Serialize()
    {
        var input = Expression.SwitchCase(
            Expression.Constant(64m),
            Expression.Constant(124)
        );
        this.AssertEqual(input);
    }
}
