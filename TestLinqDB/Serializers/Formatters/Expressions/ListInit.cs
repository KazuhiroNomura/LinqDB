using System.Linq.Expressions;

namespace TestLinqDB.Serializers.Formatters.Expressions;
public class ListInit : 共通
{
    [Fact]
    public void Serialize()
    {
        //if(writer.TryWriteNil(value)) return;
        var input = Expression.ListInit(
            Expression.New(typeof(List<int>)),
            Expression.ElementInit(typeof(List<int>).GetMethod("Add")!, Expression.Constant(1))
        );
        this.ExpressionシリアライズAssertEqual(input);
    }
}
