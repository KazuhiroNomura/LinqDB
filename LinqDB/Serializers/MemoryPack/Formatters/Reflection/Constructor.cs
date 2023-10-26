

using System.Buffers;
using MemoryPack;
namespace LinqDB.Serializers.MemoryPack.Formatters.Reflection;


using Reader = MemoryPackReader;
using T = System.Reflection.ConstructorInfo;
public class Constructor:MemoryPackFormatter<T>{
    public static readonly Constructor Instance=new();

    internal static void Write<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,T value)where TBufferWriter:IBufferWriter<byte>{
        var type=value.ReflectedType!;
        writer.WriteType(type);

        var array=writer.Serializer().TypeConstructors.Get(type);
        var index=System.Array.IndexOf(array,value);
        writer.WriteVarInt(index);
    }
    internal static void WriteNullable<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,T?value)where TBufferWriter:IBufferWriter<byte>{
        if(writer.TryWriteNil(value)) return;
        Write(ref writer,value);
    }
    public override void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,scoped ref T? value)=>WriteNullable(ref writer,value);
    internal static T Read(ref Reader reader){

        var type=reader.ReadType();

        var array=reader.Serializer().TypeConstructors.Get(type);
        var index=reader.ReadVarIntInt32();

        return array[index];
    }
    internal static T?ReadNullable(ref Reader reader)=>reader.TryReadNil()?null:Read(ref reader);
    public override void Deserialize(ref Reader reader,scoped ref T? value)=>value=ReadNullable(ref reader);
}
