using MessagePack;
using MessagePack.Formatters;
using Expressions = System.Linq.Expressions;
namespace LinqDB.Serializers.MessagePack.Formatters;
using Reflection;
using O=MessagePackSerializerOptions;
using Writer = MessagePackWriter;
using Reader = MessagePackReader;
using T = Expressions.MethodCallExpression;
public class MethodCall:IMessagePackFormatter<T> {
    public static readonly MethodCall Instance=new();
    internal static void Write(ref Writer writer,T value,O Resolver){
        var method=value!.Method;
        if(method.IsStatic){
            writer.WriteArrayHeader(3);
            writer.WriteNodeType(Expressions.ExpressionType.Call);
            Method.WriteNullable(ref writer,method,Resolver);
        } else{
            writer.WriteArrayHeader(4);
            writer.WriteNodeType(Expressions.ExpressionType.Call);
            Method.WriteNullable(ref writer,method,Resolver);
            Expression.Write(ref writer,value.Object!,Resolver);
        }
        writer.WriteCollection(value.Arguments,Resolver);
    }



    public void Serialize(ref Writer writer,T? value,O Resolver){
        if(writer.TryWriteNil(value)) return;
        var method=value!.Method;
        if(method.IsStatic){
            writer.WriteArrayHeader(2);
            Method.WriteNullable(ref writer,method,Resolver);
        } else{
            writer.WriteArrayHeader(3);
            Method.WriteNullable(ref writer,method,Resolver);
            Expression.Write(ref writer,value.Object!,Resolver);
        }
        writer.WriteCollection(value.Arguments,Resolver);
    }
    internal static T Read(ref Reader reader,O Resolver){
        var method=Method.Read(ref reader,Resolver);
        
        if(method.IsStatic){
            var arguments=reader.ReadArray<Expressions.Expression>(Resolver);
            return Expressions.Expression.Call(
                method,
                arguments
            );
        } else{
            var instance= Expression.Read(ref reader,Resolver);
            
            var arguments=reader.ReadArray<Expressions.Expression>(Resolver);
            return Expressions.Expression.Call(
                instance,
                method,
                arguments
            );
        }
    }
    public T Deserialize(ref Reader reader,O Resolver){
        if(reader.TryReadNil()) return null!;
        var count=reader.ReadArrayHeader();
        return Read(ref reader,Resolver);
    }
}
