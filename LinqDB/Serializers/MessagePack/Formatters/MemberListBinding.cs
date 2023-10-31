using MessagePack;
using MessagePack.Formatters;
using Expressions = System.Linq.Expressions;
namespace LinqDB.Serializers.MessagePack.Formatters;
using O=MessagePackSerializerOptions;
using Writer = MessagePackWriter;
using Reader = MessagePackReader;
using T = Expressions.MemberListBinding;
using Reflection;
public class MemberListBinding:IMessagePackFormatter<T> {
    public static readonly MemberListBinding Instance=new();
    internal static void Write(ref Writer writer,T value,O Resolver){
        writer.WriteArrayHeader(3);
        writer.Write((byte)Expressions.MemberBindingType.ListBinding);
        Member.Write(ref writer,value.Member,Resolver);
        
        writer.WriteCollection(value.Initializers,Resolver);
    }
    public void Serialize(ref Writer writer,T value,O Resolver){
        if(writer.TryWriteNil(value)) return;
        writer.WriteArrayHeader(2);
        Member.Write(ref writer,value.Member,Resolver);
        writer.WriteCollection(value.Initializers,Resolver);
    }
    internal static T Read(ref Reader reader,O Resolver){
        var member= Member.Read(ref reader,Resolver);
        
        return Expressions.Expression.ListBind(member,reader.ReadArray<Expressions.ElementInit>(Resolver));
    }
    public T Deserialize(ref Reader reader,O Resolver){
        if(reader.TryReadNil()) return null!;
        var _=reader.ReadArrayHeader();
        return Read(ref reader,Resolver);
    }
}
