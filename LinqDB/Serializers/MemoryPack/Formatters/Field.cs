using System;
using System.Buffers;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using MemoryPack;
namespace LinqDB.Serializers.MemoryPack.Formatters;
using Reader=MemoryPackReader;
using T=System.Reflection.FieldInfo;
using C=LinqDB.Serializers.MemoryPack.MemoryPackCustomSerializer;

public class Field:MemoryPackFormatter<T>{
    public static readonly Field Instance=new();
    internal void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,T? value)where TBufferWriter:IBufferWriter<byte> =>
        this.Serialize(ref writer,ref value);
    //internal void SerializeNullable<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,T? value) where TBufferWriter : IBufferWriter<byte> {
    //    C.WriteBoolean(ref writer,value is not null);
    //    if(value is not null) this.Serialize(ref writer,ref value);
    //}
    internal MemberInfo Deserialize(ref MemoryPackReader reader) {
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
        var ReflectedType=value.ReflectedType!;
        Type.Instance.Serialize(ref writer,ReflectedType);
        var array= C.Instance.TypeFields.Get(ReflectedType);
        writer.WriteVarInt(Array.IndexOf(array,value));
    }
    public override void Deserialize(ref MemoryPackReader reader,scoped ref T? value){
        var ReflectedType= Type.Instance.Deserialize(ref reader);
        var array= C.Instance.TypeFields.Get(ReflectedType);
        var Index=reader.ReadVarIntInt32();
        value=array[Index];
    }
}
