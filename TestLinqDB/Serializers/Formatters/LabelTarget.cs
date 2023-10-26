using Expressions = System.Linq.Expressions;
namespace TestLinqDB.Serializers.Formatters;
public class LabelTarget:共通{
    [Fact]
    public void Serialize(){
        var input=Expressions.Expression.Label();
        this.MemoryMessageJson_T_Assert全パターン(input);
    }
}
