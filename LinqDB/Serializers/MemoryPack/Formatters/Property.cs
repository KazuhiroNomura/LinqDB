using System;
using System.Buffers;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using MemoryPack;
using T=System.Reflection.PropertyInfo;
using C=LinqDB.Serializers.MemoryPack.Serializer;
namespace LinqDB.Serializers.MemoryPack.Formatters;
using Reader=MemoryPackReader;


public class Property:MemoryPackFormatter<T>{
    public static readonly Property Instance=new();
    internal void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,T? value)where TBufferWriter:IBufferWriter<byte> =>
        this.Serialize(ref writer,ref value);
    //internal T DeserializePropertyInfo(ref MemoryPackReader reader){
    //    T? value=default;
    //    this.Deserialize(ref reader,ref value);
    //    return value!;
    //}
    //internal void SerializeNullable<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,T? value) where TBufferWriter : IBufferWriter<byte> {
    //    writer.WriteBoolean(value is not null);
    //    if(value is not null) this.Serialize(ref writer,ref value);
    //}
    internal T Deserialize(ref Reader reader) {
        T? value = default;
        this.Deserialize(ref reader,ref value);
        return value!;
    }
    //internal T? DeserializeNullable(ref MemoryPackReader reader) {
    //    if(reader.PeekIsNull()){
    //        reader.Advance(1);
    //    }
    //    var value0 = reader.ReadBoolean();
    //    return value0 ? this.Deserialize(ref reader) : null;
    //}
    public override void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,scoped ref T? value){
       Debug.Assert(value!=null,nameof(value)+" != null");
       var ReflectedType=value.ReflectedType!;
       Type.Instance.Serialize(ref writer,ReflectedType);
       writer.WriteVarInt(Array.IndexOf(C.Instance.TypeProperties.Get(ReflectedType),value));
    }
    public override void Deserialize(ref Reader reader,scoped ref T? value){
        var ReflectedType= Type.Instance.Deserialize(ref reader);
        var array=C.Instance.TypeProperties.Get(ReflectedType);
        var Index=reader.ReadVarIntInt32();
        value=array[Index];
    }
}
