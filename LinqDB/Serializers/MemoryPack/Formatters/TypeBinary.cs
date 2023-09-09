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
        Expression.Instance.Serialize(ref writer,value.Expression);
    }
    private static (Expressions.Expression expression,System.Type type)PrivateDeserialize(ref Reader reader){
        var expression=Expression.Instance.Deserialize(ref reader);
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
    internal void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,T? value)where TBufferWriter:IBufferWriter<byte> =>this.Serialize(ref writer,ref value);
    internal T DeserializeTypeBinary(ref Reader reader){
        T? value=default;
        this.Deserialize(ref reader,ref value);
        return value!;
    }
    public override void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,scoped ref T? value){
        if(value is null){
            //writer.WriteNil();
            return;
        }
        writer.WriteVarInt((byte)value!.NodeType);
        Expression.Instance.Serialize(ref writer,value.Expression);
        Type.Instance.Serialize(ref writer,value.TypeOperand);
    }
    public override void Deserialize(ref Reader reader,scoped ref T? value){
        //if(reader.TryReadNil()) return;
        var NodeType=reader.ReadNodeType();
        var expression= Expression.Instance.Deserialize(ref reader);
        var type=Type.Instance.Deserialize(ref reader);
        value=NodeType switch{
            Expressions.ExpressionType.TypeEqual=>Expressions.Expression.TypeEqual(expression,type),
            Expressions.ExpressionType.TypeIs=>Expressions.Expression.TypeIs(expression,type),
            _=>throw new NotSupportedException(NodeType.ToString())
        };
    }
}
