using System;
using System.Reflection;
using Utf8Json;
namespace LinqDB.Serializers.Utf8Json.Formatters.Enumerables;
using O = IJsonFormatterResolver;
using Writer = JsonWriter;
using Reader = JsonReader;
using Collections = System.Collections.Generic;
public class IEnumerable<T> : IJsonFormatter<Collections.IEnumerable<T>>
{
#pragma warning disable CA1823 // 使用されていないプライベート フィールドを使用しません
    private static readonly IEnumerable<T> Instance = new();
#pragma warning restore CA1823 // 使用されていないプライベート フィールドを使用しません
    public void Serialize(ref Writer writer, Collections.IEnumerable<T> value, O Resolver)
    {
        if (writer.TryWriteNil(value)) return;
        writer.WriteBeginArray();
        var type = value.GetType();
        writer.WriteType(type);
        writer.WriteValueSeparator();
        writer.WriteValue(type, value, Resolver);
        writer.WriteEndArray();
    }
    public Collections.IEnumerable<T> Deserialize(ref Reader reader, O Resolver)
    {
        if (reader.TryReadNil()) return null!;
        reader.ReadIsBeginArrayWithVerify();
        var type = reader.ReadType();
        reader.ReadIsValueSeparatorWithVerify();
        var value = reader.ReadValue(type, Resolver);
        reader.ReadIsEndArrayWithVerify();
        return (Collections.IEnumerable<T>)value;
    }
}
