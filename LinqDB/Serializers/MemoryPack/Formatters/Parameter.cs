using System.Buffers;
using System.Linq.Expressions;
using MemoryPack;
namespace LinqDB.Serializers.MemoryPack.Formatters;


public class Parameter:MemoryPackFormatter<ParameterExpression>{
    private readonly 必要なFormatters Formatters;
    public Parameter(必要なFormatters Formatters)=>this.Formatters=Formatters;
    internal void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,ParameterExpression? value)where TBufferWriter:IBufferWriter<byte> =>this.Serialize(ref writer,ref value);
    internal ParameterExpression DeserializeParameter(ref MemoryPackReader reader){
        ParameterExpression? value=default;
        this.Deserialize(ref reader,ref value);
        return value!;
    }
    public override void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,scoped ref ParameterExpression? value){
        writer.WriteVarInt(this.Formatters.ListParameter.LastIndexOf(value));
    }
    public override void Deserialize(ref MemoryPackReader reader,scoped ref ParameterExpression? value){
        var index=reader.ReadVarIntInt32();
        var Parameter= this.Formatters.ListParameter[index];
        value=Parameter;
    }
}
