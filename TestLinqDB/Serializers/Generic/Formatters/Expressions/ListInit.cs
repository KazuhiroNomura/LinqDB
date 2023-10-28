using System.Linq.Expressions;
namespace TestLinqDB.Serializers.Generic.Formatters.Expressions;
public abstract class ListInit<TSerializer>:共通 where TSerializer:LinqDB.Serializers.Serializer,new(){
    protected ListInit():base(new AssertDefinition(new TSerializer())){}
    [Fact]
    public void Serialize(){
        //if(writer.TryWriteNil(value)) return;
        var input=Expression.ListInit(
            Expression.New(typeof(List<int>)),
            Expression.ElementInit(typeof(List<int>).GetMethod("Add")!,Expression.Constant(1))
        );
        this.MemoryMessageJson_T_Assert全パターン(input);
    }
}
