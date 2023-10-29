//using System.Linq.Expressions;
//using System.Linq.Expressions;
//using System.Linq.Expressions;
//using Binder = Microsoft.CSharp.RuntimeBinder;
//using MessagePack;
namespace TestLinqDB.Serializers.Generic.Formatters.Expressions;
using System.Linq.Expressions;
public abstract class Block:共通{
    protected Block(テストオプション テストオプション):base(テストオプション){}
    [Fact]
    public void Serialize(){
        var input1=Expression.Block(Expression.Constant(1m));
        this.ExpressionAssertEqual(input1);

    }
    [Fact]
    public void Block0(){
        var ParameterDecimmal=Expression.Parameter(typeof(decimal));
        this.ExpressionAssertEqual(
            Expression.Block(
                new[]{ParameterDecimmal},
                Expression.Block(
                    new[]{ParameterDecimmal},
                    ParameterDecimmal
                )
            )
        );
    }
}
