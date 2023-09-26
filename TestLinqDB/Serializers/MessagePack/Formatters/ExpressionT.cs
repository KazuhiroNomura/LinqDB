using System.Diagnostics;
using System.Drawing;
using Reflection = System.Reflection;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Expressions = System.Linq.Expressions;
using RuntimeBinder=Microsoft.CSharp.RuntimeBinder;
namespace Serializers.MessagePack.Formatters;
using Sets;
public class ExpressionT:共通 {
    [Fact]
    public void Serialize(){
        //if(writer.TryWriteNil(value)) return;
        var input=Expressions.Expression.Lambda<Action>(Expressions.Expression.Default(typeof(void)));
        this.MemoryMessageJson_Assert(new{a=default(Expressions.Expression<Action>)},output=>{});
        this.MemoryMessageJson_Assert(
            new{
                input
            },output=>{}
        );
    }
}
