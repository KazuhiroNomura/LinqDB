//using System.Linq.Expressions;
//using System.Linq.Expressions;
//using Expressions = System.Linq.Expressions;
//using Binder = Microsoft.CSharp.RuntimeBinder;
//using MessagePack;
namespace Serializers.Formatters;
using Expressions = System.Linq.Expressions;
public class Block : 共通
{
    [Fact]
    public void Serialize()
    {
        var Constant1 = Expressions.Expression.Constant(1m);
        var input1 = Expressions.Expression.Block(Constant1);
        this.MemoryMessageJson_Assert(new { a = input1 });
        this.MemoryMessageJson_Assert(new { a = default(Expressions.BlockExpression) });

    }
    [Fact]
    public void Block0()
    {
        var ParameterDecimmal = Expressions.Expression.Parameter(typeof(decimal));
        共通0(
            Expressions.Expression.Block(
                new[] { ParameterDecimmal },
                Expressions.Expression.Block(
                    new[] { ParameterDecimmal },
                    ParameterDecimmal
                )
            )
        );
        void 共通0(Expressions.Expression input)
        {
            this.MemoryMessageJson_Assert(
                input,
                output => Assert.Equal(input, output, this.ExpressionEqualityComparer));
        }
    }
}
