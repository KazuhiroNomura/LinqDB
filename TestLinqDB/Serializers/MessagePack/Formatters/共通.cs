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
//using Utf8Json2 = LinqDB.Serializers.Utf8Json;
//using MessagePack2 = LinqDB.Serializers.MessagePack;
//using MemoryPack2 = LinqDB.Serializers.MemoryPack;
//using Serializers=LinqDB.Serializers;
//using 共通=global.Serializers.MessagePack.Formatters.共通;
namespace Serializers.MessagePack.Formatters;
public abstract class 共通{
    protected Server<string> Server;
    protected readonly EnumerableSetEqualityComparer Comparer;
    protected ExpressionEqualityComparer ExpressionEqualityComparer=>new();
    private readonly LinqDB.Serializers.Utf8Json.Serializer Utf8Json=new();
    private readonly LinqDB.Serializers.MessagePack.Serializer MessagePack=new();
    private readonly LinqDB.Serializers.MemoryPack.Serializer MemoryPack=new();
    protected 共通(){
        const int ReceiveTimeout = 1000;
        this.Comparer=new(this.ExpressionEqualityComparer);
        var Server = this.Server=new Server<string>("",1,ListenerSocketポート番号) { ReadTimeout=ReceiveTimeout };
        Server.Open();
    }
    //protected IJsonFormatterResolver JsonFormatterResolver=>this.SerializerConfiguration.JsonFormatterResolver;
    //protected MessagePackSerializerOptions MessagePackSerializerOptions=>this.SerializerConfiguration.MessagePackSerializerOptions;
    //protected readonly MessagePackCustomSerializer MessagePackCustomSerializer=new();
    //protected readonly CustomSerializerMemoryPack CustomSerializerMemoryPack=new();
    //protected 共通(){
    //    var SerializerConfiguration=this.SerializerConfiguration;
    //    this.JsonFormatterResolver=Utf8Json.Resolvers.CompositeResolver.Create(
    //        new IJsonFormatter[]{
    //        },
    //        new IJsonFormatterResolver[]{
    //            SerializerConfiguration.JsonFormatterResolver
    //        }

