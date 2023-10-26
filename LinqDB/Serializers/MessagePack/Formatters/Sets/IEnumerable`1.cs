using MessagePack;
using MessagePack.Formatters;
namespace LinqDB.Serializers.MessagePack.Formatters.Sets;
using O=MessagePackSerializerOptions;
using Writer = MessagePackWriter;
using Reader = MessagePackReader;
using G=LinqDB.Sets;
public class IEnumerable<T>:IMessagePackFormatter<G.IEnumerable<T>>{
    public static readonly IEnumerable<T> Instance=new();
    public void Serialize(ref Writer writer,G.IEnumerable<T>? value,O Resolver){
        if(writer.TryWriteNil(value)) return;
        var Count=value!.Count;
        writer.WriteArrayHeader(Count);
        var Formatter=Resolver.GetFormatter<T>();
        foreach(var item in value)
            writer.Write(Formatter,item,Resolver);;
    }
    
    
    
    
    
    public G.IEnumerable<T> Deserialize(ref Reader reader,O Resolver) {
        if(reader.TryReadNil())return null!;
        var Count = reader.ReadArrayHeader();
        var value=new G.Set<T>();
        var Formatter=Resolver.GetFormatter<T>();
        while(Count-->0)
            value.Add(reader.Read(Formatter,Resolver));
        return value;
    }
}
