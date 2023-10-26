using Expressions = System.Linq.Expressions;
namespace TestLinqDB.Serializers.Formatters;
public class NewArray:共通{
    [Fact]
    public void Serialize(){
        var NewArrayBounds=Expressions.Expression.NewArrayBounds(
            typeof(int),
            Expressions.Expression.Constant(0),
            Expressions.Expression.Constant(1)
        );
        var NewArrayInit=Expressions.Expression.NewArrayInit(
            typeof(int),
            Expressions.Expression.Constant(2),
            Expressions.Expression.Constant(1)
        );
        this.MemoryMessageJson_T_Assert全パターン(NewArrayBounds);
        this.MemoryMessageJson_T_Assert全パターン(NewArrayInit);
    }
}
