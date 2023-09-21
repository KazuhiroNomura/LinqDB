using MemoryPack;
using System.Buffers;
using System.Diagnostics.CodeAnalysis;
using Expressions = System.Linq.Expressions;
namespace LinqDB.Serializers.MemoryPack.Formatters;
using Reader = MemoryPackReader;
using T = Expressions.TryExpression;

public class Try:MemoryPackFormatter<T> {
    public static readonly Try Instance=new();
    
    
    
    
    
    
    
    
    
    
    
    private static void PrivateWrite<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,T? value) where TBufferWriter:IBufferWriter<byte>{
        Expression.Write(ref writer,value!.Body);
        
        Expression.WriteNullable(ref writer,value.Finally);
        
        if(value.Finally is not null){
            writer.WriteCollection(value.Handlers);
        } else{
            Expression.WriteNullable(ref writer,value.Fault);
            if(value.Fault is null){
                
                writer.WriteCollection(value.Handlers);
            }
        }
    }
    internal static void Write<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,T? value) where TBufferWriter:IBufferWriter<byte>{
        
        writer.WriteNodeType(Expressions.ExpressionType.Try);
        PrivateWrite(ref writer,value);
    }
    public override void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,scoped ref T? value){
        if(writer.TryWriteNil(value)) return;
        
        PrivateWrite(ref writer,value);
        
    }
    
    
    internal static T Read(ref Reader reader){
        T value;
        var body= Expression.Read(ref reader);
        
        var @finally= Expression.ReadNullable(ref reader);
        
        if(@finally is not null){
            var handlers=reader.ReadArray<Expressions.CatchBlock>()!;
            if(handlers.Length>0) {
                value=Expressions.Expression.TryCatchFinally(body,@finally,handlers!);
            } else {
                value=Expressions.Expression.TryFinally(body,@finally);
            }
        } else{
            var fault= Expression.ReadNullable(ref reader);
            if(fault is not null){
                value=Expressions.Expression.TryFault(body,fault);
            } else{
                var handlers=reader.ReadArray<Expressions.CatchBlock>()!;
                value=Expressions.Expression.TryCatch(body,handlers!);
            }
        }
        return value;
    }
    [SuppressMessage("ReSharper","ConvertIfStatementToConditionalTernaryExpression")]
    public override void Deserialize(ref Reader reader,scoped ref T? value){
        if(reader.TryReadNil()) return;
        
        value=Read(ref reader);
        
        
    }
}
