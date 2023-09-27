using MemoryPack;
using System.Buffers;
using Expressions = System.Linq.Expressions;
namespace LinqDB.Serializers.MemoryPack.Formatters;


using Reader=MemoryPackReader;
using T= Expressions.InvocationExpression;
public class Invocation:MemoryPackFormatter<T> {
    public static readonly Invocation Instance=new();
    private static void PrivateWrite<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,T value) where TBufferWriter:IBufferWriter<byte>{
        Expression.Write(ref writer,value.Expression);
        
        writer.WriteCollection(value.Arguments);
    }
    internal static void Write<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,T? value) where TBufferWriter:IBufferWriter<byte>{

        writer.WriteNodeType(Expressions.ExpressionType.Invoke);

        PrivateWrite(ref writer,value);
        
    }
    public override void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,scoped ref T? value){
        if(writer.TryWriteNil(value)) return;
        
        PrivateWrite(ref writer,value);
        
    }
    internal static T Read(ref Reader reader){
        var expression= Expression.Read(ref reader);
        
        var arguments=reader.ReadArray<Expressions.Expression>();
        return Expressions.Expression.Invoke(expression,arguments!);
    }
    public override void Deserialize(ref Reader reader,scoped ref T? value){
        if(reader.TryReadNil()) return;
        value=Read(ref reader);
    }

}
