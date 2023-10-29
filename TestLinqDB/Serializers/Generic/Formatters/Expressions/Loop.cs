﻿using System.Linq.Expressions;
namespace TestLinqDB.Serializers.Generic.Formatters.Expressions;
public abstract class Loop:共通{
    protected Loop(テストオプション テストオプション):base(テストオプション){}
    [Fact]public void PrivateWrite_Read(){
        //this.MemoryMessageJson_Assert(new{a=default(LoopExpression)});
        var Label_decimal = Expression.Label(typeof(decimal), "Label_decimal");
        var Label_void = Expression.Label("Label");
        //if(value.BreakLabel is null) {//body
        {
            var input = Expression.Loop(
                Expression.Default(typeof(void))
            );
            //var expected=new{a=input};
            this.AssertEqual(input);
        }
        //} else {
        //    if(value.ContinueLabel is null) {//break,body
        {
            var input = Expression.Loop(
                Expression.Block(
                    Expression.Break(Label_decimal, Expression.Constant(1m))
                ),
                Label_decimal
            );
            this.AssertEqual(input);
        }
        //    } else {//break,continue,body
        {
            var input = Expression.Loop(
                Expression.Block(
                    Expression.Break(Label_decimal, Expression.Constant(1m)),
                    Expression.Continue(Label_void)
                ),
                Label_decimal,
                Label_void
            );
            this.AssertEqual(input);
        }
        //    }
        //}
    }
    [Fact]
    public void Serialize_Deserialize(){
        //if(writer.TryWriteNil(value)) return;
        var Default=default(LoopExpression);
        this.AssertEqual(Default);
        {
            var input=Expression.Loop(
                Expression.Default(typeof(void))
            );
            var expected=new{a=input,b=(Expression)input};
            this.AssertEqual(expected);
        }
    }
}
