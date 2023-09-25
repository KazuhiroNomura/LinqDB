﻿using System.Diagnostics;
//using System.Linq.Expressions;
using System.Drawing;
using Reflection = System.Reflection;
using System.Runtime.CompilerServices;
//using System.Linq.Expressions;
//using Expressions = System.Linq.Expressions;
using RuntimeBinder=Microsoft.CSharp.RuntimeBinder;
//using Binder = Microsoft.CSharp.RuntimeBinder;
//using MessagePack;
namespace Serializers.MessagePack.Formatters;
using Expressions=System.Linq.Expressions;
public class Block:共通 {
    [Fact]public void Serialize(){
        var Constant1= Expressions.Expression.Constant(1m);
        var input1=Expressions.Expression.Block(Constant1);
        this.MessagePack_Assert(new{a=input1},output=>{});
        this.MessagePack_Assert(new{a=default(Expressions.BlockExpression)},output=>{});

    }
    [Fact]public void Block0(){
        var ParameterDecimmal=Expressions.Expression.Parameter(typeof(decimal));
        共通0(
            Expressions.Expression.Block(
                new[]{ParameterDecimmal},
                Expressions.Expression.Block(
                    new[]{ParameterDecimmal},
                    ParameterDecimmal
                )
            )
        );
        void 共通0(Expressions.Expression input){
            this.MemoryMessageJson_Assert(
                input,
                output=>Assert.Equal(input,output,this.ExpressionEqualityComparer));
        }
    }
}
