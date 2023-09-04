using System;
using MemoryPack;
using System.Linq.Expressions;
using System.Buffers;

namespace LinqDB.Serializers.MemoryPack.Formatters;
public class NewArray:MemoryPackFormatter<NewArrayExpression>{
    private readonly 必要なFormatters Formatters;
    public NewArray(必要なFormatters Formatters)=>this.Formatters=Formatters;
    internal void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,NewArrayExpression? value)where TBufferWriter:IBufferWriter<byte> =>this.Serialize(ref writer,ref value);
    internal NewArrayExpression DeserializeNewArray(ref MemoryPackReader reader){
        NewArrayExpression? value=default;
        this.Deserialize(ref reader,ref value);
        return value!;
    }
    public override void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,scoped ref NewArrayExpression? value){
        if(value is null){
            //writer.WriteNil();
            return;
        }
        writer.WriteVarInt((byte)value.NodeType);
        this.Formatters.Type.Serialize(ref writer,value.Type.GetElementType());
        必要なFormatters.Serialize(ref writer,value.Expressions);
    }
    public override void Deserialize(ref MemoryPackReader reader,scoped ref NewArrayExpression? value){
        //if(reader.TryReadNil()) return;
        var NodeType=(ExpressionType)reader.ReadVarIntByte();
        var type=this.Formatters.Type.DeserializeType(ref reader);
        //var expressions=global::MemoryPack.Formatters.ArrayFormatter<Expression>() Deserialize_T<Expression[]>(ref reader);
        var expressions=reader.ReadArray<System.Linq.Expressions.Expression>();
        value=NodeType switch{
            ExpressionType.NewArrayBounds=>System.Linq.Expressions.Expression.NewArrayBounds(type,expressions!),
            ExpressionType.NewArrayInit=>System.Linq.Expressions.Expression.NewArrayInit(type,expressions!),
            _=>throw new NotImplementedException(NodeType.ToString())
        };
    }
}
