using System.Diagnostics;
using System.Drawing;
using Reflection = System.Reflection;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Expressions = System.Linq.Expressions;
using RuntimeBinder=Microsoft.CSharp.RuntimeBinder;
namespace Serializers.MessagePack.Formatters;
using Newtonsoft.Json.Linq;

using Sets;
public class Loop:共通 {
    [Fact]
    public void PrivateWrite(){
        this.MessagePack_Assert(new{a=default(Expressions.LoopExpression)},output=>{});
        var Label_decimal=Expressions.Expression.Label(typeof(decimal),"Label_decimal");
        var Label_void=Expressions.Expression.Label("Label");
        //if(value.BreakLabel is null) {//body
        {
            var input=Expressions.Expression.Loop(
                Expressions.Expression.Default(typeof(void))
            );
            this.MessagePack_Assert(
                new{a=input,b=(Expressions.Expression)input},output=>{}
            );
        }
        //} else {
        //    if(value.ContinueLabel is null) {//break,body
        {
            var input=Expressions.Expression.Loop(
                Expressions.Expression.Block(
                    Expressions.Expression.Break(Label_decimal,Expressions.Expression.Constant(1m))
                ),
                Label_decimal
            );
            this.MessagePack_Assert(
                new{a=input,b=(Expressions.Expression)input},output=>{}
            );
        }
        //    } else {//break,continue,body
        {
            var input=Expressions.Expression.Loop(
                Expressions.Expression.Block(
                    Expressions.Expression.Break(Label_decimal,Expressions.Expression.Constant(1m)),
                    Expressions.Expression.Continue(Label_void)
                ),
                Label_decimal,
                Label_void
            );
            this.MessagePack_Assert(
                new{a=input,b=(Expressions.Expression)input},output=>{}
            );
        }
        //    }
        //}
    }
}
