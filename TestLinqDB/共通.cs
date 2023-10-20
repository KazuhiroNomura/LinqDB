using Reflection = System.Reflection;
using LinqDB.Optimizers;
using LinqDB.Remote.Servers;
using static LinqDB.Helpers.Configulation;
using Expressions = System.Linq.Expressions;
using System.Text;
using LinqDB.Helpers;
using MessagePack;
using static LinqDB.Optimizers.Optimizer;
using LinqDB.Remote.Clients;
using System.Net;
using LinqDB;
using Microsoft.VisualStudio.TestPlatform.Utilities;
//using Utf8Json2 = LinqDB.Serializers.Utf8Json;
//using MessagePack2 = LinqDB.Serializers.MessagePack;
//using MemoryPack2 = LinqDB.Serializers.MemoryPack;
//using Serializers=LinqDB.Serializers;
//using 共通=global.Serializers.MessagePack.Formatters.共通;
namespace TestLinqDB;
using Generic=System.Collections.Generic;
public abstract class 共通{
    private static int ポート番号;
    static 共通(){
        ポート番号=ListenerSocketポート番号;
    }
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
            var bytes=s.Serialize(input);
            var output=s.Deserialize<T>(bytes);
            Assert.Equal(input,output,this.Comparer);
        }
        {
            var s=this.MessagePack;
            var bytes=s.Serialize(input);
            var output=s.Deserialize<T>(bytes);
            Assert.Equal(input,output,this.Comparer);
        }
        {
            var s=this.Utf8Json;
            var bytes=s.Serialize(input);
            var output=s.Deserialize<T>(bytes);
            Assert.Equal(input,output,this.Comparer);
        }
    }
    protected void MemoryMessageJson_TObject<T>(T input){
        this.MemoryMessageJsonObject<object>(input!);
        this.MemoryMessageJsonObject(input);
    }
    protected readonly Optimizer Optimizer=new(){IsGenerateAssembly=false,Context=typeof(共通),AssemblyFileName="デバッグ.dll"};
    protected void 共通コンパイル実行<T,TResult>(Expressions.Expression<Func<T,TResult>> input,T t){
        var Optimizer=this.Optimizer;
        var 標準=input.Compile();
        var expected0=標準(t);
        Optimizer.IsInline=true;
        var expected2=Optimizer.CreateDelegate(input)(t);
        Assert.Equal(expected0,expected2,this.Comparer);
    }
    protected void 共通コンパイル実行<TResult>(Expressions.Expression<Func<TResult>> input,Action<TResult> AssertAction){
        var Optimizer=this.Optimizer;
        var 標準=input.Compile();
        AssertAction(標準());
        Optimizer.IsInline=true;
        AssertAction(Optimizer.CreateDelegate(input)());
    }
    protected void 共通コンパイル実行<TResult>(Expressions.Expression<Func<LinqDB.Sets.IEnumerable<TResult>>> input0,Expressions.Expression<Func<Generic.IEnumerable<TResult>>> input1){
        var Optimizer=this.Optimizer;
        var 標準0=input0.Compile();
        var 標準1=input1.Compile();
        var expected0=標準0();
        var expected1=標準1();
        Assert.Equal(expected0,expected1,this.Comparer);
        Optimizer.IsInline=true;
        var Del0=Optimizer.CreateDelegate(input0);
        var Del1=Optimizer.CreateDelegate(input1);
        var actual0=Del0();
        var actual1=Del1();
        Assert.True(actual0.SequenceEqual(actual1));
        Assert.True(expected0.SequenceEqual(actual0));
    }
    protected void 共通コンパイル実行<TResult>(Expressions.Expression<Func<TResult>> input0,Expressions.Expression<Func<TResult>> input1){
        var Optimizer=this.Optimizer;
        var 標準0=input0.Compile();
        var 標準1=input1.Compile();
        var expected0=標準0();
        var expected1=標準1();
        Assert.Equal(expected0,expected1);
        Optimizer.IsInline=true;
        var Del0=Optimizer.CreateDelegate(input0);
        var Del1=Optimizer.CreateDelegate(input1);
        var actual0=Del0();
        var actual1=Del1();
        Assert.Equal(expected0,actual0);
        Assert.Equal(expected1,actual1);
    }
    protected void 共通コンパイル実行<TResult>(Expressions.Expression<Func<TResult>> input){
        var Optimizer=this.Optimizer;
        var 標準=input.Compile();
        var expected0=標準();
        Optimizer.IsInline=true;
        var expected2=Optimizer.CreateDelegate(input)();
        Assert.Equal(expected0,expected2,this.Comparer);
    }
    protected void 共通コンパイル実行(Expressions.Expression<Action> input){
        var Optimizer=this.Optimizer;
        var 標準=input.Compile();
        標準();
        Optimizer.IsInline=true;
        Optimizer.CreateDelegate(input)();
    }
    protected void MemoryMessageJson_Expression_コンパイル実行(Expressions.Expression<Action> inputLambda){
        var Optimizer=this.Optimizer;
        var 標準=inputLambda.Compile();
        標準();
        Optimizer.IsInline=false;
        Optimizer.CreateDelegate(inputLambda)();
        Optimizer.IsInline=true;
        Optimizer.CreateDelegate(inputLambda)();
        this.MemoryMessageJson_Assert(inputLambda,共通);
        this.MemoryMessageJson_Assert<Expressions.Expression>(inputLambda,共通);
        this.MemoryMessageJson_Assert<object>(inputLambda,共通);
        void 共通(object output){
            var outputLambda=(Expressions.LambdaExpression)output;
            Assert.Equal(inputLambda,outputLambda,this.ExpressionEqualityComparer);
            Optimizer.CreateDelegate(inputLambda)();
            Optimizer.IsInline=false;
            Optimizer.CreateDelegate(inputLambda)();
            Optimizer.IsInline=true;
            Optimizer.CreateDelegate(inputLambda)();
        }
    }
    protected void MemoryMessageJson_Expression_コンパイルリモート実行<TResult>(Expressions.Expression<Func<TResult>> input){
        const int receiveTimeout=1000;
        var port=Interlocked.Increment(ref ポート番号);
        var Optimizer=this.Optimizer;
        var 標準=input.Compile();
        標準();
        Optimizer.IsInline=false;
        Optimizer.CreateDelegate(input)();
        Optimizer.IsInline=true;
        Optimizer.CreateDelegate(input)();
        using var Server=new Server(1,port);
        Server.ReadTimeout=receiveTimeout;
        Server.Open();
        using var R=new Client(Dns.GetHostName(),port);
        R.Expression(input,SerializeType.MemoryPack);
        R.Expression(input,SerializeType.MessagePack);
        R.Expression(input,SerializeType.Utf8Json);
        Server.Close();
        //this.共通MemoryMessageJson_TExpressionObject_コンパイル実行(input,Delegate=>((Func<TResult>)Delegate)());
    }
    //リモート実行できるか。
    protected void MemoryMessageJson_Expression_コンパイルリモート実行<T,TResult>(Expressions.Expression<Func<T,TResult>> input,T t){
        const int receiveTimeout=1000;
        var port=Interlocked.Increment(ref ポート番号);
        using var Server=new Server<T>(t,1,port);
        Server.ReadTimeout=receiveTimeout;
        Server.Open();
        using var R=new Client(Dns.GetHostName(),port);
        R.Expression(input,SerializeType.MemoryPack);
        R.Expression(input,SerializeType.MessagePack);
        R.Expression(input,SerializeType.Utf8Json);
        Server.Close();
        //this.共通MemoryMessageJson_TExpressionObject_コンパイル実行(input,Delegate=>((Func<T,TResult>)Delegate)(t));
    }
    //protected void MemoryMessageJson_Assert全パターン<T>(T input) where T : Expressions.Expression?
    //{
    //    //protected void 共通Expressions.Expression<T>(Expressions.Expressions.Expression<Func<T>> input){
    //    //this.共通object1(input,output=>Assert.Equal(input,output,this.ExpressionEqualityComparer));
    //    this.MemoryMessageJson_Assert(input, output => Assert.Equal(input, output, this.ExpressionEqualityComparer));
    //    this.MemoryMessageJson_Assert<Expressions.Expression>(input, output => Assert.Equal(input, output, this.ExpressionEqualityComparer));
    //    this.MemoryMessageJson_Assert<object>(input, output => Assert.Equal(input, (T)output, this.ExpressionEqualityComparer));
    //}
    private static readonly object Lockobject=new();
    protected void Memory_Assert<T>(T input)=>
        this.Memory_Assert(input,actual=>Assert.Equal(input,actual,this.Comparer));
    protected void Memory_Assert<T>(T input,Action<T> AssertAction){
        var s=this.MemoryPack;
        var bytes=this.MemoryPack.Serialize(input);
        var output=s.Deserialize<T>(bytes);
        AssertAction(output!);
    }
    protected void Message_Assert<T>(T input)=>
        this.Message_Assert(input,actual=>Assert.Equal(input,actual,this.Comparer));
    protected void Message_Assert<T>(T input,Action<T> AssertAction){
        var s=this.MessagePack;
        var bytes=s.Serialize(input);
        dynamic a=new NonPublicAccessor(s);
        var json=MessagePackSerializer.ConvertToJson(bytes,a.Options);
        var output=s.Deserialize<T>(bytes);
        AssertAction(output);
    }
    protected void Utf8_Assert<T>(T input)=>
        this.Utf8_Assert(input,actual=>Assert.Equal(input,actual,this.Comparer));
    protected void Utf8_Assert<T>(T input,Action<T> AssertAction){
        var s=this.Utf8Json;
        var bytes=s.Serialize(input);
        var json=Encoding.UTF8.GetString(bytes);
        var output=s.Deserialize<T>(bytes);
        AssertAction(output);
    }
    protected void MemoryMessageJson_Assert<T>(T input,Action<T> AssertAction){
        this.Memory_Assert(input,AssertAction);
        this.Message_Assert(input,AssertAction);
        this.Utf8_Assert(input,AssertAction);
    }
    protected void MemoryMessageJson_Assert<T>(T input){
        this.Memory_Assert(input);
        this.Message_Assert(input);
        this.Utf8_Assert(input);
    }
    protected static Expressions.LambdaExpression GetLambda<T>(Expressions.Expression<Func<T>> e)=>e;
    protected static object ClassDisplay取得(){
        var a=1;
        var body=GetLambda(()=>a).Body;
        var member=(Expressions.MemberExpression)body;
        var constant=(Expressions.ConstantExpression)member.Expression!;
        return constant.Value!;
    }
    protected static Reflection.MethodInfo GetMethod<T>(Expressions.Expression<Func<T>> e)=>((Expressions.MethodCallExpression)e.Body).Method;
    protected static Reflection.MethodInfo GetMethod(string Name)=>typeof(Serializer).GetMethod(Name,Reflection.BindingFlags.Static|Reflection.BindingFlags.NonPublic)!;
    protected static Reflection.MethodInfo M(Expressions.Expression<Action> f)=>((Expressions.MethodCallExpression)f.Body).Method;
    protected void MemoryMessageJson_Assert全パターン<T>(T? input)where T:Expressions.Expression{
        共通0(input);
        共通1(input);
        void 共通0(T? input0){
            var a=input0;
            var b=(Expressions.Expression?)input0;
            var c=(object?)input0;
            var d=default(T);
            var e=(Expressions.Expression?)default(T);
            var f=(object?)default(T);
            this.MemoryMessageJson_Assert(a,output => Assert.Equal(a,output,this.ExpressionEqualityComparer!));
            this.MemoryMessageJson_Assert(b,output => Assert.Equal(b,output,this.ExpressionEqualityComparer!));
            this.MemoryMessageJson_Assert(c);
            this.MemoryMessageJson_Assert(d,Assert.Null);
            this.MemoryMessageJson_Assert(e,Assert.Null);
            this.MemoryMessageJson_Assert(f,Assert.Null);
            this.MemoryMessageJson_Assert(new{a});
            this.MemoryMessageJson_Assert(new{b});
            this.MemoryMessageJson_Assert(new{c});
            this.MemoryMessageJson_Assert(new{d});
            this.MemoryMessageJson_Assert(new{e});
            this.MemoryMessageJson_Assert(new{f});
            this.MemoryMessageJson_Assert(new{a,b,c,d,e,f});
        }
        void 共通1(T? input0){
            var a=new[]{input0,input0};
            var b=new Expressions.Expression?[]{input0,input0};
            var c=new object?[]{input0,input0};
            var d=new T?[]{default,default};
            var e=new Expressions.Expression?[]{default,default};
            var f=new object?[]{default,default};
            this.MemoryMessageJson_Assert(a,output=>Assert.Equal(a,output,this.ExpressionEqualityComparer!));
            this.MemoryMessageJson_Assert(b,output=>Assert.Equal(b,output,this.ExpressionEqualityComparer!));
            this.MemoryMessageJson_Assert(c);
            this.MemoryMessageJson_Assert(d,output=>Assert.Equal(d,output,this.ExpressionEqualityComparer!));
            this.MemoryMessageJson_Assert(e,output=>Assert.Equal(e,output,this.ExpressionEqualityComparer!));
            this.MemoryMessageJson_Assert(f);
            this.MemoryMessageJson_Assert(new{a,b,c,d,e,f});
        }
    }
}
