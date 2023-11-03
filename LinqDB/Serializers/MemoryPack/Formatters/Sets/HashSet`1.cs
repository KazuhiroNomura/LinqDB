using MemoryPack;

namespace LinqDB.Serializers.MemoryPack.Formatters.Sets;


using Reader = MemoryPackReader;
using G = LinqDB.Sets;
public class HashSet<TElement>:MemoryPackFormatter<G.HashSet<TElement>>{
    public static readonly HashSet<TElement> Instance=new();
    private HashSet(){}
    public override void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,scoped ref G.HashSet<TElement>? value){
        if(writer.TryWriteNil(value)) return;
        writer.WriteVarInt(value!.Count);
        var Formatter=FormatterResolver.GetFormatterDynamic<TElement>()??writer.GetFormatter<TElement>();
        foreach(var item in value)
            writer.Write(Formatter,item);
    }







    public override void Deserialize(ref Reader reader,scoped ref G.HashSet<TElement>? value){
        if(reader.TryReadNil())return;
        var Count=reader.ReadVarIntInt64();
        var Formatter=FormatterResolver.GetFormatterDynamic<TElement>()??reader.GetFormatter<TElement>();
        var value0=new G.HashSet<TElement>();
        while(Count-->0)
            value0.Add(reader.Read(Formatter));
        value=value0;
    }
}
