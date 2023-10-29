using RuntimeBinder = Microsoft.CSharp.RuntimeBinder;

namespace TestLinqDB.Serializers.Generic.Formatters.Expressions;
public abstract class CSharpArgumentInfo:共通{
    protected CSharpArgumentInfo(テストオプション テストオプション):base(テストオプション){}
    [Fact]
    public void Serialize(){
        this.AssertEqual(RuntimeBinder.CSharpArgumentInfo.Create(RuntimeBinder.CSharpArgumentInfoFlags.None,null));
    }
}
