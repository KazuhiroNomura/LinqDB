using System.Buffers;
using Expressions=System.Linq.Expressions;
using MemoryPack;
namespace LinqDB.Serializers.MemoryPack.Formatters;


public class Invocation:MemoryPackFormatter<Expressions.InvocationExpression>{
    private readonly 必要なFormatters Formatters;
    public Invocation(必要なFormatters Formatters)=>this.Formatters=Formatters;
    internal void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,Expressions.InvocationExpression? value)where TBufferWriter:IBufferWriter<byte>{
        this.Serialize(ref writer,ref value);
    }
    internal Expressions.InvocationExpression DeserializeInvocation(ref MemoryPackReader reader){
        Expressions.InvocationExpression? value=default;
        this.Deserialize(ref reader,ref value);
        return value!;
    }
    public override void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,scoped ref Expressions.InvocationExpression? value){
        if(value is null){
            //writer.WriteNil();
            return;
        }
        this.Formatters.Expression.Serialize(ref writer,value.Expression);
        必要なFormatters.Serialize(ref writer,value.Arguments);
    }
    // public override void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,scoped ref InvocationExpression? value) where TBufferWriter:IBufferWriter<byte>
    // {
    //     throw new NotImplementedException();
    // }
    public override void Deserialize(ref MemoryPackReader reader,scoped ref Expressions.InvocationExpression? value){
        //if(reader.TryReadNil()) return;
        var expression= this.Formatters.Expression.Deserialize(ref reader);
        var arguments=reader.ReadArray<Expressions.Expression>();
        value=Expressions.Expression.Invoke(expression,arguments!);
    }

}
