using MemoryPack;

using Expressions = System.Linq.Expressions;
namespace LinqDB.Serializers.MemoryPack.Formatters;

using Reader = MemoryPackReader;
using T = Expressions.SwitchCase;
public class SwitchCase:MemoryPackFormatter<T> {
    public static readonly SwitchCase Instance=new();
    public override void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,scoped ref T? value){
        if(writer.TryWriteNil(value)) return;
        
        writer.WriteCollection(value!.TestValues);
        
        Expression.Write(ref writer,value.Body);
        
    }
    public override void Deserialize(ref Reader reader,scoped ref T? value){
        if(reader.TryReadNil()) return;

        var testValues=reader.ReadArray<Expressions.Expression>();

        var body= Expression.Read(ref reader);

        value=Expressions.Expression.SwitchCase(body,testValues!);
    }
}
