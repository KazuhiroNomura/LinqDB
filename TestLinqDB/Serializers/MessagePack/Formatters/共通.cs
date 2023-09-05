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
using LinqDB.Serializers;
using Microsoft.VisualStudio.TestPlatform.Utilities;
using LinqDB.Serializers.MemoryPack.Formatters;
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
namespace Serializers.MessagePack.Formatters;
public abstract class 共通{
    protected Server<string> Server;
    protected 共通(){
        const int ReceiveTimeout = 1000;
        var Server = this.Server=new Server<string>("",1,ListenerSocketポート番号) { ReadTimeout=ReceiveTimeout };
        Server.Open();
    }
    protected readonly EnumerableSetEqualityComparer Comparer=new();
    protected readonly ExpressionEqualityComparer ExpressionEqualityComparer=new();
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
    protected void 共通object1<T>(T input){
        {
            ////GetFormatter<T>Tが匿名型だと例外なのであらかじめ
            //if(typeof(T).IsAnonymous()){
            //    var Type=input.GetType();
            //    var FormatterType=typeof(Anonymous<>).MakeGenericType(Type);
            //    dynamic formatter = Activator.CreateInstance(FormatterType)!;
            //    MemoryPackFormatterProvider.Register(formatter);
            //    //var Register=typeof(MemoryPackFormatterProvider).GetMethod("Register",System.Type.EmptyTypes)!.MakeGenericMethod(Type);
            //    //Register.Invoke(null,Array.Empty<object>());
            //}
            var bytes=MemoryPackCustomSerializer.Serialize(input);
            var output = MemoryPackCustomSerializer.Deserialize<T>(bytes);
            Assert.Equal(input,output,this.Comparer);
        }
        {
            var bytes=Utf8JsonCustomSerializer.Serialize(input);
            var output = Utf8JsonCustomSerializer.Deserialize<T>(bytes);
            Assert.Equal(input,output,this.Comparer);
        }
        {
            var bytes=MessagePackCustomSerializer.Serialize(input);
            var output = MessagePackCustomSerializer.Deserialize<T>(bytes);
            Assert.Equal(input,output,this.Comparer);
        }
    }
    protected void 共通object2<T>(T input){
        Debug.Assert(input!=null,nameof(input)+" != null");
        this.共通object1<object>(input);
        this.共通object1(input);
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
    protected void 共通Expression<T>(T input)where T:Expressions.Expression?{
        //Debug.Assert(input!=null,nameof(input)+" != null");
        this.共通object1(input,output=>Assert.Equal(input,output,this.ExpressionEqualityComparer));
        //Private共通object<Expression>(input,output=>Assert.IsTrue(ExpressionEqualityComparer.Equals(input,output)));
    }
    protected TResult 実行結果が一致するか確認<TResult>(Expression<Func<TResult>> Lambda){
        this.共通Expression<Expressions.Expression>(Lambda);
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
        Optimizer.IsInline=false;
        var ラムダ=(Expression<Func<TResult>>)Optimizer.CreateExpression(Lambda);
        Optimizer.IsInline=true;
        var ループ=(Expression<Func<TResult>>)Optimizer.CreateExpression(Lambda);
        using var C = new Client<object>(Dns.GetHostName(),ListenerSocketポート番号);
        {
            var actual=C.Expression(ラムダ,XmlType.Utf8Json);
            Assert.Equal(expected,actual,this.Comparer);
        }
        {
            var actual=C.Expression(ループ,XmlType.MessagePack);
            Assert.Equal(expected,actual,this.Comparer);
        }
        return expected;
    }
    protected void 共通object1<T>(T input,Action<T> AssertAction){
        {
            //GetFormatter<T>Tが匿名型だと例外なのであらかじめ
            if(typeof(T).IsAnonymous()){
                var Type=input.GetType();
                var FormatterType=typeof(Anonymous<>).MakeGenericType(Type);
                dynamic formatter = Activator.CreateInstance(FormatterType)!;
                MemoryPackFormatterProvider.Register(formatter);
                //var Register=typeof(MemoryPackFormatterProvider).GetMethod("Register",System.Type.EmptyTypes)!.MakeGenericMethod(Type);
                //Register.Invoke(null,Array.Empty<object>());
            }
            var bytes=MemoryPackCustomSerializer.Serialize(input);
            var output = MemoryPackCustomSerializer.Deserialize<T>(bytes);
            AssertAction(output!);
        }
        {
            var bytes = Utf8JsonCustomSerializer.Serialize(input);
            var output = Utf8JsonCustomSerializer.Deserialize<T>(bytes);
            AssertAction(output);
        }
        {
            var bytes = MessagePackCustomSerializer.Serialize(input);
            var output = MessagePackCustomSerializer.Deserialize<T>(bytes);
            AssertAction(output);
        }
    }
}
