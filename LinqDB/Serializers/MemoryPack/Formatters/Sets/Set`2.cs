using System;
using MemoryPack;
namespace LinqDB.Serializers.MemoryPack.Formatters.Sets;

using Reader = MemoryPackReader;
using G = LinqDB.Sets;
public class Set<TKey,TElement>:MemoryPackFormatter<G.Set<TKey,TElement>>
    where TElement:G.IKey<TKey>
    where TKey : struct, IEquatable<TKey>{
    public new static readonly Set<TKey,TElement> Instance=new();
    public override void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,scoped ref G.Set<TKey,TElement>? value){
        if(writer.TryWriteNil(value)) return;
        writer.WriteVarInt(value!.LongCount);
        var Formatter=FormatterResolver.GetRegisteredFormatter<TElement>()??writer.GetFormatter<TElement>();
        foreach(var item in value)
            writer.Write(Formatter,item);
    }
    







    public override void Deserialize(ref Reader reader,scoped ref G.Set<TKey,TElement>? value){
        if(reader.TryReadNil())return;
        var Formatter=FormatterResolver.GetRegisteredFormatter<TElement>()??reader.GetFormatter<TElement>();
        var value0=new G.Set<TKey,TElement>();
        var Count=reader.ReadVarIntInt64();
        while(Count-->0)
            value0.Add(reader.Read(Formatter));
        value=value0;
    }
}
