//using System.Linq.Expressions;
//using System.Linq.Expressions;
//using System.Linq.Expressions;
//using Binder = Microsoft.CSharp.RuntimeBinder;
//using MessagePack;
namespace TestLinqDB.Serializers.Generic.Formatters.Expressions;
using System.Linq.Expressions;
public abstract class Block<TSerializer>:共通 where TSerializer:LinqDB.Serializers.Serializer,new(){
    protected Block():base(new AssertDefinition(new TSerializer())){}
    [Fact]
    public void Serialize(){
        var input1=Expression.Block(Expression.Constant(1m));
        this.MemoryMessageJson_Expression_Assert全パターン(input1);

    }
    [Fact]
    public void Block0(){
        var ParameterDecimmal=Expression.Parameter(typeof(decimal));
        this.MemoryMessageJson_Expression_Assert全パターン(
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
