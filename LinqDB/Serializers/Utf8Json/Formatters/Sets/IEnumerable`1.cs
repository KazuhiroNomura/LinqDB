
using Utf8Json;

namespace LinqDB.Serializers.Utf8Json.Formatters.Sets;
using O=IJsonFormatterResolver;
using Writer = JsonWriter;
using Reader = JsonReader;
using G=LinqDB.Sets;
public class IEnumerable<T>:IJsonFormatter<G.IEnumerable<T>>  {
    internal static readonly IEnumerable<T> Instance=new();
    public void Serialize(ref Writer writer,G.IEnumerable<T> value,O Resolver){
        if(writer.TryWriteNil(value)) return;
        writer.WriteBeginArray();
        var type=value!.GetType();
        writer.WriteType(type);
        writer.WriteValueSeparator();
        writer.Write(type,value,Resolver);
        writer.WriteEndArray();
    }
    public G.IEnumerable<T> Deserialize(ref Reader reader,O Resolver){
        if(reader.TryReadNil()) return null!;
        reader.ReadIsBeginArrayWithVerify();
        var type=reader.ReadType();
        reader.ReadIsValueSeparatorWithVerify();
        var value=(G.IEnumerable<T>)reader.Read(type,Resolver);
        reader.ReadIsEndArrayWithVerify();
        return value;
    }
}