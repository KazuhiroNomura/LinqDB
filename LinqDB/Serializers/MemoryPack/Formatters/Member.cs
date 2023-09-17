using System.Buffers;
using System.Diagnostics;
using System;
using System.Reflection;
using MemoryPack;
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
    internal static void Write<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,T? value) where TBufferWriter:IBufferWriter<byte>{
        Debug.Assert(value!=null,nameof(value)+" != null");
        var type=value.ReflectedType!;
        writer.WriteType(type);
        var array= writer.Serializer().TypeMembers.Get(type);
        var index=Array.IndexOf(array,value);
        writer.WriteVarInt(index);
    }
    //internal T? DeserializeNullable(ref MemoryPackReader reader) {
    //    var value = reader.ReadBoolean();
    //    return value ? this.Deserialize(ref reader) : null;
    //}
    public override void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,scoped ref T? value){
        if(writer.TryWriteNil(value)) return;
        Write(ref writer,value);
    }
    internal static T Read(ref Reader reader) {
        var type= reader.ReadType();
        var array= reader.Serializer().TypeMembers.Get(type);
        var index=reader.ReadVarIntInt32();
        return array[index];
    }
    public override void Deserialize(ref Reader reader,scoped ref T? value){
        if(reader.TryReadNil()) return;
        value=Read(ref reader);
    }
}
