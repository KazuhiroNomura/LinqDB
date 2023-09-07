﻿using MemoryPack;

using System.Buffers;
using System.Diagnostics;
using Expressions=System.Linq.Expressions;
namespace LinqDB.Serializers.MemoryPack.Formatters;
using Reader=MemoryPackReader;
using T=Expressions.ConstantExpression;

public class Constant:MemoryPackFormatter<T> {
    public static readonly Constant Instance=new();
    internal void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,T? value)where TBufferWriter:IBufferWriter<byte>{
        this.Serialize(ref writer,ref value);
    }
    internal T DeserializeConstant(ref Reader reader) {
        T? value=default;
        this.Deserialize(ref reader,ref value);
        return value!;
    }
    public override void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,scoped ref T? value){
        //if(value==null) {
        //    writer.WriteNullObjectHeader();
        //    return;
        //}
        //var Type = value!.Type!;
        //writer.GetFormatter<Type>().Serialize(ref writer,ref Type);
        //var Value = value!.Value;
        //if(value.Value==null) {
        //    writer.WriteNullObjectHeader();
        //    return;
        //}
        Debug.Assert(value!=null,nameof(value)+" != null");
        writer.WriteType(value.Type);
        Object.Instance.SerializeObject(ref writer,value.Value);
    }
    public override void Deserialize(ref Reader reader,scoped ref T? value){
        var type=reader.ReadType();
        var value0=Object.Instance.DeserializeObject(ref reader);
        value=Expressions.Expression.Constant(value0,type);
    }
}
