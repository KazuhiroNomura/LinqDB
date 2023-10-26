//using System.Linq.Expressions;
//using System.Linq.Expressions;
//using Expressions = System.Linq.Expressions;
//using Binder = Microsoft.CSharp.RuntimeBinder;
//using MessagePack;
namespace TestLinqDB.Serializers.Formatters;
using Expressions = System.Linq.Expressions;
public class Block:共通{
    [Fact]
    public void Serialize(){
        var input1=Expressions.Expression.Block(Expressions.Expression.Constant(1m));
        this.MemoryMessageJson_Expression_Assert全パターン(input1);

    }
    [Fact]
    public void Block0(){
        var ParameterDecimmal=Expressions.Expression.Parameter(typeof(decimal));
        this.MemoryMessageJson_Expression_Assert全パターン(
            Expressions.Expression.Block(
                new[]{ParameterDecimmal},
                Expressions.Expression.Block(
                    new[]{ParameterDecimmal},
                    ParameterDecimmal
                )
            )
        );
    }
}
