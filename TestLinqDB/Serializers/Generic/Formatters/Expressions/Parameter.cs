using System.Linq.Expressions;
namespace TestLinqDB.Serializers.Generic.Formatters.Expressions;
public abstract class Parameter:共通{
    protected Parameter(テストオプション テストオプション):base(テストオプション){}
    [Fact]
    public void Write()
    {
        var p = Expression.Parameter(typeof(int), "p");
        //if(index0<0){
        //    if(index1<0){
        this.AssertEqual(Expression.Block(p));
        //    }else{
        this.AssertEqual(Expression.Block(p,p));
        //    }
        //}else{
        this.AssertEqual(
            Expression.Lambda<Func<int, object>>(
                Expression.Constant(
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
        var input = Expression.Parameter(typeof(int));
        //if(index0<0){
        //    if(index1<0){
        //    }else{
        //    }
        this.ExpressionAssertEqual(input);
        //}else{
        this.ExpressionAssertEqual(
            Expression.Lambda<Func<int, object>>(
                Expression.Constant(
                    new { a = input }
                ),
                input
            )
        );
        //}
    }
}
