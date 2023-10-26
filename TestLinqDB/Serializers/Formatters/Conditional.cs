//using System.Linq.Expressions;
namespace TestLinqDB.Serializers.Formatters;
using Expressions = System.Linq.Expressions;
public class Conditional:共通{
    [Fact]
    public void Serialize(){
        //if(writer.TryWriteNil(value)) return;
        this.MemoryMessageJson_Expression_Assert全パターン(
            Expressions.Expression.Condition(
                Expressions.Expression.Constant(true),
                Expressions.Expression.Constant(true),
                Expressions.Expression.Constant(true)
            )
        );
    }
}
