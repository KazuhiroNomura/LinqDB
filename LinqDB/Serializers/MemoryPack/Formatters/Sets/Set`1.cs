﻿using System.Buffers;
using MemoryPack;
namespace LinqDB.Serializers.MemoryPack.Formatters.Sets;

using Reader = MemoryPackReader;
using G = LinqDB.Sets;
public class Set<T>:MemoryPackFormatter<G.Set<T>>{
    public static readonly Set<T>Instance=new();
    private Set(){}
    private static void WriteNullable<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,G.Set<T>? value) where TBufferWriter:IBufferWriter<byte>{
        if(writer.TryWriteNil(value)) return;
        writer.WriteType(value!.GetType());
        var Count=value.LongCount;
        var Formatter=writer.GetFormatter<T>();
        writer.WriteVarInt(Count);
        foreach(var item in value){
            var item0=item;
            Formatter.Serialize(ref writer,ref item0);
        }
    }
    public override void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,scoped ref G.Set<T>? value)=>WriteNullable(ref writer,value);
    private static G.Set<T>? ReadNullable(ref Reader reader){
        if(reader.TryReadNil())return null;
        var type=reader.ReadType();
        if(typeof(G.Set<T>)==type){
            var Formatter=reader.GetFormatter<T>();
            var value=new G.Set<T>();
            var Count=reader.ReadVarIntInt64();
            for(long a=0;a<Count;a++){
                T? item=default;//ここでnull入れないと内部で作られない
                Formatter.Deserialize(ref reader,ref item);
                value.Add(item);
            }
            return value;
        } else{
            //GroupingSet<>,Set<,>など
            FormatterResolver.GetFormatter(type);
            var Formatter=reader.GetFormatter(type);
            // ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
            object? @object=default;
            Formatter.Deserialize(ref reader,ref @object);
            return(G.Set<T>)@object!;
        }
    }
    public override void Deserialize(ref Reader reader,scoped ref G.Set<T>? value)=>value=ReadNullable(ref reader);
}