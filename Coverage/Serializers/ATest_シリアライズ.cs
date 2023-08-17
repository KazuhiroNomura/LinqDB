using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using CoverageCS.LinqDB;
using LinqDB.CRC;
using LinqDB.Helpers;
using LinqDB.Optimizers;
using LinqDB.Remote.Clients;
using LinqDB.Serializers;
using LinqDB.Serializers.Formatters;
//using LinqDB.Serializers.Formatters;
//using LinqDB.Serializers.MessagePack;
using MessagePack;
using MessagePack.Formatters;
//using MessagePack.Resolvers;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Utf8Json;
using static System.Diagnostics.Contracts.Contract;
using Assert=Microsoft.VisualStudio.TestTools.UnitTesting.Assert;
using Json=Newtonsoft.Json;
// ReSharper disable PossibleNullReferenceException
//具体的なAnonymousTypeをそのままSerialize,DeserializeするときはAnonymousExpressionResolverを通過しない。AnonymousTypeを返す。
//具体的なAnonymousTypeをObjectでSerialize,DeserializeするときはAnonymousExpressionResolverを通過しない。Dictionaryを返す。
namespace CoverageCS.Serializers;
[TestClass]
public class ATest_シリアライズ:ATest{
    private static readonly IJsonFormatterResolver JsonFormatterResolver;
    //public readonly MessagePack.Resolver MessagePack_Resolver;
    private static readonly MessagePackSerializerOptions MessagePackSerializerOptions;
    static ATest_シリアライズ(){
        JsonFormatterResolver=Utf8Json.Resolvers.CompositeResolver.Create(
            new IJsonFormatter[]{
                classキーあり.JsonFormatter.Instance,
                sealed_classキーあり.JsonFormatter.Instance,
            },
            new IJsonFormatterResolver[]{
                Utf8Json.Resolvers.BuiltinResolver.Instance,//よく使う型
                Utf8Json.Resolvers.DynamicGenericResolver.Instance,//主にジェネリックコレクション
                Utf8Json.Resolvers.EnumResolver.Default,
                AnonymousExpressionJsonFormatterResolver.Instance,
            }
        );
        MessagePackSerializerOptions=MessagePackSerializerOptions.Standard.WithResolver(
            MessagePack.Resolvers.CompositeResolver.Create(
                new IMessagePackFormatter[]{
                    classキーあり.MessagePackFormatter.Instance,
                    sealed_classキーあり.MessagePackFormatter.Instance,
                },
                new IFormatterResolver[]{
                    MessagePack.Resolvers.BuiltinResolver.Instance,
                    MessagePack.Resolvers.DynamicGenericResolver.Instance,
                    AnonymousExpressionFormatterResolver.Instance,
                }
            )
        );
    }
    private static readonly Optimizer.ExpressionEqualityComparer ExpressionEqualityComparer=new(new List<ParameterExpression>());
    protected static void 共通object(object input){
        Private共通object(input,output=>Assert.IsTrue(Comparer.Equals(output,input)));
        Private共通object<object>(input,output=>Assert.IsTrue(Comparer.Equals(output,input)));
    }
    [MessagePackObject(true)]protected class classキーあり{
        public sealed class JsonFormatter:IJsonFormatter<classキーあり>{
            public static readonly JsonFormatter Instance=new();
            public void Serialize(ref JsonWriter writer,classキーあり value,IJsonFormatterResolver formatterResolver){
                Debug.Assert(value!=null,nameof(value)+" != null");
                writer.WriteBeginObject();
                writer.WriteString(nameof(value.a));
                writer.WriteNameSeparator();
                writer.WriteInt32(value.a);
                writer.WriteValueSeparator();
                writer.WriteString(nameof(value.b));
                writer.WriteNameSeparator();
                writer.WriteString(value.b);
                writer.WriteEndObject();
            }
            public classキーあり Deserialize(ref JsonReader reader,IJsonFormatterResolver formatterResolver){
                reader.ReadIsBeginObjectWithVerify();
                var Key_a= reader.ReadString();
                reader.ReadIsNameSeparatorWithVerify();
                var a=reader.ReadInt32();
                reader.ReadIsValueSeparatorWithVerify();
                var Key_b= reader.ReadString();
                reader.ReadIsNameSeparatorWithVerify();
                var b=reader.ReadString();
                var value=new classキーあり{a=a,b=b};
                reader.ReadIsEndObjectWithVerify();
                return value;
            }
        }
        public sealed class MessagePackFormatter:IMessagePackFormatter<classキーあり>{
            public static readonly MessagePackFormatter Instance=new();
            public void Serialize(ref MessagePackWriter writer,classキーあり value,MessagePackSerializerOptions options){
                writer.Write(value.a);
                writer.Write(value.b);
            }
            public classキーあり Deserialize(ref MessagePackReader reader,MessagePackSerializerOptions options){
                return new classキーあり{a=reader.ReadInt32(),b=reader.ReadString()};
            }
        }
        public int a=3;
        public string? b="b";
        public int property{get;set;}=4;
        public override bool Equals(object? obj)=>obj is classキーあり other&&this.Equals(other);
        public override int GetHashCode(){
            var CRC=new CRC32();
            CRC.Input(this.a);
            CRC.Input(this.b);
            return CRC.GetHashCode();
        }
        public bool Equals(classキーあり? other)=>other!=null&&this.a==other.a&&this.b.Equals(other.b);
    }
    [MessagePackObject(true)]protected sealed class sealed_classキーあり:IEquatable<sealed_classキーあり>{
        public sealed class JsonFormatter:IJsonFormatter<sealed_classキーあり>{
            public static readonly JsonFormatter Instance=new();
            public void Serialize(ref JsonWriter writer,sealed_classキーあり value,IJsonFormatterResolver formatterResolver){
                Debug.Assert(value!=null,nameof(value)+" != null");
                writer.WriteBeginObject();
                writer.WriteString(nameof(value.a));
                writer.WriteNameSeparator();
                writer.WriteInt32(value.a);
                writer.WriteValueSeparator();
                writer.WriteString(nameof(value.b));
                writer.WriteNameSeparator();
                writer.WriteString(value.b);
                writer.WriteEndObject();
            }
            public sealed_classキーあり Deserialize(ref JsonReader reader,IJsonFormatterResolver formatterResolver){
                reader.ReadIsBeginObjectWithVerify();
                var Key_a= reader.ReadString();
                reader.ReadIsNameSeparatorWithVerify();
                var a=reader.ReadInt32();
                reader.ReadIsValueSeparatorWithVerify();
                var Key_b= reader.ReadString();
                reader.ReadIsNameSeparatorWithVerify();
                var b=reader.ReadString();
                var value=new sealed_classキーあり{a=a,b=b};
                reader.ReadIsEndObjectWithVerify();
                return value;
            }
        }
        public sealed class MessagePackFormatter:IMessagePackFormatter<sealed_classキーあり>{
            public static readonly MessagePackFormatter Instance=new();
            public void Serialize(ref MessagePackWriter writer,sealed_classキーあり value,MessagePackSerializerOptions options){
                Debug.Assert(value!=null,nameof(value)+" != null");
                writer.Write(value.a);
                writer.Write(value.b);
            }
            public sealed_classキーあり Deserialize(ref MessagePackReader reader,MessagePackSerializerOptions options){
                return new sealed_classキーあり{a=reader.ReadInt32(),b=reader.ReadString()};
            }
        }
        public int a=3;
        public string? b="b";
        public int property{get;set;}=4;
        public override bool Equals(object? obj)=>obj is sealed_classキーあり other&&this.Equals(other);
        public override int GetHashCode(){
            var CRC=new CRC32();
            CRC.Input(this.a);
            CRC.Input(this.b);
            return CRC.GetHashCode();
        }
        public bool Equals(sealed_classキーあり? other)=>other!=null&&this.a==other.a&&this.b.Equals(other.b);
    }
    const string Messagepackファイル名="Messagepack.bin";
    const string Jsonファイル名="Json.txt";
    const string 整形済みJsonファイル名="整形済みJson.txt";
    private static void Private共通object<T>(T input,Action<T> AssertAction){
        //var jsonString = MessagePackSerializer.ConvertToJson(MessagePackSerializer.Serialize(input, SerializerSet.MessagePackSerializerOptions));
        try{
            var JsonStream=new FileStream(Jsonファイル名,FileMode.Create,FileAccess.Write,FileShare.ReadWrite);
            JsonSerializer.Serialize(JsonStream,input,JsonFormatterResolver);
            JsonStream.Close();
            var Json=File.ReadAllText(Jsonファイル名);
            File.WriteAllText(整形済みJsonファイル名,format_json(Json));
        } catch(Exception ex){

        }
        {
            var json0=File.ReadAllText(Jsonファイル名);
            var json1=File.ReadAllText(整形済みJsonファイル名);
            var T0=JsonSerializer.Deserialize<T>(json0,JsonFormatterResolver);
            AssertAction(T0);
        }
        {
            var MessagepackStream = new FileStream(Messagepackファイル名,FileMode.Create,FileAccess.Write,FileShare.ReadWrite);
            MessagePackSerializer.Serialize(MessagepackStream,input,MessagePackSerializerOptions);
            MessagepackStream.Close();
        }
        {
            var MessagepackAllBytes=File.ReadAllBytes(Messagepackファイル名);
            //var json0=MessagePackSerializer.(MessagepackAllBytes,SerializerConfiguration.MessagePackSerializerOptions);
            var json1=MessagePackSerializer.ConvertToJson(MessagepackAllBytes,MessagePackSerializerOptions);
            //var json2=MessagePackSerializer.ConvertToJson(MessagepackAllBytes,SerializerConfiguration.MessagePackSerializerOptions);
            var MessagepackStream = new FileStream(Messagepackファイル名,FileMode.Open,FileAccess.ReadWrite,FileShare.ReadWrite);
            var output = MessagePackSerializer.Deserialize<T>(MessagepackStream,MessagePackSerializerOptions);
            MessagepackStream.Close();
            AssertAction(output);
        }
    }
    protected static void 共通object<T>(T[] input){
        Private共通object(input,output=>Assert.IsTrue(output.SequenceEqual(input)));
    }
    protected static void 共通object<T>(T input){
        Private共通object(input,output=>Assert.IsTrue(Comparer.Equals(output,input)));
        Private共通object(input,output=>Assert.IsTrue(Comparer.Equals(output,input)));
        Private共通object<object>(input,output=>Assert.IsTrue(Comparer.Equals(output,input)));
    }
    protected static void 共通Expression<T>(T input)where T:Expression?{
        Debug.Assert(input!=null,nameof(input)+" != null");
        Private共通object<Expression>(input,output=>Assert.IsTrue(ExpressionEqualityComparer.Equals(input,output)));
    }
    private static string format_json(string json){
        dynamic parsedJson = Json.JsonConvert.DeserializeObject(json)!;
        return Json.JsonConvert.SerializeObject(parsedJson, Json.Formatting.Indented);
    }
}
