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

        this.AssertEqual(Expression.Call(M(() => テスト.StaticMethod(1)),arg));
        this.AssertEqual(Expression.Call(M(() => テスト.StaticMethod(1,2)),arg,arg));
        this.AssertEqual(Expression.Call(@this,M(() => o.InstanceMethod())));
        this.AssertEqual(Expression.Call(@this,M(() => o.InstanceMethod(1)),arg));
        this.AssertEqual(Expression.Call(@this,M(() => o.InstanceMethod(1,2)),arg,arg));
        this.AssertEqual(
            Expression.Call(
                M(() => string.Concat("","")),
                Expression.Constant("A"),
                Expression.Constant("B")
            )
        );

        this.AssertEqual(
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
