using System.Linq.Expressions;
namespace TestLinqDB.Serializers.Generic.Formatters.Expressions;
public abstract class ExpressionT<TSerializer>:共通 where TSerializer:LinqDB.Serializers.Serializer,new(){
    protected ExpressionT():base(new AssertDefinition(new TSerializer())){}
    [Fact]
    public void Serialize()
    {
        //if(writer.TryWriteNil(value)) return;
        var input = Expression.Lambda<Action>(Expression.Default(typeof(void)));
        this.MemoryMessageJson_T_Assert全パターン(input);
    }
}
