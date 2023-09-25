using System.Diagnostics;
using System.Drawing;
using Reflection = System.Reflection;
using System.Runtime.CompilerServices;
using Expressions = System.Linq.Expressions;
using RuntimeBinder=Microsoft.CSharp.RuntimeBinder;
namespace Serializers.MessagePack.Formatters;
using Sets;
public class New:共通 {
    [Fact]public void Serialize(){
        var input=Expressions.Expression.New(
            typeof(ValueTuple<int>).GetConstructors()[0],
            Expressions.Expression.Constant(1)
        );
        this.MessagePack_Assert(new{a=default(Expressions.NewExpression)},output=>{});
        this.MessagePack_Assert(
            new{
                a=input,b=(Expressions.Expression)input
            },output=>{}
        );
    }
}
