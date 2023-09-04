using MemoryPack;

using System.Buffers;
using System.Linq.Expressions;

namespace LinqDB.Serializers.MemoryPack.Formatters;
public class Conditional:MemoryPackFormatter<ConditionalExpression>{
    private readonly 必要なFormatters Formatters;
    public Conditional(必要なFormatters Formatters)=>this.Formatters=Formatters;
    internal void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,ConditionalExpression? value)where TBufferWriter:IBufferWriter<byte> =>this.Serialize(ref writer,ref value);
    internal ConditionalExpression DeserializeConditional(ref MemoryPackReader reader){
        ConditionalExpression? value=default;
        this.Deserialize(ref reader,ref value);
        return value!;
    }
    public override void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,scoped ref ConditionalExpression? value){
        if(value is null){
            //writer.WriteNil();
            return;
        }
        this.Formatters.Expression.Serialize(ref writer,value.Test);
        this.Formatters.Expression.Serialize(ref writer,value.IfTrue);
        this.Formatters.Expression.Serialize(ref writer,value.IfFalse);
    }
    public override void Deserialize(ref MemoryPackReader reader,scoped ref ConditionalExpression? value){
        //if(reader.TryReadNil()) return null!;
        var test= this.Formatters.Expression.Deserialize(ref reader);
        var ifTrue= this.Formatters.Expression.Deserialize(ref reader);
        var ifFalse= this.Formatters.Expression.Deserialize(ref reader);
        value=System.Linq.Expressions.Expression.Condition(
            test,
            ifTrue,
            ifFalse
        );
    }
}
