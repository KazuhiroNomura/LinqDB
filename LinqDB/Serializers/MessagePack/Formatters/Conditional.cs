using System.Diagnostics;
using MessagePack;
using MessagePack.Formatters;
using Expressions=System.Linq.Expressions;
namespace LinqDB.Serializers.MessagePack.Formatters;
using Writer=MessagePackWriter;
using Reader=MessagePackReader;
using T=Expressions.ConditionalExpression;
public class Conditional:IMessagePackFormatter<T> {
    public static readonly Conditional Instance=new();
    private static void PrivateSerialize(ref Writer writer,T value,MessagePackSerializerOptions Resolver){
        Expression.Instance.Serialize(ref writer,value.Test,Resolver);
        
        Expression.Instance.Serialize(ref writer,value.IfTrue,Resolver);
        
        Expression.Instance.Serialize(ref writer,value.IfFalse,Resolver);
        
        writer.WriteType(value.Type);
    }
    internal static void Write(ref Writer writer,T value,MessagePackSerializerOptions Resolver){
        writer.WriteArrayHeader(5);
        writer.WriteNodeType(Expressions.ExpressionType.Conditional);
        
        PrivateSerialize(ref writer,value,Resolver);
    }
    public void Serialize(ref Writer writer,T value,MessagePackSerializerOptions Resolver){
        writer.WriteArrayHeader(4);

        PrivateSerialize(ref writer,value,Resolver);

    }
    internal static T Read(ref Reader reader,MessagePackSerializerOptions Resolver){
        var test   = Expression.Instance.Deserialize(ref reader,Resolver);

        var ifTrue = Expression.Instance.Deserialize(ref reader,Resolver);

        var ifFalse= Expression.Instance.Deserialize(ref reader,Resolver);

        var type=reader.ReadType();
        return Expressions.Expression.Condition(test,ifTrue,ifFalse,type);
    }
    public T Deserialize(ref Reader reader,MessagePackSerializerOptions Resolver){
        var count=reader.ReadArrayHeader();
        Debug.Assert(count==4);
        return Read(ref reader,Resolver);
        
    }
}
