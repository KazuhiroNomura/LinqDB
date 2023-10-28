using System.Linq.Expressions;
namespace TestLinqDB.Serializers.Generic.Formatters.Expressions;
public abstract class MethodCall<TSerializer>:共通 where TSerializer:LinqDB.Serializers.Serializer,new(){
    protected MethodCall():base(new AssertDefinition(new TSerializer())){}
    [Fact]
    public void Serialize()
    {
        var o = new テスト();
        var arg = Expression.Constant(1);
        var @this = Expression.Constant(o);
        var StaticMethod = Expression.Call(M(() => テスト.StaticMethod()));
        var InstanceMethod = Expression.Call(Expression.Constant(new テスト()), M(() => new テスト().InstanceMethod()));

        this.MemoryMessageJson_T_Assert全パターン(Expression.Call(M(() => テスト.StaticMethod(1)),arg));
        this.MemoryMessageJson_T_Assert全パターン(Expression.Call(M(() => テスト.StaticMethod(1,2)),arg,arg));
        this.MemoryMessageJson_T_Assert全パターン(Expression.Call(@this,M(() => o.InstanceMethod())));
        this.MemoryMessageJson_T_Assert全パターン(Expression.Call(@this,M(() => o.InstanceMethod(1)),arg));
        this.MemoryMessageJson_T_Assert全パターン(Expression.Call(@this,M(() => o.InstanceMethod(1,2)),arg,arg));
        this.MemoryMessageJson_T_Assert全パターン(
            Expression.Call(
                M(() => string.Concat("","")),
                Expression.Constant("A"),
                Expression.Constant("B")
            )
        );

        this.MemoryMessageJson_T_Assert全パターン(
            new
            {
                StaticMethod,
                StaticMethodExpression = (Expression)StaticMethod,
                InstanceMethod,
                InstanceMethodExpression = (Expression)InstanceMethod
            }
        );
    }
}
