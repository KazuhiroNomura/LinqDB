using System.Linq.Expressions;
namespace TestLinqDB.Serializers.Generic.Formatters.Expressions;
public abstract class Switch<TSerializer>:共通 where TSerializer:LinqDB.Serializers.Serializer,new(){
    protected Switch():base(new AssertDefinition(new TSerializer())){}
    [Fact]
    public void Serialize(){
        var input=Expression.Switch(
            Expression.Constant(123),
            Expression.Constant(0m),
            Expression.SwitchCase(
                Expression.Constant(64m),
                Expression.Constant(124)
            )
        );
        this.AssertEqual(input);
    }
}
