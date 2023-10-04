using MemoryPack;
namespace LinqDB.Serializers.MemoryPack.Formatters.Sets;


using Reader = MemoryPackReader;
using G = LinqDB.Sets;
public class SetGroupingSet<TKey,TElement>:MemoryPackFormatter<G.SetGroupingSet<TKey,TElement>>{
    public static readonly SetGroupingSet<TKey,TElement>Instance=new();
    private SetGroupingSet(){}
    public override void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,scoped ref G.SetGroupingSet<TKey,TElement>? value){
        if(writer.TryWriteNil(value)) return;
        writer.WriteVarInt(value!.LongCount);
        var Formatter=writer.GetFormatter<G.GroupingSet<TKey,TElement>>()!;
        foreach(var item in value)
            Formatter.Write(ref writer,item);
    }
    public override void Deserialize(ref Reader reader,scoped ref G.SetGroupingSet<TKey,TElement>? value){
        if(reader.TryReadNil())return;
        var Count = reader.ReadVarIntInt64();
        var Formatter=reader.GetFormatter<G.GroupingSet<TKey,TElement>>()!;
        var value0=new G.SetGroupingSet<TKey,TElement>();
        for(long a=0;a<Count;a++)
            value0.Add(Formatter.Read(ref reader));
        value=value0;
    }
}
