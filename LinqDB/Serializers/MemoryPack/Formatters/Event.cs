﻿using System;
using System.Buffers;
using System.Diagnostics;
using System.Reflection;
using MemoryPack;
namespace LinqDB.Serializers.MemoryPack.Formatters;
using Reader=MemoryPackReader;
using T=EventInfo;
using C=Serializer;
public class Event:MemoryPackFormatter<T>{
    public static readonly Event Instance=new();
    internal static void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,T? value)where TBufferWriter:IBufferWriter<byte> =>
        Instance.Serialize(ref writer,ref value);
    //internal static void SerializeNullable<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,T? value) where TBufferWriter : IBufferWriter<byte> {
    //    writer.WriteBoolean(value is not null);
    //    if(value is not null) this.Serialize(ref writer,ref value);
    //}
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
        var ReflectedType=value.ReflectedType!;
        Type.Serialize(ref writer,ReflectedType);
        var array= C.Instance.TypeEvents.Get(ReflectedType);
        writer.WriteVarInt(Array.IndexOf(array,value));
    }
    public override void Deserialize(ref Reader reader,scoped ref T? value){
        var ReflectedType= Type.Deserialize(ref reader);
        var array= C.Instance.TypeEvents.Get(ReflectedType);
        var Index=reader.ReadVarIntInt32();
        value=array[Index];
    }
}
