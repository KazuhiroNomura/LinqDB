using System.Diagnostics;
using LinqDB.Serializers.MessagePack.Formatters.Reflection;
using MessagePack;
using MessagePack.Formatters;
using Expressions = System.Linq.Expressions;
namespace LinqDB.Serializers.MessagePack.Formatters;
using Writer = MessagePackWriter;
using Reader = MessagePackReader;
using T = Expressions.MemberExpression;
public class MemberAccess:IMessagePackFormatter<T> {
    public static readonly MemberAccess Instance=new();
    private const int ArrayHeader=2;
    private const int InternalArrayHeader=ArrayHeader+1;
    private static void PrivateWrite(ref Writer writer,T? value,MessagePackSerializerOptions Resolver){
        Member.Instance.Serialize(ref writer,value!.Member,Resolver);
        Expression.WriteNullable(ref writer,value.Expression,Resolver);
    }
    internal static void Write(ref Writer writer,T value,MessagePackSerializerOptions Resolver){
        writer.WriteArrayHeader(InternalArrayHeader);
        writer.WriteNodeType(Expressions.ExpressionType.MemberAccess);
        PrivateWrite(ref writer,value,Resolver);
    }
    public void Serialize(ref Writer writer,T? value,MessagePackSerializerOptions Resolver){
        if(writer.TryWriteNil(value)) return;
        writer.WriteArrayHeader(ArrayHeader);
        PrivateWrite(ref writer,value,Resolver);
    }
    internal static T Read(ref Reader reader,MessagePackSerializerOptions Resolver){
        var member=Member.Instance.Deserialize(ref reader,Resolver);
        var expression= Expression.ReadNullable(ref reader,Resolver);
        return Expressions.Expression.MakeMemberAccess(expression,member);
    }
    public T Deserialize(ref Reader reader,MessagePackSerializerOptions Resolver){
        if(reader.TryReadNil()) return null!;
        var count=reader.ReadArrayHeader();
        Debug.Assert(count==ArrayHeader);
        return Read(ref reader,Resolver);
    }
}
