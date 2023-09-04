﻿using System.Buffers;
using System.Diagnostics;
using MemoryPack;
using Expressions=System.Linq.Expressions;

namespace LinqDB.Serializers.MemoryPack.Formatters;
public class Block0:IMemoryPackFormatter<Expressions.BlockExpression>{
    //public void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,ref Expressions.BlockExpression? value)
    //    where TBufferWriter:IBufferWriter<byte>{

    //}

    //public void Deserialize(ref MemoryPackReader reader,ref Expressions.BlockExpression? value){

    //}
    public void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,scoped ref Expressions.BlockExpression? value)
        where TBufferWriter : IBufferWriter<byte> {
        throw new System.NotImplementedException();
    }
    public void Deserialize(ref MemoryPackReader reader,scoped ref Expressions.BlockExpression? value) {
        throw new System.NotImplementedException();
    }
}
public class Block:MemoryPackFormatter<Expressions.BlockExpression>{
    private readonly 必要なFormatters Formatters;
    public Block(必要なFormatters Formatters)=>this.Formatters=Formatters;
    internal void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,Expressions.BlockExpression? value) where TBufferWriter:IBufferWriter<byte>{
        this.Serialize(ref writer,ref value);
    }
    internal Expressions.BlockExpression DeserializeBlock(ref MemoryPackReader reader){
        Expressions.BlockExpression? value=default;
        this.Deserialize(ref reader,ref value);
        return value!;
    }
    public override void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,scoped ref Expressions.BlockExpression? value){
        var Formatters=this.Formatters;
        var ListParameter=Formatters.ListParameter;
        var ListParameter_Count=ListParameter.Count;
        var Variables=value!.Variables;
        ListParameter.AddRange(Variables);
        var Type=value.Type;
        Formatters.Type.Serialize(ref writer,ref Type);
        //var Variables=value.Variables;
        Formatters.Serialize宣言Parameters(ref writer,Variables);
        必要なFormatters.Serialize(ref writer,value.Expressions);
        Debug.Assert(Variables!=null,nameof(Variables)+" != null");
        ListParameter.RemoveRange(ListParameter_Count,Variables.Count);
    }
    public override void Deserialize(ref MemoryPackReader reader,scoped ref Expressions.BlockExpression? value){
        var Formatters=this.Formatters;
        var ListParameter=Formatters.ListParameter;
        var ListParameter_Count=ListParameter.Count;
        var type=this.Formatters.Type.DeserializeType(ref reader);
        var variables=Formatters.Deserialize宣言Parameters(ref reader);
        ListParameter.AddRange(variables!);
        var expressions=reader.ReadArray<Expressions.Expression>();
        ListParameter.RemoveRange(ListParameter_Count,variables!.Length);
        value=Expressions.Expression.Block(type,variables!,expressions!);
    }
}
