using System;
using System.Reflection;
using Expressions=System.Linq.Expressions;
using MemoryPack;
using Utf8Json;
using System.Buffers;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using MessagePack;
using System.Collections.ObjectModel;
using MemoryPack.Formatters;
namespace LinqDB.Serializers.MemoryPack;
//using Writer=MemoryPackWriter;
using Reader=MemoryPackReader;
public static class Common{
    public static void WriteValue<T,TBufferWriter>(this ref MemoryPackWriter<TBufferWriter> writer,scoped ref T? value)where TBufferWriter :IBufferWriter<byte> =>writer.GetFormatter<T>()!.Serialize(ref writer,ref value);
    public static void ReadValue<T>(this ref Reader reader,scoped ref T? value)=>reader.GetFormatter<T>()!.Deserialize(ref reader,ref value);
    public static void WriteType<TBufferWriter>(this ref MemoryPackWriter<TBufferWriter>writer,Type value)where TBufferWriter :IBufferWriter<byte> =>writer.WriteString(value.AssemblyQualifiedName);
    public static Type ReadType(this ref Reader reader)=>Type.GetType(reader.ReadString())!;
    public static void WriteBoolean<TBufferWriter>(this ref MemoryPackWriter<TBufferWriter> writer,bool value)where TBufferWriter :IBufferWriter<byte> =>writer.WriteVarInt((byte)(value?1:0));
    public static bool ReadBoolean(this ref Reader reader)=>reader.ReadVarIntByte()!=0;
    public static void WriteNodeType<TBufferWriter>(this ref MemoryPackWriter<TBufferWriter> writer,Expressions.ExpressionType NodeType)where TBufferWriter :IBufferWriter<byte> =>writer.WriteVarInt((byte)NodeType);
    public static Expressions.ExpressionType ReadNodeType(this ref Reader reader)=>(Expressions.ExpressionType)reader.ReadVarIntByte();
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
    internal static void SerializeReadOnlyCollection<T,TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,ReadOnlyCollection<T>? value)where TBufferWriter :IBufferWriter<byte> =>StaticReadOnlyCollectionFormatter<T>.Formatter.Serialize(ref writer,ref value!);
    private static class StaticArrayFormatter<T>{
        public static readonly ArrayFormatter<T> Formatter=new();
    }
    internal static T[] DeserializeArray<T>(ref Reader reader){
        T[] value=default!;
        StaticArrayFormatter<T>.Formatter.Deserialize(ref reader,ref value!);
        return value;
    }
    public static void Serialize宣言Parameters<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,ReadOnlyCollection<Expressions.ParameterExpression>value)where TBufferWriter :IBufferWriter<byte> {
        writer.WriteVarInt(value.Count);
        foreach(var Parameter in value){
            writer.WriteString(Parameter.Name);
            writer.WriteType(Parameter.Type);
        }
    }
    public static Expressions.ParameterExpression[]Deserialize宣言Parameters(ref Reader reader){
        var Count=reader.ReadVarIntInt32();
        var Parameters=new Expressions.ParameterExpression[Count];
        for(var a=0;a<Count;a++){
            var name=reader.ReadString();
            var type=reader.ReadType();
            Parameters[a]=Expressions.Expression.Parameter(type,name);
        }
        return Parameters;
    }
}
