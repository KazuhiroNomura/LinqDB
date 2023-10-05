using System;
using System.Buffers;

using MemoryPack;
namespace LinqDB.Serializers.MemoryPack.Formatters.Sets;

using Reader = MemoryPackReader;
using G = LinqDB.Sets;
public class Set<TKey,TElement>:MemoryPackFormatter<G.Set<TKey,TElement>>
    where TElement:G.IKey<TKey>
    where TKey : struct, IEquatable<TKey>{
    public new static readonly Set<TKey,TElement> Instance=new();
    private static void WriteNullable<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,G.Set<TKey,TElement>? value) where TBufferWriter:IBufferWriter<byte>{
        if(writer.TryWriteNil(value)) return;
        var type=value!.GetType();
        writer.WriteType(type);
        if(typeof(G.Set<TKey,TElement>)!=type){
            writer.Write(value);
            return;
        }
        var Count=value.LongCount;
        var Formatter=writer.GetFormatter<TElement>();
        writer.WriteVarInt(Count);
        foreach(var item in value)
            writer.Write(Formatter,item);
    }
    public override void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,scoped ref G.Set<TKey,TElement>? value)=>WriteNullable(ref writer,value);
    private static G.Set<TKey,TElement>? ReadNullable(ref Reader reader){
        if(reader.TryReadNil())return null;
        var type=reader.ReadType();
        if(typeof(G.Set<TKey,TElement>)!=type)return(G.Set<TKey,TElement>?)reader.Read(type);
        var Formatter=reader.GetFormatter<TElement>();
        var value=new G.Set<TKey,TElement>();
        var Count=reader.ReadVarIntInt64();
        for(long a=0;a<Count;a++) value.Add(reader.Read(Formatter));
        return value;
    }
    public override void Deserialize(ref Reader reader,scoped ref G.Set<TKey,TElement>? value)=>value=ReadNullable(ref reader);
}
