using MessagePack;
using MessagePack.Formatters;
namespace LinqDB.Serializers.MessagePack.Formatters.Reflection;
using O=MessagePackSerializerOptions;
using Writer = MessagePackWriter;
using Reader = MessagePackReader;
using G = System.Type;


public class Type:IMessagePackFormatter<G>{
    public static readonly Type Instance=new();
    internal static void Write(ref Writer writer,G value,O options)=>writer.WriteType(value);
    public void Serialize(ref Writer writer,G value,O options){
        if(writer.TryWriteNil(value)) return;
        writer.WriteType(value);
    }
    internal static G Read(ref Reader reader,O options)=>reader.ReadType();
    public G Deserialize(ref Reader reader,O options)=>reader.TryReadNil()?null!:reader.ReadType();
}
