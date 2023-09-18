using System;

using System.Reflection;
using MemoryPack;
using System.Buffers;
namespace LinqDB.Serializers.MemoryPack.Formatters;

using Reader = MemoryPackReader;
using T = MethodInfo;
public class Method:MemoryPackFormatter<T> {
    public static readonly Method Instance=new();
    
    internal static void Write<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,T value) where TBufferWriter:IBufferWriter<byte>{

        var type=value.ReflectedType!;
        writer.WriteType(type);



        var array= writer.Serializer().TypeMethods.Get(type);
        writer.WriteVarInt(Array.IndexOf(array,value));

    }
    internal static void WriteNullable<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,T? value) where TBufferWriter : IBufferWriter<byte> {
        if(writer.TryWriteNil(value)) return;
        Write(ref writer,value);
    }
    public override void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,scoped ref T? value){
        WriteNullable(ref writer,value);
    }
    internal static T Read(ref Reader reader) {


        var type= reader.ReadType();

        
        var array= reader.Serializer().TypeMethods.Get(type);
        var index=reader.ReadVarIntInt32();
        
        return array[index];
    }
    internal static T? ReadNullable(ref Reader reader){
        if(reader.TryReadNil()) return null;
        return Read(ref reader);
    }
    public override void Deserialize(ref Reader reader,scoped ref T? value){
        value=Read(ref reader);
    }
}
