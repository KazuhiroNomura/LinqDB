using System;
using System.Reflection;
using Expressions=System.Linq.Expressions;
using MessagePack;
using Utf8Json;
namespace LinqDB.Serializers.Formatters;
public static class Common{
    public static void Serialize_T<T>(ref JsonWriter writer,T value,IJsonFormatterResolver Resolver)=>Resolver.GetFormatter<T>().Serialize(ref writer,value,Resolver);
    public static void Serialize_Type(ref JsonWriter writer,Type value,IJsonFormatterResolver Resolver){
        writer.WriteString(value.AssemblyQualifiedName);
    }
    public static T Deserialize_T<T>(ref JsonReader reader,IJsonFormatterResolver Resolver)=>Resolver.GetFormatter<T>().Deserialize(ref reader,Resolver);
    public static Type Deserialize_Type(ref JsonReader reader,IJsonFormatterResolver Resolver){
        var AssemblyQualifiedName=reader.ReadString();
        return Type.GetType(AssemblyQualifiedName)!;
    }
    public static void Serialize_T<T>(ref MessagePackWriter writer,T value,MessagePackSerializerOptions options)=>options.Resolver.GetFormatter<T>()!.Serialize(ref writer,value,options);
    public static void Serialize_Type(ref MessagePackWriter writer,Type value,MessagePackSerializerOptions options){
        writer.Write(value.AssemblyQualifiedName);
    }
    public static T Deserialize_T<T>(ref MessagePackReader reader,MessagePackSerializerOptions options)=>options.Resolver.GetFormatter<T>()!.Deserialize(ref reader,options);
    public static Type Deserialize_Type(ref MessagePackReader reader,MessagePackSerializerOptions options)=>Type.GetType(reader.ReadString()!)!;
    private (Expressions.Expression Left,Expressions.Expression Right)Deserialize_Binary(ref JsonReader reader,IJsonFormatterResolver Resolver){
        var Left= this.Deserialize(ref reader,Resolver);
        reader.ReadIsValueSeparatorWithVerify();
        var Right= this.Deserialize(ref reader,Resolver);
        reader.ReadIsEndArrayWithVerify();
        return(Left,Right);
    }
    private (Expressions.Expression Left,Expressions.Expression Right,MethodInfo Method)Deserialize_Binary_MethodInfo(ref JsonReader reader,IJsonFormatterResolver Resolver){
        var Left= this.Deserialize(ref reader,Resolver);
        reader.ReadIsValueSeparatorWithVerify();
        var Right= this.Deserialize(ref reader,Resolver);
        reader.ReadIsValueSeparatorWithVerify();
        var Method=Deserialize_T<MethodInfo>(ref reader,Resolver);
        reader.ReadIsEndArrayWithVerify();
        return(Left,Right,Method);
    }
    private (Expressions.Expression Left,Expressions.Expression Right,bool IsLiftedToNull,MethodInfo Method)Deserialize_Binary_bool_MethodInfo(ref JsonReader reader,IJsonFormatterResolver Resolver){
        var Left= this.Deserialize(ref reader,Resolver);
        reader.ReadIsValueSeparatorWithVerify();
        var Right= this.Deserialize(ref reader,Resolver);
        reader.ReadIsValueSeparatorWithVerify();
        var IsLiftedToNull=reader.ReadBoolean();
        reader.ReadIsValueSeparatorWithVerify();
        var Method=Deserialize_T<MethodInfo>(ref reader,Resolver);
        reader.ReadIsEndArrayWithVerify();
        return(Left,Right,IsLiftedToNull,Method);
    }
}
