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
    private static int ポート番号;
    static AssertDefinition(){
        ポート番号=ListenerSocketポート番号;
    }
    protected readonly Optimizer Optimizer=new(){IsGenerateAssembly=false,Context=typeof(共通),AssemblyFileName="デバッグ.dll"};
    private readonly LinqDB.Serializers.Serializer Serializer;
    protected readonly 汎用Comparer 汎用Comparer;
    protected ExpressionEqualityComparer ExpressionEqualityComparer=>new();
    internal SerializeType SerializeType=>
        this.Serializer switch{
            LinqDB.Serializers.MemoryPack.Serializer=>SerializeType.MemoryPack,
            LinqDB.Serializers.MessagePack.Serializer=>SerializeType.MessagePack,
            _=>SerializeType.Utf8Json,
        };
    public AssertDefinition(LinqDB.Serializers.Serializer Serializer){
        this.Serializer=Serializer;
        this.汎用Comparer=new(this.ExpressionEqualityComparer);
    }
    internal void AssertEqual<T>(T input)=>
        this.AssertAction(input,actual=>Assert.Equal(input,actual,this.汎用Comparer));
    internal void AssertAction<T>(T input,Action<T> Action){
        var s=this.Serializer;
        var bytes=s.Serialize(input);
        var output=s.Deserialize<T>(bytes);
        Action(output!);
    }
    //internal void Assert<T>(T input,Action<T> AssertAction){
    //    this.PrivateAssert(input,AssertAction);
    //}
    private void Expression全パターンAssertEqual<T>(T input)where T: Expressions.Expression{
        共通0(input);
        共通1(input);
        void 共通0(T? input0){
            var a=input0;
            var b=(Expressions.Expression?)input0;
            var c=(object?)input0;
            var d=default(T);
            var e=(Expressions.Expression?)default(T);
            var f=(object?)default(T);
            this.AssertEqual(a);
            this.AssertEqual(b);
            this.AssertEqual(c);
            this.AssertEqual(d);
            this.AssertEqual(e);
            this.AssertEqual(f);
            this.AssertEqual(new{a});
            this.AssertEqual(new{b});
            this.AssertEqual(new{c});
            this.AssertEqual(new{d});
            this.AssertEqual(new{e});
            this.AssertEqual(new{f});
            this.AssertEqual(new{a,b,c=e,d=f});
        }
        void 共通1(T? input0){
            var a=new[]{input0,input0};
            var b=new Expressions.Expression?[]{input0,input0};
            var c=new object?[]{input0,input0};
            var d=new T?[]{default,default};
            var e=new Expressions.Expression?[]{default,default};
            var f=new object?[]{default,default};
            this.AssertEqual(a);
            this.AssertEqual(b);
            this.AssertEqual(c);
            this.AssertEqual(d);
            this.AssertEqual(new{a});
            this.AssertEqual(new{b});
            this.AssertEqual(new{c});
            this.AssertEqual(new{d});
            this.AssertEqual(new{a,b,c,d});
        }
    }
    internal void AssertEqual全パターン<T>(T input){
        共通0(input);
        共通1(input);
        void 共通0(T? input0){
            var t=input0;
            var @object=(object?)input0;
            var default_T=default(T);
            var object_default_T=(object?)default(T);
            this.AssertEqual(t);
            this.AssertEqual(@object);
            this.AssertEqual(default_T);
            this.AssertEqual(object_default_T);
            this.AssertEqual(new{t});
            this.AssertEqual(new{@object});
            this.AssertEqual(new{default_T});
            this.AssertEqual(new{object_default_T});
            this.AssertEqual(new{t,@object,default_T,object_default_T});
        }
        void 共通1(T? input0){
            var a=new[]{input0,input0};
            var b=new object?[]{input0,input0};
            var c=new T?[]{default,default};
            var d=new object?[]{default,default};
            this.AssertEqual(a);
            this.AssertEqual(b);
            this.AssertEqual(c);
            this.AssertEqual(d);
            this.AssertEqual(new{a});
            this.AssertEqual(new{b});
            this.AssertEqual(new{c});
            this.AssertEqual(new{d});
            this.AssertEqual(new{a,b,c,d});
        }
    }
    public void MemoryMessageJson_Expression_コンパイル実行<TResult>(Expressions.Expression<Func<TResult>> input){
        var Optimizer=this.Optimizer;
        this.AssertAction(input,共通);
        this.AssertAction<Expressions.Expression>(input,共通);
        this.AssertAction<object>(input,共通);
        void 共通(object output){
            var actual0=input.Compile()();
            Optimizer.IsInline=false;
            var actual1=Optimizer.CreateDelegate(input)();
            Optimizer.IsInline=true;
            var actual2=Optimizer.CreateDelegate(input)();
        }
    }
    protected TResult コンパイルリモート実行<TResult>(Expressions.Expression<Func<TResult>> input){
        //this.MemoryMessageJson_Expression_コンパイル実行(input);
        const int receiveTimeout=1000;
        var port=Interlocked.Increment(ref ポート番号);
        var 標準=input.Compile();
        var expected=標準();
        using var Server=new Server(1,port);
        Server.ReadTimeout=receiveTimeout;
        Server.Open();
        using var R=new Client(Dns.GetHostName(),port);
        var actual=R.Expression(input,this.SerializeType);
        Server.Close();
        Assert.Equal(expected,actual,this.汎用Comparer);
        return expected;
    }
    protected void コンパイルリモート実行<T0,T1>(Expressions.Expression<Func<T0>> input0,Expressions.Expression<Func<T1>> input1){
        var actual0=this.コンパイルリモート実行(input0);
        var actual1=this.コンパイルリモート実行(input1);
        Assert.Equal(actual0,actual1,this.汎用Comparer);
    }
    //リモート実行できるか。
    protected void コンパイルリモート実行<T,TResult>(Expressions.Expression<Func<T,TResult>> input,T t){
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
    protected void コンパイル実行(Expressions.Expression<Action> inputLambda){
        var Optimizer=this.Optimizer;
        var 標準=inputLambda.Compile();
        標準();
        Optimizer.IsInline=false;
        Optimizer.CreateDelegate(inputLambda)();
        Optimizer.IsInline=true;
        Optimizer.CreateDelegate(inputLambda)();
        this.AssertEqual(inputLambda,共通);
        this.AssertEqual<Expressions.Expression>(inputLambda,共通);
        this.AssertEqual<object>(inputLambda,共通);
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
    protected void コンパイル実行<TResult>(Expressions.Expression<Func<TResult>> input){
        var expected=input.Compile()();
        var Optimizer=this.Optimizer;
        this.AssertEqual(input,共通);
        this.AssertEqual<Expressions.Expression>(input,共通);
        this.AssertEqual<object>(input,共通);
        void 共通(object output){
            Optimizer.IsInline=false;
            var actual1=Optimizer.CreateDelegate(input)();
            Optimizer.IsInline=true;
            var actual2=Optimizer.CreateDelegate(input)();
            var 汎用Comparer=this.汎用Comparer;
            Assert.Equal(expected,actual1,汎用Comparer);
            Assert.Equal(expected,actual2,汎用Comparer);
        }
    }
    protected void コンパイル実行<T,TResult>(Expressions.Expression<Func<T,TResult>> input,T t){
        var expected=input.Compile()(t);
        var Optimizer=this.Optimizer;
        this.AssertEqual(input,共通);
        this.AssertEqual<Expressions.Expression>(input,共通);
        this.AssertEqual<object>(input,共通);
        void 共通(object output){
            Optimizer.IsInline=false;
            var actual1=Optimizer.CreateDelegate(input)(t);
            Optimizer.IsInline=true;
            var actual2=Optimizer.CreateDelegate(input)(t);
            var 汎用Comparer=this.汎用Comparer;
            Assert.Equal(expected,actual1,汎用Comparer);
            Assert.Equal(expected,actual2,汎用Comparer);
        }
    }
    protected void コンパイル実行<T0,T1>(Expressions.Expression<Func<T0>> input0,Expressions.Expression<Func<T1>> input1){
        this.コンパイル実行(input0);
        this.コンパイル実行(input1);
    }

    protected TResult コンパイルリモート実行<TResult>(Expressions.Expression<Func<TResult>> input){
        //this.MemoryMessageJson_Expression_コンパイル実行(input);
        const int receiveTimeout=1000;
        var port=Interlocked.Increment(ref ポート番号);
        var 標準=input.Compile();
        var expected=標準();
        using var Server=new Server(1,port);
        Server.ReadTimeout=receiveTimeout;
        Server.Open();
        using var R=new Client(Dns.GetHostName(),port);
        var actual=R.Expression(input,this.AssertDefinition.SerializeType);
        Server.Close();
        Assert.Equal(expected,actual,this.汎用Comparer);
        return expected;
    }
    protected void コンパイルリモート実行<T0,T1>(Expressions.Expression<Func<T0>> input0,Expressions.Expression<Func<T1>> input1){
        var actual0=this.コンパイルリモート実行(input0);
        var actual1=this.コンパイルリモート実行(input1);
        Assert.Equal(actual0,actual1,this.汎用Comparer);
    }
    //リモート実行できるか。
    protected void コンパイルリモート実行<T,TResult>(Expressions.Expression<Func<T,TResult>> input,T t){
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
    protected void AssertEqual<T>(T input)=>this.AssertDefinition.AssertEqual(input);
    protected void AssertEqual<T>(T input,Action<T> AssertAction){
        this.AssertDefinition.AssertAction(input,AssertAction);
    }
    protected void ExpressionAssertEqual<T>(T input)where T: Expressions.Expression{
        this.AssertDefinition.AssertEqual(input);
    }
    //protected void AssertEqual全パターン<T>(T input){
    //    this.AssertDefinition.AssertEqual全パターン(input);
    //}
    protected static object ClassDisplay取得(){
        var a=1;
        var body=GetLambda(()=>a).Body;
        var member=(Expressions.MemberExpression)body;
        var constant=(Expressions.ConstantExpression)member.Expression!;
        return constant.Value!;
        static Expressions.LambdaExpression GetLambda<T>(Expressions.Expression<Func<T>> e)=>e;
    }
    protected static Reflection.MethodInfo GetMethod<T>(Expressions.Expression<Func<T>> e)=>((Expressions.MethodCallExpression)e.Body).Method;
    protected static Reflection.MethodInfo GetMethod(string Name)=>typeof(Serializer).GetMethod(Name,Reflection.BindingFlags.Static|Reflection.BindingFlags.NonPublic)!;
    protected static Reflection.MethodInfo M(Expressions.Expression<Action> f)=>((Expressions.MethodCallExpression)f.Body).Method;
}
