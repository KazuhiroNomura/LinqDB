using Expressions = System.Linq.Expressions;
namespace TestLinqDB.Serializers.Formatters;
public class ListInit:共通{
    [Fact]
    public void Serialize(){
        //if(writer.TryWriteNil(value)) return;
        var input=Expressions.Expression.ListInit(
            Expressions.Expression.New(typeof(List<int>)),
            Expressions.Expression.ElementInit(typeof(List<int>).GetMethod("Add")!,Expressions.Expression.Constant(1))
        );
        this.MemoryMessageJson_T_Assert全パターン(input);
    }
}
