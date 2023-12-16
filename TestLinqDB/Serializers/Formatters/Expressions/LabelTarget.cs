using System.Linq.Expressions;

namespace TestLinqDB.Serializers.Formatters.Expressions;
public class LabelTarget : 共通
{
    [Fact]
    public void Serialize()
    {
        var input = Expression.Label();
        this.ObjectシリアライズAssertEqual(input);
    }
}
