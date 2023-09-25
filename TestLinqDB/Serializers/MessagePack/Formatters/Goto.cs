using System.Diagnostics;
using System.Drawing;
using Reflection = System.Reflection;
using System.Runtime.CompilerServices;
using Expressions = System.Linq.Expressions;
using RuntimeBinder=Microsoft.CSharp.RuntimeBinder;
namespace Serializers.MessagePack.Formatters;
using Sets;
public class Goto:共通 {
    [Fact]public void Serialize(){
        this.MessagePack_Assert(new{a=default(Expressions.GotoExpression)},output=>{});
        var target=Expressions.Expression.Label(typeof(int),"target");
        this.MessagePack_Assert(
            new{
                a=Expressions.Expression.MakeGoto(
                    Expressions.GotoExpressionKind.Return,
                    target,
                    Expressions.Expression.Constant(5),
                    typeof(byte)
                )
            },output=>{}
        );
    }
}
