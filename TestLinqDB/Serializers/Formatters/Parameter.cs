﻿using Expressions = System.Linq.Expressions;
namespace Serializers.Formatters;
public class Parameter : 共通
{
    [Fact]
    public void Write()
    {
        var p = Expressions.Expression.Parameter(typeof(int), "p");
        //if(index0<0){
        //    if(index1<0){
        this.MemoryMessageJson_Assert(new { a = Expressions.Expression.Block(p) });
        //    }else{
        this.MemoryMessageJson_Assert(new { a = Expressions.Expression.Block(p, p) });
        //    }
        //}else{
        this.MemoryMessageJson_Assert(
            new
            {
                a = Expressions.Expression.Lambda<Func<int, object>>(
                    Expressions.Expression.Constant(
                        new { a = p }
                    ),
                    p
                )

            }
        );
        //}
    }
    [Fact]
    public void Serialize()
    {
        this.MemoryMessageJson_Assert(new { a = default(Expressions.ParameterExpression) });
        var input = Expressions.Expression.Parameter(typeof(int));
        //if(index0<0){
        //    if(index1<0){
        this.MemoryMessageJson_Assert(new { a = input }, output => { });
        //    }else{
        this.MemoryMessageJson_Assert(new { a = input, b = input }, output => { });
        //    }
        //}else{
        this.MemoryMessageJson_Assert(
            Expressions.Expression.Lambda<Func<int, object>>(
                Expressions.Expression.Constant(
                    new { a = input }
                ),
                input
            ),
            output => { }
        );
        //}
        //匿名型に具体的な型のフィールドがありSerializeが呼ばれる
        this.MemoryMessageJson_Assert(new { a = input, b = input }, output => { });
    }
}