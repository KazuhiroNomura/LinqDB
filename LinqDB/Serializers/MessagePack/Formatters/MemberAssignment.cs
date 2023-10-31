using MessagePack;
using MessagePack.Formatters;
using Expressions = System.Linq.Expressions;
namespace LinqDB.Serializers.MessagePack.Formatters;
using O=MessagePackSerializerOptions;
using Writer = MessagePackWriter;
using Reader = MessagePackReader;
using T = Expressions.MemberAssignment;
using Reflection;
public class MemberAssignment:IMessagePackFormatter<T> {
    public static readonly MemberAssignment Instance=new();
    internal static void Write(ref Writer writer,T value,O Resolver){
        writer.WriteArrayHeader(3);
        writer.Write((byte)value.BindingType);
        Member.Write(ref writer,value.Member,Resolver);
        
        Expression.Write(ref writer,value.Expression,Resolver);
    }
    public void Serialize(ref Writer writer,T value,O Resolver){
        if(writer.TryWriteNil(value)) return;
        writer.WriteArrayHeader(2);
        Member.Write(ref writer,value.Member,Resolver);
        Expression.Write(ref writer,value.Expression,Resolver);
    }
    internal static T Read(ref Reader reader,O Resolver){        
        var member= Member.Read(ref reader,Resolver);

        return Expressions.Expression.Bind(member,Expression.Read(ref reader,Resolver));
    }
    public T Deserialize(ref Reader reader,O Resolver){
        if(reader.TryReadNil()) return null!;
        var _=reader.ReadArrayHeader();
        return Read(ref reader,Resolver);
    }
}
