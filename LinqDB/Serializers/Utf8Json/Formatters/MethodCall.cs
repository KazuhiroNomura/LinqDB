using Utf8Json;

using Expressions = System.Linq.Expressions;
namespace LinqDB.Serializers.Utf8Json.Formatters;
using Reflection;
using O=IJsonFormatterResolver;
using Writer = JsonWriter;
using Reader = JsonReader;
using T = Expressions.MethodCallExpression;
public class MethodCall:IJsonFormatter<T> {
    public static readonly MethodCall Instance=new();
    private static void PrivateWrite(ref Writer writer,T value,O Resolver){
        var method=value.Method;
        Method.Write(ref writer,method,Resolver);
        writer.WriteValueSeparator();
        if(!method.IsStatic){
            Expression.Write(ref writer,value.Object!,Resolver);
            writer.WriteValueSeparator();
        }
        writer.WriteCollection(value.Arguments,Resolver);
        writer.WriteEndArray();
    }
    internal static void Write(ref Writer writer,T value,O Resolver){
        writer.WriteBeginArray();
        writer.WriteNodeType(value);
        writer.WriteValueSeparator();
        PrivateWrite(ref writer,value,Resolver);
    }
    public void Serialize(ref Writer writer,T? value,O Resolver){
        if(writer.TryWriteNil(value))return;
        writer.WriteBeginArray();
        PrivateWrite(ref writer,value,Resolver);
    }








    internal static T Read(ref Reader reader,O Resolver){
        var method=Method.Read(ref reader,Resolver);
        reader.ReadIsValueSeparatorWithVerify();
        if(method.IsStatic){
            var arguments=reader.ReadArray<Expressions.Expression>(Resolver);
            return Expressions.Expression.Call(
                method,
                arguments
            );
        } else{
            var instance=Expression.Read(ref reader,Resolver);
            reader.ReadIsValueSeparatorWithVerify();
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
        reader.ReadIsBeginArrayWithVerify();
        var value=Read(ref reader,Resolver);
        reader.ReadIsEndArrayWithVerify();
        return value;
    }
}
