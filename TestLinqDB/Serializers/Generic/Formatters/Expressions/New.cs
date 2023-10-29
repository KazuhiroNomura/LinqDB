using System.Linq.Expressions;
namespace TestLinqDB.Serializers.Generic.Formatters.Expressions;
public abstract class New:共通{
    protected New(テストオプション テストオプション):base(テストオプション){}
    [Fact]public void Write(){
        //if(value.Constructor is null){
        {
            var input=Expression.New(
                typeof(int)
            );
            //this.Memory_Assert(new{input});
            this.AssertEqual(input);
        }
        //} else{
        {
            var input=Expression.New(
                typeof(ValueTuple<int>).GetConstructors()[0],
                Expression.Constant(1)
            );
            this.AssertEqual(input);
        }
        //}
    }
    [Fact]public void Serialize(){
        var input = Expression.New(
            typeof(ValueTuple<int>).GetConstructors()[0],
            Expression.Constant(1)
        );
        this.AssertEqual(input);
    }
}
