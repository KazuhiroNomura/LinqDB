using System.Reflection.PortableExecutable;

using Expressions = System.Linq.Expressions;
namespace TestLinqDB.Serializers.Formatters;
public class Try:共通{
    [Fact]public void PrivateWrite_Read(){
        //if(@finally is not null){
        //    if(handlers.Length>0) {
        {
            var input=Expressions.Expression.Lambda<Func<int>>(
                Expressions.Expression.TryCatchFinally(
                    Expressions.Expression.Constant(0),
                    Expressions.Expression.Default(typeof(int)),
                    Expressions.Expression.Catch(
                        typeof(Exception),
                        Expressions.Expression.Constant(1)
                    )
                )
            );
            this.MemoryMessageJson_Expression_Assert全パターン(input);
        }
        //    } else {
        {
            var input=Expressions.Expression.Lambda<Func<int>>(
                Expressions.Expression.TryFinally(
                    Expressions.Expression.Constant(0),
                    Expressions.Expression.Default(typeof(int))
                )
            );
            this.MemoryMessageJson_Expression_Assert全パターン(input);
        }
        //    }
        //}else{
        //    if(fault is not null){
        {
            var input=Expressions.Expression.TryFault(
                Expressions.Expression.Constant(0),
                Expressions.Expression.Constant(1)
            );
            this.MemoryMessageJson_Expression_Assert全パターン(input);
        }
        //    } else{
        {
            var input=Expressions.Expression.TryCatch(
                Expressions.Expression.Constant(0),
                Expressions.Expression.Catch(
                    typeof(Exception),
                    Expressions.Expression.Constant(0)
                )
            );
            this.MemoryMessageJson_Expression_Assert全パターン(input);
        }
        //    }
        //}
    }
    [Fact]public void Serialize_Deserialize(){
        //if(writer.TryWriteNil(value)) return;
        var input=Expressions.Expression.TryCatch(
            Expressions.Expression.Constant(0),
            Expressions.Expression.Catch(
                typeof(Exception),
                Expressions.Expression.Constant(0)
            )
        );
        this.MemoryMessageJson_Expression_Assert全パターン(input);
    }
    [Fact]public void PrivateWrite(){
        //if(value.Finally is not null){
        //}else{
        var input=Expressions.Expression.TryCatch(
            Expressions.Expression.Constant(0),
            Expressions.Expression.Catch(
                typeof(Exception),
                Expressions.Expression.Constant(0)
            )
        );
        this.MemoryMessageJson_Expression_Assert全パターン(input);
    }
}

