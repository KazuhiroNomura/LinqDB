using MemoryPack;

namespace LinqDB.Serializers.MemoryPack.Formatters.Enumerables;


using Reader = MemoryPackReader;
using G = LinqDB.Enumerables;
public class List<TElement>:MemoryPackFormatter<G.List<TElement>>{
    public static readonly List<TElement> Instance=new();
    private List(){}
    public override void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,scoped ref G.List<TElement>? value){
        if(writer.TryWriteNil(value)) return;
        writer.WriteVarInt(value!.Count);
        var Formatter=FormatterResolver.GetRegisteredFormatter<TElement>()??writer.GetFormatter<TElement>();
        foreach(var item in value)
            writer.Write(Formatter,item);
    }
    






    public override void Deserialize(ref Reader reader,scoped ref G.List<TElement>? value){
        if(reader.TryReadNil())return;
        var Count=reader.ReadVarIntInt64();
        var Formatter=FormatterResolver.GetRegisteredFormatter<TElement>()??reader.GetFormatter<TElement>();
        var value0=new G.List<TElement>();
        while(Count-->0)
            value0.Add(reader.Read(Formatter));
        value=value0;
    }
}
