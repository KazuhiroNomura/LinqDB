using System.Diagnostics;
using System.Drawing;
using Reflection = System.Reflection;
using System.Runtime.CompilerServices;
using Expressions = System.Linq.Expressions;
using RuntimeBinder=Microsoft.CSharp.RuntimeBinder;
namespace Serializers.MessagePack.Formatters;
using Sets;
public class Constant:共通 {
    [Fact]
    public void Serialize(){
        //if(writer.TryWriteNil(value)) return;
        this.MessagePack_Assert(new{a=default(Expressions.ConstantExpression)},output=>{});
        this.MessagePack_Assert(
            new{
                a=Expressions.Expression.Constant(true)
            },output=>{}
        );
    }
}
