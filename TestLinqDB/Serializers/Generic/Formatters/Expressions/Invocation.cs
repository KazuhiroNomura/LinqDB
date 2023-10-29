using System.Linq.Expressions;
namespace TestLinqDB.Serializers.Generic.Formatters.Expressions;
public abstract class Invocation:共通{
    protected Invocation(テストオプション テストオプション):base(テストオプション){}
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
