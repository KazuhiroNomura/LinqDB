using System.Diagnostics;
using System.Drawing;
using Reflection = System.Reflection;
using System.Runtime.CompilerServices;
using Expressions = System.Linq.Expressions;
using RuntimeBinder=Microsoft.CSharp.RuntimeBinder;
namespace Serializers.MessagePack.Formatters;
using Sets;
public class NewArray:共通 {
    [Fact]public void Serialize(){
        this.MessagePack_Assert(new{a=default(Expressions.NewArrayExpression)},output=>{});
        var NewArrayBounds=Expressions.Expression.NewArrayBounds(
                typeof(int),
                Expressions.Expression.Constant(0),
                Expressions.Expression.Constant(1)
            );
        var NewArrayInit=Expressions.Expression.NewArrayInit(
            typeof(int),
            Expressions.Expression.Constant(2),
            Expressions.Expression.Constant(1)
        );
        this.MessagePack_Assert(
            new{
                NewArrayBounds,NewArrayBoundsExpression=(Expressions.Expression)NewArrayBounds,NewArrayBoundsObject=(object)NewArrayBounds,
                NewArrayInit,NewArrayInitExpression=(Expressions.Expression)NewArrayInit,NewArrayInitObject=(object)NewArrayInit,
            },output=>{}
        );
    }
}
