using System;
using System.Buffers;
using MemoryPack;
using System.Linq.Expressions;
namespace LinqDB.Serializers.MemoryPack.Formatters;
public class TypeBinary:MemoryPackFormatter<TypeBinaryExpression>{
    private readonly 必要なFormatters Formatters;
    public TypeBinary(必要なFormatters Formatters)=>this.Formatters=Formatters;
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
        this.Formatters.Expression.Serialize(ref writer,value.Expression);
        this.Formatters.Type.Serialize(ref writer,value.TypeOperand);
    }
    public override void Deserialize(ref MemoryPackReader reader,scoped ref TypeBinaryExpression? value){
        //if(reader.TryReadNil()) return;
        var NodeType=(ExpressionType)reader.ReadVarIntByte();
        var expression= this.Formatters.Expression.Deserialize(ref reader);
        var type=this.Formatters.Type.DeserializeType(ref reader);
        value=NodeType switch{
            ExpressionType.TypeEqual=>System.Linq.Expressions.Expression.TypeEqual(expression,type),
            ExpressionType.TypeIs=>System.Linq.Expressions.Expression.TypeIs(expression,type),
            _=>throw new NotSupportedException(NodeType.ToString())
        };
    }
}
