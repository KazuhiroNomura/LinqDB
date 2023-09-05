using System.Buffers;
using Expressions = System.Linq.Expressions;
using MemoryPack;

namespace LinqDB.Serializers.MemoryPack.Formatters;


public class MethodCall:MemoryPackFormatter<Expressions.MethodCallExpression>{
    internal void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,Expressions.MethodCallExpression? value)where TBufferWriter:IBufferWriter<byte> =>this.Serialize(ref writer,ref value);
    internal Expressions.MethodCallExpression DeserializeMethodCall(ref MemoryPackReader reader){
        Expressions.MethodCallExpression? value=default;
        this.Deserialize(ref reader,ref value);
        return value!;
    }
    public override void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,scoped ref Expressions.MethodCallExpression? value){
        if(value is null){
            return;
        }
        var Method=value.Method;
        MemoryPackCustomSerializer.Method.Serialize(ref writer,Method);
        if(!Method.IsStatic){
            MemoryPackCustomSerializer.Expression.Serialize(ref writer,value.Object!);
        }
        MemoryPackCustomSerializer.Serialize(ref writer,value.Arguments);
    }
    public override void Deserialize(ref MemoryPackReader reader,scoped ref Expressions.MethodCallExpression? value){
        //if(reader.TryReadNil()) return;
        var method= MemoryPackCustomSerializer.Method.Deserialize(ref reader);
        if(method.IsStatic){
            var arguments=reader.ReadArray<System.Linq.Expressions.Expression>();
            value=System.Linq.Expressions.Expression.Call(
                method,
                arguments!
            );
        } else{
            var instance= MemoryPackCustomSerializer.Expression.Deserialize(ref reader);
            var arguments=reader.ReadArray<System.Linq.Expressions.Expression>();
            value=System.Linq.Expressions.Expression.Call(
                instance,
                method,
                arguments!
            );
        }
    }
}
