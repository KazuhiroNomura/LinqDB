using RuntimeBinder = Microsoft.CSharp.RuntimeBinder;
using MemoryPack;
using System.Buffers;

namespace LinqDB.Serializers.MemoryPack.Formatters;


using Reader = MemoryPackReader;
using T = RuntimeBinder.CSharpArgumentInfo;
public class CSharpArgumentInfo:MemoryPackFormatter<T>{
    public static readonly CSharpArgumentInfo Instance=new();
    public override void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,scoped ref T? value){
        if(writer.TryWriteNil(value)) return;
        
        var (flags,name)=value.GetFlagsName();
        writer.WriteVarInt((int)flags);

        writer.WriteString(name);
        
    }
    public override void Deserialize(ref Reader reader,scoped ref T? value){
        if(reader.TryReadNil()) return;
        
        var flags=(RuntimeBinder.CSharpArgumentInfoFlags)reader.ReadVarIntInt32();
        
        var name=reader.ReadString();
        
        value=T.Create(flags,name);
    }
}
