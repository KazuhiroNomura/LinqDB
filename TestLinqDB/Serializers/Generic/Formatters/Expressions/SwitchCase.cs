using System.Linq.Expressions;
namespace TestLinqDB.Serializers.Generic.Formatters.Expressions;
public abstract class SwitchCase<TSerializer>:共通 where TSerializer:LinqDB.Serializers.Serializer,new(){
    protected SwitchCase():base(new AssertDefinition(new TSerializer())){}
    [Fact]
    public void Serialize(){
        var input=Expression.SwitchCase(
            Expression.Constant(64m),
            Expression.Constant(124)
        );
        this.AssertEqual(input);
    }
}
