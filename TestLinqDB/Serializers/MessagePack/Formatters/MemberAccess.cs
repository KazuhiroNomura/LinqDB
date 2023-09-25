using System.Diagnostics;
using System.Drawing;
using Reflection = System.Reflection;
using System.Runtime.CompilerServices;
using Expressions = System.Linq.Expressions;
using RuntimeBinder=Microsoft.CSharp.RuntimeBinder;
namespace Serializers.MessagePack.Formatters;
using Sets;
[MemoryPack.MemoryPackable,global::MessagePack.MessagePackObject(true)]
public partial class MemberAccess対象{
    public int property=>1;
    public int field=4;
}
public class MemberAccess:共通 {
    [Fact]public void Serialize(){

        var Point=Expressions.Expression.Parameter(typeof(Point));
        this.MessagePack_Assert(new{a=default(Expressions.MemberExpression)},output=>{});
        var input=Expressions.Expression.MakeMemberAccess(
            Expressions.Expression.Constant(new MemberAccess対象()),
            typeof(MemberAccess対象).GetProperty(nameof(MemberAccess対象.property))!
        );
        this.MessagePack_Assert(
            new{
                a=input,b=(Expressions.Expression)input
            },output=>{}
        );
    }
}
