using System.Linq;
using MessagePack;
using MessagePack.Formatters;
namespace LinqDB.Serializers.MessagePack.Formatters.Enumerables;
using G=System.Linq;
using O=MessagePackSerializerOptions;
using Writer = MessagePackWriter;
using Reader = MessagePackReader;

public class IGrouping<TKey,TElement>:IMessagePackFormatter<G.IGrouping<TKey,TElement>>{
    public static readonly IGrouping<TKey,TElement> Instance=new();
    private IGrouping(){}
    public void Serialize(ref Writer writer,G.IGrouping<TKey,TElement>? value,O Resolver){
        if(writer.TryWriteNil(value)) return;
        writer.WriteArrayHeader(1+value!.Count());
        Resolver.Resolver.GetFormatter<TKey>()!.Serialize(ref writer,value!.Key,Resolver);
        var Formatter=Resolver.Resolver.GetFormatter<TElement>()!;
        foreach(var item in value) 
            Formatter.Serialize(ref writer,item,Resolver);
    }
    
    
    public G.IGrouping<TKey,TElement> Deserialize(ref Reader reader,O Resolver) {
        if(reader.TryReadNil())return null!;
        var Count = reader.ReadArrayHeader();
        var Key=Resolver.Resolver.GetFormatter<TKey>()!.Deserialize(ref reader,Resolver);
        var ElementFormatter=Resolver.Resolver.GetFormatter<TElement>()!;
        var value=new LinqDB.Enumerables.GroupingList<TKey,TElement>(Key);
        for(var a=1;a<Count;a++){
            var Element=ElementFormatter.Deserialize(ref reader,Resolver);
            value.Add(Element);
        }
        return value;
    }
}
