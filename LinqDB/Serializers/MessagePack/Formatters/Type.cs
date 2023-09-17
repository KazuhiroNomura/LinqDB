using MessagePack;
using MessagePack.Formatters;
namespace LinqDB.Serializers.MessagePack.Formatters;
using Writer = MessagePackWriter;
using Reader = MessagePackReader;
using T = System.Type;


public class Type:IMessagePackFormatter<T> {
    public static readonly Type Instance=new();
    internal static void Write(ref Writer writer,T value,MessagePackSerializerOptions options)=>writer.WriteType(value);
    public void Serialize(ref Writer writer,T value,MessagePackSerializerOptions options){
        if(writer.TryWriteNil(value)) return;
        writer.WriteType(value);
    }
    internal static T Read(ref Reader reader,MessagePackSerializerOptions options)=>reader.ReadType();
    public T Deserialize(ref Reader reader,MessagePackSerializerOptions options)=>reader.TryReadNil()?null!:reader.ReadType();
}
