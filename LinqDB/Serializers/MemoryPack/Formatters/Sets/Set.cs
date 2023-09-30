using System;
using System.Buffers;
using LinqDB.Databases;
using LinqDB.Sets;

using MemoryPack;
namespace LinqDB.Serializers.MemoryPack.Formatters.Sets;

using Reader = MemoryPackReader;
using Sets = LinqDB.Sets;
public class Set<T>:MemoryPackFormatter<Sets.Set<T>>{
#pragma warning disable CA1823// 使用されていないプライベート フィールドを使用しません
    public static readonly Set<T> Instance=new();//リフレクションで使われる
#pragma warning restore CA1823// 使用されていないプライベート フィールドを使用しません
    private static void WriteNullable<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,Sets.Set<T>? value) where TBufferWriter:IBufferWriter<byte>{
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
    public override void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,scoped ref Sets.Set<T>? value)=>WriteNullable(ref writer,value);
    private static Sets.Set<T>? ReadNullable(ref Reader reader){
        if(reader.TryReadNil())return null;
        var type=reader.ReadType();
        if(typeof(Sets.Set<T>)==type){
            var Formatter=reader.GetFormatter<T>();
            var value=new Sets.Set<T>();
            var Count=reader.ReadVarIntInt64();
            for(long a=0;a<Count;a++){
                T? item=default;//ここでnull入れないと内部で作られない
                Formatter.Deserialize(ref reader,ref item);
                value.Add(item);
            }
            return value;
        } else{
            reader.Serializer().RegisterAnonymousDisplay(type);
            var Formatter=reader.GetFormatter(type);
            // ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
            object? @object=default;
            Formatter.Deserialize(ref reader,ref @object);
            return(Sets.Set<T>)@object!;
        }
    }
    public override void Deserialize(ref Reader reader,scoped ref Sets.Set<T>? value)=>value=ReadNullable(ref reader);
}
public class Set<TElement,TKey>:MemoryPackFormatter<Sets.Set<TElement,TKey>>
    where TElement:IPrimaryKey<TKey>
    where TKey : struct, IEquatable<TKey>{
    public static readonly Set<TElement,TKey> Instance=new();//リフレクションで使われる
    private static void WriteNullable<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,Sets.Set<TElement,TKey>? value) where TBufferWriter:IBufferWriter<byte>{
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
    public override void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,scoped ref Sets.Set<TElement,TKey>? value)=>WriteNullable(ref writer,value);
    private static Sets.Set<TElement,TKey>? ReadNullable(ref Reader reader){
        if(reader.TryReadNil())return null;
        var type=reader.ReadType();
        if(typeof(Sets.Set<TElement,TKey>)==type){
            var Formatter=reader.GetFormatter<TElement>();
            var value=new Sets.Set<TElement,TKey>();
            var Count=reader.ReadVarIntInt64();
            for(long a=0;a<Count;a++){
                TElement? item=default;
                Formatter.Deserialize(ref reader,ref item);
                value.Add(item);
            }
            return value;
        } else{
            reader.Serializer().RegisterAnonymousDisplay(type);
            var Formatter=reader.GetFormatter(type);
            // ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
            object? @object=default;
            Formatter.Deserialize(ref reader,ref @object);
            return(Sets.Set<TElement,TKey>)@object!;
        }
    }
    public override void Deserialize(ref Reader reader,scoped ref Sets.Set<TElement,TKey>? value)=>value=ReadNullable(ref reader);
}
public class Set<TElement,TKey,TContainer>:MemoryPackFormatter<Sets.Set<TElement,TKey,TContainer>>
    where TElement : Entity<TKey,TContainer>, IWriteRead<TElement>
    where TKey : struct, IEquatable<TKey>
    where TContainer : Container{
    public static readonly Set<TElement,TKey,TContainer> Instance=new();//リフレクションで使われる
    private static void Write<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,Sets.Set<TElement,TKey,TContainer> value)where TBufferWriter:IBufferWriter<byte>{
        writer.WriteType(value.GetType());
        var Count=value.LongCount;
        var Formatter=writer.GetFormatter<TElement>();
        writer.WriteVarInt(Count);
        foreach(var item in value){
            var item0=item;
            Formatter.Serialize(ref writer,ref item0);
        }
    }
    private static void WriteNullable<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,Sets.Set<TElement,TKey,TContainer>? value) where TBufferWriter:IBufferWriter<byte>{
        if(writer.TryWriteNil(value)) return;
        Write(ref writer,value);
    }
    public override void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,scoped ref Sets.Set<TElement,TKey,TContainer>? value)=>WriteNullable(ref writer,value);
    private static Sets.Set<TElement,TKey,TContainer>? ReadNullable(ref Reader reader){
        if(reader.TryReadNil())return null;
        var type=reader.ReadType();
        if(typeof(Sets.Set<TElement,TKey,TContainer>)==type){
            var Formatter=reader.GetFormatter<TElement>();
            var value=new Sets.Set<TElement,TKey,TContainer>(null!);
            var Count=reader.ReadVarIntInt64();
            for(long a=0;a<Count;a++){
                TElement? item=default;
                Formatter.Deserialize(ref reader,ref item);
                value.Add(item);
            }
            return value;
        } else{
            reader.Serializer().RegisterAnonymousDisplay(type);
            var Formatter=reader.GetFormatter(type);
            // ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
            object? @object=default;
            Formatter.Deserialize(ref reader,ref @object);
            return(Sets.Set<TElement,TKey,TContainer>)@object!;
        }
    }
    public override void Deserialize(ref Reader reader,scoped ref Sets.Set<TElement,TKey,TContainer>? value)=>value=ReadNullable(ref reader);
}
