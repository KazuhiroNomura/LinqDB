using System;
using System.Reflection;
using Expressions=System.Linq.Expressions;
using MessagePack;
using Utf8Json;
using System.Collections.Generic;
using System.Collections.ObjectModel;
//using LinqDB.Serializers.Utf8Json.Formatters;
using Utf8Json.Formatters;
namespace LinqDB.Serializers.Utf8Json;
using Writer=JsonWriter;
using Reader=JsonReader;
internal static class Common{
    public static void WriteValue<T>(this ref Writer writer,T value,IJsonFormatterResolver Resolver)=>Resolver.GetFormatter<T>().Serialize(ref writer,value,Resolver);
    public static T ReadValue<T>(this ref Reader reader,IJsonFormatterResolver Resolver)=>Resolver.GetFormatter<T>().Deserialize(ref reader,Resolver);
    public static void WriteType(this ref Writer writer,Type value)=>writer.WriteString(value.AssemblyQualifiedName);
    public static Type ReadType(this ref Reader reader)=>Type.GetType(reader.ReadString())!;
    public static void WriteNodeType(this ref Writer writer,Expressions.ExpressionType NodeType)=>writer.WriteByte((byte)NodeType);
    public static Expressions.ExpressionType ReadNodeType(this ref Reader reader)=>(Expressions.ExpressionType)reader.ReadByte();
    public static bool WriteIsNull(this ref Writer writer,object? value){
        if(value is not null)return false;
        writer.WriteNull();
        return true;
    }
    private static class StaticReadOnlyCollectionFormatter<T>{
        public static readonly ReadOnlyCollectionFormatter<T> Formatter=new();
    }
    internal static void SerializeReadOnlyCollection<T>(ref Writer writer,
        ReadOnlyCollection<T>? value,IJsonFormatterResolver Resolver) =>
        StaticReadOnlyCollectionFormatter<T>.Formatter.Serialize(ref writer,value!,Resolver);
    private static class StaticArrayFormatter<T>{
        public static readonly ArrayFormatter<T> Formatter=new();
    }
    internal static T[] DeserializeArray<T>(ref Reader reader,IJsonFormatterResolver Resolver) =>
        StaticArrayFormatter<T>.Formatter.Deserialize(ref reader,Resolver)!;
    internal static void Serialize宣言Parameters(ref Writer writer,ReadOnlyCollection<Expressions.ParameterExpression> value,IJsonFormatterResolver Resolver){
        writer.WriteBeginArray();
        var Count=value.Count;
        if(Count>0){
            for(var a=0;;a++){
                var Parameter=value[a];
                writer.WriteBeginObject();
                writer.WriteString(Parameter.Name);
                writer.WriteNameSeparator();
                Formatters.Type.Instance.Serialize(ref writer,Parameter.Type,Resolver);
                writer.WriteEndObject();
                if(a==Count-1) break;
                writer.WriteValueSeparator();
            }
        }
        writer.WriteEndArray();
    }
    internal static List<Expressions.ParameterExpression> Deserialize宣言Parameters(ref Reader reader,IJsonFormatterResolver Resolver){
        var List=new List<Expressions.ParameterExpression>();
        //var t=reader;
        reader.ReadIsBeginArrayWithVerify();
        while(reader.ReadIsBeginObject()){
            var name=reader.ReadString();
            reader.ReadIsNameSeparatorWithVerify();
            var type=Formatters.Type.Instance.Deserialize(ref reader,Resolver);
            List.Add(Expressions.Expression.Parameter(type,name));
            reader.ReadIsEndObjectWithVerify();
            //var count=0;
            //if(!t.ReadIsEndObjectWithSkipValueSeparator(ref count)) break;
            if(!reader.ReadIsValueSeparator()) break;
        }
        reader.ReadIsEndArrayWithVerify();
        return List;
    }
}
