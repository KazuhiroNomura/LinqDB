using System.Linq.Expressions;
namespace TestLinqDB.Serializers.Generic.Formatters.Expressions;
public abstract class Constant<TSerializer>:共通 where TSerializer:LinqDB.Serializers.Serializer,new(){
    protected Constant():base(new AssertDefinition(new TSerializer())){}
    [Fact]
    public void Serialize(){
        //if(writer.TryWriteNil(value)) return;
        this.ExpressionAssertEqual(Expression.Constant(true));
    }
}
