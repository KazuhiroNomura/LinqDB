using System.Linq.Expressions;

namespace TestLinqDB.Serializers.Formatters.Expressions;
public class Label : 共通
{
    [Fact]
    public void Serialize()
    {
        var labelTarget = Expression.Label();
        //this.MemoryMessageJson_Expression(Expression.Label(labelTarget));
        //this.MemoryMessageJson_Expression(Expression.Label(labelTarget,Expression.Constant(1)));
        var input = Expression.Label(labelTarget);
        this.AssertEqual(input);
    }
}
