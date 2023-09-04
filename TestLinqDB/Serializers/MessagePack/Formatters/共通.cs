using LinqDB.Optimizers;
using LinqDB.Remote.Clients;
using LinqDB.Remote.Servers;
using LinqDB.Sets;
using LinqDB;

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
using Expressions=System.Linq.Expressions;
using Newtonsoft.Json.Linq;
using MessagePack;
using Utf8Json;
using static LinqDB.Optimizers.Optimizer;

namespace Serializers.MessagePack.Formatters;
public abstract class 共通{
    protected static readonly EnumerableSetEqualityComparer Comparer=new();
    protected readonly ExpressionEqualityComparer ExpressionEqualityComparer=new();
    protected IJsonFormatterResolver JsonFormatterResolver=>this.SerializerConfiguration.JsonFormatterResolver;
    protected MessagePackSerializerOptions MessagePackSerializerOptions=>this.SerializerConfiguration.MessagePackSerializerOptions;
    protected readonly SerializerConfiguration SerializerConfiguration=new();
    protected readonly 必要なFormatters Formatters=new();
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
        var Formatters=this.Formatters;
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
            Formatters.変数Register(input.GetType());
            byte[] bytes;
            Formatters.Clear();
            bytes=MemoryPackSerializer.Serialize(input);
            while(true){
                try{
                    break;
                } catch(MemoryPackSerializationException ex){
                }
            }
            Formatters.Clear();
            var output = MemoryPackSerializer.Deserialize<T>(bytes);
            Assert.Equal(input,output,Comparer);
        }
        var SerializerConfiguration=this.SerializerConfiguration;
        {
            SerializerConfiguration.ClearMessagePack();
            var bytes = Utf8Json.JsonSerializer.Serialize(input,this.JsonFormatterResolver);
            SerializerConfiguration.ClearMessagePack();
            var output = Utf8Json.JsonSerializer.Deserialize<T>(bytes,this.JsonFormatterResolver);
            Assert.Equal(input,output,Comparer);
        }
        {
            SerializerConfiguration.ClearMessagePack();
            var bytes = global::MessagePack.MessagePackSerializer.Serialize(input,this.MessagePackSerializerOptions);
            SerializerConfiguration.ClearMessagePack();
            var output = global::MessagePack.MessagePackSerializer.Deserialize<T>(bytes,this.MessagePackSerializerOptions);
            Assert.Equal(input,output,Comparer);
        }
    }
    protected void 共通object2<T>(T input){
        Debug.Assert(input!=null,nameof(input)+" != null");
        this.共通object1<object>(input);
        this.共通object1(input);
    }
}
