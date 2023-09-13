using System.Diagnostics;
using Expressions=System.Linq.Expressions;
using MessagePack;
using MessagePack.Formatters;
namespace LinqDB.Serializers.MessagePack.Formatters;
using Writer=MessagePackWriter;
using Reader=MessagePackReader;
using T=Expressions.InvocationExpression;
using static Extension;
public class Invocation:IMessagePackFormatter<T> {
    public static readonly Invocation Instance=new();
    private const int ArrayHeader=2;
    private const int InternalArrayHeader=ArrayHeader+1;
    private static void PrivateSerialize(ref Writer writer,T? value,MessagePackSerializerOptions Resolver){
        Expression.Instance.Serialize(ref writer,value!.Expression,Resolver);
        writer.SerializeReadOnlyCollection(value.Arguments,Resolver);
    }
    internal static void InternalSerialize(ref Writer writer,T value,MessagePackSerializerOptions Resolver){
        writer.WriteArrayHeader(InternalArrayHeader);
        writer.WriteNodeType(Expressions.ExpressionType.Invoke);
        PrivateSerialize(ref writer,value,Resolver);
    }
    public void Serialize(ref Writer writer,T? value,MessagePackSerializerOptions Resolver){
        if(writer.TryWriteNil(value)) return;
        writer.WriteArrayHeader(ArrayHeader);
        PrivateSerialize(ref writer,value,Resolver);
    }
    internal static T InternalDeserialize(ref Reader reader,MessagePackSerializerOptions Resolver){
        var expression= Expression.Instance.Deserialize(ref reader,Resolver);
        var arguments=reader.ReadArray<Expressions.Expression>(Resolver);
        return Expressions.Expression.Invoke(expression,arguments);
    }
    public T Deserialize(ref Reader reader,MessagePackSerializerOptions Resolver){
        //if(reader.TryReadNil()) return null!;
        var count=reader.ReadArrayHeader();
        Debug.Assert(count==ArrayHeader);
        return InternalDeserialize(ref reader,Resolver);
    }
}
