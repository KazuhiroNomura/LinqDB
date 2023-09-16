using MemoryPack;
using Expressions = System.Linq.Expressions;
namespace LinqDB.Serializers.MemoryPack.Formatters;
using Reader=MemoryPackReader;
using static Extension;
using T=Expressions.ElementInit;

public class ElementInit:MemoryPackFormatter<T> {
    public static readonly ElementInit Instance=new();
    public override void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,scoped ref T? value){
        Method.Write(ref writer,value!.AddMethod);
        writer.SerializeReadOnlyCollection(value.Arguments);
    }
    public override void Deserialize(ref Reader reader,scoped ref T? value){
        var addMethod= Method.Read(ref reader);
        var arguments=reader.ReadArray<Expressions.Expression>();
        value=Expressions.Expression.ElementInit(addMethod,arguments!);
    }
}
