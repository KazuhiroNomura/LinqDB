using System;
using System.Buffers;

using MemoryPack;
namespace LinqDB.Serializers.MemoryPack.Formatters.Sets;

using Reader = MemoryPackReader;
using G = LinqDB.Sets;
public class Set<TKey,TElement,TContainer>:MemoryPackFormatter<G.Set<TKey,TElement,TContainer>>
    where TElement :G.Entity<TKey,TContainer>
    where TKey : struct, IEquatable<TKey>
    where TContainer : LinqDB.Databases.Container{
    public static readonly Set<TKey,TElement,TContainer>Instance=new();
    private static void Write<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,G.Set<TKey,TElement,TContainer> value)where TBufferWriter:IBufferWriter<byte>{
        //var type=value.GetType();
        //var GenericArguments=type.GetGenericArguments();
        //writer.WriteType(typeof(Sets.Set<,>).MakeGenericType(GenericArguments[0],GenericArguments[1]));
        //writer.WriteType(value.GetType());
        var Count=value.LongCount;
        var Formatter=writer.GetFormatter<TElement>();
        writer.WriteVarInt(Count);
        foreach(var item in value){
            var item0=item;
            Formatter.Serialize(ref writer,ref item0);
        }
    }
    private static void WriteNullable<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,G.Set<TKey,TElement,TContainer>? value) where TBufferWriter:IBufferWriter<byte>{
        if(writer.TryWriteNil(value)) return;
        Write(ref writer,value);
    }
    public override void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,scoped ref G.Set<TKey,TElement,TContainer>? value)=>WriteNullable(ref writer,value);
    private static G.Set<TKey,TElement,TContainer>? ReadNullable(ref Reader reader){
        if(reader.TryReadNil())return null;
        var Formatter=reader.GetFormatter<TElement>();
        var value=new G.Set<TKey,TElement,TContainer>(null!);
        var Count=reader.ReadVarIntInt64();
        for(long a=0;a<Count;a++)
            value.Add(reader.Read(Formatter));
        return value;
    }
    public override void Deserialize(ref Reader reader,scoped ref G.Set<TKey,TElement,TContainer>? value)=>value=ReadNullable(ref reader);
}
