using Expressions = System.Linq.Expressions;
namespace TestLinqDB.Serializers.Formatters;
public class Switch:共通{
    [Fact]
    public void Serialize(){
        var input=Expressions.Expression.Switch(
            Expressions.Expression.Constant(123),
            Expressions.Expression.Constant(0m),
            Expressions.Expression.SwitchCase(
                Expressions.Expression.Constant(64m),
                Expressions.Expression.Constant(124)
            )
        );
        this.MemoryMessageJson_T_Assert全パターン(input);
    }
}
