using System.Diagnostics;
using MessagePack;
using MessagePack.Formatters;
namespace LinqDB.Serializers.MessagePack.Formatters.Sets;
using O=MessagePackSerializerOptions;
using Writer = MessagePackWriter;
using Reader = MessagePackReader;
using G=LinqDB.Sets;

public class IEnumerable:IMessagePackFormatter<G.IEnumerable>{
    public static readonly IEnumerable Instance=new();
    
    public void Serialize(ref Writer writer,G.IEnumerable? value,O Resolver){
        if(writer.TryWriteNil(value)) return;
        writer.WriteArrayHeader(2);
        var type=value!.GetType();
        writer.WriteType(type);
        writer.Write(type,value,Resolver);
    }


    public G.IEnumerable Deserialize(ref Reader reader,O Resolver) {
        if(reader.TryReadNil())return null!;
        var Count = reader.ReadArrayHeader();
        Debug.Assert(Count==2);
        var type = reader.ReadType();
        var value=reader.Read(type,Resolver);
        return (G.IEnumerable)value;
    }
}
