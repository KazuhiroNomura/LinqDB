using MemoryPack;

using System.Buffers;
using System.Linq.Expressions;

namespace LinqDB.Serializers.MemoryPack.Formatters;
using Reader=MemoryPackReader;

public class Goto:MemoryPackFormatter<GotoExpression>{
    public static readonly Goto Instance=new();
    internal void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,GotoExpression? value)where TBufferWriter:IBufferWriter<byte> =>this.Serialize(ref writer,ref value);
    internal GotoExpression DeserializeGoto(ref MemoryPackReader reader){
        GotoExpression? value=default;
        this.Deserialize(ref reader,ref value);
        return value!;
    }
    public override void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,scoped ref GotoExpression? value){
        writer.WriteVarInt((byte)value!.Kind);
        LabelTarget.Instance.Serialize(ref writer,value.Target);
        Expression.Instance.Serialize(ref writer,value.Value);
        writer.WriteType(value.Type);
    }
    public override void Deserialize(ref MemoryPackReader reader,scoped ref GotoExpression? value){
        var kind=(GotoExpressionKind)reader.ReadVarIntByte();
        var target= LabelTarget.Instance.DeserializeLabelTarget(ref reader);
        var Goto_value=Expression.Instance.Deserialize(ref reader);
        var type=reader.ReadType();
        value=System.Linq.Expressions.Expression.MakeGoto(kind,target,Goto_value,type);
    }
}