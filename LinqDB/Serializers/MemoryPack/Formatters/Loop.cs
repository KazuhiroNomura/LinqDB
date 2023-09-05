using System.Buffers;
using System.Linq.Expressions;
using MemoryPack;
namespace LinqDB.Serializers.MemoryPack.Formatters;


public class Loop:MemoryPackFormatter<LoopExpression>{
    internal void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,LoopExpression? value)where TBufferWriter:IBufferWriter<byte> =>this.Serialize(ref writer,ref value);
    internal LoopExpression DeserializeLoop(ref MemoryPackReader reader){
        LoopExpression? value=default;
        this.Deserialize(ref reader,ref value);
        return value!;
    }
    //private LoopExpression DeserializeMethod(ref MemoryPackReader reader){
    //    LoopExpression? value=default;
    //    this.Deserialize(ref reader,ref value);
    //    return value!;
    //}
    public override void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,scoped ref LoopExpression? value){
        MemoryPackCustomSerializer.LabelTarget.Serialize(ref writer,value!.BreakLabel);
        MemoryPackCustomSerializer.LabelTarget.Serialize(ref writer,value.ContinueLabel);
        MemoryPackCustomSerializer.Expression.Serialize(ref writer,value.Body);
    }
    public override void Deserialize(ref MemoryPackReader reader,scoped ref LoopExpression? value){
        var breakLabel= MemoryPackCustomSerializer.LabelTarget.DeserializeLabelTarget(ref reader);
        var continueLabel= MemoryPackCustomSerializer.LabelTarget.DeserializeLabelTarget(ref reader);
        var body= MemoryPackCustomSerializer.Expression.Deserialize(ref reader);
        value=System.Linq.Expressions.Expression.Loop(body,breakLabel,continueLabel);
    }
}
