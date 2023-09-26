using System.Diagnostics;
//using System.Linq.Expressions;
using System.Drawing;
using Reflection = System.Reflection;
using System.Runtime.CompilerServices;
using RuntimeBinder=Microsoft.CSharp.RuntimeBinder;
namespace Serializers.MessagePack.Formatters;
using Sets;

using Expressions=System.Linq.Expressions;
public class Conditional:共通 {
    [Fact]
    public void Serialize(){
        //if(writer.TryWriteNil(value)) return;
        this.MemoryMessageJson_Assert(new{a=default(Expressions.ConditionalExpression)},output=>{});
        this.MemoryMessageJson_Assert(
            new{
                a=Expressions.Expression.Condition(
                    Expressions.Expression.Constant(true),
                    Expressions.Expression.Constant(true),
                    Expressions.Expression.Constant(true)
                )
            },output=>{}
        );
    }
}
