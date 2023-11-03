//using System.Linq.Expressions;
//using System.Linq.Expressions;
//using System.Linq.Expressions;
//using Binder = Microsoft.CSharp.RuntimeBinder;
//using MessagePack;

namespace TestLinqDB.Serializers.Formatters.Expressions;
using System.Linq.Expressions;
public class Block : 共通
{
    [Fact]
    public void Serialize()
    {
        var input1 = Expression.Block(Expression.Constant(1m));
        this.ExpressionシリアライズAssertEqual(input1);

    }
    [Fact]
    public void Block0()
    {
        var ParameterDecimmal = Expression.Parameter(typeof(decimal));
        this.ExpressionシリアライズAssertEqual(
            Expression.Block(
                new[] { ParameterDecimmal },
                Expression.Block(
                    new[] { ParameterDecimmal },
                    ParameterDecimmal
                )
            )
        );
    }
}
