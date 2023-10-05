using System.Buffers;
using MemoryPack;
namespace LinqDB.Serializers.MemoryPack.Formatters.Sets;

using Reader = MemoryPackReader;
using G = LinqDB.Sets;
public class IGrouping<TKey, TElement>:MemoryPackFormatter<G.IGrouping<TKey,TElement>> {
    public static readonly IGrouping<TKey, TElement> Instance=new();
    private IGrouping(){}
    private static void WriteNullable<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,G.IGrouping<TKey,TElement>? value) where TBufferWriter : IBufferWriter<byte> {
        if(writer.TryWriteNil(value)) return;
        var type=value!.GetType();
        writer.WriteType(type);
        writer.Write(value);
        //writer.WriteValue(value!.Key);
        //var Count = value.LongCount;
        //var Formatter = writer.GetFormatter<TElement>();
        //writer.WriteVarInt(Count);
        //foreach(var item in value)
        //    writer.Write(Formatter,item);
    }
    public override void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,scoped ref G.IGrouping<TKey,TElement>? value) => WriteNullable(ref writer,value);
    private static G.IGrouping<TKey,TElement>? ReadNullable(ref Reader reader) {
        if(reader.TryReadNil()) return null;
        var type=reader.ReadType();
        return(G.IGrouping<TKey,TElement>?)reader.Read(type);
        //var Key=reader.ReadValue<TKey>();
        //var value = new G.GroupingSet<TKey,TElement>(Key);
        //var Formatter = reader.GetFormatter<TElement>();
        //var Count = reader.ReadVarIntInt64();
        //for(long a = 0;a<Count;a++)
        //    value.Add(reader.Read(Formatter));
        //return value;
    }
    public override void Deserialize(ref Reader reader,scoped ref G.IGrouping<TKey,TElement>? value) => value=ReadNullable(ref reader);
}
