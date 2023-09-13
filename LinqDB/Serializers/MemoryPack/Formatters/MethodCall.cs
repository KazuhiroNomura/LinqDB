using System.Buffers;
using Expressions = System.Linq.Expressions;
using MemoryPack;

namespace LinqDB.Serializers.MemoryPack.Formatters;
using Reader=MemoryPackReader;
using T=Expressions.MethodCallExpression;
using C=Serializer;
using static Extension;


public class MethodCall:MemoryPackFormatter<T> {
    public static readonly MethodCall Instance=new();
    internal static void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,T? value)where TBufferWriter:IBufferWriter<byte> =>
        Instance.Serialize(ref writer,ref value);
    internal static T DeserializeMethodCall(ref Reader reader){
        T? value=default;
        Instance.Deserialize(ref reader,ref value);
        return value!;
    }
    public override void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,scoped ref T? value){
        if(value is null){
            return;
        }
        var method=value.Method;
        Method.Serialize(ref writer,method);
        if(!method.IsStatic){
            Expression.Serialize(ref writer,value.Object!);
        }
        writer.SerializeReadOnlyCollection(value.Arguments);
    }
    public override void Deserialize(ref Reader reader,scoped ref T? value){
        //if(reader.TryReadNil()) return;
        var method= Method.Deserialize(ref reader);
        if(method.IsStatic){
            var arguments=reader.ReadArray<Expressions.Expression>();
            value=Expressions.Expression.Call(
                method,
                arguments!
            );
        } else{
            var instance= Expression.Deserialize(ref reader);
            var arguments=reader.ReadArray<Expressions.Expression>();
            value=Expressions.Expression.Call(
                instance,
                method,
                arguments!
            );
        }
    }
}
