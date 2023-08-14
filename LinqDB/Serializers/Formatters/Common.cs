using System;
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
    public static void Serialize_T<T>(ref MessagePackWriter writer,T value,MessagePackSerializerOptions options)=>options.Resolver.GetFormatter<T>().Serialize(ref writer,value,options);
    public static void Serialize_Type(ref MessagePackWriter writer,Type value,MessagePackSerializerOptions options){
        writer.Write(value.AssemblyQualifiedName);
    }
    public static T Deserialize_T<T>(ref MessagePackReader reader,MessagePackSerializerOptions options)=>options.Resolver.GetFormatter<T>().Deserialize(ref reader,options);
    public static Type Deserialize_Type(ref MessagePackReader reader,MessagePackSerializerOptions options){
        var AssemblyQualifiedName=reader.ReadString();
        return Type.GetType(AssemblyQualifiedName)!;
    }
}
