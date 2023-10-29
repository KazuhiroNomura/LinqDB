using System.Linq.Expressions;
namespace TestLinqDB.Serializers.Generic.Formatters.Expressions;
public abstract class SwitchCase:共通{
    protected SwitchCase(テストオプション テストオプション):base(テストオプション){}
    [Fact]
    public void Serialize(){
        var input=Expression.SwitchCase(
            Expression.Constant(64m),
            Expression.Constant(124)
        );
        this.AssertEqual(input);
    }
}
