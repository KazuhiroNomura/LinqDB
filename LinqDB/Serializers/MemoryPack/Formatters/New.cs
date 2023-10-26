using MemoryPack;
using System.Buffers;
using Expressions = System.Linq.Expressions;
namespace LinqDB.Serializers.MemoryPack.Formatters;
using Reflection;


using Reader = MemoryPackReader;
using T = Expressions.NewExpression;
public class New:MemoryPackFormatter<T> {
    public static readonly New Instance=new();
    internal static void Write<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,T value) where TBufferWriter:IBufferWriter<byte>{
        writer.WriteNodeType(Expressions.ExpressionType.New);
        WriteNew(ref writer,value);
    }
    
    
    
    
    
    
    
    
    internal static void WriteNew<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,T value) where TBufferWriter:IBufferWriter<byte>{
        if(writer.TryWriteNil(value.Constructor)){
            writer.WriteType(value.Type);
        } else{
            Constructor.Write(ref writer,value.Constructor);
            writer.WriteCollection(value.Arguments);
        }
    }
    
    
    
    
    
    public override void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,scoped ref T? value){
        if(writer.TryWriteNil(value)) return;
        WriteNew(ref writer,value);
    }
    
    
    internal static T Read(ref Reader reader){
        if(reader.TryReadNil()){
            var type=reader.ReadType();
            return Expressions.Expression.New(type);
        } else{
            
            var constructor= Constructor.Read(ref reader);
            var arguments=reader.ReadArray<Expressions.Expression>();
            return Expressions.Expression.New(
                constructor,
                arguments!
            );
        }
    }
    internal static T ReadNew(ref Reader reader)=>Read(ref reader);














    public override void Deserialize(ref Reader reader,scoped ref T? value){
        if(reader.TryReadNil()) return;
        value=Read(ref reader);

    }
}
