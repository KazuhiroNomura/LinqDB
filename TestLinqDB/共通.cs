#define MEMORY
#define MESSAGE
#define JSON
using Reflection = System.Reflection;
using LinqDB.Optimizers;
using LinqDB.Remote.Servers;
using static LinqDB.Helpers.Configulation;
using Expressions = System.Linq.Expressions;
using static LinqDB.Optimizers.Optimizer;
using LinqDB.Remote.Clients;
using System.Net;
using LinqDB;
namespace TestLinqDB;
//リモートで呼び出す、ローカルだけでやる、最適化なしでやるなど
public class AssertDefinition{
    protected readonly Optimizer Optimizer=new(){IsGenerateAssembly=false,Context=typeof(共通),AssemblyFileName="デバッグ.dll"};
    private readonly LinqDB.Serializers.Serializer Serializer;
    protected readonly 汎用Comparer 汎用Comparer;
    protected ExpressionEqualityComparer ExpressionEqualityComparer=>new();
    public AssertDefinition(LinqDB.Serializers.Serializer Serializer){
        this.Serializer=Serializer;
        this.汎用Comparer=new(this.ExpressionEqualityComparer);
    }
    internal void AssertEqual<T>(T input)=>
        this.Assert(input,actual=>Xunit.Assert.Equal(input,actual,this.汎用Comparer));
    internal void Assert<T>(T input,Action<T> AssertAction){
        var s=this.Serializer;
        var bytes=s.Serialize(input);
        var output=s.Deserialize<T>(bytes);
        AssertAction(output!);
    }
    internal void Message_Assert<T>(T input)=>
        this.Message_Assert(input,actual=>Xunit.Assert.Equal(input,actual,this.汎用Comparer));
    internal void Message_Assert<T>(T input,Action<T> AssertAction){
        var s=this.Serializer;
        var bytes=s.Serialize(input);
        var output=s.Deserialize<T>(bytes);
        AssertAction(output!);
    }
    internal void Utf8_Assert<T>(T input)=>
        this.Utf8_Assert(input,actual=>Xunit.Assert.Equal(input,actual,this.汎用Comparer));
    internal void Utf8_Assert<T>(T input,Action<T> AssertAction){
        var s=this.Serializer;
        var bytes=s.Serialize(input);
        var output=s.Deserialize<T>(bytes);
        AssertAction(output!);
    }
    internal void MemoryMessageJson_T_Assert<T>(T input){
        this.AssertEqual(input);
    }
    internal void MemoryMessageJson_T_Assert<T>(T input,Action<T> AssertAction){
#if MEMORY
        this.Assert(input,AssertAction);
#elif MESSAGE
        this.Message_Assert(input,AssertAction);
#elif JSON
        this.Utf8_Assert(input,AssertAction);
#endif
    }
    internal void MemoryMessageJson_Expression_Assert全パターン<T>(T input)where T: Expressions.Expression{
        共通0(input);
        共通1(input);
        void 共通0(T? input0){
            var a=input0;
            var b=(Expressions.Expression?)input0;
            var c=(object?)input0;
            var d=default(T);
            var e=(Expressions.Expression?)default(T);
            var f=(object?)default(T);
            this.MemoryMessageJson_T_Assert(a);
            this.MemoryMessageJson_T_Assert(b);
            this.MemoryMessageJson_T_Assert(c);
            this.MemoryMessageJson_T_Assert(d);
            this.MemoryMessageJson_T_Assert(e);
            this.MemoryMessageJson_T_Assert(f);
            this.MemoryMessageJson_T_Assert(new{a});
            this.MemoryMessageJson_T_Assert(new{b});
            this.MemoryMessageJson_T_Assert(new{c});
            this.MemoryMessageJson_T_Assert(new{d});
            this.MemoryMessageJson_T_Assert(new{e});
            this.MemoryMessageJson_T_Assert(new{f});
            this.MemoryMessageJson_T_Assert(new{a,b,c=e,d=f});
        }
        void 共通1(T? input0){
            var a=new[]{input0,input0};
            var b=new Expressions.Expression?[]{input0,input0};
            var c=new object?[]{input0,input0};
            var d=new T?[]{default,default};
            var e=new Expressions.Expression?[]{default,default};
            var f=new object?[]{default,default};
            this.MemoryMessageJson_T_Assert(a);
            this.MemoryMessageJson_T_Assert(b);
            this.MemoryMessageJson_T_Assert(c);
            this.MemoryMessageJson_T_Assert(d);
            this.MemoryMessageJson_T_Assert(new{a});
            this.MemoryMessageJson_T_Assert(new{b});
            this.MemoryMessageJson_T_Assert(new{c});
            this.MemoryMessageJson_T_Assert(new{d});
            this.MemoryMessageJson_T_Assert(new{a,b,c,d});
        }
    }
    internal void MemoryMessageJson_T_Assert全パターン<T>(T input){
        共通0(input);
        共通1(input);
        void 共通0(T? input0){
            var t=input0;
            var @object=(object?)input0;
            var default_T=default(T);
            var object_default_T=(object?)default(T);
            this.MemoryMessageJson_T_Assert(t);
            this.MemoryMessageJson_T_Assert(@object);
            this.MemoryMessageJson_T_Assert(default_T);
            this.MemoryMessageJson_T_Assert(object_default_T);
            this.MemoryMessageJson_T_Assert(new{t});
            this.MemoryMessageJson_T_Assert(new{@object});
            this.MemoryMessageJson_T_Assert(new{default_T});
            this.MemoryMessageJson_T_Assert(new{object_default_T});
            this.MemoryMessageJson_T_Assert(new{t,@object,default_T,object_default_T});
        }
        void 共通1(T? input0){
            var a=new[]{input0,input0};
            var b=new object?[]{input0,input0};
            var c=new T?[]{default,default};
            var d=new object?[]{default,default};
            this.MemoryMessageJson_T_Assert(a);
            this.MemoryMessageJson_T_Assert(b);
            this.MemoryMessageJson_T_Assert(c);
            this.MemoryMessageJson_T_Assert(d);
            this.MemoryMessageJson_T_Assert(new{a});
            this.MemoryMessageJson_T_Assert(new{b});
            this.MemoryMessageJson_T_Assert(new{c});
            this.MemoryMessageJson_T_Assert(new{d});
            this.MemoryMessageJson_T_Assert(new{a,b,c,d});
        }
    }
    public void MemoryMessageJson_Expression_コンパイル実行(Expressions.Expression<Action> input){
        throw new NotImplementedException();
    }
    public void MemoryMessageJson_Expression_コンパイル実行<TResult>(Expressions.Expression<Func<TResult>> input){
        var Optimizer=this.Optimizer;
        this.MemoryMessageJson_T_Assert(input,共通);
        this.MemoryMessageJson_T_Assert<Expressions.Expression>(input,共通);
        this.MemoryMessageJson_T_Assert<object>(input,共通);
        void 共通(object output){
            var actual0=input.Compile()();
            Optimizer.IsInline=false;
            var actual1=Optimizer.CreateDelegate(input)();
            Optimizer.IsInline=true;
            var actual2=Optimizer.CreateDelegate(input)();
        }
    }
}
public abstract class 共通{
    private readonly AssertDefinition AssertDefinition;
    protected 共通(AssertDefinition AssertDefinition){
        this.AssertDefinition=AssertDefinition;
        this.汎用Comparer=new(this.ExpressionEqualityComparer);
    }
    protected 共通(){
        this.AssertDefinition=new AssertDefinition(new LinqDB.Serializers.Utf8Json.Serializer());
        this.汎用Comparer=new(this.ExpressionEqualityComparer);
    }
    private static int ポート番号;
    static 共通(){
        ポート番号=ListenerSocketポート番号;
    }
    protected readonly 汎用Comparer 汎用Comparer;
    protected ExpressionEqualityComparer ExpressionEqualityComparer=>new();
    protected readonly LinqDB.Serializers.Utf8Json.Serializer Utf8Json=new();
    protected readonly LinqDB.Serializers.MessagePack.Serializer MessagePack=new();
    protected readonly LinqDB.Serializers.MemoryPack.Serializer MemoryPack=new();
    protected readonly Optimizer Optimizer=new(){IsGenerateAssembly=false,Context=typeof(共通),AssemblyFileName="デバッグ.dll"};
    protected void MemoryMessageJson_Expression_コンパイル実行(Expressions.Expression<Action> inputLambda){
        var Optimizer=this.Optimizer;
        var 標準=inputLambda.Compile();
        標準();
        Optimizer.IsInline=false;
        Optimizer.CreateDelegate(inputLambda)();
        Optimizer.IsInline=true;
        Optimizer.CreateDelegate(inputLambda)();
        this.MemoryMessageJson_T_Assert(inputLambda,共通);
        this.MemoryMessageJson_T_Assert<Expressions.Expression>(inputLambda,共通);
        this.MemoryMessageJson_T_Assert<object>(inputLambda,共通);
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
    protected void MemoryMessageJson_Expression_コンパイル実行<T0,T1>(Expressions.Expression<Func<T0>> input0,Expressions.Expression<Func<T1>> input1){
        this.MemoryMessageJson_Expression_コンパイル実行(input0);
        this.MemoryMessageJson_Expression_コンパイル実行(input1);
    }
    protected void MemoryMessageJson_Expression_コンパイル実行<TResult>(Expressions.Expression<Func<TResult>> input){
        this.AssertDefinition.MemoryMessageJson_Expression_コンパイル実行(input);
        var Optimizer=this.Optimizer;
        this.MemoryMessageJson_T_Assert(input,共通);
        this.MemoryMessageJson_T_Assert<Expressions.Expression>(input,共通);
        this.MemoryMessageJson_T_Assert<object>(input,共通);
        void 共通(object output){
            var actual0=input.Compile()();
            Optimizer.IsInline=false;
            var actual1=Optimizer.CreateDelegate(input)();
            Optimizer.IsInline=true;
            var actual2=Optimizer.CreateDelegate(input)();
        }
    }

    protected TResult MemoryMessageJson_Expression_コンパイルリモート実行<TResult>(Expressions.Expression<Func<TResult>> input){
        //this.MemoryMessageJson_Expression_コンパイル実行(input);
        const int receiveTimeout=1000;
        var port=Interlocked.Increment(ref ポート番号);
        var Optimizer=this.Optimizer;
        var 標準=input.Compile();
        var expected0=標準();
        using var Server=new Server(1,port);
        Server.ReadTimeout=receiveTimeout;
        Server.Open();
        using var R=new Client(Dns.GetHostName(),port);
#if MEMORY
        var actual0=R.Expression(input,SerializeType.MemoryPack);
#elif MESSAGE
        var actual1=R.Expression(input,SerializeType.MessagePack);
#elif JSON
        var actual2=R.Expression(input,SerializeType.Utf8Json);
#endif
        Server.Close();
#if MEMORY
        Assert.Equal(expected0,actual0,this.汎用Comparer);
#elif MESSAGE
        Assert.Equal(expected0,actual1,this.汎用Comparer);
#elif JSON
        Assert.Equal(expected0,actual2,this.汎用Comparer);
#endif
        return expected0;
    }
    protected void MemoryMessageJson_Expression_コンパイルリモート実行<T0,T1>(Expressions.Expression<Func<T0>> input0,Expressions.Expression<Func<T1>> input1){
        var actual0=this.MemoryMessageJson_Expression_コンパイルリモート実行(input0);
        var actual1=this.MemoryMessageJson_Expression_コンパイルリモート実行(input1);
        Assert.Equal(actual0,actual1,this.汎用Comparer);
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
    protected void Memory_Assert<T>(T input)=>
        this.AssertDefinition.Assert(input,actual=>Assert.Equal(input,actual,this.汎用Comparer));
    private void Memory_Assert<T>(T input,Action<T> AssertAction){
        this.AssertDefinition.Assert(input,AssertAction);
    }
    protected void Message_Assert<T>(T input)=>
        this.AssertDefinition.Message_Assert(input,actual=>Assert.Equal(input,actual,this.汎用Comparer));
    private void Message_Assert<T>(T input,Action<T> AssertAction){
        this.AssertDefinition.Message_Assert(input,AssertAction);
    }
    protected void Utf8_Assert<T>(T input)=>
        this.AssertDefinition.Utf8_Assert(input,actual=>Assert.Equal(input,actual,this.汎用Comparer));
    protected void Utf8_Assert<T>(T input,Action<T> AssertAction){
        this.AssertDefinition.Utf8_Assert(input,AssertAction);
    }
    protected void MemoryMessageJson_T_Assert<T>(T input){
#if MEMORY
        this.AssertDefinition.AssertEqual(input);
#elif MESSAGE
        this.AssertDefinition.Message_Assert(input);
#elif JSON
        this.AssertDefinition.Utf8_Assert(input);
#endif
    }
    protected void MemoryMessageJson_T_Assert<T>(T input,Action<T> AssertAction){
        this.AssertDefinition.MemoryMessageJson_T_Assert(input,AssertAction);
    }
    protected void MemoryMessageJson_Expression_Assert全パターン<T>(T input)where T: Expressions.Expression{
        this.AssertDefinition.MemoryMessageJson_Expression_Assert全パターン(input);
    }
    protected void MemoryMessageJson_T_Assert全パターン<T>(T input){
        this.AssertDefinition.MemoryMessageJson_T_Assert全パターン(input);
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
}
