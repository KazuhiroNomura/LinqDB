using System.Diagnostics;
using Expressions=System.Linq.Expressions;
using MessagePack;
using MessagePack.Formatters;
namespace LinqDB.Serializers.MessagePack.Formatters;
using Writer=MessagePackWriter;
using Reader=MessagePackReader;
using T=Expressions.InvocationExpression;
using C=MessagePackCustomSerializer;
using static Common;
public class Invocation:IMessagePackFormatter<Expressions.InvocationExpression>{
    public static readonly Invocation Instance=new();
    public void Serialize(ref MessagePackWriter writer,Expressions.InvocationExpression? value,MessagePackSerializerOptions Resolver){
        Debug.Assert(value!=null,nameof(value)+" != null");
        Expression.Instance.Serialize(ref writer,value.Expression,Resolver);
        SerializeReadOnlyCollection(ref writer,value.Arguments,Resolver);
    }
    public Expressions.InvocationExpression Deserialize(ref MessagePackReader reader,MessagePackSerializerOptions Resolver){
        var expression= Expression.Instance.Deserialize(ref reader,Resolver);
        var arguments=DeserializeArray<Expressions.Expression>(ref reader,Resolver);
        return Expressions.Expression.Invoke(expression,arguments);
    }
}
