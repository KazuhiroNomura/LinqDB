﻿using System;
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
    internal static void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,T? value)where TBufferWriter:IBufferWriter<byte> =>
        Instance.Serialize(ref writer,ref value);
    internal static T Deserialize(ref Reader reader){
        T? value=default;
        Instance.Deserialize(ref reader,ref value);
        return value!;
    }
    public override void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,scoped ref T? value){
        Debug.Assert(value!=null,nameof(value)+" != null");
        var ReflectedType=value.ReflectedType!;
        Type.Serialize(ref writer,ReflectedType);
        var array= C.Instance.TypeConstructors.Get(ReflectedType);
        writer.WriteVarInt(Array.IndexOf(array,value));
    }
    public override void Deserialize(ref Reader reader,scoped ref T? value){
        var type=reader.ReadType();
        var array= C.Instance.TypeConstructors.Get(type);
        var index=reader.ReadVarIntInt32();
        value=array[index];
    }
}
