using System.Linq.Expressions;

namespace TestLinqDB.Serializers.Formatters.Expressions;
public class MethodCall : 共通
{
    [Fact]
    public void Serialize()
    {
        var o = new テスト();
        var arg = Expression.Constant(1);
        var @this = Expression.Constant(o);
        var StaticMethod = Expression.Call(M(() => テスト.StaticMethod()));
        var InstanceMethod = Expression.Call(Expression.Constant(new テスト()), M(() => new テスト().InstanceMethod()));

        this.ExpressionシリアライズAssertEqual(Expression.Call(M(() => テスト.StaticMethod(1)), arg));
        this.ExpressionシリアライズAssertEqual(Expression.Call(M(() => テスト.StaticMethod(1, 2)), arg, arg));
        this.ExpressionシリアライズAssertEqual(Expression.Call(@this, M(() => o.InstanceMethod())));
        this.ExpressionシリアライズAssertEqual(Expression.Call(@this, M(() => o.InstanceMethod(1)), arg));
        this.ExpressionシリアライズAssertEqual(Expression.Call(@this, M(() => o.InstanceMethod(1, 2)), arg, arg));
        this.ExpressionシリアライズAssertEqual(
            Expression.Call(
                M(() => string.Concat("", "")),
                Expression.Constant("A"),
                Expression.Constant("B")
            )
        );

        this.ObjectシリアライズAssertEqual(
            new
            {
                StaticMethod,
                StaticMethodExpression = StaticMethod,
                InstanceMethod,
                InstanceMethodExpression = InstanceMethod
            }
        );
    }
}
