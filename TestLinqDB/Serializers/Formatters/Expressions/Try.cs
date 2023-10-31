using System.Linq.Expressions;

namespace TestLinqDB.Serializers.Formatters.Expressions;
public class Try : 共通
{
    [Fact]
    public void PrivateWrite_Read()
    {
        //if(@finally is not null){
        //    if(handlers.Length>0) {
        {
            var input = Expression.Lambda<Func<int>>(
                Expression.TryCatchFinally(
                    Expression.Constant(0),
                    Expression.Default(typeof(int)),
                    Expression.Catch(
                        typeof(Exception),
                        Expression.Constant(1)
                    )
                )
            );
            this.ExpressionAssertEqual(input);
        }
        //    } else {
        {
            var input = Expression.Lambda<Func<int>>(
                Expression.TryFinally(
                    Expression.Constant(0),
                    Expression.Default(typeof(int))
                )
            );
            this.ExpressionAssertEqual(input);
        }
        //    }
        //}else{
        //    if(fault is not null){
        {
            var input = Expression.TryFault(
                Expression.Constant(0),
                Expression.Constant(1)
            );
            this.ExpressionAssertEqual(input);
        }
        //    } else{
        {
            var input = Expression.TryCatch(
                Expression.Constant(0),
                Expression.Catch(
                    typeof(Exception),
                    Expression.Constant(0)
                )
            );
            this.ExpressionAssertEqual(input);
        }
        //    }
        //}
    }
    [Fact]
    public void Serialize_Deserialize()
    {
        //if(writer.TryWriteNil(value)) return;
        var input = Expression.TryCatch(
            Expression.Constant(0),
            Expression.Catch(
                typeof(Exception),
                Expression.Constant(0)
            )
        );
        this.ExpressionAssertEqual(input);
    }
    [Fact]
    public void PrivateWrite()
    {
        //if(value.Finally is not null){
        //}else{
        var input = Expression.TryCatch(
            Expression.Constant(0),
            Expression.Catch(
                typeof(Exception),
                Expression.Constant(0)
            )
        );
        this.ExpressionAssertEqual(input);
    }
}

