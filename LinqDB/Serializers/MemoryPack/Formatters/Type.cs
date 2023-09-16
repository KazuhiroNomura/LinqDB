using MemoryPack;
using System.Buffers;

namespace LinqDB.Serializers.MemoryPack.Formatters;
using Reader = MemoryPackReader;
using T = System.Type;


public class Type:MemoryPackFormatter<T> {
    public static readonly Type Instance=new();
    internal static void Write<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,T? value) where TBufferWriter:IBufferWriter<byte>{
        Instance.Serialize(ref writer,ref value);
    }
    internal static T Read(ref Reader reader){
        T? value=default;
        Instance.Deserialize(ref reader,ref value);
        return value!;
    }
    public override void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,scoped ref T? value){
        writer.WriteType(value);
    }
    public override void Deserialize(ref Reader reader,scoped ref T? value){
        value=reader.ReadType();
    }
}
