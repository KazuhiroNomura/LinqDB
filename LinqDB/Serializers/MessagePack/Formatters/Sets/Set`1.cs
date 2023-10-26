using MessagePack;
using MessagePack.Formatters;
namespace LinqDB.Serializers.MessagePack.Formatters.Sets;
using O=MessagePackSerializerOptions;
using Writer = MessagePackWriter;
using Reader = MessagePackReader;
using G=LinqDB.Sets;
public class Set<T>:IMessagePackFormatter<G.Set<T>>{
    public static readonly Set<T> Instance=new();
    private Set(){}
    public void Serialize(ref Writer writer,G.Set<T>? value,O Resolver){
        if(writer.TryWriteNil(value)) return;
        writer.WriteArrayHeader(value!.Count);
        var Formatter=Resolver.GetFormatter<T>();
        foreach(var item in value)
            writer.Write(Formatter,item,Resolver);;
    }

    
    



    
    public G.Set<T>Deserialize(ref Reader reader,O Resolver) {
        if(reader.TryReadNil())return null!;
        var Count = reader.ReadArrayHeader();
        var Formatter=Resolver.GetFormatter<T>();
        var value=new G.Set<T>();
        while(Count-->0)
            value.Add(reader.Read(Formatter,Resolver));
        return value;
    }
}
