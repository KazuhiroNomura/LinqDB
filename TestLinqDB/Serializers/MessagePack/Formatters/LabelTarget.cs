using System.Diagnostics;
using System.Drawing;
using Reflection = System.Reflection;
using System.Runtime.CompilerServices;
using Expressions = System.Linq.Expressions;
using RuntimeBinder=Microsoft.CSharp.RuntimeBinder;
namespace Serializers.MessagePack.Formatters;
using Sets;
public class LabelTarget:共通 {
    [Fact]public void Serialize(){
        var input=Expressions.Expression.Label();
        this.MemoryMessageJson_Assert(new{a=default(Expressions.LabelTarget)},output=>{});
        this.MemoryMessageJson_Assert(
            new{
                a=input,b=(object)input
            },output=>{}
        );
    }
}
