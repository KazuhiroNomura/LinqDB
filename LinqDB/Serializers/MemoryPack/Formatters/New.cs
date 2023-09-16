﻿using MemoryPack;

using System.Buffers;
using System.Diagnostics;
using System.Linq.Expressions;

namespace LinqDB.Serializers.MemoryPack.Formatters;
using Reader=MemoryPackReader;
using T= NewExpression;
using static Extension;


public class New:MemoryPackFormatter<T> {
    public static readonly New Instance=new();
    internal static void InternalSerializeNew<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,T value) where TBufferWriter:IBufferWriter<byte>{
        Constructor.Serialize(ref writer,value.Constructor!);
        writer.SerializeReadOnlyCollection(value.Arguments);
    }
    //internal static void InternalSerializeNew<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,T? value) where TBufferWriter:IBufferWriter<byte>{
    //    writer.WriteNodeType(ExpressionType.New);
    //    PrivateSerialize(ref writer,value);
    //}
    internal static void InternalSerialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,T? value) where TBufferWriter:IBufferWriter<byte>{
        writer.WriteNodeType(ExpressionType.New);
        InternalSerializeNew(ref writer,value);
    }
    public override void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,scoped ref T? value){
        Debug.Assert(value!=null,nameof(value)+" != null");
        InternalSerializeNew(ref writer,value);
    }
    internal static T InternaDeserialize(ref Reader reader){
        var constructor= Constructor.Deserialize(ref reader);
        var arguments=reader.ReadArray<System.Linq.Expressions.Expression>();
        return System.Linq.Expressions.Expression.New(
            constructor,
            arguments!
        );
    }
    public override void Deserialize(ref Reader reader,scoped ref T? value){
        value=InternaDeserialize(ref reader);
    }
}
