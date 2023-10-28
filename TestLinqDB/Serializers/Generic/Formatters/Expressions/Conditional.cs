//using System.Linq.Expressions;
namespace TestLinqDB.Serializers.Generic.Formatters.Expressions;
using System.Linq.Expressions;
public abstract class Conditional<TSerializer>:共通 where TSerializer:LinqDB.Serializers.Serializer,new(){
    protected Conditional():base(new AssertDefinition(new TSerializer())){}
    [Fact]
    public void Serialize(){
        //if(writer.TryWriteNil(value)) return;
        this.ExpressionAssertEqual(
            Expression.Condition(
                Expression.Constant(true),
                Expression.Constant(true),
                Expression.Constant(true)
            )
        );
    }
}
