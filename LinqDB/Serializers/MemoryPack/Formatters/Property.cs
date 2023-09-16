using System;
using System.Buffers;
using System.Diagnostics;
using MemoryPack;
using T=System.Reflection.PropertyInfo;
using C=LinqDB.Serializers.MemoryPack.Serializer;
namespace LinqDB.Serializers.MemoryPack.Formatters;
using Reader=MemoryPackReader;


public class Property:MemoryPackFormatter<T>{
    public static readonly Property Instance=new();
    internal static void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,T value) where TBufferWriter:IBufferWriter<byte>{
        var type=value.ReflectedType!;
        writer.WriteType(type);
        var array= writer.Serializer().TypeProperties.Get(type);
        writer.WriteVarInt(Array.IndexOf(array,value));
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
       writer.WriteType(ReflectedType);
       writer.WriteVarInt(Array.IndexOf(writer.Serializer().TypeProperties.Get(ReflectedType),value));
    }
    internal static T Deserialize(ref Reader reader) {
        var type= reader.ReadType();
        var array=reader.Serializer().TypeProperties.Get(type);
        var Index=reader.ReadVarIntInt32();
        return array[Index];
    }
    public override void Deserialize(ref Reader reader,scoped ref T? value){
        value=Deserialize(ref reader);
    }
}
