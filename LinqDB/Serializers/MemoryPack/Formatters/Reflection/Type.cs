using MemoryPack;
using System.Buffers;

namespace LinqDB.Serializers.MemoryPack.Formatters.Reflection;
using Reader = MemoryPackReader;
using G = System.Type;


public class Type:MemoryPackFormatter<G>{
    public static readonly Type Instance=new();
    internal static void Write<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,G? value) where TBufferWriter:IBufferWriter<byte>{
        writer.WriteType(value);
    }
    public override void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,scoped ref G? value){
        if(writer.TryWriteNil(value)) return;
        writer.WriteType(value);
    }
    internal static G Read(ref Reader reader){
        return reader.ReadType();
    }
    public override void Deserialize(ref Reader reader,scoped ref G? value){
        value=reader.ReadType();
    }
}
