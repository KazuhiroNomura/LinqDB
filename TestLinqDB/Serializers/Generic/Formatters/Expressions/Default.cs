using System.Linq.Expressions;
namespace TestLinqDB.Serializers.Generic.Formatters.Expressions;
public abstract class Default:共通{
    protected Default(テストオプション テストオプション):base(テストオプション){}
    [Fact]
    public void Serialize(){
        //if(writer.TryWriteNil(value)) return;
        this.ExpressionAssertEqual(Expression.Default(typeof(void)));
    }
}
