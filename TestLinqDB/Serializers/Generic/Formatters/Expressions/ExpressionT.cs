using System.Linq.Expressions;
namespace TestLinqDB.Serializers.Generic.Formatters.Expressions;
public abstract class ExpressionT:共通{
    protected ExpressionT(テストオプション テストオプション):base(テストオプション){}
    [Fact]
    public void Serialize()
    {
        //if(writer.TryWriteNil(value)) return;
        var input = Expression.Lambda<Action>(Expression.Default(typeof(void)));
        this.AssertEqual(input);
    }
}
