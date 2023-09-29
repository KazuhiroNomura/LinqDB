using Expressions = System.Linq.Expressions;
namespace TestLinqDB.Serializers.Formatters;
public class MethodCall : 共通
{
    [Fact]
    public void Serialize()
    {
        var o = new テスト();
        var arg = Expressions.Expression.Constant(1);
        var @this = Expressions.Expression.Constant(o);
        var StaticMethod = Expressions.Expression.Call(M(() => テスト.StaticMethod()));
        var InstanceMethod = Expressions.Expression.Call(Expressions.Expression.Constant(new テスト()), M(() => new テスト().InstanceMethod()));
        //this.MemoryMessageJson_Expression(Expressions.Expression.Call(M(()=>テスト.StaticMethod(1)),arg));
        //this.MemoryMessageJson_Expression(Expressions.Expression.Call(M(()=>テスト.StaticMethod(1,2)),arg,arg));
        //this.MemoryMessageJson_Expression(Expressions.Expression.Call(@this,M(()=>o.InstanceMethod())));
        //this.MemoryMessageJson_Expression(Expressions.Expression.Call(@this,M(()=>o.InstanceMethod(1)),arg));
        //this.MemoryMessageJson_Expression(Expressions.Expression.Call(@this,M(()=>o.InstanceMethod(1,2)),arg,arg));
        //this.MemoryMessageJson_Expression(
        //    Expressions.Expression.Call(
        //        M(()=>string.Concat("","")),
        //        Expressions.Expression.Constant("A"),
        //        Expressions.Expression.Constant("B")
        //    )
        //);
        this.MemoryMessageJson_Assert(new { a = default(Expressions.MethodCallExpression) });
        this.MemoryMessageJson_Assert(
            new
            {
                StaticMethod,
                StaticMethodExpression = (Expressions.Expression)StaticMethod,
                InstanceMethod,
                InstanceMethodExpression = (Expressions.Expression)InstanceMethod
            }
        );
    }
}
