using System.Linq.Expressions;
namespace TestLinqDB.Serializers.Generic.Formatters.Expressions;
public abstract class TypeBinary:共通{
    protected TypeBinary(テストオプション テストオプション):base(テストオプション){}
    [Fact]
    public void Serialize(){
        //if(writer.TryWriteNil(value)) return;
        var TypeIs=Expression.TypeIs(
            Expression.Constant(1m),
            typeof(decimal)
        );
        var TypeEqual=Expression.TypeEqual(
            Expression.Constant(1m),
            typeof(decimal)
        );
        this.ExpressionAssertEqual(TypeIs);
        this.ExpressionAssertEqual(TypeEqual);
    }
}
