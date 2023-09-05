﻿using MemoryPack;

using System.Buffers;
using System.Linq.Expressions;
namespace LinqDB.Serializers.MemoryPack.Formatters;
public class Constant:MemoryPackFormatter<ConstantExpression>{
    internal void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,ConstantExpression? value)where TBufferWriter:IBufferWriter<byte>{
        this.Serialize(ref writer,ref value);
    }
    internal ConstantExpression DeserializeConstant(ref MemoryPackReader reader){
        ConstantExpression? value=default;
        this.Deserialize(ref reader,ref value);
        return value!;
    }
    public override void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,scoped ref ConstantExpression? value){
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
        CustomSerializerMemoryPack.Type.Serialize(ref writer,value!.Type);
        CustomSerializerMemoryPack.Object.SerializeObject(ref writer,value.Value);
    }
    public override void Deserialize(ref MemoryPackReader reader,scoped ref ConstantExpression? value){
        var Constant_type=CustomSerializerMemoryPack.Type.DeserializeType(ref reader);
        var Constant_value=CustomSerializerMemoryPack.Object.DeserializeObject(ref reader);
        value=System.Linq.Expressions.Expression.Constant(Constant_value,Constant_type);
    }
}
