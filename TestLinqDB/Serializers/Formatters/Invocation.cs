using Expressions = System.Linq.Expressions;
namespace TestLinqDB.Serializers.Formatters;
public class Invocation:共通{
    [Fact]
    public void Serialize(){
        var @string=Expressions.Expression.Parameter(typeof(string));
        var input=Expressions.Expression.Invoke(
            Expressions.Expression.Lambda(@string,@string),
            Expressions.Expression.Constant("B")
        );
        this.MemoryMessageJson_T_Assert全パターン(input);
    }
}
