﻿using System.Linq.Expressions;

namespace TestLinqDB.Serializers.Formatters.Expressions;
public class New : 共通
{
    [Fact]
    public void Write()
    {
        //if(value.Constructor is null){
        {
            var input = Expression.New(
                typeof(int)
            );
            //this.Memory_Assert(new{input});
            this.ExpressionシリアライズAssertEqual(input);
        }
        //} else{
        {
            var input = Expression.New(
                typeof(ValueTuple<int>).GetConstructors()[0],
                Expression.Constant(1)
            );
            this.ExpressionシリアライズAssertEqual(input);
        }
        //}
    }
    [Fact]
    public void Serialize()
    {
        var input = Expression.New(
            typeof(ValueTuple<int>).GetConstructors()[0],
            Expression.Constant(1)
        );
        this.ExpressionシリアライズAssertEqual(input);
    }
}
