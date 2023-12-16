using System.Linq.Expressions;

namespace TestLinqDB.Serializers.Formatters.Expressions;
public class Index : 共通
{
    [Fact]
    public void Serialize()
    {
        var List = Expression.Parameter(typeof(List<int>));
        var input = Expression.Block(
            new[] { List },
            Expression.MakeIndex(
                List,
                typeof(List<int>).GetProperty("Item"),
                new[] { Expression.Constant(0) }
            )
        );
        this.ExpressionシリアライズAssertEqual(input);
    }
}
