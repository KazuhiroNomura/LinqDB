using MemoryPack;
namespace LinqDB.Serializers.MemoryPack.Formatters.Sets;


using Reader = MemoryPackReader;
using G = LinqDB.Sets;
public class ILookup<TKey,TElement>:MemoryPackFormatter<G.ILookup<TKey,TElement>> {
    public static readonly ILookup<TKey,TElement> Instance=new();
    private ILookup(){}
    public override void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,scoped ref G.ILookup<TKey,TElement>? value){
        if(writer.TryWriteNil(value)) return;
        writer.WriteVarInt(value!.LongCount);
        var Formatter=FormatterResolver.GetFormatterDynamic<TElement>()??writer.GetFormatter<TElement>();
        foreach(var item in value)
            writer.Write(Formatter,item);
    }
    
    




    
    public override void Deserialize(ref Reader reader,scoped ref G.ILookup<TKey,TElement>? value){
        if(reader.TryReadNil()) return;
        var Count = reader.ReadVarIntInt64();
        var value0 = new G.SetGroupingSet<TKey,TElement>();
        //var Formatter=FormatterResolver.GetFormatterDynamic<TElement>()??reader.GetFormatter<TElement>();
        var Formatter=GroupingSet<TKey,TElement>.Instance;
        while(Count-->0)
            value0.Add(reader.Read(Formatter));
        value=value0;
    }
}
