using Expressions=System.Linq.Expressions;
using MessagePack;
using MessagePack.Formatters;
namespace LinqDB.Serializers.MessagePack.Formatters;
using Writer=MessagePackWriter;
using Reader=MessagePackReader;
using T=Expressions.ConditionalExpression;
using C=MessagePackCustomSerializer;
public class Conditional:IMessagePackFormatter<Expressions.ConditionalExpression>{
    public static readonly Conditional Instance=new();
    public void Serialize(ref MessagePackWriter writer,Expressions.ConditionalExpression? value,MessagePackSerializerOptions Resolver){
        if(value is null){
            writer.WriteNil();
            return;
        }
        Expression.Instance.Serialize(ref writer,value.Test,Resolver);
        Expression.Instance.Serialize(ref writer,value.IfTrue,Resolver);
        Expression.Instance.Serialize(ref writer,value.IfFalse,Resolver);
    }
    public Expressions.ConditionalExpression Deserialize(ref MessagePackReader reader,MessagePackSerializerOptions Resolver){
        if(reader.TryReadNil()) return null!;
        var test   = Expression.Instance.Deserialize(ref reader,Resolver);
        var ifTrue = Expression.Instance.Deserialize(ref reader,Resolver);
        var ifFalse= Expression.Instance.Deserialize(ref reader,Resolver);
        return Expressions.Expression.Condition(
            test,
            ifTrue,
            ifFalse
        );
    }
}
