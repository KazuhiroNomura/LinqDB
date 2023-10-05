using MemoryPack;

namespace LinqDB.Serializers.MemoryPack.Formatters.Enumerables;


using Reader = MemoryPackReader;
using G = LinqDB.Enumerables;
public class GroupingList<TKey,TElement>:MemoryPackFormatter<G.GroupingList<TKey,TElement>>{
    public static readonly GroupingList<TKey,TElement> Instance=new();
    private GroupingList(){}
    public override void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,scoped ref G.GroupingList<TKey,TElement>? value){
        if(writer.TryWriteNil(value)) return;
        writer.Write(value!.Key);
        var Formatter=writer.GetFormatter<TElement>();
        writer.WriteVarInt(value.LongCount);
        foreach(var item in value){
        
            writer.Write(Formatter,item);
        }
    }
    public override void Deserialize(ref Reader reader,scoped ref G.GroupingList<TKey,TElement>? value){
        if(reader.TryReadNil())return;
        FormatterResolver.GetAnonymousDisplaySetFormatter(typeof(TKey));
        var Key=reader.ReadValue<TKey>();
        var value0=new G.GroupingList<TKey,TElement>(Key);
        var Formatter=reader.GetFormatter<TElement>();
        var Count=reader.ReadVarIntInt64();
        for(long a=0;a<Count;a++)
            value0.Add(reader.Read(Formatter));
        value=value0;
    }
}
