using System.Diagnostics;
using MessagePack;
using MessagePack.Formatters;
using Expressions=System.Linq.Expressions;
namespace LinqDB.Serializers.MessagePack.Formatters;
using O=MessagePackSerializerOptions;
using Writer=MessagePackWriter;
using Reader=MessagePackReader;
using T=Expressions.ConditionalExpression;
public class Conditional:IMessagePackFormatter<T> {
    public static readonly Conditional Instance=new();
    private static void PrivateWrite(ref Writer writer,T value,O Resolver){
        Expression.Write(ref writer,value.Test,Resolver);
        
        Expression.Write(ref writer,value.IfTrue,Resolver);
        
        Expression.Write(ref writer,value.IfFalse,Resolver);
        
        writer.WriteType(value.Type);
    }
    internal static void Write(ref Writer writer,T value,O Resolver){
        writer.WriteArrayHeader(5);
        writer.WriteNodeType(Expressions.ExpressionType.Conditional);
        
        PrivateWrite(ref writer,value,Resolver);
        
    }
    public void Serialize(ref Writer writer,T value,O Resolver){
        if(writer.TryWriteNil(value)) return;
        writer.WriteArrayHeader(4);

        PrivateWrite(ref writer,value,Resolver);

    }
    internal static T Read(ref Reader reader,O Resolver){
        var test   = Expression.Read(ref reader,Resolver);

        var ifTrue = Expression.Read(ref reader,Resolver);

        var ifFalse= Expression.Read(ref reader,Resolver);

        var type=reader.ReadType();
        return Expressions.Expression.Condition(test,ifTrue,ifFalse,type);
    }
    public T Deserialize(ref Reader reader,O Resolver){
        if(reader.TryReadNil()) return null!;
        var count=reader.ReadArrayHeader();
        Debug.Assert(count==4);
        return Read(ref reader,Resolver);
        
    }
}
