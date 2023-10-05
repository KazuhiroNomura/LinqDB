using Utf8Json;
namespace LinqDB.Serializers.Utf8Json.Formatters.Enumerables;
using O = IJsonFormatterResolver;
using Writer = JsonWriter;
using Reader = JsonReader;
using G = System.Linq;
public class IGrouping<TKey, TElement> : IJsonFormatter<G.IGrouping<TKey, TElement>>
{
    public static readonly IGrouping<TKey, TElement> Instance = new();//リフレクションで使われる
#pragma warning disable CA1823// 使用されていないプライベート フィールドを使用しません
#pragma warning restore CA1823// 使用されていないプライベート フィールドを使用しません
    public void Serialize(ref Writer writer,G.IGrouping<TKey, TElement> value, O Resolver)
    {
        if (writer.TryWriteNil(value)) return;
        writer.WriteBeginArray();
        var type = value.GetType();
        writer.WriteType(type);
        writer.WriteValueSeparator();
        writer.Write(type, value, Resolver);
        writer.WriteEndArray();
    }
    public G.IGrouping<TKey, TElement> Deserialize(ref Reader reader, O Resolver)
    {
        if (reader.TryReadNil()) return null!;
        reader.ReadIsBeginArrayWithVerify();
        var type = reader.ReadType();
        reader.ReadIsValueSeparatorWithVerify();
        var value = reader.Read(type, Resolver);
        reader.ReadIsEndArrayWithVerify();
        return (G.IGrouping<TKey, TElement>)value;
    }
}
