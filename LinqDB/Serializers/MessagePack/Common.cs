using System;
using System.Reflection;
using Expressions=System.Linq.Expressions;
using MessagePack;
using Utf8Json;
using MemoryPack;
using System.Buffers;
using System.Collections.ObjectModel;
using MessagePack.Formatters;
using System.Diagnostics;

namespace LinqDB.Serializers.MessagePack;
using Writer=MessagePackWriter;
using Reader=MessagePackReader;
public static class Common{
    public static void WriteValue<T>(this ref Writer writer,T value,MessagePackSerializerOptions options)=>options.Resolver.GetFormatter<T>()!.Serialize(ref writer,value,options);
    public static T ReadValue<T>(this ref Reader reader,MessagePackSerializerOptions options)=>options.Resolver.GetFormatter<T>()!.Deserialize(ref reader,options);
    public static void WriteType(this ref Writer writer,Type value)=>writer.Write(value.AssemblyQualifiedName);
    public static Type ReadType(this ref Reader reader)=>Type.GetType(reader.ReadString())!;
    public static void WriteBoolean(this ref Writer writer,bool value)=>writer.WriteInt8((sbyte)(value?1:0));
    public static bool ReadBoolean(this ref Reader reader)=>reader.ReadByte()!=0;
    public static void WriteNodeType(this ref Writer writer,Expressions.ExpressionType NodeType)=>writer.WriteInt8((sbyte)NodeType);
    public static Expressions.ExpressionType ReadNodeType(this ref Reader reader)=>(Expressions.ExpressionType)reader.ReadByte();
    private static class StaticReadOnlyCollectionFormatter<T>{
        public static readonly ReadOnlyCollectionFormatter<T> Formatter=new();
    }
    internal static void SerializeReadOnlyCollection<T>(ref Writer writer,ReadOnlyCollection<T>? value,MessagePackSerializerOptions Resolver){
        if(value is null){
            writer.WriteNil();
            return;
        }
        writer.WriteArrayHeader(value.Count);
        var Instance=Resolver.Resolver.GetFormatter<T>();
        Debug.Assert(Instance!=null,nameof(Instance)+" != null");
        foreach(var item in value)Instance.Serialize(ref writer,item,Resolver);
    }
    //internal static void SerializeReadOnlyCollection<T>(ref Writer writer,ReadOnlyCollection<T>? value,MessagePackSerializerOptions options) =>
    //    StaticReadOnlyCollectionFormatter<T>.Formatter.Serialize(ref writer,value!,options);
    private static class StaticArrayFormatter<T>{
        public static readonly ArrayFormatter<T> Formatter=new();
    }
    internal static T[] DeserializeArray<T>(ref Reader reader,MessagePackSerializerOptions Resolver){
        var count=reader.ReadInt32();
        var Instance=Resolver.Resolver.GetFormatter<T>();
        Debug.Assert(Instance!=null,nameof(Instance)+" != null");
        var array=new T[count];
        for(var a=0;a<count;a++)array[a]=Instance.Deserialize(ref reader,Resolver);
        return array;
    }
    //internal static T[] DeserializeArray<T>(ref Reader reader,MessagePackSerializerOptions options) =>
    //    StaticArrayFormatter<T>.Formatter.Deserialize(ref reader,options)!;
    public static void Serialize宣言Parameters(ref Writer writer,ReadOnlyCollection<Expressions.ParameterExpression>value) {
        writer.WriteArrayHeader(value.Count);
        foreach(var Parameter in value){
            writer.Write(Parameter.Name);
            writer.Write(Parameter.Type.AssemblyQualifiedName);
        }
    }
    public static Expressions.ParameterExpression[]Deserialize宣言Parameters(ref Reader reader,MessagePackSerializerOptions Resolver){
        var Count=reader.ReadArrayHeader();
        var Parameters=new Expressions.ParameterExpression[Count];
        for(var a=0;a<Count;a++){
            var name=reader.ReadString();
            var type= System.Type.GetType(reader.ReadString()!)!;
            Parameters[a]=Expressions.Expression.Parameter(type,name);
        }
        return Parameters;
    }
    //writer.Write((byte)value.NodeType);

    //private (Expressions.Expression Left,Expressions.Expression Right)Deserialize_Binary(ref JsonReader reader,IJsonFormatterResolver Resolver){
    //    var Left= Expression.Instance.Deserialize(ref reader,Resolver);
    //    reader.ReadIsValueSeparatorWithVerify();
    //    var Right= Expression.Instance.Deserialize(ref reader,Resolver);
    //    reader.ReadIsEndArrayWithVerify();
    //    return(Left,Right);
    //}
    //private (Expressions.Expression Left,Expressions.Expression Right,MethodInfo Method)Deserialize_Binary_MethodInfo(ref JsonReader reader,IJsonFormatterResolver Resolver){
    //    var Left= Expression.Instance.Deserialize(ref reader,Resolver);
    //    reader.ReadIsValueSeparatorWithVerify();
    //    var Right= Expression.Instance.Deserialize(ref reader,Resolver);
    //    reader.ReadIsValueSeparatorWithVerify();
    //    var Method=Deserialize_T<MethodInfo>(ref reader,Resolver);
    //    reader.ReadIsEndArrayWithVerify();
    //    return(Left,Right,Method);
    //}
    //private (Expressions.Expression Left,Expressions.Expression Right,bool IsLiftedToNull,MethodInfo Method)Deserialize_Binary_bool_MethodInfo(ref JsonReader reader,IJsonFormatterResolver Resolver){
    //    var Left= Expression.Instance.Deserialize(ref reader,Resolver);
    //    reader.ReadIsValueSeparatorWithVerify();
    //    var Right= Expression.Instance.Deserialize(ref reader,Resolver);
    //    reader.ReadIsValueSeparatorWithVerify();
    //    var IsLiftedToNull=reader.ReadBoolean();
    //    reader.ReadIsValueSeparatorWithVerify();
    //    var Method=Deserialize_T<MethodInfo>(ref reader,Resolver);
    //    reader.ReadIsEndArrayWithVerify();
    //    return(Left,Right,IsLiftedToNull,Method);
    //}
}
