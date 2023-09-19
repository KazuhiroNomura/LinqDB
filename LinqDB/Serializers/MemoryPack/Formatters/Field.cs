using System;

using System.Reflection;
using MemoryPack;
using System.Buffers;
namespace LinqDB.Serializers.MemoryPack.Formatters;

using Reader = MemoryPackReader;
using T = FieldInfo;
public class Field:MemoryPackFormatter<T>{
    public static readonly Field Instance=new();
    internal static void Write<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,T value) where TBufferWriter:IBufferWriter<byte>{
        var type=value.ReflectedType!;
        writer.WriteType(type);
        
        
        
        
        var array=writer.Serializer().TypeFields.Get(type);
        var index=Array.IndexOf(array,value);
        writer.WriteVarInt(index);

    }
    public override void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,scoped ref T? value){
        if(writer.TryWriteNil(value)) return;
        Write(ref writer,value);
    }
    internal static T Read(ref Reader reader) {

        var type= reader.ReadType();
        
        
        
        var array=reader.Serializer().TypeFields.Get(type);
        var index=reader.ReadVarIntInt32();
        
        return array[index];
    }
    public override void Deserialize(ref Reader reader,scoped ref T? value)=>value=reader.TryReadNil()?null:Read(ref reader);
}
