using Utf8Json;
namespace LinqDB.Serializers.Utf8Json.Formatters.Sets;
using O=IJsonFormatterResolver;
using Writer = JsonWriter;
using Reader = JsonReader;
using Sets=LinqDB.Sets;
public class IEnumerable:IJsonFormatter<Sets.IEnumerable>  {
    public static readonly IEnumerable Instance=new();
    public void Serialize(ref Writer writer,Sets.IEnumerable value,O Resolver){
        if(writer.TryWriteNil(value)) return;
        writer.WriteBeginArray();
        var type=value.GetType();
        writer.WriteType(type);
        writer.WriteValueSeparator();
        writer.WriteValue(type,value,Resolver);
        writer.WriteEndArray();
    }
    public Sets.IEnumerable Deserialize(ref Reader reader,O Resolver){
        if(reader.TryReadNil()) return null!;
        reader.ReadIsBeginArrayWithVerify();
        var type=reader.ReadType();
        reader.ReadIsValueSeparatorWithVerify();
        var value=reader.ReadValue(type,Resolver);
        reader.ReadIsEndArrayWithVerify();
        return (Sets.IEnumerable)value;
    }
}