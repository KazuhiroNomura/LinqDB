using System.Diagnostics;
using MemoryPack;
using System.Buffers;
using Expressions = System.Linq.Expressions;
namespace LinqDB.Serializers.MemoryPack.Formatters;


using Reader = MemoryPackReader;
using T = Expressions.LoopExpression;
public class Loop:MemoryPackFormatter<T>{
    public static readonly Loop Instance=new();
    private static void PrivateWrite<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,T value)where TBufferWriter:IBufferWriter<byte>{
        if(value.BreakLabel is null){
            writer.WriteVarInt(2);
            Expression.Write(ref writer,value.Body);
        } else if(value.ContinueLabel is null){
            writer.WriteVarInt(3);
            LabelTarget.Write(ref writer,value.BreakLabel);
            Expression.Write(ref writer,value.Body);
        } else{
            writer.WriteVarInt(4);
            LabelTarget.Write(ref writer,value.BreakLabel);
            LabelTarget.Write(ref writer,value.ContinueLabel);
            Expression.Write(ref writer,value.Body);
        }
    }
    internal static void Write<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,T? value)where TBufferWriter:IBufferWriter<byte>{
        writer.WriteNodeType(Expressions.ExpressionType.Loop);
        PrivateWrite(ref writer,value);
    }
    public override void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,scoped ref T? value){
        if(writer.TryWriteNil(value)) return;
        PrivateWrite(ref writer,value);
    }
    internal static T Read(ref Reader reader){
        var ArrayHeader=reader.ReadVarIntInt32();
        T value;
        if(ArrayHeader==2){
            var body=Expression.Read(ref reader);
            value=Expressions.Expression.Loop(body);
        } else if(ArrayHeader==3){
            var breakLabel=LabelTarget.Read(ref reader);
            var body=Expression.Read(ref reader);
            value=Expressions.Expression.Loop(body,breakLabel);
        } else{
            Debug.Assert(ArrayHeader==4);
            var breakLabel=LabelTarget.Read(ref reader);
            var continueLabel=LabelTarget.Read(ref reader);
            var body=Expression.Read(ref reader);
            value=Expressions.Expression.Loop(body,breakLabel,continueLabel);
        }
        return value;
    }
    public override void Deserialize(ref Reader reader,scoped ref T? value){
        if(reader.TryReadNil()) return;
        value=Read(ref reader);
    }
}
