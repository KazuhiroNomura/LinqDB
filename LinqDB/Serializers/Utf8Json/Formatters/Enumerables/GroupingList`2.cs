using Utf8Json;
namespace LinqDB.Serializers.Utf8Json.Formatters.Enumerables;
using O = IJsonFormatterResolver;
using Writer = JsonWriter;
using Reader = JsonReader;
using Sets = LinqDB.Sets;
public class GroupingList<TKey, TElement> : IJsonFormatter<LinqDB.Enumerables.GroupingList<TKey, TElement>>
{
    public static readonly GroupingList<TKey, TElement> Instance = new();//リフレクションで使われる
    public void Serialize(ref Writer writer, LinqDB.Enumerables.GroupingList<TKey, TElement> value, O Resolver)
    {
        if (writer.TryWriteNil(value)) return;
        writer.WriteBeginArray();
        Resolver.GetFormatter<TKey>().Serialize(ref writer, value!.Key, Resolver);
        var Formatter = Resolver.GetFormatter<TElement>();
        foreach (var item in value!)
        {
            writer.WriteValueSeparator();
            Formatter.Serialize(ref writer, item, Resolver);
        }
        writer.WriteEndArray();
    }
    public LinqDB.Enumerables.GroupingList<TKey, TElement> Deserialize(ref Reader reader, O Resolver)
    {
        if (reader.TryReadNil()) return null!;
        reader.ReadIsBeginArrayWithVerify();
        var Key = Resolver.GetFormatter<TKey>().Deserialize(ref reader, Resolver);
        var value = new LinqDB.Enumerables.GroupingList<TKey, TElement>(Key);
        var Formatter = Resolver.GetFormatter<TElement>();
        while (!reader.ReadIsEndArray())
        {
            reader.ReadIsValueSeparatorWithVerify();
            var item = Formatter.Deserialize(ref reader, Resolver);
            value.Add(item);
        }
        return value;
    }
}
