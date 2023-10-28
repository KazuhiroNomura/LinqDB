using System.Linq.Expressions;
namespace TestLinqDB.Serializers.Generic.Formatters.Expressions;
public abstract class New<TSerializer>:共通 where TSerializer:LinqDB.Serializers.Serializer,new(){
    protected New():base(new AssertDefinition(new TSerializer())){}
    [Fact]public void Write(){
        //if(value.Constructor is null){
        {
            var input=Expression.New(
                typeof(int)
            );
            //this.Memory_Assert(new{input});
            this.MemoryMessageJson_T_Assert全パターン(input);
        }
        //} else{
        {
            var input=Expression.New(
                typeof(ValueTuple<int>).GetConstructors()[0],
                Expression.Constant(1)
            );
            this.MemoryMessageJson_T_Assert全パターン(input);
        }
        //}
    }
    [Fact]public void Serialize(){
        var input = Expression.New(
            typeof(ValueTuple<int>).GetConstructors()[0],
            Expression.Constant(1)
        );
        this.MemoryMessageJson_T_Assert全パターン(input);
    }
}
