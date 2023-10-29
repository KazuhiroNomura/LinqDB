

namespace TestLinqDB.Serializers.Generic.Formatters.Others;
public abstract class DisplayClass:共通{
    protected DisplayClass(テストオプション テストオプション):base(テストオプション){}
    [Fact]public void Test(){
        this.AssertEqual(ClassDisplay取得());
    }
}
