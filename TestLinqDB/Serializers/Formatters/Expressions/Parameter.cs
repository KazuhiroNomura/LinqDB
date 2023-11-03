using System.Linq.Expressions;

using Types;

namespace TestLinqDB.Serializers.Formatters.Expressions;
public class Parameter:共通{
    [Fact]
    public void エラー特定(){
        var p=Expression.Parameter(typeof(int),"p");
        this.AssertEqual(Expression.Assign(p,Expression.Constant(0)));
        this.ExpressionシリアライズCoverage(
            Expression.Lambda<Func<int,object>>(
                Expression.Constant(
                    p
                ),
                p
            )
        );
        this.ExpressionシリアライズCoverage(
            Expression.Lambda<Func<int,object>>(
                Expression.Constant(
                    new{p}
                ),
                p
            )
        );
        this.AssertEqual(
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
        this.AssertEqual(new object[]{p},_=>{});
        //    }else{
        this.AssertEqual(new object[]{p,p},_=>{});
        //    }
        //}else{
        this.AssertEqual(
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
        this.Coverage(new []{p});
        //    }else{
        this.Coverage(new []{p,p});
        //this.AssertEqual(Expression.Block(new[]{p},p,p));
        //this.AssertEqual(new{p,q=p});
        //    }
        //}else{
        this.ExpressionシリアライズCoverage(
            Expression.Lambda<Func<int,object>>(
                Expression.Constant(
                    new{p}
                ),
                p
            )
        );
        //}
    }
}
