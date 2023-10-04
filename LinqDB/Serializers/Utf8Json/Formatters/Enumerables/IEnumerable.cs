using Utf8Json;
namespace LinqDB.Serializers.Utf8Json.Formatters.Enumerables;
using O=IJsonFormatterResolver;
using Writer = JsonWriter;
using Reader = JsonReader;
using G=System.Collections;
public class IEnumerable:IJsonFormatter<G.IEnumerable>  {
#pragma warning disable CA1823 // 使用されていないプライベート フィールドを使用しません
    public static readonly IEnumerable Instance=new();
#pragma warning restore CA1823 // 使用されていないプライベート フィールドを使用しません
    public void Serialize(ref Writer writer,G.IEnumerable value,O Resolver){
        if(writer.TryWriteNil(value)) return;
        writer.WriteBeginArray();
        var type=value.GetType();
        writer.WriteType(type);
        writer.WriteValueSeparator();
        writer.WriteValue(type,value,Resolver);
        writer.WriteEndArray();
    }
    public G.IEnumerable Deserialize(ref Reader reader,O Resolver){
        if(reader.TryReadNil()) return null!;
        reader.ReadIsBeginArrayWithVerify();
        var type=reader.ReadType();
        reader.ReadIsValueSeparatorWithVerify();
        var value=reader.ReadValue(type,Resolver);
        reader.ReadIsEndArrayWithVerify();
        return (G.IEnumerable)value;
    }
}
