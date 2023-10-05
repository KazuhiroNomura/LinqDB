using System.Linq;
using MessagePack;
using MessagePack.Formatters;
namespace LinqDB.Serializers.MessagePack.Formatters.Sets;
using O=MessagePackSerializerOptions;
using Writer = MessagePackWriter;
using Reader = MessagePackReader;
using G=LinqDB.Sets;
public class Set<T>:IMessagePackFormatter<G.Set<T>>{
    public static readonly Set<T> Instance=new();
    //private Set(){}
    public void Serialize(ref Writer writer,G.Set<T>? value,O Resolver){
        if(writer.TryWriteNil(value)) return;
        var Count=value!.Count;
        writer.WriteArrayHeader(Count);
        var Formatter=Resolver.Resolver.GetFormatter<T>()!;
        foreach(var item in value)
            Formatter.Serialize(ref writer,item,Resolver);
    }
    public G.Set<T>Deserialize(ref Reader reader,O Resolver) {
        if(reader.TryReadNil())return null!;
        var Count = reader.ReadArrayHeader();
        var Formatter=Resolver.Resolver.GetFormatter<T>()!;
        var value=new G.Set<T>();
        for(long a=0;a<Count;a++)
            value.Add(Formatter.Deserialize(ref reader,Resolver));
        return value;
    }
}
