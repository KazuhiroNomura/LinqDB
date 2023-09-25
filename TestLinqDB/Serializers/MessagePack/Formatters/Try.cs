﻿using System.Diagnostics;
using System.Drawing;
using Reflection = System.Reflection;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Expressions = System.Linq.Expressions;
using RuntimeBinder=Microsoft.CSharp.RuntimeBinder;
namespace Serializers.MessagePack.Formatters;
using Sets;
public class Try:共通 {
    [Fact]
    public void Serialize(){
        //if(writer.TryWriteNil(value)) return;
        this.MessagePack_Assert(new{a=default(Expressions.TryExpression)},output=>{});
        var input=Expressions.Expression.TryCatch(
            Expressions.Expression.Constant(0),
            Expressions.Expression.Catch(
                typeof(Exception),
                Expressions.Expression.Constant(0)
            )
        );
        this.MessagePack_Assert(
            new{
                a=input,b=(Expressions.Expression)input,c=(object)input
            },output=>{}
        );
    }
}
