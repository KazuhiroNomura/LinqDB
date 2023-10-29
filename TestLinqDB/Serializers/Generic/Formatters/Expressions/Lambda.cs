using System.Linq.Expressions;
namespace TestLinqDB.Serializers.Generic.Formatters.Expressions;
public abstract class Lambda:共通{
    protected Lambda(テストオプション テストオプション):base(テストオプション){}
    [Fact]
    public void Serialize(){
        //if(writer.TryWriteNil(value)) return;
        this.ExpressionAssertEqual<LambdaExpression>(Expression.Lambda<Action>(Expression.Default(typeof(void))));
    }
}

