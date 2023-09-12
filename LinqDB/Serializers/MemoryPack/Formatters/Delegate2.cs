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
    internal void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,T? value)where TBufferWriter:IBufferWriter<byte> =>
        this.Serialize(ref writer,ref value);
    internal void SerializeNullable<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,T? value) where TBufferWriter : IBufferWriter<byte> {
        if(value is null)writer.WriteNullObjectHeader();
        else this.Serialize(ref writer,ref value);
    }
    internal T Deserialize(ref Reader reader) {
        T? value = default;
        this.Deserialize(ref reader,ref value);
        return value!;
    }
    internal T? DeserializeNullable(ref Reader reader) {
        if(reader.PeekIsNull()){
            reader.Advance(1);
            return null;
        }
        return this.Deserialize(ref reader);
    }
    public override void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,scoped ref T? value){
        Type.Instance.Serialize(ref writer,value!.GetType());
        Method.Instance.Serialize(ref writer,value.Method);
        Object.Instance.Serialize(ref writer,value.Target);
    }
    public override void Deserialize(ref Reader reader,scoped ref T? value){
        var delegateType=Type.Instance.Deserialize(ref reader);
        var method=Method.Instance.Deserialize(ref reader);
        var target=Object.Instance.Deserialize(ref reader);
        value=method.CreateDelegate(delegateType,target);
    }
}
