using System.Linq.Expressions;
namespace TestLinqDB.Serializers.Generic.Formatters.Expressions;
public abstract class Default<TSerializer>:共通 where TSerializer:LinqDB.Serializers.Serializer,new(){
    protected Default():base(new AssertDefinition(new TSerializer())){
    }
    [Fact]
    public void Serialize(){
        //if(writer.TryWriteNil(value)) return;
        this.MemoryMessageJson_Expression_Assert全パターン(Expression.Default(typeof(void)));
    }
}
