using System.Buffers;
using MemoryPack;
namespace LinqDB.Serializers.MemoryPack.Formatters.Enumerables;

using Reader = MemoryPackReader;
using G =System.Linq;
using System.Linq;
public class IGrouping<TKey, TElement>:MemoryPackFormatter<G.IGrouping<TKey,TElement>> {
    public static readonly IGrouping<TKey, TElement> Instance=new();
    private IGrouping(){}
    private static void WriteNullable<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,G.IGrouping<TKey,TElement>? value) where TBufferWriter : IBufferWriter<byte> {
        if(writer.TryWriteNil(value)) return;
        writer.WriteValue(value!.Key);
        var Formatter = writer.GetFormatter<TElement>();
        writer.WriteVarInt(value.LongCount());
        foreach(var item in value) {
            var item0 = item;
            Formatter.Serialize(ref writer,ref item0);
        }
    }
    public override void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,scoped ref G.IGrouping<TKey,TElement>? value) => WriteNullable(ref writer,value);
    private static G.IGrouping<TKey,TElement>? ReadNullable(ref Reader reader) {
        if(reader.TryReadNil()) return null;
        var Key=reader.ReadValue<TKey>();
        var value = new LinqDB.Enumerables.GroupingList<TKey,TElement>(Key);
        var Formatter = reader.GetFormatter<TElement>();
        var Count = reader.ReadVarIntInt64();
        for(long a = 0;a<Count;a++) {
            TElement? item = default;//ここでnull入れないと内部で作られない
            Formatter.Deserialize(ref reader,ref item);
            value.Add(item);
        }
        return value;
    }
    public override void Deserialize(ref Reader reader,scoped ref G.IGrouping<TKey,TElement>? value) => value=ReadNullable(ref reader);
}
