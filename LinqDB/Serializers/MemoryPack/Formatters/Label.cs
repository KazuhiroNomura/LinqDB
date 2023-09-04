using System.Buffers;
using System.Linq.Expressions;
using MemoryPack;
namespace LinqDB.Serializers.MemoryPack.Formatters;


public class Label:MemoryPackFormatter<LabelExpression>{
    private readonly 必要なFormatters Formatters;
    public Label(必要なFormatters Formatters)=>this.Formatters=Formatters;
    internal void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,LabelExpression? value)where TBufferWriter:IBufferWriter<byte>{
        this.Serialize(ref writer,ref value);
    }
    internal LabelExpression DeserializeLabel(ref MemoryPackReader reader){
        LabelExpression? value=default;
        this.Deserialize(ref reader,ref value);
        return value!;
    }
    public override void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,scoped ref LabelExpression? value){
        if(value is null){
            //writer.WriteNil();
            return;
        }
        this.Formatters.LabelTarget.Serialize(ref writer,value.Target);
        this.Formatters.Expression.Serialize(ref writer,value.DefaultValue);
    }
    public override void Deserialize(ref MemoryPackReader reader,scoped ref LabelExpression? value){
        //if(reader.TryReadNil()) return;
        var target= this.Formatters.LabelTarget.DeserializeLabelTarget(ref reader);
        var defaultValue=this.Formatters.Expression.Deserialize(ref reader);
        value=System.Linq.Expressions.Expression.Label(target,defaultValue);
    }
}
