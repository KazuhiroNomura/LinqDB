using Utf8Json;
namespace LinqDB.Serializers.Utf8Json.Formatters.Enumerables;
using O = IJsonFormatterResolver;
using Writer = JsonWriter;
using Reader = JsonReader;
using Sets = LinqDB.Sets;
public class IGrouping<TKey, TElement> : IJsonFormatter<System.Linq.IGrouping<TKey, TElement>>
{
    public static readonly IGrouping<TKey, TElement> Instance = new();//リフレクションで使われる
#pragma warning disable CA1823// 使用されていないプライベート フィールドを使用しません
#pragma warning restore CA1823// 使用されていないプライベート フィールドを使用しません
    public void Serialize(ref Writer writer, System.Linq.IGrouping<TKey, TElement> value, O Resolver)
    {
        if (writer.TryWriteNil(value)) return;
        writer.WriteBeginArray();
        var type = value.GetType();
        writer.WriteType(type);
        writer.WriteValueSeparator();
        writer.WriteValue(type, value, Resolver);
        writer.WriteEndArray();
    }
    public System.Linq.IGrouping<TKey, TElement> Deserialize(ref Reader reader, O Resolver)
    {
        if (reader.TryReadNil()) return null!;
        reader.ReadIsBeginArrayWithVerify();
        var type = reader.ReadType();
        reader.ReadIsValueSeparatorWithVerify();
        var value = reader.ReadValue(type, Resolver);
        reader.ReadIsEndArrayWithVerify();
        return (System.Linq.IGrouping<TKey, TElement>)value;
    }
}
