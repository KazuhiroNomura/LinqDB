using MemoryPack;
using System.Buffers;
using Expressions = System.Linq.Expressions;
namespace LinqDB.Serializers.MemoryPack.Formatters;


using Reader = MemoryPackReader;
using T = Expressions.MemberAssignment;
using Reflection;
public class MemberAssignment:MemoryPackFormatter<T> {
    public static readonly MemberAssignment Instance=new();
    internal static void Write<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,T value)where TBufferWriter:IBufferWriter<byte>{


        Member.Write(ref writer,value.Member);
        
        Expression.Write(ref writer,value.Expression);
    }
    public override void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,scoped ref T? value){
        if(writer.TryWriteNil(value)) return;
        Write(ref writer,value);
    }
    
    
    internal static T Read(ref Reader reader){        
        var member= Member.Read(ref reader);

        return Expressions.Expression.Bind(member,Expression.Read(ref reader));
    }
    public override void Deserialize(ref Reader reader,scoped ref T? value){
        if(reader.TryReadNil()) return;
        
        value=Read(ref reader);
    }
}
