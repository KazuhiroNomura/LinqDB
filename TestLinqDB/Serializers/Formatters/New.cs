using TestLinqDB.Sets;

using Expressions = System.Linq.Expressions;
namespace TestLinqDB.Serializers.Formatters;
public class New : 共通{
    [Fact]public void Write(){
        //if(value.Constructor is null){
        {
            var input=Expressions.Expression.New(
                typeof(int)
            );
            //this.Memory_Assert(new{input});
            this.MemoryMessageJson_T_Assert全パターン(input);
        }
        //} else{
        {
            var input=Expressions.Expression.New(
                typeof(ValueTuple<int>).GetConstructors()[0],
                Expressions.Expression.Constant(1)
            );
            this.MemoryMessageJson_T_Assert全パターン(input);
        }
        //}
    }
    [Fact]public void Serialize(){
        var input = Expressions.Expression.New(
            typeof(ValueTuple<int>).GetConstructors()[0],
            Expressions.Expression.Constant(1)
        );
        this.MemoryMessageJson_T_Assert全パターン(input);
    }
}
