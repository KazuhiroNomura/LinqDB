using System.Linq.Expressions;
namespace TestLinqDB.Serializers.Generic.Formatters.Expressions;
public abstract class LabelTarget<TSerializer>:共通 where TSerializer:LinqDB.Serializers.Serializer,new(){
    protected LabelTarget():base(new AssertDefinition(new TSerializer())){}
    [Fact]
    public void Serialize(){
        var input=Expression.Label();
        this.AssertEqual(input);
    }
}
