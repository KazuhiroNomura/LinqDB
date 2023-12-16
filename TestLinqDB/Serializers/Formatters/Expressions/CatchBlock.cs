namespace TestLinqDB.Serializers.Formatters.Expressions;
using System.Linq.Expressions;
public class CatchBlock : 共通
{
    [Fact]
    public void Serialize()
    {
        var Variable = Expression.Parameter(typeof(Exception));
        //if(writer.TryWriteNil(value)) return;
        //if(value.Variable is null){
        //    if(value.Filter is null){
        this.ObjectシリアライズAssertEqual(
            Expression.Catch(
                typeof(Exception),
                Expression.Default(typeof(void))
            )
        );
        //    } else{
        this.ObjectシリアライズAssertEqual(
            Expression.Catch(
                typeof(Exception),
                Expression.Constant(0),
                Expression.Constant(true)
            )
        );
        //    }
        //} else{
        //    if(value.Filter is null){
        this.ObjectシリアライズAssertEqual(
            Expression.Catch(
                Variable,
                Expression.Default(typeof(void))
            )
        );
        //    } else{
        this.ObjectシリアライズAssertEqual(
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
    public void Block0()
    {
        var ParameterDecimmal = Expression.Parameter(typeof(decimal));
        this.ObjectシリアライズAssertEqual(
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
