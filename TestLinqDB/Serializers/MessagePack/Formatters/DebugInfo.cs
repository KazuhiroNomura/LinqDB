﻿using System.Diagnostics;
using System.Drawing;
using Reflection = System.Reflection;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Expressions = System.Linq.Expressions;
using RuntimeBinder=Microsoft.CSharp.RuntimeBinder;
namespace Serializers.MessagePack.Formatters;
using Sets;
public class DebugInfo:共通 {
    [Fact]
    public void Serialize(){
        //if(writer.TryWriteNil(value)) return;
        this.MemoryMessageJson_Assert(new{a=default(Expressions.DebugInfoExpression)});
        var SymbolDocument0=Expressions.Expression.SymbolDocument("ソースファイル名0.cs");
        var DebugInfo=Expressions.Expression.DebugInfo(SymbolDocument0,1,2,3,4);
        this.MemoryMessageJson_Assert(
            new{
                DebugInfo
            }
        );
    }
}
