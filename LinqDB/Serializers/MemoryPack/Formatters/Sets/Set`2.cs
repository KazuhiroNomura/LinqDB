using System;
using System.Buffers;
using LinqDB.Sets;

using MemoryPack;
namespace LinqDB.Serializers.MemoryPack.Formatters.Sets;

using Reader = MemoryPackReader;
using Sets = LinqDB.Sets;
// ReSharper disable once InconsistentNaming
public class Set<TKey,TElement>:MemoryPackFormatter<Sets.Set<TKey,TElement>>
    where TElement:IKey<TKey>
    where TKey : struct, IEquatable<TKey>{
    public static readonly Set<TKey,TElement> Instance=new();//リフレクションで使われる
    private Set(){}
    private static void WriteNullable<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,Sets.Set<TKey,TElement>? value) where TBufferWriter:IBufferWriter<byte>{
        if(writer.TryWriteNil(value)) return;
        writer.WriteType(value!.GetType());
        var Count=value.LongCount;
        var Formatter=writer.GetFormatter<TElement>();
        writer.WriteVarInt(Count);
        foreach(var item in value){
            var item0=item;
            Formatter.Serialize(ref writer,ref item0);
        }
    }
    public override void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,scoped ref Sets.Set<TKey,TElement>? value)=>WriteNullable(ref writer,value);
    private static Sets.Set<TKey,TElement>? ReadNullable(ref Reader reader){
        if(reader.TryReadNil())return null;
        var type=reader.ReadType();
        if(typeof(Sets.Set<TKey,TElement>)==type){
            var Formatter=reader.GetFormatter<TElement>();
            var value=new Sets.Set<TKey,TElement>();
            var Count=reader.ReadVarIntInt64();
            for(long a=0;a<Count;a++){
                TElement? item=default;
                Formatter.Deserialize(ref reader,ref item);
                value.Add(item);
            }
            return value;
        } else{
            FormatterResolver.GetFormatter(type);
            var Formatter=reader.GetFormatter(type);
            // ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
            object? @object=default;
            Formatter.Deserialize(ref reader,ref @object);
            return(Sets.Set<TKey,TElement>)@object!;
        }
    }
    public override void Deserialize(ref Reader reader,scoped ref Sets.Set<TKey,TElement>? value)=>value=ReadNullable(ref reader);
}
