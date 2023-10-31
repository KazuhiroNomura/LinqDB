using System.Linq.Expressions;

namespace TestLinqDB.Serializers.Formatters.Expressions;
public class Invocation : 共通
{
    [Fact]
    public void Serialize()
    {
        var @string = Expression.Parameter(typeof(string));
        var input = Expression.Invoke(
            Expression.Lambda(@string, @string),
            Expression.Constant("B")
        );
        this.AssertEqual(input);
    }
}
