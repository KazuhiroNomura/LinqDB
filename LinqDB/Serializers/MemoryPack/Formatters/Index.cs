using System.Buffers;
using Expressions=System.Linq.Expressions;
using MemoryPack;
namespace LinqDB.Serializers.MemoryPack.Formatters;


public class Index:MemoryPackFormatter<Expressions.IndexExpression>{
    private readonly 必要なFormatters Formatters;
    public Index(必要なFormatters Formatters)=>this.Formatters=Formatters;
    internal void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,Expressions.IndexExpression? value)where TBufferWriter:IBufferWriter<byte>{
        this.Serialize(ref writer,ref value);
    }
    internal Expressions.IndexExpression DeserializeIndex(ref MemoryPackReader reader){
        Expressions.IndexExpression? value=default;
        this.Deserialize(ref reader,ref value);
        return value!;
    }
    public override void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,scoped ref Expressions.IndexExpression? value){
        if(value is null){
            //writer.WriteNil();
            return;
        }
        var Formatters=this.Formatters;
        Formatters.Expression.Serialize(ref writer,value.Object);
        Formatters.Property.Serialize(ref writer,value.Indexer);
        必要なFormatters.Serialize(ref writer,value.Arguments);
    }
    public override void Deserialize(ref MemoryPackReader reader,scoped ref Expressions.IndexExpression? value){
        //if(reader.TryReadNil()) return;
        var Formatters=this.Formatters;
        var instance= Formatters.Expression.Deserialize(ref reader);
        var indexer= Formatters.Property.DeserializePropertyInfo(ref reader);
        var arguments=reader.ReadArray<Expressions.Expression>();
        value=Expressions.Expression.MakeIndex(instance,indexer,arguments!);
    }
}
