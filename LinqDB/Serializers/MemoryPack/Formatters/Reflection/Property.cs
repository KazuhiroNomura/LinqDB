﻿

using MemoryPack;
using System.Buffers;

namespace LinqDB.Serializers.MemoryPack.Formatters.Reflection;

using Reader = MemoryPackReader;
using T = System.Reflection.PropertyInfo;
public class Property:MemoryPackFormatter<T>{
    public static readonly Property Instance=new();
    internal static void Write<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,T value)
        where TBufferWriter:IBufferWriter<byte>{

        var type=value.ReflectedType!;
        writer.WriteType(type);



        var array=writer.Serializer().TypeProperties.Get(type);
        var index=System.Array.IndexOf(array,value);
        writer.WriteVarInt(index);

    }
    internal static void WriteNullable<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,T? value)where TBufferWriter:IBufferWriter<byte>{
        if(writer.TryWriteNil(value)) return;
        Write(ref writer,value);
    }
    public override void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,scoped ref T? value)=>WriteNullable(ref writer,value);
    internal static T Read(ref Reader reader){

        var type=reader.ReadType();



        var array=reader.Serializer().TypeProperties.Get(type);
        var index=reader.ReadVarIntInt32();

        return array[index];
    }
    internal static T? ReadNullable(ref Reader reader)=>reader.TryReadNil()?null:Read(ref reader);
    public override void Deserialize(ref Reader reader,scoped ref T? value)=>value=ReadNullable(ref reader);
}
