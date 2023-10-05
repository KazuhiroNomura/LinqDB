using Utf8Json;
namespace LinqDB.Serializers.Utf8Json.Formatters.Enumerables;
using O = IJsonFormatterResolver;
using Writer = JsonWriter;
using Reader = JsonReader;
using G = System.Collections.Generic;
public class IEnumerable<T> : IJsonFormatter<G.IEnumerable<T>>
{
#pragma warning disable CA1823 // 使用されていないプライベート フィールドを使用しません
    private static readonly IEnumerable<T> Instance = new();
#pragma warning restore CA1823 // 使用されていないプライベート フィールドを使用しません
    private static readonly global::Utf8Json.Formatters.InterfaceEnumerableFormatter<T> Formatter=new();
    public void Serialize(ref Writer writer, G.IEnumerable<T> value, O Resolver)
    {
        writer.Write(Formatter,value,Resolver);
    }
    public G.IEnumerable<T> Deserialize(ref Reader reader, O Resolver)
    {
        return reader.Read(Formatter,Resolver)!;
    }
}
