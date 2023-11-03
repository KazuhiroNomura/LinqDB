using System;
using LinqDB.Databases;
using MemoryPack;
namespace LinqDB.Serializers.MemoryPack.Formatters.Sets;


using Reader = MemoryPackReader;
using G = LinqDB.Sets;
public class Set<TKey,TElement,TContainer>:MemoryPackFormatter<G.Set<TKey,TElement,TContainer>>
    where TElement :G.Entity<TKey,TContainer>
    where TKey : struct, IEquatable<TKey>
    where TContainer : Container{
    public static readonly Set<TKey,TElement,TContainer>Instance=new();
    public override void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,scoped ref G.Set<TKey,TElement,TContainer>? value){
        if(writer.TryWriteNil(value)) return;
        var Count=value!.LongCount;
        
        writer.WriteVarInt(Count);
        var Formatter=FormatterResolver.GetFormatterDynamic<TElement>()??writer.GetFormatter<TElement>();
        foreach(var item in value)
            writer.Write(Formatter,item);
    }
    
    
    
    public override void Deserialize(ref Reader reader,scoped ref G.Set<TKey,TElement,TContainer>? value){
        if(reader.TryReadNil())return;
        var Formatter=FormatterResolver.GetFormatterDynamic<TElement>()??reader.GetFormatter<TElement>();
        var value0=new G.Set<TKey,TElement,TContainer>(null!);
        var Count=reader.ReadVarIntInt64();
        for(long a=0;a<Count;a++)
            value0.Add(reader.Read(Formatter));
        value=value0;
    }
}
