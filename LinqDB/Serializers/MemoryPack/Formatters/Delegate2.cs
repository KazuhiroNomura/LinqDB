using System;
using System.Buffers;
using System.Diagnostics;
using System.Reflection;
using MemoryPack;
namespace LinqDB.Serializers.MemoryPack.Formatters;
using Reader=MemoryPackReader;

using T= System.Delegate;
using C=Serializer;

public class Delegate2:MemoryPackFormatter<T> {
    public static readonly Delegate2 Instance=new();
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
        Type.Serialize(ref writer,value!.GetType());
        Method.Serialize(ref writer,value.Method);
        Object.Serialize(ref writer,value.Target);
    }
    public override void Deserialize(ref Reader reader,scoped ref T? value){
        var delegateType=Type.Deserialize(ref reader);
        var method=Method.Deserialize(ref reader);
        var target=Object.Deserialize(ref reader);
        value=method.CreateDelegate(delegateType,target);
    }
}
