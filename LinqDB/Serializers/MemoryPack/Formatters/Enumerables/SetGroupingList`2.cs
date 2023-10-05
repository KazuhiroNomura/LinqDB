using MemoryPack;

namespace LinqDB.Serializers.MemoryPack.Formatters.Enumerables;


using Reader = MemoryPackReader;
using G = LinqDB.Sets;
public class SetGroupingList<TKey,TElement>:MemoryPackFormatter<G.SetGroupingList<TKey,TElement>>{
    public static readonly SetGroupingList<TKey,TElement>Instance=new();
    public override void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,scoped ref G.SetGroupingList<TKey,TElement>? value){
        if(writer.TryWriteNil(value)) return;
        writer.WriteVarInt(value!.LongCount);
        var Formatter=GroupingList<TKey,TElement>.Instance;
        foreach(var item in value)
            writer.Write(Formatter,item);
    }
    public override void Deserialize(ref Reader reader,scoped ref G.SetGroupingList<TKey,TElement>? value){
        if(reader.TryReadNil())return;
        var Count = reader.ReadVarIntInt64();
        var Formatter=GroupingList<TKey,TElement>.Instance;
        var value0=new G.SetGroupingList<TKey,TElement>();
        for(long a=0;a<Count;a++)
            value0.Add(reader.Read(Formatter));
        value=value0;
    }
}
