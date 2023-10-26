using Expressions = System.Linq.Expressions;
namespace TestLinqDB.Serializers.Formatters;
public class Parameter : 共通
{
    [Fact]
    public void Write()
    {
        var p = Expressions.Expression.Parameter(typeof(int), "p");
        //if(index0<0){
        //    if(index1<0){
        this.MemoryMessageJson_T_Assert全パターン(Expressions.Expression.Block(p));
        //    }else{
        this.MemoryMessageJson_T_Assert全パターン(Expressions.Expression.Block(p,p));
        //    }
        //}else{
        this.MemoryMessageJson_T_Assert全パターン(
            Expressions.Expression.Lambda<Func<int, object>>(
                Expressions.Expression.Constant(
                    new { a = p }
                ),
                p
            )
        );
        //}
    }
    [Fact]
    public void Serialize()
    {
        var input = Expressions.Expression.Parameter(typeof(int));
        //if(index0<0){
        //    if(index1<0){
        //    }else{
        //    }
        this.MemoryMessageJson_Expression_Assert全パターン(input);
        //}else{
        this.MemoryMessageJson_Expression_Assert全パターン(
            Expressions.Expression.Lambda<Func<int, object>>(
                Expressions.Expression.Constant(
                    new { a = input }
                ),
                input
            )
        );
        //}
    }
}
