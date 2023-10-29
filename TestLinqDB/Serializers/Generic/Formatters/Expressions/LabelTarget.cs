using System.Linq.Expressions;
namespace TestLinqDB.Serializers.Generic.Formatters.Expressions;
public abstract class LabelTarget:共通{
    protected LabelTarget(テストオプション テストオプション):base(テストオプション){}
    [Fact]
    public void Serialize(){
        var input=Expression.Label();
        this.AssertEqual(input);
    }
}
