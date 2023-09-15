//using System;
using System.Buffers;
using System.Collections.ObjectModel;
using System.Reflection;

using MemoryPack;
using MemoryPack.Formatters;
using Expressions = System.Linq.Expressions;
namespace LinqDB.Serializers.MemoryPack;
using Reader = MemoryPackReader;
public static class Extension{
    public static void WriteValue<T,TBufferWriter>(this ref MemoryPackWriter<TBufferWriter> writer,scoped ref T? value)where TBufferWriter :IBufferWriter<byte> =>writer.GetFormatter<T>()!.Serialize(ref writer,ref value);
    public static void ReadValue<T>(this ref Reader reader,scoped ref T? value)=>reader.GetFormatter<T>()!.Deserialize(ref reader,ref value);
    //public static void WriteType<TBufferWriter>(this ref MemoryPackWriter<TBufferWriter>writer,Type value)where TBufferWriter :IBufferWriter<byte> =>writer.WriteString(value.AssemblyQualifiedName);
    //public static Type ReadType(this ref Reader reader)=>Type.Deserialize().GetType(reader.ReadString())!;
    public static void WriteType<TBufferWriter>(this ref MemoryPackWriter<TBufferWriter>writer,System.Type value)where TBufferWriter :IBufferWriter<byte>{
        writer.WriteString(value.AssemblyQualifiedName);
    }
    public static System.Type ReadType(this ref Reader reader){
        return System.Type.GetType(reader.ReadString())!;
    }
    public static void WriteBoolean<TBufferWriter>(this ref MemoryPackWriter<TBufferWriter> writer,bool value)where TBufferWriter :IBufferWriter<byte> =>writer.WriteVarInt((byte)(value?1:0));
    public static bool ReadBoolean(this ref Reader reader)=>reader.ReadVarIntByte()!=0;
    public static void WriteNodeType<TBufferWriter>(this ref MemoryPackWriter<TBufferWriter> writer,Expressions.ExpressionType NodeType)where TBufferWriter :IBufferWriter<byte> =>writer.WriteVarInt((byte)NodeType);
    public static Expressions.ExpressionType ReadNodeType(this ref Reader reader){
        var value=reader.ReadVarIntByte();
        return (Expressions.ExpressionType)value;
    }
    public static bool TryWriteNil<TBufferWriter>(this ref MemoryPackWriter<TBufferWriter> writer,object? value)where TBufferWriter:IBufferWriter<byte>{
        if(value is not null){
            writer.WriteBoolean(false);
            return false;
        } else{
            writer.WriteBoolean(true);
            return true;
        }
    }
    public static bool TryReadNil(this ref Reader reader)=>reader.ReadBoolean();
    private static class StaticReadOnlyCollectionFormatter<T>{
        public static readonly ReadOnlyCollectionFormatter<T> Formatter=new();
    }
    internal static void SerializeReadOnlyCollection<T,TBufferWriter>(this ref MemoryPackWriter<TBufferWriter> writer,ReadOnlyCollection<T>? value)where TBufferWriter :IBufferWriter<byte> =>StaticReadOnlyCollectionFormatter<T>.Formatter.Serialize(ref writer,ref value!);
    private static class StaticArrayFormatter<T>{
        public static readonly ArrayFormatter<T> Formatter=new();
    }
    internal static T[] DeserializeArray<T>(this ref Reader reader){
        T[] value=default!;
        StaticArrayFormatter<T>.Formatter.Deserialize(ref reader,ref value!);
        return value;
    }
    public static void Serialize宣言Parameters<TBufferWriter>(this ref MemoryPackWriter<TBufferWriter> writer,ReadOnlyCollection<Expressions.ParameterExpression>value)where TBufferWriter :IBufferWriter<byte> {
        writer.WriteVarInt(value.Count);
        foreach(var Parameter in value){
            writer.WriteString(Parameter.Name);
            writer.WriteType(Parameter.Type);
        }
    }
    public static Expressions.ParameterExpression[]Deserialize宣言Parameters(this ref Reader reader){
        var Count=reader.ReadVarIntInt32();
        var Parameters=new Expressions.ParameterExpression[Count];
        for(var a=0;a<Count;a++){
            var name=reader.ReadString();
            var type=reader.ReadType();
            Parameters[a]=Expressions.Expression.Parameter(type,name);
        }
        return Parameters;
    }
    private static void Serialize2<TBufferWriter, TValue>(ref MemoryPackWriter<TBufferWriter> writer,
        scoped ref TValue? value) where TBufferWriter : IBufferWriter<byte> {
        writer.WriteValue(value);
    }
    public static readonly MethodInfo MethodSerialize = typeof(Extension).GetMethod(nameof(Serialize2),BindingFlags.Static|BindingFlags.NonPublic)!;
    private static void Deserialize2<TValue>(ref Reader reader,scoped ref TValue? value) {
        reader.ReadValue(ref value);
    }
    public static readonly MethodInfo MethodDeserialize = typeof(Extension).GetMethod(nameof(Deserialize2),BindingFlags.Static|BindingFlags.NonPublic)!;
    public static object ReadValue(this ref Reader reader,System.Type type){
        object? value=default;
        MemoryPack.Serializer.RegisterAnonymousDisplay(type);
        reader.ReadValue(type,ref value);
        return value!;
    }
    public static Serializer Serializer<TBufferWriter>(this ref MemoryPackWriter<TBufferWriter> writer)where TBufferWriter:IBufferWriter<byte> =>
        (Serializer)writer.Options.ServiceProvider!;
    public static Serializer Serializer(this ref Reader reader)=>
        (Serializer)reader.Options.ServiceProvider!;
}
