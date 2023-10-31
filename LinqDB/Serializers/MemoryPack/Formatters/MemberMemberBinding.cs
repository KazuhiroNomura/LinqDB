using MemoryPack;
using System.Buffers;
using Expressions = System.Linq.Expressions;
namespace LinqDB.Serializers.MemoryPack.Formatters;


using Reader = MemoryPackReader;
using T = Expressions.MemberMemberBinding;
using Reflection;
public class MemberMemberBinding:MemoryPackFormatter<T> {
    public static readonly MemberMemberBinding Instance=new();
    internal static void Write<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,T value)where TBufferWriter:IBufferWriter<byte>{


        Member.Write(ref writer,value.Member);
        writer.WriteCollection(value.Bindings);
    }
    public override void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,scoped ref T? value){
        if(writer.TryWriteNil(value)) return;
        Write(ref writer,value);
    }
    
    
    internal static T Read(ref Reader reader){
        var member= Member.Read(ref reader);

        return Expressions.Expression.MemberBind(member,reader.ReadArray<Expressions.MemberBinding>()!);
    }
    public override void Deserialize(ref Reader reader,scoped ref T? value){
        if(reader.TryReadNil()) return;
        value=Read(ref reader);
    }
}
