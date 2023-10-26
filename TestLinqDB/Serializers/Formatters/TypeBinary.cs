using Expressions = System.Linq.Expressions;
namespace TestLinqDB.Serializers.Formatters;
public class TypeBinary:共通{
    [Fact]
    public void Serialize(){
        //if(writer.TryWriteNil(value)) return;
        var TypeIs=Expressions.Expression.TypeIs(
            Expressions.Expression.Constant(1m),
            typeof(decimal)
        );
        var TypeEqual=Expressions.Expression.TypeEqual(
            Expressions.Expression.Constant(1m),
            typeof(decimal)
        );
        this.MemoryMessageJson_Expression_Assert全パターン(TypeIs);
        this.MemoryMessageJson_Expression_Assert全パターン(TypeEqual);
    }
}
