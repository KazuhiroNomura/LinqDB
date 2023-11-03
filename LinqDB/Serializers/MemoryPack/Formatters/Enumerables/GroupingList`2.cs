using MemoryPack;

namespace LinqDB.Serializers.MemoryPack.Formatters.Enumerables;


using Reader = MemoryPackReader;
using G = LinqDB.Enumerables;
public class GroupingList<TKey,TElement>:MemoryPackFormatter<G.GroupingList<TKey,TElement>>{
    public static readonly GroupingList<TKey,TElement> Instance=new();
    private GroupingList(){}
    public override void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,scoped ref G.GroupingList<TKey,TElement>? value){
        if(writer.TryWriteNil(value)) return;
        writer.WriteVarInt(value!.LongCount);
        writer.Write(value.Key);
        var Formatter=FormatterResolver.GetFormatterDynamic<TElement>()??writer.GetFormatter<TElement>();
        foreach(var item in value){
        
            writer.Write(Formatter,item);
        }
    }
    
    public override void Deserialize(ref Reader reader,scoped ref G.GroupingList<TKey,TElement>? value){
        if(reader.TryReadNil())return;
        var Count=reader.ReadVarIntInt64();
        var Key=reader.Read<TKey>();
        var Formatter=FormatterResolver.GetFormatterDynamic<TElement>()??reader.GetFormatter<TElement>();
        var value0=new G.GroupingList<TKey,TElement>(Key);
        while(Count-->0)
            value0.Add(reader.Read(Formatter));


        value=value0;
    }
}
