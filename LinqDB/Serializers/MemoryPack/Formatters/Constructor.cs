using System;
using System.Buffers;
using System.Diagnostics;
using System.Reflection;
using MemoryPack;
namespace LinqDB.Serializers.MemoryPack.Formatters;
using Reader=MemoryPackReader;
using C=Serializer;
using T=ConstructorInfo;
public class Constructor:MemoryPackFormatter<T> {
    public static readonly Constructor Instance=new();
    internal static void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,T value) where TBufferWriter:IBufferWriter<byte>{
        var type=value.ReflectedType!;
        writer.WriteType(type);
        var array= writer.Serializer().TypeConstructors.Get(type);
        writer.WriteVarInt(Array.IndexOf(array,value));
    }
    internal static T Deserialize(ref Reader reader){
        var type=reader.ReadType();
        var array= reader.Serializer().TypeConstructors.Get(type);
        var index=reader.ReadVarIntInt32();
        return array[index];
    }
    public override void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,scoped ref T? value){
        Debug.Assert(value!=null,nameof(value)+" != null");
        Serialize(ref writer,value);
    }
    public override void Deserialize(ref Reader reader,scoped ref T? value){
        value=Deserialize(ref reader);
    }
}
