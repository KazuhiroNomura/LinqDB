
using MemoryPack;
using System.Buffers;
using Expressions = System.Linq.Expressions;
namespace LinqDB.Serializers.MemoryPack.Formatters;

using Reader = MemoryPackReader;
using T = Expressions.MemberInitExpression;
public class MemberInit:MemoryPackFormatter<T> {
    public static readonly MemberInit Instance=new();
    private static void PrivateWrite<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,T value)where TBufferWriter:IBufferWriter<byte>{
        New.WriteNodeTypeなし(ref writer,value.NewExpression);
        
        writer.WriteCollection(value.Bindings);
    }
    internal static void Write<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,T value)where TBufferWriter:IBufferWriter<byte>{

        writer.WriteNodeType(Expressions.ExpressionType.MemberInit);
        PrivateWrite(ref writer,value);
    }
    public override void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,scoped ref T? value){
        if(writer.TryWriteNil(value)) return;
        
        PrivateWrite(ref writer,value);
        
    }
    internal static T Read(ref Reader reader){
        var @new= New.Read(ref reader);
        
        var bindings=reader.ReadArray<Expressions.MemberBinding>();
        return Expressions.Expression.MemberInit(@new,bindings!);
    }
    public override void Deserialize(ref Reader reader,scoped ref T? value){
        if(reader.TryReadNil()) return;
        
        
        
        
        value=Read(ref reader);
    }
}
