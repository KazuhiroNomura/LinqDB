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
    internal static void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,T value) where TBufferWriter:IBufferWriter<byte>{
        var type=value.ReflectedType!;
        writer.WriteType(type);
        var array= writer.Serializer().TypeFields.Get(type);
        var index=Array.IndexOf(array,value);
        writer.WriteVarInt(index);
    }
    //internal static void SerializeNullable<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,T? value) where TBufferWriter : IBufferWriter<byte> {
    //    Resolver.Serializer().WriteBoolean(ref writer,value is not null);
    //    if(value is not null) this.Serialize(ref writer,ref value);
    //}
    //internal MemberInfo? DeserializeNullable(ref MemoryPackReader reader) {
    //    var value0 = Resolver.Serializer().ReadBoolean(ref reader);
    //    return value0 ? this.Deserialize(ref reader) : null;
    //}
    public override void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,scoped ref T? value){
        Debug.Assert(value!=null,nameof(value)+" != null");
        Serialize(ref writer,value);
    }
    internal static T Deserialize(ref Reader reader) {
        var type= reader.ReadType();
        var array= reader.Serializer().TypeFields.Get(type);
        var index=reader.ReadVarIntInt32();
        return array[index];
    }
    public override void Deserialize(ref Reader reader,scoped ref T? value){
        value=Deserialize(ref reader);
    }
}
