using MemoryPack;
using System.Buffers;

namespace LinqDB.Serializers.MemoryPack.Formatters;
using Reader = MemoryPackReader;
using T = System.Type;


public class Type:MemoryPackFormatter<T> {
    public static readonly Type Instance=new();
    internal static void Write<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,T? value) where TBufferWriter:IBufferWriter<byte>{
        writer.WriteType(value);
    }
    public override void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,scoped ref T? value){
        writer.WriteType(value);
    }
    internal static T Read(ref Reader reader){
        return reader.ReadType();
    }
    public override void Deserialize(ref Reader reader,scoped ref T? value){
        value=reader.ReadType();
    }
}
