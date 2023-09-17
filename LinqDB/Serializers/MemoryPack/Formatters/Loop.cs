using System.Buffers;
using Expressions = System.Linq.Expressions;
using MemoryPack;
namespace LinqDB.Serializers.MemoryPack.Formatters;
using Reader = MemoryPackReader;
using T = Expressions.LoopExpression;

public class Loop:MemoryPackFormatter<T>{
    public static readonly Loop Instance=new();
    private static void PrivateSerialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,T value)where TBufferWriter:IBufferWriter<byte>{
        if(value.BreakLabel is null){
            writer.WriteNullObjectHeader();
            Expression.Write(ref writer,value.Body);
        } else{
            LabelTarget.Write(ref writer,value.BreakLabel);
            if(value.ContinueLabel is null){
                writer.WriteNullObjectHeader();
                Expression.Write(ref writer,value.Body);
            } else{
                LabelTarget.Write(ref writer,value.ContinueLabel);
                Expression.Write(ref writer,value.Body);
            }
        }
    }
    internal static void Write<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,T? value)where TBufferWriter:IBufferWriter<byte>{
        writer.WriteNodeType(Expressions.ExpressionType.Loop);
        PrivateSerialize(ref writer,value);
    }
    public override void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,scoped ref T? value){
        PrivateSerialize(ref writer,value);
    }
    internal static T Read(ref Reader reader){
        if(reader.PeekIsNull()){
            reader.Advance(1);
            var body=Expression.Read(ref reader);
            return Expressions.Expression.Loop(body);
        } else{
            var breakLabel=LabelTarget.Read(ref reader);
            if(reader.PeekIsNull()){
                reader.Advance(1);
                var body=Expression.Read(ref reader);
                return Expressions.Expression.Loop(body,breakLabel);
            } else{
                var continueLabel=LabelTarget.Read(ref reader);
                var body=Expression.Read(ref reader);
                return Expressions.Expression.Loop(body,breakLabel,continueLabel);
            }
        }
    }
    public override void Deserialize(ref Reader reader,scoped ref T? value){
        value=Read(ref reader);
    }
}
