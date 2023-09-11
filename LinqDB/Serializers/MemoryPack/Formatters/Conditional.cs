using MemoryPack;

using System.Buffers;
using System.Linq.Expressions;
using Expressions=System.Linq.Expressions;
namespace LinqDB.Serializers.MemoryPack.Formatters;
using Reader=MemoryPackReader;
using T=Expressions.ConditionalExpression;
using C=Serializer;

public class Conditional:MemoryPackFormatter<T> {
    public static readonly Conditional Instance=new();
    internal void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,T? value)where TBufferWriter:IBufferWriter<byte> =>this.Serialize(ref writer,ref value);
    internal T DeserializeConditional(ref Reader reader){
        T? value=default;
        this.Deserialize(ref reader,ref value);
        return value!;
    }
    public override void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,scoped ref T? value){
        if(value is null){
            //writer.WriteNil();
            //return;
        }
        Expression.Instance.Serialize(ref writer,value!.Test);
        Expression.Instance.Serialize(ref writer,value.IfTrue);
        Expression.Instance.Serialize(ref writer,value.IfFalse);
        writer.WriteType(value.Type);
    }
    public override void Deserialize(ref Reader reader,scoped ref T? value){
        //if(reader.TryReadNil()) return null!;
        var test= Expression.Instance.Deserialize(ref reader);
        var ifTrue= Expression.Instance.Deserialize(ref reader);
        var ifFalse= Expression.Instance.Deserialize(ref reader);
        var type=reader.ReadType();
        value=Expressions.Expression.Condition(test,ifTrue,ifFalse,type);
    }
}
