using System.Linq.Expressions;
namespace TestLinqDB.Serializers.Generic.Formatters.Expressions;
public abstract class Lambda<TSerializer>:共通 where TSerializer:LinqDB.Serializers.Serializer,new(){
    protected Lambda():base(new AssertDefinition(new TSerializer())){}
    [Fact]
    public void Serialize(){
        //if(writer.TryWriteNil(value)) return;
        this.ExpressionAssertEqual<LambdaExpression>(Expression.Lambda<Action>(Expression.Default(typeof(void))));
    }
}

