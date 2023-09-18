using System.Diagnostics;
using MessagePack;
using MessagePack.Formatters;
using Expressions=System.Linq.Expressions;
namespace LinqDB.Serializers.MessagePack.Formatters;
using Writer=MessagePackWriter;
using Reader=MessagePackReader;
using T=Expressions.MethodCallExpression;
using static Extension;
public class MethodCall:IMessagePackFormatter<T> {
    public static readonly MethodCall Instance=new();
    private const int ArrayHeader0=2;
    private const int ArrayHeader1=3;
    private const int InternalArrayHeader0=ArrayHeader0+1;
    private const int InternalArrayHeader1=ArrayHeader1+1;
    internal static void Write(ref Writer writer,T value,MessagePackSerializerOptions Resolver){
        var method=value!.Method;
        if(method.IsStatic){
            writer.WriteArrayHeader(InternalArrayHeader0);
            writer.WriteNodeType(Expressions.ExpressionType.Call);
            Method.WriteNullable(ref writer,method,Resolver);
        } else{
            writer.WriteArrayHeader(InternalArrayHeader1);
            writer.WriteNodeType(Expressions.ExpressionType.Call);
            Method.WriteNullable(ref writer,method,Resolver);
            Expression.Write(ref writer,value.Object!,Resolver);
        }
        writer.WriteCollection(value.Arguments,Resolver);
    }
    public void Serialize(ref Writer writer,T? value,MessagePackSerializerOptions Resolver){
        if(writer.TryWriteNil(value)) return;
        var method=value!.Method;
        if(method.IsStatic){
            writer.WriteArrayHeader(ArrayHeader0);
            Method.WriteNullable(ref writer,method,Resolver);
        } else{
            writer.WriteArrayHeader(ArrayHeader1);
            Method.WriteNullable(ref writer,method,Resolver);
            Expression.Write(ref writer,value.Object!,Resolver);
        }
        writer.WriteCollection(value.Arguments,Resolver);
    }
    internal static T Read(ref Reader reader,MessagePackSerializerOptions Resolver){
        var method= Method.Instance.Deserialize(ref reader,Resolver);
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
    public T Deserialize(ref Reader reader,MessagePackSerializerOptions Resolver){
        if(reader.TryReadNil()) return null!;
        var count=reader.ReadArrayHeader();
        var method= Method.Instance.Deserialize(ref reader,Resolver);
        if(method.IsStatic){
            Debug.Assert(count==ArrayHeader0);
            var arguments=reader.ReadArray<Expressions.Expression>(Resolver);
            return Expressions.Expression.Call(
                method,
                arguments
            );
        } else{
            Debug.Assert(count==ArrayHeader1);
            var instance= Expression.Read(ref reader,Resolver);
            var arguments=reader.ReadArray<Expressions.Expression>(Resolver);
            return Expressions.Expression.Call(
                instance,
                method,
                arguments
            );
        }
    }
}
