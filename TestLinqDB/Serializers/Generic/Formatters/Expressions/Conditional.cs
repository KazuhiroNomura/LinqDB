//using System.Linq.Expressions;
namespace TestLinqDB.Serializers.Generic.Formatters.Expressions;
using System.Linq.Expressions;
public abstract class Conditional:共通{
    protected Conditional(テストオプション テストオプション):base(テストオプション){}
    [Fact]
    public void Serialize(){
        //if(writer.TryWriteNil(value)) return;
        this.ExpressionAssertEqual(
            Expression.Condition(
                Expression.Constant(true),
                Expression.Constant(true),
                Expression.Constant(true)
            )
        );
    }
}
