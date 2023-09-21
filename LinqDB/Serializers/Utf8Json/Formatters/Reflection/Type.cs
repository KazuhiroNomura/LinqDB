using Utf8Json;
using System.Diagnostics;

namespace LinqDB.Serializers.Utf8Json.Formatters.Reflection;
using Writer = JsonWriter;
using Reader = JsonReader;
using T = System.Type;
public class Type : IJsonFormatter<T>
{
    public static readonly Type Instance = new();
    internal static void Write(ref Writer writer, T value, IJsonFormatterResolver options) => writer.WriteType(value);
    public void Serialize(ref Writer writer, T? value, IJsonFormatterResolver Resolver)
    {
        Debug.Assert(value!=null, nameof(value)+" != null");
        writer.WriteType(value);
    }
    internal static T Read(ref Reader reader, IJsonFormatterResolver options) => reader.ReadType();
    public T Deserialize(ref Reader reader, IJsonFormatterResolver Resolver)
    {
        return reader.ReadType();
    }
}
