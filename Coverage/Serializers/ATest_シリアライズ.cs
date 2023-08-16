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
using LinqDB.Helpers;
using LinqDB.Optimizers;
using LinqDB.Remote.Clients;
using LinqDB.Serializers;
using LinqDB.Serializers.Formatters;
//using LinqDB.Serializers.Formatters;
//using LinqDB.Serializers.MessagePack;
using MessagePack;
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
    protected static readonly SerializerConfiguration SerializerConfiguration=new();
    private static readonly Optimizer.ExpressionEqualityComparer ExpressionEqualityComparer=new(new List<ParameterExpression>());
    protected static void 共通object(object input){
        Private共通object(input,output=>Assert.IsTrue(Comparer.Equals(output,input)));
        Private共通object<object>(input,output=>Assert.IsTrue(Comparer.Equals(output,input)));
    }
    const string Messagepackファイル名="Messagepack.bin";
    const string Jsonファイル名="Json.txt";
    const string 整形済みJsonファイル名="整形済みJson.txt";
    private static void Private共通object<T>(T input,Action<T> AssertAction){
        SerializerConfiguration.Clear();
        //var jsonString = MessagePackSerializer.ConvertToJson(MessagePackSerializer.Serialize(input, SerializerSet.MessagePackSerializerOptions));
        {
            SerializerConfiguration.Clear();
            var JsonStream = new FileStream(Jsonファイル名,FileMode.Create,FileAccess.Write,FileShare.ReadWrite);
            JsonSerializer.Serialize(JsonStream,input,SerializerConfiguration.JsonFormatterResolver);
            JsonStream.Close();
            var Json=File.ReadAllText(Jsonファイル名);
            File.WriteAllText(整形済みJsonファイル名,format_json(Json));
        }
        {
            SerializerConfiguration.Clear();
            var json0=File.ReadAllText(Jsonファイル名);
            var json1=File.ReadAllText(整形済みJsonファイル名);
            var T0=JsonSerializer.Deserialize<T>(json0,SerializerConfiguration.JsonFormatterResolver);
            SerializerConfiguration.Clear();
            AssertAction(T0);
        }
        {
            SerializerConfiguration.Clear();
            var MessagepackStream = new FileStream(Messagepackファイル名,FileMode.Create,FileAccess.Write,FileShare.ReadWrite);
            MessagePackSerializer.Serialize(MessagepackStream,input,SerializerConfiguration.MessagePackSerializerOptions);
            MessagepackStream.Close();
        }
        {
            SerializerConfiguration.Clear();
            var MessagepackAllBytes=File.ReadAllBytes(Messagepackファイル名);
            //var json0=MessagePackSerializer.(MessagepackAllBytes,SerializerConfiguration.MessagePackSerializerOptions);
            //var json1=MessagePackSerializer.ConvertToJson(MessagepackAllBytes,SerializerConfiguration.MessagePackSerializerOptions);
            //var json2=MessagePackSerializer.ConvertToJson(MessagepackAllBytes,SerializerConfiguration.MessagePackSerializerOptions);
            SerializerConfiguration.Clear();
            var MessagepackStream = new FileStream(Messagepackファイル名,FileMode.Open,FileAccess.ReadWrite,FileShare.ReadWrite);
            var output = MessagePackSerializer.Deserialize<T>(MessagepackStream,SerializerConfiguration.MessagePackSerializerOptions);
            MessagepackStream.Close();
            AssertAction(output);
        }
    }
    protected static void 共通object<T>(T[] input){
        Private共通object(input,output=>Assert.IsTrue(output.SequenceEqual(input)));
    }
    protected static void 共通object<T>(T input){
        Private共通object(input,output=>Assert.IsTrue(Comparer.Equals(output,input)));
        Private共通object<object>(input,output=>Assert.IsTrue(Comparer.Equals(output,input)));
    }
    protected static void 共通Expression<T>(T input)where T:Expression?{
        Private共通object<Expression>(input,output=>Assert.IsTrue(ExpressionEqualityComparer.Equals(output,input)));
    }
    private static string format_json(string json){
        dynamic parsedJson = Json.JsonConvert.DeserializeObject(json)!;
        return Json.JsonConvert.SerializeObject(parsedJson, Json.Formatting.Indented);
    }
}
