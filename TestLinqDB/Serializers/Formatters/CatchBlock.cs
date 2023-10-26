namespace TestLinqDB.Serializers.Formatters;
using Expressions = System.Linq.Expressions;
public class CatchBlock:共通{
    [Fact]
    public void Serialize(){
        var Variable=Expressions.Expression.Parameter(typeof(Exception));
        //if(writer.TryWriteNil(value)) return;
        //if(value.Variable is null){
        //    if(value.Filter is null){
        this.MemoryMessageJson_T_Assert全パターン(
            Expressions.Expression.Catch(
                typeof(Exception),
                Expressions.Expression.Default(typeof(void))
            )
        );
        //    } else{
        this.MemoryMessageJson_T_Assert全パターン(
            Expressions.Expression.Catch(
                typeof(Exception),
                Expressions.Expression.Constant(0),
                Expressions.Expression.Constant(true)
            )
        );
        //    }
        //} else{
        //    if(value.Filter is null){
        this.MemoryMessageJson_T_Assert全パターン(
            Expressions.Expression.Catch(
                Variable,
                Expressions.Expression.Default(typeof(void))
            )
        );
        //    } else{
        this.MemoryMessageJson_T_Assert全パターン(
            Expressions.Expression.Catch(
                Variable,
                Expressions.Expression.Constant(0),
                Expressions.Expression.Constant(true)
            )
        );
        //    }
        //}

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
