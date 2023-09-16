using System.Buffers;
using MemoryPack;

using Expressions = System.Linq.Expressions;
namespace LinqDB.Serializers.MemoryPack.Formatters;

using Reader = MemoryPackReader;
using T = Expressions.ConditionalExpression;
public class Conditional:MemoryPackFormatter<T> {
    public static readonly Conditional Instance=new();
    private static void PrivateSerialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,T? value)where TBufferWriter:IBufferWriter<byte>{

        Expression.InternalSerialize(ref writer,value!.Test);
        
        Expression.InternalSerialize(ref writer,value.IfTrue);
        
        Expression.InternalSerialize(ref writer,value.IfFalse);
        
        writer.WriteType(value.Type);
    }
    internal static void InternalSerialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,T? value)where TBufferWriter:IBufferWriter<byte>{

        writer.WriteNodeType(Expressions.ExpressionType.Conditional);
        
        PrivateSerialize(ref writer,value);
    }
    public override void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,scoped ref T? value){

        
        PrivateSerialize(ref writer,value);

    }
    internal static T InternalDeserialize(ref Reader reader){
        var test= Expression.InternalDeserialize(ref reader);

        var ifTrue= Expression.InternalDeserialize(ref reader);

        var ifFalse= Expression.InternalDeserialize(ref reader);

        var type=reader.ReadType();

        return Expressions.Expression.Condition(test,ifTrue,ifFalse,type);
    }
    public override void Deserialize(ref Reader reader,scoped ref T? value){



        value=InternalDeserialize(ref reader);
    }
}
