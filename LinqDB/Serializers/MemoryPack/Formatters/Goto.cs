using MemoryPack;

using System.Buffers;
using System.Linq.Expressions;

namespace LinqDB.Serializers.MemoryPack.Formatters;


public class Goto:MemoryPackFormatter<GotoExpression>{
    internal void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,GotoExpression? value)where TBufferWriter:IBufferWriter<byte> =>this.Serialize(ref writer,ref value);
    internal GotoExpression DeserializeGoto(ref MemoryPackReader reader){
        GotoExpression? value=default;
        this.Deserialize(ref reader,ref value);
        return value!;
    }
    public override void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,scoped ref GotoExpression? value){
        writer.WriteVarInt((byte)value!.Kind);
        CustomSerializerMemoryPack.LabelTarget.Serialize(ref writer,value.Target);
        CustomSerializerMemoryPack.Expression.Serialize(ref writer,value.Value);
        CustomSerializerMemoryPack.Type.Serialize(ref writer,value.Type);
    }
    public override void Deserialize(ref MemoryPackReader reader,scoped ref GotoExpression? value){
        var kind=(GotoExpressionKind)reader.ReadVarIntByte();
        var target= CustomSerializerMemoryPack.LabelTarget.DeserializeLabelTarget(ref reader);
        var Goto_value=CustomSerializerMemoryPack.Expression.Deserialize(ref reader);
        var type=CustomSerializerMemoryPack.Type.DeserializeType(ref reader);
        value=System.Linq.Expressions.Expression.MakeGoto(kind,target,Goto_value,type);
    }
}