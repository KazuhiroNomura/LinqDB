using System.Linq.Expressions;
namespace TestLinqDB.Serializers.Formatters.Expressions;
public class Parameter:共通{
    [Fact]
    public void エラー特定(){
        var p=Expression.Parameter(typeof(int),"p");
        this.ExpressionシリアライズAssertEqual(Expression.Assign(p,Expression.Constant(0)));
        this.Expressionシリアライズ(
            Expression.Lambda<Func<int,object>>(
                Expression.Constant(
                    p
                ),
                p
            )
        );
        this.ExpressionシリアライズAssertEqual(
            Expression.Lambda<Func<int,int>>(
                p,
                p
            )
        );
    }
    [Fact]
    public void Write(){
        var p=Expression.Parameter(typeof(int),"p");
        //if(index0<0){
        //    if(index1<0){
        this.Objectシリアライズ(new object[]{p});
        //    }else{
        this.Objectシリアライズ(new object[]{p,p});
        //    }
        //}else{
        this.ExpressionシリアライズAssertEqual(
            Expression.Lambda<Func<int,int>>(
                p,
                p
            )
        );
        //}
    }
    [Fact]
    public void Serialize(){
        var p=Expression.Parameter(typeof(int));
        //if(index0<0){
        //    if(index1<0){
        this.Objectシリアライズ(new []{p});
        //    }else{
        //this.Objectシリアライズ(new []{p,p});
        this.Objectシリアライズ(new{p,q=p});
        //    }
        //}
        this.ExpressionシリアライズAssertEqual(Expression.Block(new[]{p},p,p));
    }
    [Fact]
    public void Desrialize(){
        //if(reader.TryReadNil()) return;
        var p=Expression.Parameter(typeof(int));
        this.Expressionシリアライズ(p);
        this.Expressionシリアライズ((ParameterExpression?)null);
    }
}
