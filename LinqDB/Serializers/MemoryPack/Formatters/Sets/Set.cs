using System;
using System.Buffers;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Reflection.Emit;
using LinqDB.Databases;
using LinqDB.Helpers;
using LinqDB.Sets;

using MemoryPack;

using Expressions = System.Linq.Expressions;
namespace LinqDB.Serializers.MemoryPack.Formatters.Sets;

using Reader = MemoryPackReader;
using Sets = LinqDB.Sets;
public class Set<TValue>:MemoryPackFormatter<Sets.Set<TValue>>{
#pragma warning disable CA1823// 使用されていないプライベート フィールドを使用しません
    public static readonly Set<TValue> Instance=new();//リフレクションで使われる
#pragma warning restore CA1823// 使用されていないプライベート フィールドを使用しません
    internal static void WriteNullable<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,Sets.Set<TValue>? value) where TBufferWriter:IBufferWriter<byte>{
        if(writer.TryWriteNil(value)) return;
        writer.WriteType(value!.GetType());
        var Count=value.Count;
        var Formatter=writer.GetFormatter<TValue>();
        writer.WriteVarInt(Count);
        foreach(var item in value){
            var item0=item;
            Formatter.Serialize(ref writer,ref item0);
        }
    }
    public override void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,scoped ref Sets.Set<TValue>? value)=>WriteNullable(ref writer,value);
    internal static Sets.Set<TValue>? ReadNullable(ref Reader reader){
        if(reader.TryReadNil())return null;
        var type=reader.ReadType();
        if(typeof(Sets.Set<TValue>)==type){
            var Formatter=reader.GetFormatter<TValue>();
            var value=new Sets.Set<TValue>();
            var Count=reader.ReadVarIntInt64();
            TValue? item=default;
            for(long a=0;a<Count;a++){
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
            return(Sets.Set<TValue>)@object!;
        }
    }
    public override void Deserialize(ref Reader reader,scoped ref Sets.Set<TValue>? value)=>value=ReadNullable(ref reader);
}
public class Set<TValue,TKey>:MemoryPackFormatter<Sets.Set<TValue,TKey>>
    where TValue:IPrimaryKey<TKey>
    where TKey : struct, IEquatable<TKey>{
#pragma warning disable CA1823// 使用されていないプライベート フィールドを使用しません
    public static readonly Set<TValue,TKey> Instance=new();//リフレクションで使われる
#pragma warning restore CA1823// 使用されていないプライベート フィールドを使用しません
    internal static void WriteNullable<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,Sets.Set<TValue,TKey>? value) where TBufferWriter:IBufferWriter<byte>{
        if(writer.TryWriteNil(value)) return;
        writer.WriteType(value!.GetType());
        var Count=value.Count;
        var Formatter=writer.GetFormatter<TValue>();
        writer.WriteVarInt(Count);
        foreach(var item in value){
            var item0=item;
            Formatter.Serialize(ref writer,ref item0);
        }
    }
    public override void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,scoped ref Sets.Set<TValue,TKey>? value)=>WriteNullable(ref writer,value);
    private static Sets.Set<TValue,TKey>? ReadNullable(ref Reader reader){
        if(reader.TryReadNil())return null;
        var type=reader.ReadType();
        if(typeof(Sets.Set<TValue,TKey>)==type){
            var Formatter=reader.GetFormatter<TValue>();
            var value=new Sets.Set<TValue,TKey>();
            var Count=reader.ReadVarIntInt64();
            TValue? item=default;
            for(long a=0;a<Count;a++){
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
            return(Sets.Set<TValue,TKey>)@object!;
        }
        //IMemoryPackFormatter<TValue> Formatter;
        //if(typeof(Sets.Set<TValue,TKey>)!=type){
        //    var FormatterNullable=reader.GetFormatter(type);
        //    // ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
        //    if(FormatterNullable is null){
        //        var GenericTypeArguments=new[]{type};
        //        var FormatterType = typeof(Set<>).MakeGenericType(GenericTypeArguments);
        //        var Instance = FormatterType.GetField("Instance")!;
        //        Formatter=(IMemoryPackFormatter<TValue>)Instance.GetValue(null)!;
        //        var Register = Serializer.Register.MakeGenericMethod(GenericTypeArguments);
        //        Register.Invoke(null,new object?[]{Formatter});
        //        //Sets.Set<TValue,TKey> value=default!;
        //        //reader.ReadValue(type,ref value);
        //        //return value;
        //    } else{
        //        Formatter=(IMemoryPackFormatter<TValue>)FormatterNullable;
        //    }
        //} else{
        //    Formatter=reader.GetFormatter<TValue>();
        //}
    }
    public override void Deserialize(ref Reader reader,scoped ref Sets.Set<TValue,TKey>? value)=>value=ReadNullable(ref reader);
}
public class Set<TValue,TKey,TContainer>:MemoryPackFormatter<Sets.Set<TValue,TKey,TContainer>>
    where TValue : Entity<TKey,TContainer>, IWriteRead<TValue>
    where TKey : struct, IEquatable<TKey>
    where TContainer : Container{
#pragma warning disable CA1823// 使用されていないプライベート フィールドを使用しません
    public static readonly Set<TValue,TKey,TContainer> Instance=new();//リフレクションで使われる
#pragma warning restore CA1823// 使用されていないプライベート フィールドを使用しません
    internal static void Write<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,Sets.Set<TValue,TKey,TContainer> value)where TBufferWriter:IBufferWriter<byte>{
        writer.WriteType(value.GetType());
        var Count=value.Count;
        var Formatter=writer.GetFormatter<TValue>();
        writer.WriteVarInt(Count);
        foreach(var item in value){
            var item0=item;
            Formatter.Serialize(ref writer,ref item0);
        }
    }
    internal static void WriteNullable<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,Sets.Set<TValue,TKey,TContainer>? value) where TBufferWriter:IBufferWriter<byte>{
        if(writer.TryWriteNil(value)) return;
        Write(ref writer,value);
    }
    public override void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,scoped ref Sets.Set<TValue,TKey,TContainer>? value)=>WriteNullable(ref writer,value);
    private static Sets.Set<TValue,TKey,TContainer>? ReadNullable(ref Reader reader){
        if(reader.TryReadNil())return null;
        var type=reader.ReadType();
        if(typeof(Sets.Set<TValue,TKey,TContainer>)==type){
            var Formatter=reader.GetFormatter<TValue>();
            var value=new Sets.Set<TValue,TKey,TContainer>(null!);
            var Count=reader.ReadVarIntInt64();
            TValue? item=default;
            for(long a=0;a<Count;a++){
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
            return(Sets.Set<TValue,TKey,TContainer>)@object!;
        }
        //var type=reader.ReadType();
        //IMemoryPackFormatter<TValue> Formatter;
        //if(typeof(Sets.Set<TValue,TKey>)!=type){
        //    var FormatterNullable=reader.GetFormatter(type);
        //    // ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
        //    if(FormatterNullable is null){
        //        var GenericTypeArguments=new[]{type};
        //        var FormatterType = typeof(Set<>).MakeGenericType(GenericTypeArguments);
        //        var Instance = FormatterType.GetField("Instance")!;
        //        Formatter=(IMemoryPackFormatter<TValue>)Instance.GetValue(null)!;
        //        var Register = Serializer.Register.MakeGenericMethod(GenericTypeArguments);
        //        Register.Invoke(null,new object?[]{Formatter});
        //    } else{
        //        Formatter=(IMemoryPackFormatter<TValue>)FormatterNullable;
        //    }
        //} else{
        //    Formatter=reader.GetFormatter<TValue>();
        //}
        //var value=new Sets.Set<TValue,TKey,TContainer>(null!);
        //var Count=reader.ReadVarIntInt64();
        //TValue? item=default;
        //for(long a=0;a<Count;a++){
        //    Formatter.Deserialize(ref reader,ref item);
        //    value.Add(item);
        //}
        //return value;
    }
    public override void Deserialize(ref Reader reader,scoped ref Sets.Set<TValue,TKey,TContainer>? value)=>value=ReadNullable(ref reader);
}
