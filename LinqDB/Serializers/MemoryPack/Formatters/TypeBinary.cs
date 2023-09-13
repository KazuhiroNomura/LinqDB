using System;
using System.Buffers;
using MemoryPack;
using Expressions=System.Linq.Expressions;

namespace LinqDB.Serializers.MemoryPack.Formatters;
using Reader=MemoryPackReader;
using T=Expressions.TypeBinaryExpression;
public class TypeBinary:MemoryPackFormatter<T> {
    public static readonly TypeBinary Instance=new();
    internal static void InternalSerializeExpression<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,T value)where TBufferWriter:IBufferWriter<byte>{
        Expression.Serialize(ref writer,value.Expression);
        writer.WriteType(value.TypeOperand);
    }
    private static (Expressions.Expression expression,System.Type type)PrivateDeserialize(ref Reader reader){
        var expression=Expression.Deserialize(ref reader);
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
    internal static void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,T? value)where TBufferWriter:IBufferWriter<byte> =>
        Instance.Serialize(ref writer,ref value);
    public override void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,scoped ref T? value){
        if(value is null){
            //writer.WriteNil();
        }
        writer.WriteVarInt((byte)value!.NodeType);
        Expression.Serialize(ref writer,value.Expression);
        Type.Serialize(ref writer,value.TypeOperand);
    }
    internal static T DeserializeTypeBinary(ref Reader reader){
        T? value=default;
        Instance.Deserialize(ref reader,ref value);
        return value!;
    }
    public override void Deserialize(ref Reader reader,scoped ref T? value){
        //if(reader.TryReadNil()) return;
        var NodeType=reader.ReadNodeType();
        var expression= Expression.Deserialize(ref reader);
        var type=Type.Deserialize(ref reader);
        value=NodeType switch{
            Expressions.ExpressionType.TypeEqual=>Expressions.Expression.TypeEqual(expression,type),
            Expressions.ExpressionType.TypeIs=>Expressions.Expression.TypeIs(expression,type),
            _=>throw new NotSupportedException(NodeType.ToString())
        };
    }
}
