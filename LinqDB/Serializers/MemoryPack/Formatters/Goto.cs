﻿using MemoryPack;

using System.Buffers;
using Expressions = System.Linq.Expressions;

namespace LinqDB.Serializers.MemoryPack.Formatters;
using Reader = MemoryPackReader;
using T = Expressions.GotoExpression;

public class Goto:MemoryPackFormatter<T>{
    public static readonly Goto Instance=new();
    private static void PrivateSerialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,T? value)where TBufferWriter:IBufferWriter<byte>{
        writer.WriteVarInt((byte)value!.Kind);
        LabelTarget.Serialize(ref writer,value.Target);
        Expression.SerializeNullable(ref writer,value.Value);
        writer.WriteType(value.Type);
    }
    internal static void Write<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,T? value)where TBufferWriter:IBufferWriter<byte>{
        writer.WriteNodeType(Expressions.ExpressionType.Goto);
        PrivateSerialize(ref writer,value);
    }
    public override void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,scoped ref T? value){
        PrivateSerialize(ref writer,value);
    }
    internal static T Read(ref Reader reader){
        var kind=(Expressions.GotoExpressionKind)reader.ReadVarIntByte();
        var target= LabelTarget.DeserializeLabelTarget(ref reader);
        var value0=Expression.InternalDeserializeNullable(ref reader);
        var type=reader.ReadType();
        return Expressions.Expression.MakeGoto(kind,target,value0,type);
    }
    public override void Deserialize(ref Reader reader,scoped ref T? value){
        value=Read(ref reader);
    }
}