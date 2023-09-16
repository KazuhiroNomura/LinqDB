using System.Buffers;
using Expressions = System.Linq.Expressions;
using MemoryPack;

namespace LinqDB.Serializers.MemoryPack.Formatters;
using Reader = MemoryPackReader;
using T = Expressions.MethodCallExpression;
using static Extension;


public class MethodCall:MemoryPackFormatter<T> {
    public static readonly MethodCall Instance=new();
    private static void PrivateSerialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,T? value)where TBufferWriter:IBufferWriter<byte>{
        var method=value!.Method;
        Method.Serialize(ref writer,method);
        if(!method.IsStatic){
            Expression.InternalSerialize(ref writer,value.Object!);
        }
        writer.SerializeReadOnlyCollection(value.Arguments);
    }
    internal static void InternalSerialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,T? value)where TBufferWriter:IBufferWriter<byte>{
        writer.WriteNodeType(Expressions.ExpressionType.Call);
        PrivateSerialize(ref writer,value);
    }
    public override void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,scoped ref T? value){
        PrivateSerialize(ref writer,value);
    }
    internal static T InternalDeserialize(ref Reader reader){
        var method= Method.Deserialize(ref reader);
        if(method.IsStatic){
            var arguments=reader.ReadArray<Expressions.Expression>();
            return Expressions.Expression.Call(
                method,
                arguments!
            );
        } else{
            var instance= Expression.InternalDeserialize(ref reader);
            var arguments=reader.ReadArray<Expressions.Expression>();
            return Expressions.Expression.Call(
                instance,
                method,
                arguments!
            );
        }
    }
    public override void Deserialize(ref Reader reader,scoped ref T? value){
        value=InternalDeserialize(ref reader);
    }
}
