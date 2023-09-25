using System.Diagnostics;
using System.Drawing;
using Reflection = System.Reflection;
using System.Runtime.CompilerServices;
using Expressions = System.Linq.Expressions;
using RuntimeBinder=Microsoft.CSharp.RuntimeBinder;
namespace Serializers.MessagePack.Formatters;
using Sets;
public class Dynamic:共通 {
    private static Expressions.Expression PrivateDynamicConvert<TInput,TResult>(TInput input,RuntimeBinder.CSharpBinderFlags Flag){
        var Constant = Expressions.Expression.Constant(input);
        var binder=RuntimeBinder.Binder.Convert(
            Flag,
            typeof(TResult),
            typeof(Dynamic)
        );
        return Expressions.Expression.Dynamic(
            binder,
            typeof(TResult),
            Constant
        );
    }
    private static readonly RuntimeBinder.CSharpArgumentInfo CSharpArgumentInfo1 = RuntimeBinder.CSharpArgumentInfo.Create(RuntimeBinder.CSharpArgumentInfoFlags.None, null);
    private static readonly RuntimeBinder.CSharpArgumentInfo[]CSharpArgumentInfoArray1 = {
        CSharpArgumentInfo1
    };
    private static readonly RuntimeBinder.CSharpArgumentInfo[]CSharpArgumentInfoArray2 = {
        CSharpArgumentInfo1,
        CSharpArgumentInfo1
    };
    private static readonly RuntimeBinder.CSharpArgumentInfo[]CSharpArgumentInfoArray3 = {
        CSharpArgumentInfo1,
        CSharpArgumentInfo1,
        CSharpArgumentInfo1
    };
    private static readonly RuntimeBinder.CSharpArgumentInfo[]CSharpArgumentInfoArray4 = {
        CSharpArgumentInfo1,
        CSharpArgumentInfo1,
        CSharpArgumentInfo1,
        CSharpArgumentInfo1
    };
    [Fact]public void PrivateWrite(){
        //case BinderType.BinaryOperationBinder:{
        {
            var arg1=1;
            var arg2=1;
            var binder=RuntimeBinder.Binder.BinaryOperation(
                RuntimeBinder.CSharpBinderFlags.None,
                Expressions.ExpressionType.Add,
                this.GetType(),
                new[]{
                    RuntimeBinder.CSharpArgumentInfo.Create(RuntimeBinder.CSharpArgumentInfoFlags.None,null),
                    RuntimeBinder.CSharpArgumentInfo.Create(RuntimeBinder.CSharpArgumentInfoFlags.None,null)
                }
            );
            var Dynamic0 = Expressions.Expression.Dynamic(
                binder,typeof(object),
                Expressions.Expression.Constant(arg1),Expressions.Expression.Constant(arg2)
            );
            this.MessagePack_Assert(
                new{
                    a=Dynamic0
                },output=>{}
            );
        }
        //case BinderType.ConvertBinder:{
        {
            this.MessagePack_Assert(
                new{
                    a=PrivateDynamicConvert<int,long>(1,RuntimeBinder.CSharpBinderFlags.None)
                },output=>{}
            );
        }
        //case BinderType.GetIndexBinder:{
        {
            const int expected = 2;
            var arg1 = new[] {
                1,expected,3
            };
            var arg2=1;
            var binder=RuntimeBinder.Binder.GetIndex(
                RuntimeBinder.CSharpBinderFlags.None,
                this.GetType(),
                CSharpArgumentInfoArray2
            );
            var Dynamic0=Expressions.Expression.Dynamic(
                binder,
                typeof(object),
                Expressions.Expression.Constant(arg1),Expressions.Expression.Constant(arg2)
            );
            this.MessagePack_Assert(
                new{
                    a=Dynamic0
                },output=>{}
            );
        }
        //case BinderType.GetMemberBinder:{
        {
            var arg1=new TestDynamic<int>(1,2);
            var binder=RuntimeBinder.Binder.GetMember(
                RuntimeBinder.CSharpBinderFlags.None,
                nameof(TestDynamic<int>.メンバー1),
                this.GetType(),
                CSharpArgumentInfoArray1
            );
            var Dynamic0=Expressions.Expression.Dynamic(
                binder,
                typeof(object),
                Expressions.Expression.Constant(arg1)
            );
            this.MessagePack_Assert(
                new{
                    a=Dynamic0
                },output=>{}
            );
        }
        //case BinderType.InvokeBinder:{
        {
            共通((int a,int b,int c)=>a==b&&a==c,1,2,3);

            void 共通(object オブジェクト, object a,object b,object c){
                var binder=RuntimeBinder.Binder.Invoke(
                    RuntimeBinder.CSharpBinderFlags.None,
                    typeof(Serializer),
                    CSharpArgumentInfoArray4
                );
                var CallSite=CallSite<Func<CallSite,object,object,object,object,object>>.Create(binder);
                var Dynamic0 = Expressions.Expression.Dynamic(
                    binder,
                    typeof(object),
                    Expressions.Expression.Constant(オブジェクト),
                    Expressions.Expression.Constant(a),
                    Expressions.Expression.Constant(b),
                    Expressions.Expression.Constant(c)
                );
                this.MessagePack_Assert(
                    new{
                        a=Dynamic0
                    },output=>{}
                );
            }
        }
        //case BinderType.InvokeMemberBinder:{
        {
            var o=new テスト();
            Action1(o,nameof(テスト.InstanceMethod),1);
            void Action1<T0,T1>(T0 arg0,string Name,T1 arg1){
                var CSharpArgumentInfos=new[]{RuntimeBinder.CSharpArgumentInfo.Create(RuntimeBinder.CSharpArgumentInfoFlags.None,null),RuntimeBinder.CSharpArgumentInfo.Create(RuntimeBinder.CSharpArgumentInfoFlags.UseCompileTimeType,null)};
                var binder=RuntimeBinder.Binder.InvokeMember(
                    RuntimeBinder.CSharpBinderFlags.ResultDiscarded,Name,null,typeof(テスト),CSharpArgumentInfos
                );
                var Dynamic0=Expressions.Expression.Dynamic(
                    binder,typeof(object),
                    Expressions.Expression.Constant(arg0),Expressions.Expression.Constant(arg1)
                );
                this.MessagePack_Assert(
                    new{
                        a=Dynamic0
                    },output=>{}
                );
            }
        }
        //case BinderType.SetIndexBinder:{
        {
            var arg1= new[,] {
                {1,2,3},
                {4,5,6},
                {7,8,9},
            };
            var arg2=1;
            var arg3=1;
            var arg4=1;
            var binder=RuntimeBinder.Binder.SetIndex(
                RuntimeBinder.CSharpBinderFlags.None,
                this.GetType(),
                CSharpArgumentInfoArray4
            );
            var Dynamic0=Expressions.Expression.Dynamic(
                binder,
                typeof(object),
                Expressions.Expression.Constant(arg1),Expressions.Expression.Constant(arg2),Expressions.Expression.Constant(arg3),Expressions.Expression.Constant(arg4)
            );
            this.MessagePack_Assert(
                new{
                    a=Dynamic0
                },output=>{}
            );
        }
        //}
        {
            var arg1=new TestDynamic<int>(1,2);
            var arg2=2;
            var binder=RuntimeBinder.Binder.SetMember(
                RuntimeBinder.CSharpBinderFlags.ResultIndexed,
                nameof(TestDynamic<int>.メンバー1),
                this.GetType(),
                CSharpArgumentInfoArray2
            );
            var Dynamic0=Expressions.Expression.Dynamic(
                binder,
                typeof(object),
                Expressions.Expression.Constant(arg1),
                Expressions.Expression.Constant(arg2)
            );
            this.MessagePack_Assert(
                new{
                    a=Dynamic0
                },output=>{}
            );
        }
        //case BinderType.UnaryOperationBinder:{
        {
            var arg1=1;
            var binder=RuntimeBinder.Binder.UnaryOperation(
                RuntimeBinder.CSharpBinderFlags.None,
                Expressions.ExpressionType.Increment,
                this.GetType(),
                new[]{
                    RuntimeBinder.CSharpArgumentInfo.Create(RuntimeBinder.CSharpArgumentInfoFlags.None, null),
                    //RuntimeBinder.CSharpArgumentInfo.Create(RuntimeBinder.CSharpArgumentInfoFlags.NamedArgument,"a"),
                }
            );
            var Dynamic0=Expressions.Expression.Dynamic(
                binder,
                typeof(object),
                Expressions.Expression.Constant(arg1,typeof(object))
            );
            this.MessagePack_Assert(
                new{
                    a=Dynamic0
                },output=>{}
            );
        }
    }
    [Fact]public void Serialize(){
        //if(writer.TryWriteNil(value)) return;
        this.MessagePack_Assert(new{a=default(Expressions.DynamicExpression)},output=>{});
        {
            var arg1=1;
            var arg2=1;
            var binder=RuntimeBinder.Binder.BinaryOperation(
                RuntimeBinder.CSharpBinderFlags.None,
                Expressions.ExpressionType.Add,
                this.GetType(),
                new[]{
                    RuntimeBinder.CSharpArgumentInfo.Create(RuntimeBinder.CSharpArgumentInfoFlags.None,null),
                    RuntimeBinder.CSharpArgumentInfo.Create(RuntimeBinder.CSharpArgumentInfoFlags.None,null)
                }
            );
            var Dynamic0 = Expressions.Expression.Dynamic(
                binder,typeof(object),
                Expressions.Expression.Constant(arg1),Expressions.Expression.Constant(arg2)
            );
            this.MessagePack_Assert(
                new{
                    a=Dynamic0
                },output=>{}
            );
        }
    }
}
