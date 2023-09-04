using MemoryPack;
using Expressions=System.Linq.Expressions;
namespace LinqDB.Serializers.MemoryPack.Formatters;
public class ElementInit:MemoryPackFormatter<Expressions.ElementInit>{
    private readonly 必要なFormatters Formatters;
    public ElementInit(必要なFormatters Formatters)=>this.Formatters=Formatters;
    public override void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,scoped ref Expressions.ElementInit? value){
        this.Formatters.Method.Serialize(ref writer,value!.AddMethod);
        必要なFormatters.Serialize(ref writer,value.Arguments);
    }
    public override void Deserialize(ref MemoryPackReader reader,scoped ref Expressions.ElementInit? value){
        var addMethod= this.Formatters.Method.Deserialize(ref reader);
        var arguments=reader.ReadArray<Expressions.Expression>();
        value=Expressions.Expression.ElementInit(addMethod,arguments!);
    }
}
