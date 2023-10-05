
using MemoryPack;
namespace LinqDB.Serializers.MemoryPack.Formatters.Sets;


using Reader = MemoryPackReader;
using G = LinqDB.Sets;
public class GroupingSet<TKey,TElement>:MemoryPackFormatter<G.GroupingSet<TKey,TElement>>{
    public static readonly GroupingSet<TKey,TElement> Instance=new();
    private GroupingSet(){}
    public override void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,scoped ref G.GroupingSet<TKey,TElement>? value){
        if(writer.TryWriteNil(value)) return;
        writer.Write(value!.Key);
        var Count=value.LongCount;
        var Formatter=writer.GetFormatter<TElement>();
        writer.WriteVarInt(Count);
        foreach(var item in value)
            writer.Write(Formatter,item);
    }
    public override void Deserialize(ref Reader reader,scoped ref G.GroupingSet<TKey,TElement>? value){
        if(reader.TryReadNil())return;
        var Key=reader.ReadValue<TKey>();
        var value0=new G.GroupingSet<TKey,TElement>(Key);
        var Formatter=reader.GetFormatter<TElement>();
        var Count=reader.ReadVarIntInt64();
        for(long a=0;a<Count;a++)
            value0.Add(reader.Read(Formatter));
        value=value0;
    }
}
