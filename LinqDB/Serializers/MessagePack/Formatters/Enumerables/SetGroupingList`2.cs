using MessagePack;
using MessagePack.Formatters;
namespace LinqDB.Serializers.MessagePack.Formatters.Enumerables;
using O = MessagePackSerializerOptions;
using Writer = MessagePackWriter;
using Reader = MessagePackReader;
using G = LinqDB.Sets;
public class SetGroupingList<TKey,TElement>:IMessagePackFormatter<G.SetGroupingList<TKey,TElement>>{
    public new static readonly SetGroupingList<TKey,TElement>Instance=new();
    public void Serialize(ref Writer writer,G.SetGroupingList<TKey,TElement>? value,O Resolver){
        if(writer.TryWriteNil(value)) return;
        writer.WriteArrayHeader(value!.Count);
        var Formatter=GroupingList<TKey,TElement>.Instance;
        foreach(var item in value)
            writer.Write(Formatter,item,Resolver);
    }
    public G.SetGroupingList<TKey,TElement> Deserialize(ref Reader reader,O Resolver){
        if(reader.TryReadNil())return null!;
        var Count = reader.ReadArrayHeader();
        var Formatter=GroupingList<TKey,TElement>.Instance;
        var value = new G.SetGroupingList<TKey,TElement>();
        for(long a = 0;a<Count;a++)
            value.Add(reader.Read(Formatter,Resolver));
        return value;
    }
}
