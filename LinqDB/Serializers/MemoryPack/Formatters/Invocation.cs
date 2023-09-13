using System.Buffers;
using Expressions = System.Linq.Expressions;
using MemoryPack;
using System.Linq.Expressions;
namespace LinqDB.Serializers.MemoryPack.Formatters;
using Reader=MemoryPackReader;
using T= InvocationExpression;
using static Extension;

public class Invocation:MemoryPackFormatter<T> {
    public static readonly Invocation Instance=new();
    internal static void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,T? value)where TBufferWriter:IBufferWriter<byte> =>
        Instance.Serialize(ref writer,ref value);
    internal static T DeserializeInvocation(ref Reader reader){
        T? value=default;
        Instance.Deserialize(ref reader,ref value);
        return value!;
    }
    public override void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,scoped ref T? value){
        if(value is null){
            //writer.WriteNil();
            return;
        }
        Expression.Serialize(ref writer,value.Expression);
        writer.SerializeReadOnlyCollection(value.Arguments);
    }
    // public override void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,scoped ref InvocationExpression? value) where TBufferWriter:IBufferWriter<byte>
    // {
    //     throw new NotImplementedException();
    // }
    public override void Deserialize(ref Reader reader,scoped ref T? value){
        //if(reader.TryReadNil()) return;
        var expression= Expression.Deserialize(ref reader);
        var arguments=reader.ReadArray<Expressions.Expression>();
        value=Expressions.Expression.Invoke(expression,arguments!);
    }

}
