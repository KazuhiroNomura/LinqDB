﻿using Expressions = System.Linq.Expressions;
namespace Serializers.Formatters;
public class Lambda : 共通
{
    [Fact]
    public void Serialize()
    {
        //if(writer.TryWriteNil(value)) return;
        Expressions.LambdaExpression input = Expressions.Expression.Lambda<Action>(Expressions.Expression.Default(typeof(void)));
        this.MemoryMessageJson_Assert(new { a = default(Expressions.LambdaExpression) });
        this.MemoryMessageJson_Assert(
            new
            {
                Lambda0 = input,
                Lambda1 = (object)input
            }
        );
    }
}