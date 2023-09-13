using MemoryPack;

using System.Buffers;
using System.Linq.Expressions;
using Expressions=System.Linq.Expressions;
namespace LinqDB.Serializers.MemoryPack.Formatters;
using Reader=MemoryPackReader;
using T= ConditionalExpression;
using C=Serializer;

public class Conditional:MemoryPackFormatter<T> {
    public static readonly Conditional Instance=new();
    internal static void InternalSerialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,T? value)where TBufferWriter:IBufferWriter<byte> =>
        Instance.Serialize(ref writer,ref value);
    internal static T InternalDeserialize(ref Reader reader){
        T? value=default;
        Instance.Deserialize(ref reader,ref value);
        return value!;
    }
    public override void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,scoped ref T? value){
        Expression.InternalSerialize(ref writer,value!.Test);
        Expression.InternalSerialize(ref writer,value.IfTrue);
        Expression.InternalSerialize(ref writer,value.IfFalse);
        writer.WriteType(value.Type);
    }
    public override void Deserialize(ref Reader reader,scoped ref T? value){
        var test= Expression.InternalDeserialize(ref reader);
        var ifTrue= Expression.InternalDeserialize(ref reader);
        var ifFalse= Expression.InternalDeserialize(ref reader);
        var type=reader.ReadType();
        value=Expressions.Expression.Condition(test,ifTrue,ifFalse,type);
    }
}
