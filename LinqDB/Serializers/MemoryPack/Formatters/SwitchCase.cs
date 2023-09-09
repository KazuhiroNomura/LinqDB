﻿using System.Buffers;
using MemoryPack;
using Expressions = System.Linq.Expressions;
namespace LinqDB.Serializers.MemoryPack.Formatters;
using Reader=MemoryPackReader;
using static Common;
using T=Expressions.SwitchCase;

public class SwitchCase:MemoryPackFormatter<T> {
    public static readonly SwitchCase Instance=new();
    internal void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,T? value)where TBufferWriter:IBufferWriter<byte> =>this.Serialize(ref writer,ref value);
    private T DeserializeSwitchCase(ref Reader reader){
        T? value=default;
        this.Deserialize(ref reader,ref value);
        return value!;
    }
    public override void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,scoped ref T? value){
        SerializeReadOnlyCollection(ref writer,value!.TestValues);
        Expression.Instance.Serialize(ref writer,value.Body);
    }
    public override void Deserialize(ref Reader reader,scoped ref T? value){
        var testValues=reader.ReadArray<Expressions.Expression>();
        var body= Expression.Instance.Deserialize(ref reader);
        value=Expressions.Expression.SwitchCase(body,testValues!);
    }
}
