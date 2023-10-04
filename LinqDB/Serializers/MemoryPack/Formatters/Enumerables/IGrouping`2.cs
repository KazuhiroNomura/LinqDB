using System.Buffers;
using System.Linq;
using MemoryPack;
namespace LinqDB.Serializers.MemoryPack.Formatters.Enumerables;

using Reader = MemoryPackReader;
using G = LinqDB.Enumerables;
// ReSharper disable once InconsistentNaming
public class IGrouping<TKey, TElement>:MemoryPackFormatter<System.Linq.IGrouping<TKey,TElement>> {
    public static readonly IGrouping<TKey, TElement> Instance=new();
    private IGrouping(){}
    private static void WriteNullable<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,System.Linq.IGrouping<TKey,TElement>? value) where TBufferWriter : IBufferWriter<byte> {
        if(writer.TryWriteNil(value)) return;
        writer.WriteValue(value!.Key);
        var Formatter = writer.GetFormatter<TElement>();
        writer.WriteVarInt(value.LongCount());
        foreach(var item in value) {
            var item0 = item;
            Formatter.Serialize(ref writer,ref item0);
        }
    }
    public override void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,scoped ref System.Linq.IGrouping<TKey,TElement>? value) => WriteNullable(ref writer,value);
    private static System.Linq.IGrouping<TKey,TElement>? ReadNullable(ref Reader reader) {
        if(reader.TryReadNil()) return null;
        var Key=reader.ReadValue<TKey>();
        var value = new G.GroupingList<TKey,TElement>(Key);
        var Formatter = reader.GetFormatter<TElement>();
        var Count = reader.ReadVarIntInt64();
        for(long a = 0;a<Count;a++) {
            TElement? item = default;//ここでnull入れないと内部で作られない
            Formatter.Deserialize(ref reader,ref item);
            value.Add(item);
        }
        return value;
    }
    public override void Deserialize(ref Reader reader,scoped ref System.Linq.IGrouping<TKey,TElement>? value) => value=ReadNullable(ref reader);
}
