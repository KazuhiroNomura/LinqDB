using System;
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
    
    internal static void Write<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,T value) where TBufferWriter:IBufferWriter<byte>{
        var type=value.ReflectedType!;
        writer.WriteType(type);
        
        var array= writer.Serializer().TypeConstructors.Get(type);
        writer.WriteVarInt(Array.IndexOf(array,value));
    }
    public override void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,scoped ref T? value){
        if(writer.TryWriteNil(value)) return;
        Write(ref writer,value);
    }
    internal static T Read(ref Reader reader){
        
        var type=reader.ReadType();
        
        var array= reader.Serializer().TypeConstructors.Get(type);
        var index=reader.ReadVarIntInt32();
        
        return array[index];
    }
    public override void Deserialize(ref Reader reader,scoped ref T? value){
        if(reader.TryReadNil()) return;
        value=Read(ref reader);
    }
}
