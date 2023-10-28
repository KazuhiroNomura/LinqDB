using System.Linq.Expressions;
namespace TestLinqDB.Serializers.Generic.Formatters.Expressions;
public abstract class Index<TSerializer>:共通 where TSerializer:LinqDB.Serializers.Serializer,new(){
    protected Index():base(new AssertDefinition(new TSerializer())){}
    [Fact]
    public void Serialize(){
        var List=Expression.Parameter(typeof(List<int>));
        this.AssertEqual(new{a=default(IndexExpression)});
        var input=Expression.Block(
            new[]{List},
            Expression.MakeIndex(
                List,
                typeof(List<int>).GetProperty("Item"),
                new[]{Expression.Constant(0)}
            )
        );
        this.AssertEqual(input);
    }
}
