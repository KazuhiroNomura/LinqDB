using System;
using System.Buffers;
using System.Diagnostics;
using System.Reflection;
using MemoryPack;
namespace LinqDB.Serializers.MemoryPack.Formatters;
using Reader=MemoryPackReader;

using T= MethodInfo;
using C=Serializer;

public class Method:MemoryPackFormatter<T> {
    public static readonly Method Instance=new();
    internal static void Write<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,T value) where TBufferWriter:IBufferWriter<byte>{
        Debug.Assert(value!=null,nameof(value)+" != null");
        var ReflectedType=value.ReflectedType!;
        writer.WriteType(ReflectedType);
        var array= writer.Serializer().TypeMethods.Get(ReflectedType);
        writer.WriteVarInt(Array.IndexOf(array,value));
    }
    internal static void InternalSerializeNullable<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,T? value) where TBufferWriter : IBufferWriter<byte> {
        if(value is null)writer.WriteNullObjectHeader();
        else Instance.Serialize(ref writer,ref value);
    }
    public override void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,scoped ref T? value){
        Debug.Assert(value!=null,nameof(value)+" != null");
        Write(ref writer,value);
    }
    internal static T Read(ref Reader reader) {
        var type= reader.ReadType();
        var array= reader.Serializer().TypeMethods.Get(type);
        var index=reader.ReadVarIntInt32();
        return array[index];
    }
    internal static T? InternalDeserializeNullable(ref Reader reader) {
        if(reader.PeekIsNull()){
            reader.Advance(1);
            return null;
        }
        return Read(ref reader);
    }
    public override void Deserialize(ref Reader reader,scoped ref T? value){
        value=Read(ref reader);
    }
}
