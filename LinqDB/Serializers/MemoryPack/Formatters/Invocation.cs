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
    private static void PrivateSerialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,T value) where TBufferWriter:IBufferWriter<byte>{
        Expression.InternalSerialize(ref writer,value!.Expression);
        writer.SerializeReadOnlyCollection(value.Arguments);
    }
    internal static void InternalSerialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,T? value) where TBufferWriter:IBufferWriter<byte>{
        writer.WriteNodeType(ExpressionType.Invoke);
        PrivateSerialize(ref writer,value);
    }
    public override void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,scoped ref T? value){
        PrivateSerialize(ref writer,value);
    }
    internal static T InternalDeserialize(ref Reader reader){
        var expression= Expression.InternalDeserialize(ref reader);
        var arguments=reader.ReadArray<Expressions.Expression>();
        return Expressions.Expression.Invoke(expression,arguments!);
    }
    public override void Deserialize(ref Reader reader,scoped ref T? value){
        value=InternalDeserialize(ref reader);
    }

}
