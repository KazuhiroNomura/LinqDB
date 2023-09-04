using System.Buffers;
using Expressions=System.Linq.Expressions;
using MemoryPack;
namespace LinqDB.Serializers.MemoryPack.Formatters;


public class MethodCall:MemoryPackFormatter<Expressions.MethodCallExpression>{
    private readonly 必要なFormatters Formatters;
    public MethodCall(必要なFormatters Formatters)=>this.Formatters=Formatters;
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
        this.Formatters.Method.Serialize(ref writer,Method);
        if(!Method.IsStatic){
            this.Formatters.Expression.Serialize(ref writer,value.Object!);
        }
        必要なFormatters.Serialize(ref writer,value.Arguments);
    }
    public override void Deserialize(ref MemoryPackReader reader,scoped ref Expressions.MethodCallExpression? value){
        //if(reader.TryReadNil()) return;
        var method= this.Formatters.Method.Deserialize(ref reader);
        if(method.IsStatic){
            var arguments=reader.ReadArray<System.Linq.Expressions.Expression>();
            value=System.Linq.Expressions.Expression.Call(
                method,
                arguments!
            );
        } else{
            var instance= this.Formatters.Expression.Deserialize(ref reader);
            var arguments=reader.ReadArray<System.Linq.Expressions.Expression>();
            value=System.Linq.Expressions.Expression.Call(
                instance,
                method,
                arguments!
            );
        }
    }
}
