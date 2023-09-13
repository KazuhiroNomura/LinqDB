using System;
using System.Buffers;
using MemoryPack;
using Expressions=System.Linq.Expressions;

namespace LinqDB.Serializers.MemoryPack.Formatters;
using Reader=MemoryPackReader;
using T=Expressions.TypeBinaryExpression;
public class TypeBinary:MemoryPackFormatter<T> {
    public static readonly TypeBinary Instance=new();
    internal static void InternalSerialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,T value)where TBufferWriter:IBufferWriter<byte>{
        Expression.InternalSerialize(ref writer,value.Expression);
        writer.WriteType(value.TypeOperand);
    }
    internal static void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,T? value)where TBufferWriter:IBufferWriter<byte> =>
        Instance.Serialize(ref writer,ref value);
    public override void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,scoped ref T? value){
        writer.WriteVarInt((byte)value!.NodeType);
        Expression.InternalSerialize(ref writer,value.Expression);
        Type.Serialize(ref writer,value.TypeOperand);
    }
    private static (Expressions.Expression expression,System.Type type)PrivateDeserialize(ref Reader reader){
        var expression=Expression.InternalDeserialize(ref reader);
        var type=reader.ReadType();
        return (expression,type);
    }
    internal static T InternalDeserializeTypeEqual(ref Reader reader){
        var (expression,type)=PrivateDeserialize(ref reader);
        return Expressions.Expression.TypeEqual(expression,type);
    }
    internal static T InternalDeserializeTypeIs(ref Reader reader){
        var (expression,type)=PrivateDeserialize(ref reader);
        return Expressions.Expression.TypeIs(expression,type);
    }
    public override void Deserialize(ref Reader reader,scoped ref T? value){
        var NodeType=reader.ReadNodeType();
        value=NodeType switch{
            Expressions.ExpressionType.TypeEqual=>InternalDeserializeTypeEqual(ref reader),
            Expressions.ExpressionType.TypeIs=>InternalDeserializeTypeIs(ref reader),
            _=>throw new NotSupportedException(NodeType.ToString())
        };
    }
}
