using System.Buffers;
using System.Diagnostics;
using Expressions=System.Linq.Expressions;
using MemoryPack;
namespace LinqDB.Serializers.MemoryPack.Formatters;
using Reader=MemoryPackReader;
using T=Expressions.LoopExpression;
using C=Serializer;

public class Loop:MemoryPackFormatter<T>{
    public static readonly Loop Instance=new();
    internal void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,T? value)where TBufferWriter:IBufferWriter<byte> =>this.Serialize(ref writer,ref value);
    internal T DeserializeLoop(ref Reader reader){
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
        if(value!.BreakLabel is null){
            writer.WriteNullObjectHeader();
            Expression.Instance.Serialize(ref writer,value.Body);
        } else{
            LabelTarget.Instance.Serialize(ref writer,value.BreakLabel);
            if(value.ContinueLabel is null){
                writer.WriteNullObjectHeader();
                Expression.Instance.Serialize(ref writer,value.Body);
            } else{
                LabelTarget.Instance.Serialize(ref writer,value.ContinueLabel);
                Expression.Instance.Serialize(ref writer,value.Body);
            }
        }
    }
    public override void Deserialize(ref Reader reader,scoped ref T? value){
        if(reader.PeekIsNull()){
            reader.Advance(1);
            var body=Expression.Instance.Deserialize(ref reader);
            value=Expressions.Expression.Loop(body);
        } else{
            var breakLabel=LabelTarget.Instance.DeserializeLabelTarget(ref reader);
            if(reader.PeekIsNull()){
                reader.Advance(1);
                var body=Expression.Instance.Deserialize(ref reader);
                value=Expressions.Expression.Loop(body,breakLabel);
            } else{
                var continueLabel=LabelTarget.Instance.DeserializeLabelTarget(ref reader);
                var body=Expression.Instance.Deserialize(ref reader);
                value=Expressions.Expression.Loop(body,breakLabel,continueLabel);
            }
        }
    }
}
