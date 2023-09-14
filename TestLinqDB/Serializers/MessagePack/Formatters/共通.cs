using LinqDB.Optimizers;
using LinqDB.Remote.Clients;
using LinqDB.Remote.Servers;
using LinqDB.Sets;
using LinqDB;
using static LinqDB.Helpers.Configulation;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Contracts;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Microsoft.VisualStudio.TestPlatform.Utilities;
using MemoryPack;
using System.Buffers;
using System.Collections.ObjectModel;
using System.Reflection;
using LinqDB.Helpers;
using Expressions = System.Linq.Expressions;
using Newtonsoft.Json.Linq;
using MessagePack;
using Utf8Json;
using static LinqDB.Optimizers.Optimizer;
using System.Configuration;
using Utf8Json2=LinqDB.Serializers.Utf8Json;
using MessagePack2=LinqDB.Serializers.MessagePack;
using MemoryPack2=LinqDB.Serializers.MemoryPack;
using System.Reflection.PortableExecutable;

namespace Serializers.MessagePack.Formatters;
public abstract class 共通{
    protected Server<string> Server;
    protected readonly EnumerableSetEqualityComparer Comparer;
    protected readonly ExpressionEqualityComparer ExpressionEqualityComparer=new();
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
    protected void シリアライズデシリアライズ3パターン<T>(T input){
        {
            var bytes = MemoryPack2.Serializer.Instance.Serialize(input);
            var output = MemoryPack2.Serializer.Instance.Deserialize<T>(bytes);
            Assert.Equal(input,output,this.Comparer);
        }
        {
            var bytes = Utf8Json2.Serializer.Instance.Serialize(input);
            var output = Utf8Json2.Serializer.Instance.Deserialize<T>(bytes);
            Assert.Equal(input,output,this.Comparer);
        }
        {
            var bytes = MessagePack2.Serializer.Instance.Serialize(input);
            var output = MessagePack2.Serializer.Instance.Deserialize<T>(bytes);
            Assert.Equal(input,output,this.Comparer);
        }
    }
    protected void シリアライズデシリアライズ3パターンジェネリクス非ジェネリクス<T>(T input){
        Debug.Assert(input!=null,nameof(input)+" != null");
        this.シリアライズデシリアライズ3パターン<object>(input);
        this.シリアライズデシリアライズ3パターン(input);
    }
    protected readonly Optimizer Optimizer=new(){IsGenerateAssembly = false,Context=typeof(共通),AssemblyFileName="デバッグ.dll"};
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
    protected void シリアライズMemoryMessageJson(Expression input){
        this.シリアライズMemoryMessageJson(input,output=>{
            Assert.Equal(input,output,this.ExpressionEqualityComparer);
        });
        this.シリアライズMemoryMessageJson<Expression>(input,output=>{
            Assert.Equal(input,output,this.ExpressionEqualityComparer);
        });
        this.シリアライズMemoryMessageJson<object>(input,output=>{
            Assert.Equal(input,(Expression)output,this.ExpressionEqualityComparer);
        });
    }
    //リモート実行できるか。
    protected void シリアライズMemoryMessageJsonコンパイル<T>(Expression<Func<T>> input){
        var Optimizer=this.Optimizer;
        var 標準=input.Compile();
        var expected=標準();
        this.シリアライズMemoryMessageJson(input,共通);
        this.シリアライズMemoryMessageJson<Expression>(input,共通);
        this.シリアライズMemoryMessageJson<object>(input,共通);
        void 共通(object output){
            var output0=(Expression<Func<T>>)output;
            Assert.Equal(input,output0,this.ExpressionEqualityComparer);
            Optimizer.IsInline=false;
            {
                var M=Optimizer.CreateDelegate(output0);
                var actual=M();
                Assert.Equal(expected,actual,this.Comparer);
            }
            Optimizer.IsInline=true;
            {
                var M=Optimizer.CreateDelegate(output0);
                var actual=M();
                Assert.Equal(expected,actual,this.Comparer);
            }
        }
    }
    //リモート実行できるか。
    protected void シリアライズMemoryMessageJsonコンパイル<T,TResult>(Expression<Func<T,TResult>> input,T t){
        var Optimizer=this.Optimizer;
        var 標準=input.Compile();
        var expected=標準(t);
        this.シリアライズMemoryMessageJson(input,共通);
        this.シリアライズMemoryMessageJson<Expression>(input,共通);
        this.シリアライズMemoryMessageJson<object>(input,共通);
        void 共通(object output){
            var output0=(Expression<Func<T,TResult>>)output;
            Assert.Equal(input,output0,this.ExpressionEqualityComparer);
            Optimizer.IsInline=false;
            {
                var M=Optimizer.CreateDelegate(output0);
                var actual=M(t);
                Assert.Equal(expected,actual,this.Comparer);
            }
            Optimizer.IsInline=true;
            {
                var M=Optimizer.CreateDelegate(output0);
                var actual=M(t);
                Assert.Equal(expected,actual,this.Comparer);
            }
        }
    }
    protected void コンパイル<T>(T input) where T:Expression?{
        //var Optimizer=this.Optimizer;
        //var 標準=Lambda.Compile();
        //var expected=標準();
        //{
        //    Optimizer.IsInline=false;
        //    {
        //        var M=Optimizer.CreateDelegate(Lambda);
        //        var actual=M();
        //        Assert.Equal(expected,actual,this.Comparer);
        //    }
        //    Optimizer.IsInline=true;
        //    {
        //        var M=Optimizer.CreateDelegate(Lambda);
        //        var actual=M();
        //        Assert.Equal(expected,actual,this.Comparer);
        //    }
        //}
    }
    protected void シリアライズ<T>(T input) where T:Expression?{
    //protected void 共通Expression<T>(Expressions.Expression<Func<T>> input){
        //this.共通object1(input,output=>Assert.Equal(input,output,this.ExpressionEqualityComparer));
        this.シリアライズMemoryMessageJson(input,output=>Assert.Equal(input,output,this.ExpressionEqualityComparer));
        this.シリアライズMemoryMessageJson<Expression>(input,output=>Assert.Equal(input,output,this.ExpressionEqualityComparer));
        this.シリアライズMemoryMessageJson<object>(input,output=>Assert.Equal(input,(T)output,this.ExpressionEqualityComparer));
    }
    protected void 実行結果が一致するか確認<TResult>(Expression<Func<TResult>> Lambda){
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
    protected void シリアライズMemoryMessageJson<T>(T input,Action<T> AssertAction){
        lock(lockobject) {
            {
                var bytes = MemoryPack2.Serializer.Instance.Serialize(input);
                var output = MemoryPack2.Serializer.Instance.Deserialize<T>(bytes);
                AssertAction(output!);
            }
            {
                var bytes = MessagePack2.Serializer.Instance.Serialize(input);
                var s = MessagePackSerializer.ConvertToJson(bytes,MessagePack2.Serializer.Instance.Options);
                var output = MessagePack2.Serializer.Instance.Deserialize<T>(bytes);
                AssertAction(output);
            }
            {
                var bytes = Utf8Json2.Serializer.Instance.Serialize(input);
                var s = Encoding.UTF8.GetString(bytes);
                var output = Utf8Json2.Serializer.Instance.Deserialize<T>(bytes);
                AssertAction(output);
            }
        }
    }
}
