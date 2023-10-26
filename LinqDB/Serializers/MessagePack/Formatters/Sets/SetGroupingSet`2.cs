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
        var Formatter=GroupingSet<TKey,TElement>.Instance;
        
        foreach(var item in value)
            writer.Write(Formatter,item,Resolver);;
            
            
            
            
    }
    public G.SetGroupingSet<TKey,TElement> Deserialize(ref Reader reader,O Resolver){
        if(reader.TryReadNil())return null!;
        var Count = reader.ReadArrayHeader();
        var Formatter=GroupingSet<TKey,TElement>.Instance;
        var value = new G.SetGroupingSet<TKey,TElement>();
        while(Count-->0)
            value.Add(reader.Read(Formatter,Resolver));
        return value;
        
        
        
        
    }
}
