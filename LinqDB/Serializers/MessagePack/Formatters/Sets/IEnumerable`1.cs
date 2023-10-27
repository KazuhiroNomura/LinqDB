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
        writer.WriteArrayHeader(2);
        var type=value!.GetType();
        writer.WriteType(type);
        writer.Write(type,value,Resolver);
    }
    
    
    
    
    
    public G.IEnumerable<T> Deserialize(ref Reader reader,O Resolver) {
        if(reader.TryReadNil())return null!;
        var Count = reader.ReadArrayHeader();
        return(G.IEnumerable<T>)reader.Read(reader.ReadType(),Resolver);
    }
}
