using Expressions = System.Linq.Expressions;
using RuntimeBinder = Microsoft.CSharp.RuntimeBinder;

namespace TestLinqDB.Serializers.Formatters;
public class CSharpArgumentInfo:共通{
    [Fact]
    public void Serialize(){
        this.MemoryMessageJson_T_Assert全パターン(RuntimeBinder.CSharpArgumentInfo.Create(RuntimeBinder.CSharpArgumentInfoFlags.None,null));
    }
}
