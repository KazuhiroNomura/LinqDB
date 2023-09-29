using Expressions = System.Linq.Expressions;
using RuntimeBinder = Microsoft.CSharp.RuntimeBinder;

namespace TestLinqDB.Serializers.Formatters;
public class CSharpArgumentInfo : 共通
{
    [Fact]
    public void Serialize()
    {
        //if(writer.TryWriteNil(value)) return;
        this.MemoryMessageJson_Assert(new { a = default(RuntimeBinder.CSharpArgumentInfo) });
        this.MemoryMessageJson_Assert(
            new
            {
                a = Expressions.Expression.Constant(RuntimeBinder.CSharpArgumentInfo.Create(RuntimeBinder.CSharpArgumentInfoFlags.None, null))
            }
        );
    }
}
