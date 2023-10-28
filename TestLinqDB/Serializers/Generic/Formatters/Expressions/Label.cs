using System.Linq.Expressions;
namespace TestLinqDB.Serializers.Generic.Formatters.Expressions;
public abstract class Label<TSerializer>:共通 where TSerializer:LinqDB.Serializers.Serializer,new(){
    protected Label():base(new AssertDefinition(new TSerializer())){}
    [Fact]
    public void Serialize(){
        var labelTarget=Expression.Label();
        //this.MemoryMessageJson_Expression(Expression.Label(labelTarget));
        //this.MemoryMessageJson_Expression(Expression.Label(labelTarget,Expression.Constant(1)));
        var input=Expression.Label(labelTarget);
        this.AssertEqual(input);
    }
}
