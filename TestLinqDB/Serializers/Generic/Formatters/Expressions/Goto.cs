using System.Linq.Expressions;
namespace TestLinqDB.Serializers.Generic.Formatters.Expressions;
public abstract class Goto<TSerializer>:共通 where TSerializer:LinqDB.Serializers.Serializer,new(){
    protected Goto():base(new AssertDefinition(new TSerializer())){}
    [Fact]
    public void Serialize(){
        this.AssertEqual(new{a=default(GotoExpression)});
        var target=Expression.Label(typeof(int),"target");
        var input=Expression.MakeGoto(
            GotoExpressionKind.Return,
            target,
            Expression.Constant(5),
            typeof(byte)
        );
        this.AssertEqual(input);
    }
}
