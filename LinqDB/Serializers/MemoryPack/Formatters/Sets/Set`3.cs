using System;
using System.Buffers;
using LinqDB.Databases;
using LinqDB.Sets;

using MemoryPack;
namespace LinqDB.Serializers.MemoryPack.Formatters.Sets;

using Reader = MemoryPackReader;
using Sets = LinqDB.Sets;
// ReSharper disable once InconsistentNaming
public class Set<TKey,TElement,TContainer>:MemoryPackFormatter<Sets.Set<TKey,TElement,TContainer>>
    where TElement : Entity<TKey,TContainer>
    where TKey : struct, IEquatable<TKey>
    where TContainer : Container{
    public static readonly Set<TKey,TElement,TContainer>Instance=new();
    private Set(){}
    private static void Write<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,Sets.Set<TKey,TElement,TContainer> value)where TBufferWriter:IBufferWriter<byte>{
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
    private static void WriteNullable<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,Sets.Set<TKey,TElement,TContainer>? value) where TBufferWriter:IBufferWriter<byte>{
        if(writer.TryWriteNil(value)) return;
        Write(ref writer,value);
    }
    public override void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,scoped ref Sets.Set<TKey,TElement,TContainer>? value)=>WriteNullable(ref writer,value);
    private static Sets.Set<TKey,TElement,TContainer>? ReadNullable(ref Reader reader){
        if(reader.TryReadNil())return null;
        var Formatter=reader.GetFormatter<TElement>();
        var value=new Sets.Set<TKey,TElement,TContainer>(null!);
        var Count=reader.ReadVarIntInt64();
        for(long a=0;a<Count;a++){
            TElement? item=default;
            Formatter.Deserialize(ref reader,ref item);
            value.Add(item);
        }
        return value;
    }
    public override void Deserialize(ref Reader reader,scoped ref Sets.Set<TKey,TElement,TContainer>? value)=>value=ReadNullable(ref reader);
}
