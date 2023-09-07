using System;
using System.Buffers;
using MemoryPack;
using Expressions=System.Linq.Expressions;

namespace LinqDB.Serializers.MemoryPack.Formatters;
using Reader=MemoryPackReader;
using T=Expressions.TypeBinaryExpression;
public class TypeBinary:MemoryPackFormatter<T> {
    public static readonly TypeBinary Instance=new();
    internal void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,T? value)where TBufferWriter:IBufferWriter<byte> =>this.Serialize(ref writer,ref value);
    internal T DeserializeTypeBinary(ref MemoryPackReader reader){
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
    public override void Deserialize(ref MemoryPackReader reader,scoped ref T? value){
        //if(reader.TryReadNil()) return;
        var NodeType=(Expressions.ExpressionType)reader.ReadVarIntByte();
        var expression= Expression.Instance.Deserialize(ref reader);
        var type=Type.Instance.Deserialize(ref reader);
        value=NodeType switch{
            Expressions.ExpressionType.TypeEqual=>Expressions.Expression.TypeEqual(expression,type),
            Expressions.ExpressionType.TypeIs=>Expressions.Expression.TypeIs(expression,type),
            _=>throw new NotSupportedException(NodeType.ToString())
        };
    }
}
