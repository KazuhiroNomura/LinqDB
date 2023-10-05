using MessagePack;
using MessagePack.Formatters;
namespace LinqDB.Serializers.MessagePack.Formatters.Enumerables;
using O = MessagePackSerializerOptions;
using Writer = MessagePackWriter;
using Reader = MessagePackReader;
using G=System.Collections;
public class IEnumerable:IMessagePackFormatter<G.IEnumerable>{
    public static readonly IEnumerable Instance=new();
    private IEnumerable() { }
    public void Serialize(ref Writer writer,G.IEnumerable? value,O Resolver){
        if(writer.TryWriteNil(value)) return;
        writer.WriteArrayHeader(2);
        var type=value!.GetType();
        writer.WriteType(type);
        
        writer.WriteValue(type,value,Resolver);
        
    }
    public G.IEnumerable Deserialize(ref Reader reader,O Resolver){
        if(reader.TryReadNil()) return null!;
        var Count=reader.ReadArrayHeader();
        System.Diagnostics.Debug.Assert(Count==2);
        var type=reader.ReadType();
        var o=type.GetValue("InstanceMemoryPack");
        var value=reader.ReadValue(type,Resolver);
        return(G.IEnumerable)value;
    }
}
