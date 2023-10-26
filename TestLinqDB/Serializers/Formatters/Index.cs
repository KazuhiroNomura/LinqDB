using Expressions = System.Linq.Expressions;
namespace TestLinqDB.Serializers.Formatters;
public class Index:共通{
    [Fact]
    public void Serialize(){
        var List=Expressions.Expression.Parameter(typeof(List<int>));
        this.MemoryMessageJson_T_Assert全パターン(new{a=default(Expressions.IndexExpression)});
        var input=Expressions.Expression.MakeIndex(
            List,
            typeof(List<int>).GetProperty("Item"),
            new[]{Expressions.Expression.Constant(0)}
        );
        this.MemoryMessageJson_T_Assert全パターン(input);
    }
}
