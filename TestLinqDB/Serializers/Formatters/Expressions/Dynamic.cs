using System.Runtime.CompilerServices;
using System.Linq.Expressions;
using RuntimeBinder = Microsoft.CSharp.RuntimeBinder;

namespace TestLinqDB.Serializers.Formatters.Expressions;
public class Dynamic : 共通
{
    private static DynamicExpression PrivateDynamicConvert<TInput, TResult>(TInput input, RuntimeBinder.CSharpBinderFlags Flag)
    {
        var Constant = Expression.Constant(input);
        var binder = RuntimeBinder.Binder.Convert(
            Flag,
            typeof(TResult),
            typeof(Dynamic)
        );
        return Expression.Dynamic(
            binder,
            typeof(TResult),
            Constant
        );
    }
    private static readonly RuntimeBinder.CSharpArgumentInfo CSharpArgumentInfo1 = RuntimeBinder.CSharpArgumentInfo.Create(RuntimeBinder.CSharpArgumentInfoFlags.None, null);
    private static readonly RuntimeBinder.CSharpArgumentInfo[] CSharpArgumentInfoArray1 = { CSharpArgumentInfo1 };
    private static readonly RuntimeBinder.CSharpArgumentInfo[] CSharpArgumentInfoArray2 = { CSharpArgumentInfo1, CSharpArgumentInfo1 };
    private static readonly RuntimeBinder.CSharpArgumentInfo[] CSharpArgumentInfoArray3 = { CSharpArgumentInfo1, CSharpArgumentInfo1, CSharpArgumentInfo1 };
    private static readonly RuntimeBinder.CSharpArgumentInfo[] CSharpArgumentInfoArray4 = { CSharpArgumentInfo1, CSharpArgumentInfo1, CSharpArgumentInfo1, CSharpArgumentInfo1 };
    [Fact]
    public void PrivateWrite()
    {
        //case BinderType.BinaryOperationBinder:{
        {
            var arg1 = 1;
            var arg2 = 1;
            var binder = RuntimeBinder.Binder.BinaryOperation(
                RuntimeBinder.CSharpBinderFlags.None,
                ExpressionType.Add,
                this.GetType(),
                new[] { RuntimeBinder.CSharpArgumentInfo.Create(RuntimeBinder.CSharpArgumentInfoFlags.None, null), RuntimeBinder.CSharpArgumentInfo.Create(RuntimeBinder.CSharpArgumentInfoFlags.None, null) }
            );
            var input = Expression.Dynamic(
                binder, typeof(object),
                Expression.Constant(arg1), Expression.Constant(arg2)
            );
            this.ExpressionシリアライズAssertEqual(input);
        }
        //case BinderType.ConvertBinder:{
        {
            var input = PrivateDynamicConvert<int, long>(1, RuntimeBinder.CSharpBinderFlags.None);
            this.ExpressionシリアライズAssertEqual(input);
        }
        //case BinderType.GetIndexBinder:{
        {
            const int expected = 2;
            var arg1 = new[] { 1, expected, 3 };
            var arg2 = 1;
            var binder = RuntimeBinder.Binder.GetIndex(
                RuntimeBinder.CSharpBinderFlags.None,
                this.GetType(),
                CSharpArgumentInfoArray2
            );
            var input = Expression.Dynamic(
                binder,
                typeof(object),
                Expression.Constant(arg1), Expression.Constant(arg2)
            );
            this.ExpressionシリアライズAssertEqual(input);
        }
        //case BinderType.GetMemberBinder:{
        {
            var arg1 = new TestDynamic<int>(1, 2);
            var binder = RuntimeBinder.Binder.GetMember(
                RuntimeBinder.CSharpBinderFlags.None,
                nameof(TestDynamic<int>.メンバー1),
                this.GetType(),
                CSharpArgumentInfoArray1
            );
            var input = Expression.Dynamic(
                binder,
                typeof(object),
                Expression.Constant(arg1)
            );
            this.ExpressionシリアライズAssertEqual(input);
        }
        //case BinderType.InvokeBinder:{
        {
            共通((int a, int b, int c) => a==b&&a==c, 1, 2, 3);

            void 共通(object オブジェクト, object a, object b, object c)
            {
                var binder = RuntimeBinder.Binder.Invoke(
                    RuntimeBinder.CSharpBinderFlags.None,
                    typeof(Serializer),
                    CSharpArgumentInfoArray4
                );
                var CallSite = CallSite<Func<CallSite, object, object, object, object, object>>.Create(binder);
                var input = Expression.Dynamic(
                    binder,
                    typeof(object),
                    Expression.Constant(オブジェクト),
                    Expression.Constant(a),
                    Expression.Constant(b),
                    Expression.Constant(c)
                );
                this.ExpressionシリアライズAssertEqual(input);
            }
        }
        //case BinderType.InvokeMemberBinder:{
        {
            var o = new テスト1();
            Action1(o, nameof(テスト1.InstanceMethod), 1);
            void Action1<T0, T1>(T0 arg0, string Name, T1 arg1)
            {
                var CSharpArgumentInfos = new[] { RuntimeBinder.CSharpArgumentInfo.Create(RuntimeBinder.CSharpArgumentInfoFlags.None, null), RuntimeBinder.CSharpArgumentInfo.Create(RuntimeBinder.CSharpArgumentInfoFlags.UseCompileTimeType, null) };
                var binder = RuntimeBinder.Binder.InvokeMember(
                    RuntimeBinder.CSharpBinderFlags.ResultDiscarded, Name, null, typeof(テスト1), CSharpArgumentInfos
                );
                var input = Expression.Dynamic(
                    binder, typeof(object),
                    Expression.Constant(arg0), Expression.Constant(arg1)
                );
                this.ExpressionシリアライズAssertEqual(input);
            }
        }
        //case BinderType.SetIndexBinder:{
        {
            var arg1 = new[,] { { 1, 2, 3 }, { 4, 5, 6 }, { 7, 8, 9 }, };
            var arg2 = 1;
            var arg3 = 1;
            var arg4 = 1;
            var binder = RuntimeBinder.Binder.SetIndex(
                RuntimeBinder.CSharpBinderFlags.None,
                this.GetType(),
                CSharpArgumentInfoArray4
            );
            var input = Expression.Dynamic(
                binder,
                typeof(object),
                Expression.Constant(arg1), Expression.Constant(arg2), Expression.Constant(arg3), Expression.Constant(arg4)
            );
            this.ExpressionシリアライズAssertEqual(input);
        }
        //}
        {
            var arg1 = new TestDynamic<int>(1, 2);
            var arg2 = 2;
            var binder = RuntimeBinder.Binder.SetMember(
                RuntimeBinder.CSharpBinderFlags.ResultIndexed,
                nameof(TestDynamic<int>.メンバー1),
                this.GetType(),
                CSharpArgumentInfoArray2
            );
            var input = Expression.Dynamic(
                binder,
                typeof(object),
                Expression.Constant(arg1),
                Expression.Constant(arg2)
            );
            this.ExpressionシリアライズAssertEqual(input);
        }
        //case BinderType.UnaryOperationBinder:{
        {
            var arg1 = 1;
            var binder = RuntimeBinder.Binder.UnaryOperation(
                RuntimeBinder.CSharpBinderFlags.None,
                ExpressionType.Increment,
                this.GetType(),
                new[]{
                    RuntimeBinder.CSharpArgumentInfo.Create(RuntimeBinder.CSharpArgumentInfoFlags.None,null),
                    //RuntimeBinder.CSharpArgumentInfo.Create(RuntimeBinder.CSharpArgumentInfoFlags.NamedArgument,"a"),
                }
            );
            var input = Expression.Dynamic(
                binder,
                typeof(object),
                Expression.Constant(arg1, typeof(object))
            );
            this.ExpressionシリアライズAssertEqual(input);
        }
    }
    [Fact]
    public void Serialize()
    {
        //if(writer.TryWriteNil(value)) return;
        {
            var arg1 = 1;
            var arg2 = 1;
            var binder = RuntimeBinder.Binder.BinaryOperation(
                RuntimeBinder.CSharpBinderFlags.None,
                ExpressionType.Add,
                this.GetType(),
                new[] { RuntimeBinder.CSharpArgumentInfo.Create(RuntimeBinder.CSharpArgumentInfoFlags.None, null), RuntimeBinder.CSharpArgumentInfo.Create(RuntimeBinder.CSharpArgumentInfoFlags.None, null) }
            );
            var input = Expression.Dynamic(
                binder, typeof(object),
                Expression.Constant(arg1), Expression.Constant(arg2)
            );
            this.ExpressionシリアライズAssertEqual(input);
        }
    }
}
