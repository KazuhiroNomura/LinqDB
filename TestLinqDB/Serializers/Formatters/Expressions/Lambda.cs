using System.Linq.Expressions;

namespace TestLinqDB.Serializers.Formatters.Expressions;
public class Lambda : 共通
{
    [Fact]
    public void Serialize()
    {
        //if(writer.TryWriteNil(value)) return;
        this.ExpressionAssertEqual(Expression.Lambda<Action>(Expression.Default(typeof(void))));
    }
}

