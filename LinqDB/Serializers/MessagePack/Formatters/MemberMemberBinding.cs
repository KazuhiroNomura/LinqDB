using MessagePack;
using MessagePack.Formatters;
using Expressions = System.Linq.Expressions;
namespace LinqDB.Serializers.MessagePack.Formatters;
using O=MessagePackSerializerOptions;
using Writer = MessagePackWriter;
using Reader = MessagePackReader;
using T = Expressions.MemberMemberBinding;
using Reflection;
public class MemberMemberBinding:IMessagePackFormatter<T> {
    public static readonly MemberMemberBinding Instance=new();
    internal static void Write(ref Writer writer,T value,O Resolver){
        writer.WriteArrayHeader(3);
        writer.Write((byte)Expressions.MemberBindingType.MemberBinding);
        Member.Write(ref writer,value.Member,Resolver);
        writer.WriteCollection(value.Bindings,Resolver);
    }
    public void Serialize(ref Writer writer,T value,O Resolver){
        if(writer.TryWriteNil(value)) return;
        writer.WriteArrayHeader(2);
        Member.Write(ref writer,value.Member,Resolver);
        writer.WriteCollection(value.Bindings,Resolver);
    }
    internal static T Read(ref Reader reader,O Resolver){
        var member= Member.Read(ref reader,Resolver);

        return Expressions.Expression.MemberBind(member,reader.ReadArray<Expressions.MemberBinding>(Resolver));
    }
    public T Deserialize(ref Reader reader,O Resolver){
        if(reader.TryReadNil()) return null!;
        var _=reader.ReadArrayHeader();
        return Read(ref reader,Resolver);
    }
}
