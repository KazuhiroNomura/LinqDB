using System;
using System.Buffers;
using MemoryPack;
using System.Linq.Expressions;
namespace LinqDB.Serializers.MemoryPack.Formatters;
public class TypeBinary:MemoryPackFormatter<TypeBinaryExpression>{
    internal void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,TypeBinaryExpression? value)where TBufferWriter:IBufferWriter<byte> =>this.Serialize(ref writer,ref value);
    internal TypeBinaryExpression DeserializeTypeBinary(ref MemoryPackReader reader){
        TypeBinaryExpression? value=default;
        this.Deserialize(ref reader,ref value);
        return value!;
    }
    public override void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,scoped ref TypeBinaryExpression? value){
        if(value is null){
            //writer.WriteNil();
            return;
        }
        writer.WriteVarInt((byte)value!.NodeType);
        CustomSerializerMemoryPack.Expression.Serialize(ref writer,value.Expression);
        CustomSerializerMemoryPack.Type.Serialize(ref writer,value.TypeOperand);
    }
    public override void Deserialize(ref MemoryPackReader reader,scoped ref TypeBinaryExpression? value){
        //if(reader.TryReadNil()) return;
        var NodeType=(ExpressionType)reader.ReadVarIntByte();
        var expression= CustomSerializerMemoryPack.Expression.Deserialize(ref reader);
        var type=CustomSerializerMemoryPack.Type.DeserializeType(ref reader);
        value=NodeType switch{
            ExpressionType.TypeEqual=>System.Linq.Expressions.Expression.TypeEqual(expression,type),
            ExpressionType.TypeIs=>System.Linq.Expressions.Expression.TypeIs(expression,type),
            _=>throw new NotSupportedException(NodeType.ToString())
        };
    }
}
