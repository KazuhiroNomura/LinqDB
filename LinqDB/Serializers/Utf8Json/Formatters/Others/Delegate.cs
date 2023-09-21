using LinqDB.Serializers.Utf8Json.Formatters.Reflection;
using Utf8Json;
namespace LinqDB.Serializers.Utf8Json.Formatters.Others;
using Writer = JsonWriter;
using Reader = JsonReader;
using static Extension;
using T = System.Delegate;
public class Delegate : IJsonFormatter<T>
{
    public static readonly Delegate Instance = new();

    internal static void Write(ref Writer writer, T? value, IJsonFormatterResolver Resolver)
    {
        writer.WriteBeginArray();
        writer.WriteType(value!.GetType());
        writer.WriteValueSeparator();
        Method.Write(ref writer, value.Method, Resolver);
        writer.WriteValueSeparator();
        Object.WriteNullable(ref writer, value.Target, Resolver);
        writer.WriteEndArray();
    }
    public void Serialize(ref Writer writer, T? value, IJsonFormatterResolver Resolver)
    {
        if (writer.TryWriteNil(value)) return;
        Write(ref writer, value, Resolver);
    }
    internal static T Read(ref Reader reader, IJsonFormatterResolver Resolver)
    {
        reader.ReadIsBeginArrayWithVerify();
        var delegateType = reader.ReadType();
        reader.ReadIsValueSeparatorWithVerify();
        var method = Method.Read(ref reader, Resolver);
        reader.ReadIsValueSeparatorWithVerify();
        var target = Object.ReadNullable(ref reader, Resolver);
        reader.ReadIsEndArrayWithVerify();
        return method.CreateDelegate(delegateType, target);
    }
    public T Deserialize(ref Reader reader, IJsonFormatterResolver Resolver)
    {
        if (reader.TryReadNil()) return null!;
        return Read(ref reader, Resolver);
    }
}
