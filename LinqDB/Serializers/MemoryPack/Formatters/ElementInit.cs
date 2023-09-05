using MemoryPack;
using Expressions = System.Linq.Expressions;
namespace LinqDB.Serializers.MemoryPack.Formatters;
public class ElementInit:MemoryPackFormatter<Expressions.ElementInit>{
    public override void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,scoped ref Expressions.ElementInit? value){
        MemoryPackCustomSerializer.Method.Serialize(ref writer,value!.AddMethod);
        MemoryPackCustomSerializer.Serialize(ref writer,value.Arguments);
    }
    public override void Deserialize(ref MemoryPackReader reader,scoped ref Expressions.ElementInit? value){
        var addMethod= MemoryPackCustomSerializer.Method.Deserialize(ref reader);
        var arguments=reader.ReadArray<Expressions.Expression>();
        value=Expressions.Expression.ElementInit(addMethod,arguments!);
    }
}
