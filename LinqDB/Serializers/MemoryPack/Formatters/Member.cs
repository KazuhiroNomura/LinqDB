using System.Buffers;
using System.Diagnostics;
using System;
using System.Reflection;
using MemoryPack;
using System.Reflection.PortableExecutable;

namespace LinqDB.Serializers.MemoryPack.Formatters;
using Reader=MemoryPackReader;
using T=MemberInfo;
using C=Serializer;


public class Member:MemoryPackFormatter<T>{
    public static readonly Member Instance=new();
    //internal static void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,T? value)where TBufferWriter:IBufferWriter<byte> =>
    //    this.Serialize(ref writer,ref value);
    //internal static T DeserializeMemberInfo(ref MemoryPackReader reader){
    //    T? value=default;
    //    this.Deserialize(ref reader,ref value);
    //    return value!;
    //}
    internal static void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,T? value)where TBufferWriter:IBufferWriter<byte> =>
        Instance.Serialize(ref writer,ref value);
    internal static T Deserialize(ref Reader reader) {
        T? value = default;
        Instance.Deserialize(ref reader,ref value);
        return value!;
    }
    //internal T? DeserializeNullable(ref MemoryPackReader reader) {
    //    var value = reader.ReadBoolean();
    //    return value ? this.Deserialize(ref reader) : null;
    //}
    public override void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,scoped ref T? value){
        Debug.Assert(value!=null,nameof(value)+" != null");
        var type=value.ReflectedType!;
        writer.WriteType(type);
        var array= writer.Serializer().TypeMembers.Get(type);
        var index=Array.IndexOf(array,value);
        writer.WriteVarInt(index);
    }
    public override void Deserialize(ref Reader reader,scoped ref T? value){
        var type= reader.ReadType();
        var array= reader.Serializer().TypeMembers.Get(type);
        var index=reader.ReadVarIntInt32();
        value=array[index];
    }
}
