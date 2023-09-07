using System.Buffers;
using Expressions = System.Linq.Expressions;
using MemoryPack;

namespace LinqDB.Serializers.MemoryPack.Formatters;
using Reader=MemoryPackReader;
using T=Expressions.MethodCallExpression;
using C=MemoryPackCustomSerializer;
using static Common;


public class MethodCall:MemoryPackFormatter<T> {
    public static readonly MethodCall Instance=new();
    internal void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,T? value)where TBufferWriter:IBufferWriter<byte> =>this.Serialize(ref writer,ref value);
    internal T DeserializeMethodCall(ref MemoryPackReader reader){
        T? value=default;
        this.Deserialize(ref reader,ref value);
        return value!;
    }
    public override void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,scoped ref T? value){
        if(value is null){
            return;
        }
        var method=value.Method;
        Method.Instance.Serialize(ref writer,method);
        if(!method.IsStatic){
            Expression.Instance.Serialize(ref writer,value.Object!);
        }
        SerializeReadOnlyCollection(ref writer,value.Arguments);
    }
    public override void Deserialize(ref MemoryPackReader reader,scoped ref T? value){
        //if(reader.TryReadNil()) return;
        var method= Method.Instance.Deserialize(ref reader);
        if(method.IsStatic){
            var arguments=reader.ReadArray<Expressions.Expression>();
            value=Expressions.Expression.Call(
                method,
                arguments!
            );
        } else{
            var instance= Expression.Instance.Deserialize(ref reader);
            var arguments=reader.ReadArray<Expressions.Expression>();
            value=Expressions.Expression.Call(
                instance,
                method,
                arguments!
            );
        }
    }
}
