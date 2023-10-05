using MessagePack;
using MessagePack.Formatters;


namespace LinqDB.Serializers.MessagePack.Formatters.Sets;
using O=MessagePackSerializerOptions;
using Writer = MessagePackWriter;
using Reader = MessagePackReader;
using G=LinqDB.Sets;

public class SetGroupingSet<TKey,TElement>:IMessagePackFormatter<G.SetGroupingSet<TKey,TElement>>{
    public new static readonly SetGroupingSet<TKey,TElement>Instance=new();
    public void Serialize(ref Writer writer,G.SetGroupingSet<TKey,TElement>? value,O Resolver){
        if(writer.TryWriteNil(value)) return;
        writer.WriteArrayHeader(value!.Count);
        var Formatter=Resolver.Resolver.GetFormatter<G.GroupingSet<TKey,TElement>>()!;
        foreach(var item in value)
            Formatter.Serialize(ref writer,item,Resolver);
    }
    public G.SetGroupingSet<TKey,TElement> Deserialize(ref Reader reader,O Resolver){
        if(reader.TryReadNil())return null!;
        var Count = reader.ReadArrayHeader();
        var Formatter=Resolver.Resolver.GetFormatter<G.GroupingSet<TKey,TElement>>()!;
        var value = new G.SetGroupingSet<TKey,TElement>();
        for(long a = 0;a<Count;a++)
            value.Add(Formatter.Deserialize(ref reader,Resolver)!);
        return value;
    }
}
