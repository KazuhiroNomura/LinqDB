using System.Linq.Expressions;
namespace TestLinqDB.Serializers.Generic.Formatters.Expressions;
public abstract class TypeBinary<TSerializer>:共通 where TSerializer:LinqDB.Serializers.Serializer,new(){
    protected TypeBinary():base(new AssertDefinition(new TSerializer())){}
    [Fact]
    public void Serialize(){
        //if(writer.TryWriteNil(value)) return;
        var TypeIs=Expression.TypeIs(
            Expression.Constant(1m),
            typeof(decimal)
        );
        var TypeEqual=Expression.TypeEqual(
            Expression.Constant(1m),
            typeof(decimal)
        );
        this.MemoryMessageJson_Expression_Assert全パターン(TypeIs);
        this.MemoryMessageJson_Expression_Assert全パターン(TypeEqual);
    }
}
