using System;
using System.Buffers;
using LinqDB.Sets;

using MemoryPack;
namespace LinqDB.Serializers.MemoryPack.Formatters.Sets;
using Sets = LinqDB.Sets;


using Reader = MemoryPackReader;
// ReSharper disable once InconsistentNaming
public class SetGroupingSet<TKey,TElement>:MemoryPackFormatter<Sets.SetGroupingSet<TKey,TElement>>{
    public static readonly SetGroupingSet<TKey,TElement>Instance=new();
    private SetGroupingSet(){}
    public override void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,scoped ref Sets.SetGroupingSet<TKey,TElement>? value){
        if(writer.TryWriteNil(value)) return;
        writer.WriteVarInt(value!.LongCount);
        var Formatter=writer.GetFormatter<Sets.GroupingSet<TKey,TElement>>()!;
        foreach(var item in value)
            Formatter.Write(ref writer,item);
    }
    public override void Deserialize(ref Reader reader,scoped ref Sets.SetGroupingSet<TKey,TElement>? value){
        if(reader.TryReadNil())return;
        var Count = reader.ReadVarIntInt64();
        var Formatter=reader.GetFormatter<Sets.GroupingSet<TKey,TElement>>()!;
        var value0=new Sets.SetGroupingSet<TKey,TElement>();
        for(long a=0;a<Count;a++)
            value0.Add(Formatter.Read(ref reader));
        value=value0;
    }
}
