using System.Buffers;
using Expressions = System.Linq.Expressions;
using MemoryPack;
namespace LinqDB.Serializers.MemoryPack.Formatters;
using Reader = MemoryPackReader;
using T = Expressions.LoopExpression;

public class Loop:MemoryPackFormatter<T>{
    public static readonly Loop Instance=new();
    internal static void InternalSerialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,T? value)where TBufferWriter:IBufferWriter<byte> =>
        Instance.Serialize(ref writer,ref value);
    public override void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,scoped ref T? value){
        if(value!.BreakLabel is null){
            writer.WriteNullObjectHeader();
            Expression.InternalSerialize(ref writer,value.Body);
        } else{
            LabelTarget.Serialize(ref writer,value.BreakLabel);
            if(value.ContinueLabel is null){
                writer.WriteNullObjectHeader();
                Expression.InternalSerialize(ref writer,value.Body);
            } else{
                LabelTarget.Serialize(ref writer,value.ContinueLabel);
                Expression.InternalSerialize(ref writer,value.Body);
            }
        }
    }
    internal static T InternalDeserialize(ref Reader reader){
        T? value=default;
        Instance.Deserialize(ref reader,ref value);
        return value!;
    }
    public override void Deserialize(ref Reader reader,scoped ref T? value){
        if(reader.PeekIsNull()){
            reader.Advance(1);
            var body=Expression.InternalDeserialize(ref reader);
            value=Expressions.Expression.Loop(body);
        } else{
            var breakLabel=LabelTarget.DeserializeLabelTarget(ref reader);
            if(reader.PeekIsNull()){
                reader.Advance(1);
                var body=Expression.InternalDeserialize(ref reader);
                value=Expressions.Expression.Loop(body,breakLabel);
            } else{
                var continueLabel=LabelTarget.DeserializeLabelTarget(ref reader);
                var body=Expression.InternalDeserialize(ref reader);
                value=Expressions.Expression.Loop(body,breakLabel,continueLabel);
            }
        }
    }
}
