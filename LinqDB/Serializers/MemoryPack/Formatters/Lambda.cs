﻿using System.Buffers;
using System.Linq.Expressions;
using MemoryPack;
namespace LinqDB.Serializers.MemoryPack.Formatters;


public class Lambda:MemoryPackFormatter<LambdaExpression>{
    internal void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,LambdaExpression? value)where TBufferWriter:IBufferWriter<byte>{
        this.Serialize(ref writer,ref value);
    }
    internal LambdaExpression DeserializeLambda(ref MemoryPackReader reader){
        LambdaExpression? value=default;
        this.Deserialize(ref reader,ref value);
        return value!;
    }
    public override void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,scoped ref LambdaExpression? value){
        var ListParameter=MemoryPackCustomSerializer.ListParameter;
        var ListParameter_Count=ListParameter.Count;
        var Parameters=value!.Parameters;
        ListParameter.AddRange(Parameters);
        MemoryPackCustomSerializer.Type.Serialize(ref writer,value.Type);
        MemoryPackCustomSerializer.Serialize宣言Parameters(ref writer,value.Parameters);
        MemoryPackCustomSerializer.Expression.Serialize(ref writer,value.Body);
        writer.WriteVarInt((byte)(value.TailCall ? 1 : 0));
        
        ListParameter.RemoveRange(ListParameter_Count,Parameters.Count);
    }
    public override void Deserialize(ref MemoryPackReader reader,scoped ref LambdaExpression? value){
        var ListParameter=MemoryPackCustomSerializer.ListParameter;
        var ListParameter_Count=ListParameter.Count;
        var type = MemoryPackCustomSerializer.Type.DeserializeType(ref reader);
        var parameters=MemoryPackCustomSerializer.Deserialize宣言Parameters(ref reader);
        ListParameter.AddRange(parameters!);
        var body =MemoryPackCustomSerializer.Expression.Deserialize(ref reader);
        var tailCall =MemoryPackCustomSerializer.ReadBoolean(ref reader);
        ListParameter.RemoveRange(ListParameter_Count,parameters!.Length);
        value=System.Linq.Expressions.Expression.Lambda(
            type,
            body,
            tailCall,
            parameters!
        );
    }
}
