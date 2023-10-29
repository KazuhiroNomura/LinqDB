using System.Linq.Expressions;
namespace TestLinqDB.Serializers.Generic.Formatters.Expressions;
public abstract class ListInit:共通{
    protected ListInit(テストオプション テストオプション):base(テストオプション){}
    [Fact]
    public void Serialize(){
        //if(writer.TryWriteNil(value)) return;
        var input=Expression.ListInit(
            Expression.New(typeof(List<int>)),
            Expression.ElementInit(typeof(List<int>).GetMethod("Add")!,Expression.Constant(1))
        );
        this.AssertEqual(input);
    }
}
