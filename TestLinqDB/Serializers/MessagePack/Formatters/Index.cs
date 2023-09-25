using System.Diagnostics;
using System.Drawing;
using Reflection = System.Reflection;
using System.Runtime.CompilerServices;
using Expressions = System.Linq.Expressions;
using RuntimeBinder=Microsoft.CSharp.RuntimeBinder;
namespace Serializers.MessagePack.Formatters;
using Sets;
public class Index:共通 {
    [Fact]public void Serialize(){
        var List=Expressions.Expression.Parameter(typeof(List<int>));
        this.MessagePack_Assert(new{a=default(Expressions.IndexExpression)},output=>{});
        var input=Expressions.Expression.MakeIndex(
            List,
            typeof(List<int>).GetProperty("Item"),
            new[]{Expressions.Expression.Constant(0)}
        );
        this.MessagePack_Assert(
            new{
                a=input,b=(Expressions.Expression)input
            },output=>{}
        );
    }
}
