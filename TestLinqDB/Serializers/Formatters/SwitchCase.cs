using Expressions = System.Linq.Expressions;
namespace TestLinqDB.Serializers.Formatters;
public class SwitchCase:共通{
    [Fact]
    public void Serialize(){
        var input=Expressions.Expression.SwitchCase(
            Expressions.Expression.Constant(64m),
            Expressions.Expression.Constant(124)
        );
        this.MemoryMessageJson_T_Assert全パターン(input);
    }
}
