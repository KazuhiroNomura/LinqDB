using Utf8Json;
using System.Diagnostics;
namespace LinqDB.Serializers.Utf8Json.Formatters;
using Writer = JsonWriter;
using Reader = JsonReader;
using T=System.Type;
public class Type:IJsonFormatter<T> {
    public static readonly Type Instance=new();
    public void Serialize(ref Writer writer,T? value,IJsonFormatterResolver Resolver){
        Debug.Assert(value!=null,nameof(value)+" != null");
        writer.WriteType(value);
    }
    public T Deserialize(ref Reader reader,IJsonFormatterResolver Resolver){
        return reader.ReadType();
    }
}
