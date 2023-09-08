using System.Buffers;
using System.Diagnostics;
using System.Linq;
using System;
using System.Reflection;
using MemoryPack;
namespace LinqDB.Serializers.MemoryPack.Formatters;
using Reader=MemoryPackReader;
using T=MemberInfo;
using C=Serializer;


public class Member:MemoryPackFormatter<T>{
    public static readonly Member Instance=new();
    //internal void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,T? value)where TBufferWriter:IBufferWriter<byte> =>
    //    this.Serialize(ref writer,ref value);
    //internal T DeserializeMemberInfo(ref MemoryPackReader reader){
    //    T? value=default;
    //    this.Deserialize(ref reader,ref value);
    //    return value!;
    //}
    internal void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,T? value)where TBufferWriter:IBufferWriter<byte> =>
        this.Serialize(ref writer,ref value);
    internal T Deserialize(ref Reader reader) {
        T? value = default;
        this.Deserialize(ref reader,ref value);
        return value!;
    }
    //internal T? DeserializeNullable(ref MemoryPackReader reader) {
    //    var value = reader.ReadBoolean();
    //    return value ? this.Deserialize(ref reader) : null;
    //}
    public override void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,scoped ref T? value){
        Debug.Assert(value!=null,nameof(value)+" != null");
        var ReflectedType=value.ReflectedType!;
        Type.Instance.Serialize(ref writer,ReflectedType);
        var array= C.Instance.TypeMembers.Get(ReflectedType);
        writer.WriteVarInt(Array.IndexOf(array,value));
    }
    public override void Deserialize(ref Reader reader,scoped ref T? value){
        var ReflectedType= Type.Instance.Deserialize(ref reader);
        var array= C.Instance.TypeMembers.Get(ReflectedType);
        var Index=reader.ReadVarIntInt32();
        value=array[Index];
    }
}
