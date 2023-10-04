using MemoryPack;
namespace LinqDB.Serializers.MemoryPack.Formatters.Enumerables;


using Reader = MemoryPackReader;
using G = LinqDB.Sets;
public class SetGroupingList<TKey,TElement>:MemoryPackFormatter<G.SetGroupingList<TKey,TElement>>{
    public static readonly SetGroupingList<TKey,TElement>Instance=new();
    private SetGroupingList(){}
    public override void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,scoped ref G.SetGroupingList<TKey,TElement>? value){
        if(writer.TryWriteNil(value)) return;
        writer.WriteVarInt(value!.LongCount);
        var Formatter=writer.GetFormatter<LinqDB.Enumerables.GroupingList<TKey,TElement>>()!;
        foreach(var item in value)
            Formatter.Write(ref writer,item);
    }
    public override void Deserialize(ref Reader reader,scoped ref G.SetGroupingList<TKey,TElement>? value){
        if(reader.TryReadNil())return;
        var Count = reader.ReadVarIntInt64();
        var Formatter=reader.GetFormatter<LinqDB.Enumerables.GroupingList<TKey,TElement>>()!;
        var value0=new G.SetGroupingList<TKey,TElement>();
        for(long a=0;a<Count;a++)
            value0.Add(Formatter.Read(ref reader));
        value=value0;
    }
}
