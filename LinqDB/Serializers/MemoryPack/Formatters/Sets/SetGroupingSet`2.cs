using MemoryPack;

namespace LinqDB.Serializers.MemoryPack.Formatters.Sets;


using Reader = MemoryPackReader;
using G = LinqDB.Sets;
public class SetGroupingSet<TKey,TElement>:MemoryPackFormatter<G.SetGroupingSet<TKey,TElement>>{
    public static readonly SetGroupingSet<TKey,TElement>Instance=new();
    public override void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,scoped ref G.SetGroupingSet<TKey,TElement>? value){
        if(writer.TryWriteNil(value)) return;
        writer.WriteVarInt(value!.LongCount);
        var Formatter=GroupingSet<TKey,TElement>.Instance;
        foreach(var item in value)
            writer.Write(Formatter,item);
            
            
            
            
            
    }
    public override void Deserialize(ref Reader reader,scoped ref G.SetGroupingSet<TKey,TElement>? value){
        if(reader.TryReadNil())return;
        var Count = reader.ReadVarIntInt64();
        var Formatter=GroupingSet<TKey,TElement>.Instance;
        var value0=new G.SetGroupingSet<TKey,TElement>();
        for(long a=0;a<Count;a++)
            value0.Add(reader.Read(Formatter));
        value=value0;
    }
}
