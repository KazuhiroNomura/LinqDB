using MessagePack;
using MessagePack.Formatters;
namespace LinqDB.Serializers.MessagePack.Formatters;
using Writer = MessagePackWriter;
using Reader = MessagePackReader;
using T = System.Type;


public class Type:IMessagePackFormatter<T> {
    public static readonly Type Instance=new();
    public void Serialize(ref Writer writer,T value,MessagePackSerializerOptions options)=>writer.WriteType(value);
    public T Deserialize(ref Reader reader,MessagePackSerializerOptions options)=>reader.ReadType();
}
