using System.Linq;
using MemoryPack;

namespace LinqDB.Serializers.MemoryPack.Formatters.Enumerables;


using Reader = MemoryPackReader;
using G =System.Linq;
public class ILookup<TKey,TElement>:MemoryPackFormatter<G.ILookup<TKey,TElement>> {
    internal static readonly ILookup<TKey,TElement> Instance=new();
    private ILookup(){}
    public override void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,scoped ref G.ILookup<TKey,TElement>? value){
        if(writer.TryWriteNil(value)) return;
        writer.WriteVarInt(value.LongCount());
        var Formatter=FormatterResolver.GetFormatterDynamic<TElement>()??writer.GetFormatter<TElement>();
        foreach(var item in value)
            writer.Write(Formatter,item);
    }
    
    
    
    public override void Deserialize(ref Reader reader,scoped ref G.ILookup<TKey,TElement>? value){
        if(reader.TryReadNil()) return;
        var Count = reader.ReadVarIntInt64();
        //var Formatter=FormatterResolver.GetFormatterDynamic<TElement>()??reader.GetFormatter<TElement>();
        var value0=new LinqDB.Enumerables.Lookup<TKey,TElement>();
        var Formatter=GroupingList<TKey,TElement>.Instance;
        while(Count-->0)
            value0.Add(reader.Read(Formatter));
        value=value0;
    }
}
