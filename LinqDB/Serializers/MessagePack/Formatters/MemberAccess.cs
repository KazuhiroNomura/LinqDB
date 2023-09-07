using Expressions=System.Linq.Expressions;
using System.Reflection;
using MessagePack;
using MessagePack.Formatters;
namespace LinqDB.Serializers.MessagePack.Formatters;
using Writer=MessagePackWriter;
using Reader=MessagePackReader;
using T=Expressions.MemberExpression;
using C=MessagePackCustomSerializer;

public class MemberAccess:IMessagePackFormatter<Expressions.MemberExpression>{
    public static readonly MemberAccess Instance=new();
    public void Serialize(ref MessagePackWriter writer,Expressions.MemberExpression value,MessagePackSerializerOptions Resolver){
        Member.Instance.Serialize(ref writer,value.Member,Resolver);
        Expression.Instance.Serialize(ref writer,value.Expression,Resolver);
    }
    public Expressions.MemberExpression Deserialize(ref MessagePackReader reader,MessagePackSerializerOptions Resolver){
        var member=Member.Instance.Deserialize(ref reader,Resolver);
        var expression= Expression.Instance.Deserialize(ref reader,Resolver);
        return Expressions.Expression.MakeMemberAccess(expression,member);
    }
}
