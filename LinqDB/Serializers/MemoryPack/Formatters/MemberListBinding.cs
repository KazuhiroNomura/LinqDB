using MemoryPack;
using System.Buffers;
using Expressions = System.Linq.Expressions;
namespace LinqDB.Serializers.MemoryPack.Formatters;


using Reader = MemoryPackReader;
using T = Expressions.MemberListBinding;
using Reflection;
public class MemberListBinding:MemoryPackFormatter<T> {
    public static readonly MemberListBinding Instance=new();
    internal static void Write<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,T value)where TBufferWriter:IBufferWriter<byte>{


        Member.Write(ref writer,value.Member);
        
        writer.WriteCollection(value.Initializers);
    }
    public override void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,scoped ref T? value){
        if(writer.TryWriteNil(value)) return;

        Write(ref writer,value);
    }
    
    internal static T Read(ref Reader reader){
        var member= Member.Read(ref reader);
        
        return Expressions.Expression.ListBind(member,reader.ReadArray<Expressions.ElementInit>()!);
    }
    public override void Deserialize(ref Reader reader,scoped ref T? value){
        if(reader.TryReadNil()) return;
        value=Read(ref reader);
    }
}
