using System.Diagnostics;
using System.Drawing;
using Reflection = System.Reflection;
using System.Runtime.CompilerServices;
using Expressions = System.Linq.Expressions;
using RuntimeBinder=Microsoft.CSharp.RuntimeBinder;
namespace Serializers.MessagePack.Formatters;
using Sets;
public class Label:共通 {
    [Fact]public void Serialize(){
        var labelTarget=Expressions.Expression.Label();
        //this.MemoryMessageJson_Expression(Expressions.Expression.Label(labelTarget));
        //this.MemoryMessageJson_Expression(Expressions.Expression.Label(labelTarget,Expressions.Expression.Constant(1)));
        var input=Expressions.Expression.Label(labelTarget);
        this.MessagePack_Assert(new{a=default(Expressions.LabelExpression)},output=>{});
        this.MessagePack_Assert(
            new{
                a=input,b=(Expressions.Expression)input
            },output=>{}
        );
    }
}
