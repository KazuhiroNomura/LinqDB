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
    internal static void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,T? value)where TBufferWriter:IBufferWriter<byte> =>
        Instance.Serialize(ref writer,ref value);
    internal static T DeserializeConditional(ref Reader reader){
        T? value=default;
        Instance.Deserialize(ref reader,ref value);
        return value!;
    }
    public override void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,scoped ref T? value){
        if(value is null){
            //writer.WriteNil();
            //return;
        }
        Expression.Serialize(ref writer,value!.Test);
        Expression.Serialize(ref writer,value.IfTrue);
        Expression.Serialize(ref writer,value.IfFalse);
        writer.WriteType(value.Type);
    }
    public override void Deserialize(ref Reader reader,scoped ref T? value){
        //if(reader.TryReadNil()) return null!;
        var test= Expression.Deserialize(ref reader);
        var ifTrue= Expression.Deserialize(ref reader);
        var ifFalse= Expression.Deserialize(ref reader);
        var type=reader.ReadType();
        value=Expressions.Expression.Condition(test,ifTrue,ifFalse,type);
    }
}
