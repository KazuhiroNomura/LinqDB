using MessagePack;
using MessagePack.Formatters;
using System.Diagnostics;
using Expressions=System.Linq.Expressions;
namespace LinqDB.Serializers.MessagePack.Formatters;
using Writer=MessagePackWriter;
using Reader=MessagePackReader;
using T=Expressions.ConditionalExpression;
public class Conditional:IMessagePackFormatter<T> {
    public static readonly Conditional Instance=new();
    private const int ArrayHeader=3;
    internal static void InternalSerialize(ref Writer writer,T? value,MessagePackSerializerOptions Resolver){
        Debug.Assert(value!=null,nameof(value)+" != null");
        writer.WriteArrayHeader(ArrayHeader);
        Expression.Instance.Serialize(ref writer,value.Test,Resolver);
        Expression.Instance.Serialize(ref writer,value.IfTrue,Resolver);
        Expression.Instance.Serialize(ref writer,value.IfFalse,Resolver);
    }
    internal static T InternalDeserialize(ref Reader reader,MessagePackSerializerOptions Resolver){
        var count=reader.ReadArrayHeader();
        Debug.Assert(count==ArrayHeader);
        var test   = Expression.Instance.Deserialize(ref reader,Resolver);
        var ifTrue = Expression.Instance.Deserialize(ref reader,Resolver);
        var ifFalse= Expression.Instance.Deserialize(ref reader,Resolver);
        return Expressions.Expression.Condition(
            test,
            ifTrue,
            ifFalse
        );
    }
    public void Serialize(ref Writer writer,T? value,MessagePackSerializerOptions Resolver){
        InternalSerialize(ref writer,value,Resolver);
    }
    public T Deserialize(ref Reader reader,MessagePackSerializerOptions Resolver){
        return InternalDeserialize(ref reader,Resolver);
    }
}
