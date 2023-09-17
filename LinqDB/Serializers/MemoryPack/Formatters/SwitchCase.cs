using MemoryPack;
using Expressions = System.Linq.Expressions;
namespace LinqDB.Serializers.MemoryPack.Formatters;
using Reader = MemoryPackReader;
using static Extension;
using T = Expressions.SwitchCase;

public class SwitchCase:MemoryPackFormatter<T> {
    public static readonly SwitchCase Instance=new();
    //internal static void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,T? value)where TBufferWriter:IBufferWriter<byte> =>this.Serialize(ref writer,ref value);
    //private T DeserializeSwitchCase(ref Reader reader){
    //    T? value=default;
    //    this.Deserialize(ref reader,ref value);
    //    return value!;
    //}
    public override void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,scoped ref T? value){
        if(writer.TryWriteNil(value)) return;
        writer.SerializeReadOnlyCollection(value!.TestValues);
        Expression.Write(ref writer,value.Body);
    }
    public override void Deserialize(ref Reader reader,scoped ref T? value){
        var testValues=reader.ReadArray<Expressions.Expression>();
        var body= Expression.Read(ref reader);
        value=Expressions.Expression.SwitchCase(body,testValues!);
    }
}
