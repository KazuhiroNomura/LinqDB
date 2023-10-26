using System.Buffers;
using MemoryPack;
namespace LinqDB.Serializers.MemoryPack.Formatters.Sets;


using Reader = MemoryPackReader;
using G = LinqDB.Sets;
public class Set<T>:MemoryPackFormatter<G.Set<T>>{
    public static readonly Set<T> Instance=new();
    public override void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,scoped ref G.Set<T>? value){
        if(writer.TryWriteNil(value)) return;
        writer.WriteVarInt(value!.LongCount);
        var Formatter=writer.GetFormatter<T>();
        foreach(var item in value)
            writer.Write(Formatter,item);
    }
    
    





    
    public override void Deserialize(ref Reader reader,scoped ref G.Set<T>? value){
        if(reader.TryReadNil())return;
        var Count=reader.ReadVarIntInt64();
        var Formatter=reader.GetFormatter<T>();
        var value0=new G.Set<T>();
        while(Count-->0)
            value0.Add(reader.Read(Formatter));
        value=value0;
    }
}
