using System;
using System.Buffers;
using System.Diagnostics;
using System.Reflection;
using MemoryPack;
namespace LinqDB.Serializers.MemoryPack.Formatters;
using Reader=MemoryPackReader;
using T= FieldInfo;
using C= Serializer;

public class Field:MemoryPackFormatter<T>{
    public static readonly Field Instance=new();
    internal void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,T? value)where TBufferWriter:IBufferWriter<byte> =>
        this.Serialize(ref writer,ref value);
    //internal void SerializeNullable<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,T? value) where TBufferWriter : IBufferWriter<byte> {
    //    C.WriteBoolean(ref writer,value is not null);
    //    if(value is not null) this.Serialize(ref writer,ref value);
    //}
    internal MemberInfo Deserialize(ref Reader reader) {
        T? value = default;
        this.Deserialize(ref reader,ref value);
        return value!;
    }
    //internal MemberInfo? DeserializeNullable(ref MemoryPackReader reader) {
    //    var value0 = C.ReadBoolean(ref reader);
    //    return value0 ? this.Deserialize(ref reader) : null;
    //}
    public override void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,scoped ref T? value){
        Debug.Assert(value!=null,nameof(value)+" != null");
        var type=value.ReflectedType!;
        Type.Instance.Serialize(ref writer,type);
        var array= C.Instance.TypeFields.Get(type);
        var index=Array.IndexOf(array,value);
        writer.WriteVarInt(index);
    }
    public override void Deserialize(ref Reader reader,scoped ref T? value){
        var type= Type.Instance.Deserialize(ref reader);
        var array= C.Instance.TypeFields.Get(type);
        var index=reader.ReadVarIntInt32();
        value=array[index];
    }
}
