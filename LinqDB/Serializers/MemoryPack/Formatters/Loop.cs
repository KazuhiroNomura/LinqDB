using System.Buffers;
using Expressions=System.Linq.Expressions;
using MemoryPack;
namespace LinqDB.Serializers.MemoryPack.Formatters;
using Reader=MemoryPackReader;
using T=Expressions.LoopExpression;
using C=MemoryPackCustomSerializer;

public class Loop:MemoryPackFormatter<T>{
    public static readonly Loop Instance=new();
    internal void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,T? value)where TBufferWriter:IBufferWriter<byte> =>this.Serialize(ref writer,ref value);
    internal T DeserializeLoop(ref MemoryPackReader reader){
        T? value=default;
        this.Deserialize(ref reader,ref value);
        return value!;
    }
    //private LoopExpression DeserializeMethod(ref MemoryPackReader reader){
    //    LoopExpression? value=default;
    //    this.Deserialize(ref reader,ref value);
    //    return value!;
    //}
    public override void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,scoped ref T? value){
        LabelTarget.Instance.Serialize(ref writer,value!.BreakLabel);
        LabelTarget.Instance.Serialize(ref writer,value.ContinueLabel);
        Expression.Instance.Serialize(ref writer,value.Body);
    }
    public override void Deserialize(ref MemoryPackReader reader,scoped ref T? value){
        var breakLabel= LabelTarget.Instance.DeserializeLabelTarget(ref reader);
        var continueLabel= LabelTarget.Instance.DeserializeLabelTarget(ref reader);
        var body= Expression.Instance.Deserialize(ref reader);
        value=Expressions.Expression.Loop(body,breakLabel,continueLabel);
    }
}
