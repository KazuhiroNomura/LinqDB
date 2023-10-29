using System.Linq.Expressions;
namespace TestLinqDB.Serializers.Generic.Formatters.Expressions;
public abstract class NewArray:共通{
    protected NewArray(テストオプション テストオプション):base(テストオプション){}
    [Fact]
    public void Serialize(){
        var NewArrayBounds=Expression.NewArrayBounds(
            typeof(int),
            Expression.Constant(0),
            Expression.Constant(1)
        );
        var NewArrayInit=Expression.NewArrayInit(
            typeof(int),
            Expression.Constant(2),
            Expression.Constant(1)
        );
        this.AssertEqual(NewArrayBounds);
        this.AssertEqual(NewArrayInit);
    }
}
