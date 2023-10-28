using System.Linq.Expressions;
namespace TestLinqDB.Serializers.Generic.Formatters.Expressions;
public abstract class Invocation<TSerializer>:共通 where TSerializer:LinqDB.Serializers.Serializer,new(){
    protected Invocation():base(new AssertDefinition(new TSerializer())){}
    [Fact]
    public void Serialize(){
        var @string=Expression.Parameter(typeof(string));
        var input=Expression.Invoke(
            Expression.Lambda(@string,@string),
            Expression.Constant("B")
        );
        this.AssertEqual(input);
    }
}
