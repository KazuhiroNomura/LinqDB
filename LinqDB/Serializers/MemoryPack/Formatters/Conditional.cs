using System.Buffers;
using MemoryPack;

using Expressions = System.Linq.Expressions;
namespace LinqDB.Serializers.MemoryPack.Formatters;

using Reader = MemoryPackReader;
using T = Expressions.ConditionalExpression;
public class Conditional:MemoryPackFormatter<T> {
    public static readonly Conditional Instance=new();
    private static void PrivateWrite<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,T? value)where TBufferWriter:IBufferWriter<byte>{
        Expression.Write(ref writer,value!.Test);
        
        Expression.Write(ref writer,value.IfTrue);
        
        Expression.Write(ref writer,value.IfFalse);
        
        writer.WriteType(value.Type);
    }
    internal static void Write<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,T? value)where TBufferWriter:IBufferWriter<byte>{

        writer.WriteNodeType(Expressions.ExpressionType.Conditional);
        
        PrivateWrite(ref writer,value);
    }
    public override void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,scoped ref T? value){
        if(writer.TryWriteNil(value)) return;
        
        PrivateWrite(ref writer,value);

    }
    internal static T Read(ref Reader reader){
        var test= Expression.Read(ref reader);

        var ifTrue= Expression.Read(ref reader);

        var ifFalse= Expression.Read(ref reader);

        var type=reader.ReadType();
        return Expressions.Expression.Condition(test,ifTrue,ifFalse,type);
    }
    public override void Deserialize(ref Reader reader,scoped ref T? value){
        if(reader.TryReadNil()) return;



        value=Read(ref reader);
    }
}
