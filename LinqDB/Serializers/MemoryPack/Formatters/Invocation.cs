using System.Buffers;
using Expressions = System.Linq.Expressions;
using MemoryPack;
using System.Linq.Expressions;
using MessagePack;

namespace LinqDB.Serializers.MemoryPack.Formatters;
using Reader=MemoryPackReader;
using T= InvocationExpression;
using static Common;

public class Invocation:MemoryPackFormatter<T> {
    public static readonly Invocation Instance=new();
    internal void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,T? value)where TBufferWriter:IBufferWriter<byte>{
        this.Serialize(ref writer,ref value);
    }
    internal T DeserializeInvocation(ref Reader reader){
        T? value=default;
        this.Deserialize(ref reader,ref value);
        return value!;
    }
    public override void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,scoped ref T? value){
        if(value is null){
            //writer.WriteNil();
            return;
        }
        Expression.Instance.Serialize(ref writer,value.Expression);
        SerializeReadOnlyCollection(ref writer,value.Arguments);
    }
    // public override void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,scoped ref InvocationExpression? value) where TBufferWriter:IBufferWriter<byte>
    // {
    //     throw new NotImplementedException();
    // }
    public override void Deserialize(ref Reader reader,scoped ref T? value){
        //if(reader.TryReadNil()) return;
        var expression= Expression.Instance.Deserialize(ref reader);
        var arguments=reader.ReadArray<Expressions.Expression>();
        value=Expressions.Expression.Invoke(expression,arguments!);
    }

}