    //        //new IJsonFormatterResolver[]{
    //        //    Utf8Json.Resolvers.BuiltinResolver.Instance,//よく使う型
    //        //    Utf8Json.Resolvers.DynamicGenericResolver.Instance,//主にジェネリックコレクション
    //        //    Utf8Json.Resolvers.EnumResolver.Default,
    //        //    AnonymousExpressionJsonFormatterResolver,
    //        //}
    //    );
    //    this.MessagePackSerializerOptions=MessagePackSerializerOptions.Standard.WithResolver(
    //        global::MessagePack.Resolvers.CompositeResolver.Create(
    //            new global::MessagePack.Formatters.IMessagePackFormatter[]{
    //            },
    //            new IFormatterResolver[]{
    //                SerializerConfiguration.MessagePackSerializerOptions.Resolver
    //            }
    //        )
    //    );
    //}
    class Display{
        public int a;
    }
    private static void Serialize2<TBufferWriter, TValue>(ref MemoryPackWriter<TBufferWriter> writer,
        scoped ref TValue? value) where TBufferWriter : IBufferWriter<byte> {
        writer.WriteValue(value);
        //writer.GetFormatter<TValue>()!.Serialize(ref writer,ref value);
    }
    public static readonly MethodInfo MethodSerialize = typeof(色んなデータ型).GetMethod(nameof(Serialize2),BindingFlags.Static|BindingFlags.NonPublic)!;
    private static void serialize<TBufferWriter,T>(ref MemoryPackWriter<TBufferWriter> writer,ref Display value)where TBufferWriter:IBufferWriter<byte>{
        Serialize2(ref writer,ref value.a);
    }
    private static void Deserialize2<TValue>(ref MemoryPackReader reader,scoped ref TValue? value) {
        reader.ReadValue(ref value);
        //reader.GetFormatter<TValue>()!.Deserialize(ref reader,ref value);
    }
    private static void deserialize<T>(ref MemoryPackReader reader,ref Display value){
        Deserialize2(ref reader,ref value.a);
    }
    protected void MemoryMessageJsonObject<T>(T input){
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
        Debug.Assert(input!=null,nameof(input)+" != null");
        this.MemoryMessageJsonObject<object>(input);
        this.MemoryMessageJsonObject(input);
    }
    protected readonly Optimizer Optimizer=new(){IsGenerateAssembly=false,Context=typeof(共通),AssemblyFileName="デバッグ.dll"};
    protected void 実行結果が一致するか確認(Expression<Action> Lambda){
        Lambda.Compile()();
        var Optimizer=this.Optimizer;
        Optimizer.IsInline=false;
        Optimizer.CreateDelegate(Lambda)();
        Optimizer.IsInline=true;
        Optimizer.CreateDelegate(Lambda)();
    }
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
    //リモート実行できるか。
    private void 共通MemoryMessageJson_TExpressionObject_コンパイル実行<T>(LambdaExpression inputLambda,Func<Delegate,T>x){
        var Optimizer=this.Optimizer;
        var 標準=inputLambda.Compile();
        var expected0=x(標準);
        Optimizer.IsInline=false;
        var expected1=x(Optimizer.CreateDelegate(inputLambda));
        Assert.Equal(expected0,expected1,this.Comparer);
        Optimizer.IsInline=true;
        var expected2=x(Optimizer.CreateDelegate(inputLambda));
        Assert.Equal(expected0,expected2,this.Comparer);
        this.MemoryMessageJson_Assert(inputLambda,共通);
        this.MemoryMessageJson_Assert<Expression>(inputLambda,共通);
        this.MemoryMessageJson_Assert<object>(inputLambda,共通);
        void 共通(object output){
            Assert.Equal(expected0,x(inputLambda.Compile()),this.Comparer);
            var outputLambda=(LambdaExpression)output;
            Assert.Equal(inputLambda,outputLambda,this.ExpressionEqualityComparer);
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
    protected void MemoryMessageJson_TExpressionObject_コンパイル実行<TResult>(Expression<Func<TResult>> input)=>
        this.共通MemoryMessageJson_TExpressionObject_コンパイル実行(input,Delegate=>((Func<TResult>)Delegate)());
    //リモート実行できるか。
    protected void MemoryMessageJson_TExpressionObject_コンパイル実行<T,TResult>(Expression<Func<T,TResult>> input,T t)=>
        this.共通MemoryMessageJson_TExpressionObject_コンパイル実行(input,Delegate=>((Func<T,TResult>)Delegate)(t));
    protected void MemoryMessageJson_TExpressionObject<T>(T input) where T:Expression?{
    //protected void 共通Expression<T>(Expressions.Expression<Func<T>> input){
        //this.共通object1(input,output=>Assert.Equal(input,output,this.ExpressionEqualityComparer));
        this.MemoryMessageJson_Assert(input,output=>Assert.Equal(input,output,this.ExpressionEqualityComparer));
        this.MemoryMessageJson_Assert<Expression>(input,output=>Assert.Equal(input,output,this.ExpressionEqualityComparer));
        this.MemoryMessageJson_Assert<object>(input,output=>Assert.Equal(input,(T)output,this.ExpressionEqualityComparer));
    }
    protected void MemoryMessageJson_TExpressionObject2_コンパイル実行<TResult>(Expression<Func<TResult>> Lambda){
        //this.シリアライズ(Lambda);
        //var 標準=Lambda.Compile();
        //var Optimizer=this.Optimizer;
        //Optimizer.IsInline=false;
        //var ラムダ=Optimizer.CreateDelegate(Lambda);
        //Optimizer.IsInline=true;
        //var ループ=Optimizer.CreateDelegate(Lambda);
        //var expected=標準();
        //{
        //    var actual=ラムダ();
        //    Assert.IsTrue(Comparer.Equals(expected,actual));
        //}
        //{
        //    var actual=ループ();
        //    Assert.IsTrue(Comparer.Equals(expected,actual));
        //}
        var Optimizer=this.Optimizer;
        var 標準=Lambda.Compile();
        var expected=標準();
        {
            Optimizer.IsInline=false;
            {
                var M=Optimizer.CreateDelegate(Lambda);
                var actual=M();
                Assert.Equal(expected,actual,this.Comparer);
            }
            Optimizer.IsInline=true;
            {
                var M=Optimizer.CreateDelegate(Lambda);
                var actual=M();
                Assert.Equal(expected,actual,this.Comparer);
            }
        }
        //{
        //    Optimizer.IsInline=false;
        //    var ラムダ=(Expression<Func<TResult>>)Optimizer.CreateExpression(Lambda);
        //    Optimizer.IsInline=true;
        //    var ループ=(Expression<Func<TResult>>)Optimizer.CreateExpression(Lambda);
        //    using var C=new Client<object>(Dns.GetHostName(),ListenerSocketポート番号);
        //    {
        //        var actual=C.Expression(ラムダ,XmlType.Utf8Json);
        //        Assert.Equal(expected,actual,this.Comparer);
        //    }
        //    {
        //        var actual=C.Expression(ループ,XmlType.MessagePack);
        //        Assert.Equal(expected,actual,this.Comparer);
        //    }
        //}
    }
    private static readonly object lockobject=new();
    protected void MemoryMessageJson_Assert<T>(T input,Action<T> AssertAction){
        lock(lockobject) {
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
