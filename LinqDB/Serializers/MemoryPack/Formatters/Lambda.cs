﻿using System.Buffers;
using System.Linq.Expressions;
using MemoryPack;
namespace LinqDB.Serializers.MemoryPack.Formatters;
using Reader=MemoryPackReader;
using static Common;
using T=LambdaExpression;
using C=Serializer;

public class Lambda:MemoryPackFormatter<T> {
    public static readonly Lambda Instance=new();
    internal void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,T? value)where TBufferWriter:IBufferWriter<byte>{
        this.Serialize(ref writer,ref value);
    }
    internal T DeserializeLambda(ref Reader reader){
        T? value=default;
        this.Deserialize(ref reader,ref value);
        return value!;
    }
    public override void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,scoped ref T? value){
        var ListParameter= C.Instance.ListParameter;
        var ListParameter_Count=ListParameter.Count;
        var Parameters=value!.Parameters;
        ListParameter.AddRange(Parameters);
        writer.WriteType(value.Type);
        Serialize宣言Parameters(ref writer,value.Parameters);
        Expression.Instance.Serialize(ref writer,value.Body);
        writer.WriteBoolean(value.TailCall);
        
        ListParameter.RemoveRange(ListParameter_Count,Parameters.Count);
    }
    public override void Deserialize(ref Reader reader,scoped ref T? value){
        var ListParameter= C.Instance.ListParameter;
        var ListParameter_Count=ListParameter.Count;
        var type = reader.ReadType();
        var parameters= Deserialize宣言Parameters(ref reader);
        ListParameter.AddRange(parameters!);
        var body = Expression.Instance.Deserialize(ref reader);
        var tailCall = reader.ReadBoolean();
        ListParameter.RemoveRange(ListParameter_Count,parameters!.Length);
        value=System.Linq.Expressions.Expression.Lambda(
            type,
            body,
            tailCall,
            parameters!
        );
    }
}
