using RuntimeBinder = Microsoft.CSharp.RuntimeBinder;

namespace TestLinqDB.Serializers.Formatters.Expressions;
public class CSharpArgumentInfo : 共通
{
    [Fact]
    public void Serialize()
    {
        this.AssertEqual(RuntimeBinder.CSharpArgumentInfo.Create(RuntimeBinder.CSharpArgumentInfoFlags.None, null));
    }
}
