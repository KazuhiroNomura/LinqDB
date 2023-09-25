using System.Diagnostics;
using System.Drawing;
using Reflection = System.Reflection;
using System.Runtime.CompilerServices;
using Expressions = System.Linq.Expressions;
using RuntimeBinder=Microsoft.CSharp.RuntimeBinder;
namespace Serializers.MessagePack.Formatters;
using Sets;
public class Invocation:共通 {
    [Fact]public void Serialize(){
        var @string=Expressions.Expression.Parameter(typeof(string));
        var input=Expressions.Expression.Invoke(
            Expressions.Expression.Lambda(@string,@string),
            Expressions.Expression.Constant("B")
        );
        this.MessagePack_Assert(new{a=default(Expressions.InvocationExpression)},output=>{});
        this.MessagePack_Assert(
            new{
                a=input,b=(Expressions.Expression)input
            },output=>{}
        );
    }
}
