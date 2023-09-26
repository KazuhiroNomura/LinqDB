using System.Diagnostics;
using System.Drawing;
using Reflection = System.Reflection;
using System.Runtime.CompilerServices;
using Expressions = System.Linq.Expressions;
using RuntimeBinder=Microsoft.CSharp.RuntimeBinder;
namespace Serializers.MessagePack.Formatters;
using Sets;
public class SwitchCase:共通 {
    [Fact]public void Serialize(){
        var input=Expressions.Expression.SwitchCase(
            Expressions.Expression.Constant(64m),
            Expressions.Expression.Constant(124)
        );
        this.MemoryMessageJson_Assert(new{a=default(Expressions.SwitchCase)},output=>{});
        this.MemoryMessageJson_Assert(
            new{
                a=input
            },output=>{}
        );
        this.MemoryMessageJson_Assert(
            new{
                a=(object)input
            },output=>{}
        );
    }
}
