﻿using System.Linq.Expressions;

namespace TestLinqDB.Serializers.Formatters.Expressions;
public class TypeBinary : 共通
{
    [Fact]
    public void Serialize()
    {
        //if(writer.TryWriteNil(value)) return;
        var TypeIs = Expression.TypeIs(
            Expression.Constant(1m),
            typeof(decimal)
        );
        var TypeEqual = Expression.TypeEqual(
            Expression.Constant(1m),
            typeof(decimal)
        );
        this.ExpressionシリアライズAssertEqual(TypeIs);
        this.ExpressionシリアライズAssertEqual(TypeEqual);
    }
}
