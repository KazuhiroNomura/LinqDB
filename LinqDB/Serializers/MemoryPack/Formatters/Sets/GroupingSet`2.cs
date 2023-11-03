
using MemoryPack;
namespace LinqDB.Serializers.MemoryPack.Formatters.Sets;


using Reader = MemoryPackReader;
using G = LinqDB.Sets;
public class GroupingSet<TKey,TElement>:MemoryPackFormatter<G.GroupingSet<TKey,TElement>>{
    public static readonly GroupingSet<TKey,TElement> Instance=new();
    private GroupingSet(){}
    public override void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,scoped ref G.GroupingSet<TKey,TElement>? value){
        if(writer.TryWriteNil(value)) return;
        writer.WriteVarInt(value!.LongCount);
        writer.Write(value.Key);
        var Formatter=FormatterResolver.GetFormatterDynamic<TElement>()??writer.GetFormatter<TElement>();
        foreach(var item in value)
            writer.Write(Formatter,item);
    }
    public override void Deserialize(ref Reader reader,scoped ref G.GroupingSet<TKey,TElement>? value){
        if(reader.TryReadNil())return;
        var Count=reader.ReadVarIntInt64();
        var Key=reader.ReadValue<TKey>();
        var value0=new G.GroupingSet<TKey,TElement>(Key);
        var Formatter=FormatterResolver.GetFormatterDynamic<TElement>()??reader.GetFormatter<TElement>();
        while(Count-->0)
            value0.Add(reader.Read(Formatter));
        value=value0;
    }
}
