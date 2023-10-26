using MessagePack;
using MessagePack.Formatters;
namespace LinqDB.Serializers.MessagePack.Formatters.Sets;
using O = MessagePackSerializerOptions;
using Writer = MessagePackWriter;
using Reader = MessagePackReader;
using G = LinqDB.Sets;
public class HashSet<TElement>:IMessagePackFormatter<G.HashSet<TElement>>{
    public static readonly HashSet<TElement> Instance = new();
    private HashSet(){}
    public void Serialize(ref Writer writer,G.HashSet<TElement>? value,O Resolver){
        if (writer.TryWriteNil(value)) return;
        writer.WriteArrayHeader(value!.Count);
        var Formatter = Resolver.GetFormatter<TElement>();
        foreach (var item in value)
            writer.Write(Formatter,item,Resolver);
    }
    
    
    
    
    
    
    
    public G.HashSet<TElement> Deserialize(ref Reader reader,O Resolver){
        if (reader.TryReadNil()) return null!;
        var Count = reader.ReadArrayHeader();
        var Formatter = Resolver.GetFormatter<TElement>();
        var value = new G.HashSet<TElement>();
        while(Count-->0)
            value.Add(Formatter.Deserialize(ref reader, Resolver));
        return value;
    }
}
