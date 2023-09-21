using LinqDB.Optimizers;
using LinqDB.Remote.Servers;
using static LinqDB.Helpers.Configulation;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Text;
using MemoryPack;
using System.Buffers;
using System.Reflection;
using LinqDB.Helpers;
using MessagePack;
using static LinqDB.Optimizers.Optimizer;
using LinqDB.Remote.Clients;
using System.Net;
using LinqDB;
using static Microsoft.FSharp.Core.ByRefKinds;
//using Utf8Json2 = LinqDB.Serializers.Utf8Json;
//using MessagePack2 = LinqDB.Serializers.MessagePack;
//using MemoryPack2 = LinqDB.Serializers.MemoryPack;
//using Serializers=LinqDB.Serializers;
//using 共通=global.Serializers.MessagePack.Formatters.共通;
namespace Serializers.MessagePack.Formatters;
public abstract class 共通{
    //protected Server<string> Server;
    protected readonly EnumerableSetEqualityComparer Comparer;
    protected ExpressionEqualityComparer ExpressionEqualityComparer=>new();
    protected readonly LinqDB.Serializers.Utf8Json.Serializer Utf8Json=new();
    protected readonly LinqDB.Serializers.MessagePack.Serializer MessagePack=new();
    protected readonly LinqDB.Serializers.MemoryPack.Serializer MemoryPack=new();
    protected 共通(){
        this.Comparer=new(this.ExpressionEqualityComparer);
        //var Server = this.Server=new Server<string>("",1,ListenerSocketポート番号) { ReadTimeout=receiveTimeout };
        //Server.Open();
    }
    private void MemoryMessageJsonObject<T>(T input){
        {
            var s=this.MemoryPack;
            var bytes = s.Serialize(input);
            var output = s.Deserialize<T>(bytes); 
            Assert.Equal(input,output,this.Comparer);
        }
        {
            var s=this.MessagePack;
            var bytes =s.Serialize(input);
            var output=s.Deserialize<T>(bytes);
            Assert.Equal(input,output,this.Comparer);
        }
        {
            var s=this.Utf8Json;
            var bytes =s.Serialize(input);
            var output=s.Deserialize<T>(bytes);
            Assert.Equal(input,output,this.Comparer);
        }
    }
    protected void シリアライズデシリアライズ3パターンジェネリクス非ジェネリクス<T>(T input){
        this.MemoryMessageJsonObject<object>(input);
        this.MemoryMessageJsonObject(input);
    }
    protected readonly Optimizer Optimizer=new(){IsGenerateAssembly=false,Context=typeof(共通),AssemblyFileName="デバッグ.dll"};
    //シリアライズ。色んな方法でやってデシリアライズ成功するか
    //実行。結果が一致するから
    protected void MemoryMessageJson_Expression<T>(T input) where T : Expression {
        this.MemoryMessageJson_Assert<T>(null!,Assert.Null);
        this.MemoryMessageJson_Assert<Expression>(null!,Assert.Null);
        this.MemoryMessageJson_Assert<object>(null!,Assert.Null);
        this.MemoryMessageJson_Assert(input,output =>Assert.Equal(input,output,this.ExpressionEqualityComparer));
        this.MemoryMessageJson_Assert<Expression>(input,output =>Assert.Equal(input,output,this.ExpressionEqualityComparer));
        this.MemoryMessageJson_Assert<object>(input,output =>Assert.Equal(input,(Expression)output,this.ExpressionEqualityComparer));
    }
    protected void 共通コンパイル実行<T,TResult>(Expression<Func<T,TResult>> input,T t){
        var Optimizer=this.Optimizer;
        var 標準=input.Compile();
        var expected0=標準(t);
        Optimizer.IsInline=true;
        var expected2=(Optimizer.CreateDelegate(input)(t));
        Assert.Equal(expected0,expected2,this.Comparer);
    }
    protected void 共通コンパイル実行<TResult>(Expression<Func<TResult>> input){
        var Optimizer=this.Optimizer;
        var 標準=input.Compile();
        var expected0=標準();
        //Optimizer.IsInline=true;
        var expected2=(Optimizer.CreateDelegate(input)());
        Assert.Equal(expected0,expected2,this.Comparer);
    }
    protected void 共通コンパイル実行(Expression<Action> input){
        var Optimizer=this.Optimizer;
        var 標準=input.Compile();
        標準();
        Optimizer.IsInline=true;
        Optimizer.CreateDelegate(input)();
    }
    private void 共通MemoryMessageJson_TExpressionObject_コンパイル実行<T>(LambdaExpression input,Func<Delegate,T>x){
        var Optimizer=this.Optimizer;
        var 標準=input.Compile();
        var expected0=x(標準);
        Optimizer.IsInline=false;
        var expected1=x(Optimizer.CreateDelegate(input));
        Assert.Equal(expected0,expected1,this.Comparer);
        Optimizer.IsInline=true;
        var expected2=x(Optimizer.CreateDelegate(input));
        Assert.Equal(expected0,expected2,this.Comparer);
        this.MemoryMessageJson_Assert(input,共通);
        this.MemoryMessageJson_Assert<Expression>(input,共通);
        this.MemoryMessageJson_Assert<object>(input,共通);
        void 共通(object output){
            Assert.Equal(expected0,x(input.Compile()),this.Comparer);
            var outputLambda=(LambdaExpression)output;
            Assert.Equal(input,outputLambda,this.ExpressionEqualityComparer);
            var actual0=x(outputLambda.Compile());
            Assert.Equal(expected0,actual0,this.Comparer);
            Assert.Equal(expected1,actual0,this.Comparer);
            Assert.Equal(expected2,actual0,this.Comparer);
            Optimizer.IsInline=false;
            var actual1=x(Optimizer.CreateDelegate(outputLambda));
            Assert.Equal(expected0,actual1,this.Comparer);
            Assert.Equal(expected1,actual1,this.Comparer);
            Assert.Equal(expected2,actual1,this.Comparer);
            Optimizer.IsInline=true;
            var actual2=x(Optimizer.CreateDelegate(outputLambda));
            Assert.Equal(expected0,actual2,this.Comparer);
            Assert.Equal(expected1,actual2,this.Comparer);
            Assert.Equal(expected2,actual2,this.Comparer);
        }
    }
    protected void MemoryMessageJson_TExpressionObject_コンパイル実行(Expression<Action> inputLambda){
        var Optimizer=this.Optimizer;
        var 標準=inputLambda.Compile();
        標準();
        Optimizer.IsInline=false;
        Optimizer.CreateDelegate(inputLambda)();
        Optimizer.IsInline=true;
        Optimizer.CreateDelegate(inputLambda)();
        this.MemoryMessageJson_Assert(inputLambda,共通);
        this.MemoryMessageJson_Assert<Expression>(inputLambda,共通);
        this.MemoryMessageJson_Assert<object>(inputLambda,共通);
        void 共通(object output){
            var outputLambda=(LambdaExpression)output;
            Assert.Equal(inputLambda,outputLambda,this.ExpressionEqualityComparer);
            (Optimizer.CreateDelegate(inputLambda))();
            Optimizer.IsInline=false;
            (Optimizer.CreateDelegate(inputLambda))();
            Optimizer.IsInline=true;
            (Optimizer.CreateDelegate(inputLambda))();
        }
    }
    protected void MemoryMessageJson_TExpressionObject_コンパイル実行<TResult>(Expression<Func<TResult>> input){
        const int receiveTimeout = 1000;
        using var Server =new Server(1,ListenerSocketポート番号);
        Server.ReadTimeout=receiveTimeout;
        Server.Open();
        using var R = new Client(Dns.GetHostName(),ListenerSocketポート番号);
        R.Expression(input,XmlType.Utf8Json);
        this.共通MemoryMessageJson_TExpressionObject_コンパイル実行(input,Delegate=>((Func<TResult>)Delegate)());
    }
    //リモート実行できるか。
    protected void MemoryMessageJson_TExpressionObject_コンパイル実行<T,TResult>(Expression<Func<T,TResult>> input,T t){
        const int receiveTimeout = 1000;
        using var Server = new Server<T>(t,1,ListenerSocketポート番号);
        Server.ReadTimeout=receiveTimeout;
        Server.Open();
        using var R = new Client(Dns.GetHostName(),ListenerSocketポート番号);
        R.Expression(input,XmlType.Utf8Json);
        this.共通MemoryMessageJson_TExpressionObject_コンパイル実行(input,Delegate=>((Func<T,TResult>)Delegate)(t));
    }
    protected void MemoryMessageJson_TExpressionObject<T>(T input) where T:Expression?{
    //protected void 共通Expression<T>(Expressions.Expression<Func<T>> input){
        //this.共通object1(input,output=>Assert.Equal(input,output,this.ExpressionEqualityComparer));
        this.MemoryMessageJson_Assert(input,output=>Assert.Equal(input,output,this.ExpressionEqualityComparer));
        this.MemoryMessageJson_Assert<Expression>(input,output=>Assert.Equal(input,output,this.ExpressionEqualityComparer));
        this.MemoryMessageJson_Assert<object>(input,output=>Assert.Equal(input,(T)output,this.ExpressionEqualityComparer));
    }
    private static readonly object Lockobject=new();
    protected void MemoryMessageJson_Assert<T>(T input,Action<T> AssertAction){
        lock(Lockobject) {
            {
                var s = this.MemoryPack;
                var bytes = this.MemoryPack.Serialize(input);
                var output = s.Deserialize<T>(bytes);
                AssertAction(output!);
            }
            {
                var s = this.MessagePack;
                var bytes = s.Serialize(input);
                dynamic a = new NonPublicAccessor(s);
                var json = MessagePackSerializer.ConvertToJson(bytes,a.Options);
                var output = s.Deserialize<T>(bytes);
                AssertAction(output);
            }
            {
                var s = this.Utf8Json;
                var bytes = s.Serialize(input);
                var json = Encoding.UTF8.GetString(bytes);
                var output = s.Deserialize<T>(bytes);
                AssertAction(output);
            }
        }
    }
    protected static LambdaExpression Lambda<T>(Expression<Func<T>> e)=>e;
    protected static object ClassDisplay取得(){
        var a=1;
        var body=Lambda(()=>a).Body;
        var member=(MemberExpression)body;
        var constant=(ConstantExpression)member.Expression!;
        return constant.Value;
    }
}
