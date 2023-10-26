using Expressions = System.Linq.Expressions;
namespace TestLinqDB.Serializers.Formatters;
public class Loop : 共通{
    [Fact]public void PrivateWrite_Read(){
        //this.MemoryMessageJson_Assert(new{a=default(Expressions.LoopExpression)});
        var Label_decimal = Expressions.Expression.Label(typeof(decimal), "Label_decimal");
        var Label_void = Expressions.Expression.Label("Label");
        //if(value.BreakLabel is null) {//body
        {
            var input = Expressions.Expression.Loop(
                Expressions.Expression.Default(typeof(void))
            );
            //var expected=new{a=input};
            this.MemoryMessageJson_T_Assert全パターン(input);
        }
        //} else {
        //    if(value.ContinueLabel is null) {//break,body
        {
            var input = Expressions.Expression.Loop(
                Expressions.Expression.Block(
                    Expressions.Expression.Break(Label_decimal, Expressions.Expression.Constant(1m))
                ),
                Label_decimal
            );
            this.MemoryMessageJson_T_Assert全パターン(input);
        }
        //    } else {//break,continue,body
        {
            var input = Expressions.Expression.Loop(
                Expressions.Expression.Block(
                    Expressions.Expression.Break(Label_decimal, Expressions.Expression.Constant(1m)),
                    Expressions.Expression.Continue(Label_void)
                ),
                Label_decimal,
                Label_void
            );
            this.MemoryMessageJson_T_Assert全パターン(input);
        }
        //    }
        //}
    }
    [Fact]
    public void Serialize_Deserialize(){
        //if(writer.TryWriteNil(value)) return;
        var Default=default(Expressions.LoopExpression);
        this.MemoryMessageJson_T_Assert全パターン(Default);
        {
            var input=Expressions.Expression.Loop(
                Expressions.Expression.Default(typeof(void))
            );
            var expected=new{a=input,b=(Expressions.Expression)input};
            this.MemoryMessageJson_T_Assert全パターン(expected);
        }
    }
}
