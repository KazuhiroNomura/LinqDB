
using MemoryPack;
using System.Buffers;
namespace LinqDB.Serializers.MemoryPack.Formatters.Sets;
using Sets = LinqDB.Sets;


using Reader = MemoryPackReader;
// ReSharper disable once InconsistentNaming
public class GroupingSet<TKey,TElement>:MemoryPackFormatter<Sets.GroupingSet<TKey,TElement>>{
    public static readonly GroupingSet<TKey,TElement> Instance=new();
    private GroupingSet(){}
    public override void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,scoped ref Sets.GroupingSet<TKey,TElement>? value){
        if(writer.TryWriteNil(value)) return;
        FormatterResolver.GetFormatter(typeof(TKey));
        writer.WriteValue(value!.Key);
        var Count=value.LongCount;
        var Formatter=writer.GetFormatter<TElement>();
        writer.WriteVarInt(Count);
        foreach(var item in value)
            Formatter.Write(ref writer,item);
    }
    public override void Deserialize(ref Reader reader,scoped ref Sets.GroupingSet<TKey,TElement>? value){
        if(reader.TryReadNil())return;
        var Key=reader.ReadValue<TKey>();
        var value0=new Sets.GroupingSet<TKey,TElement>(Key);
        var Formatter=reader.GetFormatter<TElement>();
        var Count=reader.ReadVarIntInt64();
        for(long a=0;a<Count;a++){
            TElement? item=default;//ここでnull入れないと内部で作られない
            Formatter.Deserialize(ref reader,ref item);
            value0.Add(item);
        }
        value=value0;
    }
}
