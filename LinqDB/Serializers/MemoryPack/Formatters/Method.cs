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
    internal static void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,T? value)where TBufferWriter:IBufferWriter<byte> =>
        Instance.Serialize(ref writer,ref value);
    internal static void SerializeNullable<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,T? value) where TBufferWriter : IBufferWriter<byte> {
        if(value is null)writer.WriteNullObjectHeader();
        else Instance.Serialize(ref writer,ref value);
    }
    internal static T Deserialize(ref Reader reader) {
        T? value = default;
        Instance.Deserialize(ref reader,ref value);
        return value!;
    }
    internal static T? DeserializeNullable(ref Reader reader) {
        if(reader.PeekIsNull()){
            reader.Advance(1);
            return null;
        }
        return Deserialize(ref reader);
    }
    public override void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,scoped ref T? value){
        Debug.Assert(value!=null,nameof(value)+" != null");
        var ReflectedType=value.ReflectedType!;
        Type.Serialize(ref writer,ReflectedType);
        var array= C.Instance.TypeMethods.Get(ReflectedType);
        writer.WriteVarInt(Array.IndexOf(array,value));
    }
    public override void Deserialize(ref Reader reader,scoped ref T? value){
        var ReflectedType= Type.Deserialize(ref reader);
        var array= C.Instance.TypeMethods.Get(ReflectedType);
        var index=reader.ReadVarIntInt32();
        value=array[index];
    }
}
