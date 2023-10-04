using MessagePack;
using MessagePack.Formatters;
namespace LinqDB.Serializers.MessagePack.Formatters.Enumerables;
using Sets = LinqDB.Enumerables;
using O = MessagePackSerializerOptions;
using Writer = MessagePackWriter;
using Reader = MessagePackReader;
public class GroupingList<TKey, TElement> : IMessagePackFormatter<Sets.GroupingList<TKey, TElement>>{
    public static readonly GroupingList<TKey, TElement> Instance = new();
    public void Serialize(ref Writer writer,Sets.GroupingList<TKey,TElement>? value,O Resolver){
        if (writer.TryWriteNil(value)) return;
        writer.WriteArrayHeader(2+value!.Count);
        writer.WriteType(value.GetType());
        Resolver.Resolver.GetFormatter<TKey>()!.Serialize(ref writer, value.Key, Resolver);
        var ElementFormatter = Resolver.Resolver.GetFormatter<TElement>()!;
        foreach (var item in value)
            ElementFormatter.Serialize(ref writer, item, Resolver);
    }
    public Sets.GroupingList<TKey,TElement> Deserialize(ref Reader reader,O Resolver){
        if (reader.TryReadNil()) return null!;
        var Count = reader.ReadArrayHeader();
        var type = reader.ReadType();
        var Key = Resolver.Resolver.GetFormatter<TKey>()!.Deserialize(ref reader, Resolver);
        var ElementFormatter = Resolver.Resolver.GetFormatter<TElement>()!;
        var value = new Sets.GroupingList<TKey, TElement>(Key);
        for (var a = 2; a<Count; a++){
            var Element = ElementFormatter.Deserialize(ref reader, Resolver);
            value.Add(Element);
        }
        return value;
    }
}
