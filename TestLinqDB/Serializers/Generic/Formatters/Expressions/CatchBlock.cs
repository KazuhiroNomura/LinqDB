namespace TestLinqDB.Serializers.Generic.Formatters.Expressions;
using System.Linq.Expressions;
public abstract class CatchBlock<TSerializer>:共通 where TSerializer:LinqDB.Serializers.Serializer,new(){
    protected CatchBlock():base(new AssertDefinition(new TSerializer())){}
    [Fact]
    public void Serialize(){
        var Variable=Expression.Parameter(typeof(Exception));
        //if(writer.TryWriteNil(value)) return;
        //if(value.Variable is null){
        //    if(value.Filter is null){
        this.AssertEqual(
            Expression.Catch(
                typeof(Exception),
                Expression.Default(typeof(void))
            )
        );
        //    } else{
        this.AssertEqual(
            Expression.Catch(
                typeof(Exception),
                Expression.Constant(0),
                Expression.Constant(true)
            )
        );
        //    }
        //} else{
        //    if(value.Filter is null){
        this.AssertEqual(
            Expression.Catch(
                Variable,
                Expression.Default(typeof(void))
            )
        );
        //    } else{
        this.AssertEqual(
            Expression.Catch(
                Variable,
                Expression.Constant(0),
                Expression.Constant(true)
            )
        );
        //    }
        //}

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
