using System.Diagnostics;
using MessagePack;
using MessagePack.Formatters;
using Expressions = System.Linq.Expressions;
namespace LinqDB.Serializers.MessagePack.Formatters;
using Reflection;
using O=MessagePackSerializerOptions;
using Writer = MessagePackWriter;
using Reader = MessagePackReader;
using T = Expressions.MemberExpression;
public class MemberAccess:IMessagePackFormatter<T> {
    public static readonly MemberAccess Instance=new();
    private static void PrivateWrite(ref Writer writer,T? value,O Resolver){
        Member.Write(ref writer,value!.Member,Resolver);
        
        Expression.WriteNullable(ref writer,value.Expression,Resolver);
    }
    internal static void Write(ref Writer writer,T value,O Resolver){
        writer.WriteArrayHeader(3);
        writer.WriteNodeType(Expressions.ExpressionType.MemberAccess);
        
        PrivateWrite(ref writer,value,Resolver);
        
    }
    public void Serialize(ref Writer writer,T? value,O Resolver){
        if(writer.TryWriteNil(value)) return;
        writer.WriteArrayHeader(2);
        PrivateWrite(ref writer,value,Resolver);
        
    }
    internal static T Read(ref Reader reader,O Resolver){
        var member=Member.Read(ref reader,Resolver);
        
        var expression= Expression.ReadNullable(ref reader,Resolver);
        return Expressions.Expression.MakeMemberAccess(expression,member);
    }
    public T Deserialize(ref Reader reader,O Resolver){
        if(reader.TryReadNil()) return null!;
        var count=reader.ReadArrayHeader();
        Debug.Assert(count==2);
        return Read(ref reader,Resolver);
        
    }
}
