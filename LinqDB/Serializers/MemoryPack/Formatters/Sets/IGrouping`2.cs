using MemoryPack;
using System.Buffers;
namespace LinqDB.Serializers.MemoryPack.Formatters.Sets;


using Reader = MemoryPackReader;
using G = LinqDB.Sets;
public class IGrouping<TKey,TElement>:MemoryPackFormatter<G.IGrouping<TKey,TElement>> {
    public static readonly IGrouping<TKey,TElement> Instance=new();
    private IGrouping(){}
    public override void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,scoped ref G.IGrouping<TKey,TElement>? value){
        if(writer.TryWriteNil(value)) return;
        writer.WriteVarInt(value!.LongCount);
        writer.WriteValue(value.Key);
        var Formatter = writer.GetFormatter<TElement>();
        foreach(var item in value)
            writer.Write(Formatter,item);
    }
    
    




    
    public override void Deserialize(ref Reader reader,scoped ref G.IGrouping<TKey,TElement>? value){
        if(reader.TryReadNil()) return;
        var Count = reader.ReadVarIntInt64();
        var Key=reader.ReadValue<TKey>();
        var Formatter = reader.GetFormatter<TElement>();
        var value0 = new G.GroupingSet<TKey,TElement>(Key);
        while(Count-->0)
            value0.Add(reader.Read(Formatter));
        value=value0;
    }
}
