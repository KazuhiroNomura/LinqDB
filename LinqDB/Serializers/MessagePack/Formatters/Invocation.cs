using System.Diagnostics;
using MessagePack;
using MessagePack.Formatters;
using Expressions = System.Linq.Expressions;
namespace LinqDB.Serializers.MessagePack.Formatters;
using O=MessagePackSerializerOptions;
using Writer = MessagePackWriter;
using Reader = MessagePackReader;
using T = Expressions.InvocationExpression;
public class Invocation:IMessagePackFormatter<T> {
    public static readonly Invocation Instance=new();
    private static void PrivateWrite(ref Writer writer,T? value,O Resolver){
        Expression.Write(ref writer,value!.Expression,Resolver);
        
        writer.WriteCollection(value.Arguments,Resolver);
    }
    internal static void Write(ref Writer writer,T value,O Resolver){
        writer.WriteArrayHeader(3);
        writer.WriteNodeType(Expressions.ExpressionType.Invoke);
        
        PrivateWrite(ref writer,value,Resolver);
        
    }
    public void Serialize(ref Writer writer,T? value,O Resolver){
        if(writer.TryWriteNil(value)) return;
        writer.WriteArrayHeader(2);
        PrivateWrite(ref writer,value,Resolver);
        
    }
    internal static T Read(ref Reader reader,O Resolver){
        var expression= Expression.Read(ref reader,Resolver);
        
        var arguments=reader.ReadArray<Expressions.Expression>(Resolver);
        return Expressions.Expression.Invoke(expression,arguments);
    }
    public T Deserialize(ref Reader reader,O Resolver){
        if(reader.TryReadNil()) return null!;
        var count=reader.ReadArrayHeader();
        Debug.Assert(count==2);
        return Read(ref reader,Resolver);
    }
}
