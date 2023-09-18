using Expressions = System.Linq.Expressions;
using Utf8Json;

namespace LinqDB.Serializers.Utf8Json.Formatters;
using Writer = JsonWriter;
using Reader = JsonReader;
using T = Expressions.MethodCallExpression;
using static Extension;
public class MethodCall:IJsonFormatter<T> {
    public static readonly MethodCall Instance=new();
    private static void PrivateSerialize(ref Writer writer,T value,IJsonFormatterResolver Resolver){
        var method=value.Method;
        Method.Write(ref writer,method,Resolver);
        writer.WriteValueSeparator();
        if(!method.IsStatic){
            Expression.Write(ref writer,value.Object!,Resolver);
            writer.WriteValueSeparator();
        }
        writer.WriteCollection(value.Arguments,Resolver);
    }
    internal static void Write(ref Writer writer,T value,IJsonFormatterResolver Resolver){
        writer.WriteNodeType(value);
        writer.WriteValueSeparator();
        PrivateSerialize(ref writer,value,Resolver);
    }
    public void Serialize(ref Writer writer,T? value,IJsonFormatterResolver Resolver){
        if(writer.TryWriteNil(value))return;
        writer.WriteBeginArray();
        PrivateSerialize(ref writer,value,Resolver);
        writer.WriteEndArray();
    }
    internal static T Read(ref Reader reader,IJsonFormatterResolver Resolver){
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
    public T Deserialize(ref Reader reader,IJsonFormatterResolver Resolver){
        if(reader.TryReadNil()) return null!;
        reader.ReadIsBeginArrayWithVerify();
        var value=Read(ref reader,Resolver);
        reader.ReadIsEndArrayWithVerify();
        return value;
    }
}
