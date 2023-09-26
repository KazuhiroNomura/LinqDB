using System.Diagnostics;
using System.Drawing;
using Reflection = System.Reflection;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Expressions = System.Linq.Expressions;
using RuntimeBinder=Microsoft.CSharp.RuntimeBinder;
namespace Serializers.MessagePack.Formatters;
using Sets;
public class TypeBinary:共通 {
    [Fact]
    public void Serialize(){
        //if(writer.TryWriteNil(value)) return;
        this.MemoryMessageJson_Assert(new{a=default(Expressions.TypeBinaryExpression)},output=>{});
        var TypeIs=Expressions.Expression.TypeIs(
                Expressions.Expression.Constant(1m),
                typeof(decimal)
            );
        var TypeEqual=Expressions.Expression.TypeEqual(
            Expressions.Expression.Constant(1m),
            typeof(decimal)
        );
        this.MemoryMessageJson_Assert(
            new{
                TypeIs,TypeIsExpression=(Expressions.Expression)TypeIs,TypeIsObject=(object)TypeIs,
                TypeEqual,TypeEqualExpression=(Expressions.Expression)TypeIs,TypeEqualObject=(object)TypeIs,
            },output=>{}
        );
    }
}
