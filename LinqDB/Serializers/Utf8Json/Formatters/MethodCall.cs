using Expressions=System.Linq.Expressions;
using MessagePack;
using MessagePack.Formatters;
using Utf8Json;
using System.Reflection;

namespace LinqDB.Serializers.Utf8Json.Formatters;
using Writer=JsonWriter;
using Reader=JsonReader;
using T= Expressions.MethodCallExpression;
using static Utf8JsonCustomSerializer;
using static Common;
public class MethodCall:IJsonFormatter<T> {
    public static readonly MethodCall Instance=new();
    public void Serialize(ref Writer writer,T? value,IJsonFormatterResolver Resolver){
        if(value is null){
            writer.WriteNull();
            return;
        }
        writer.WriteBeginArray();
        var method=value.Method;
        Method.Instance.Serialize(ref writer,method,Resolver);
        writer.WriteValueSeparator();
        if(!method.IsStatic){
            Expression.Instance.Serialize(ref writer,value.Object!,Resolver);
            writer.WriteValueSeparator();
        }
        SerializeReadOnlyCollection(ref writer,value.Arguments,Resolver);
        writer.WriteEndArray();
    }
    public T Deserialize(ref Reader reader,IJsonFormatterResolver Resolver){
        if(reader.ReadIsNull()) return null!;
        reader.ReadIsBeginArrayWithVerify();
        var method= Method.Instance.Deserialize(ref reader,Resolver);
        reader.ReadIsValueSeparatorWithVerify();
        if(method.IsStatic){
            var arguments=DeserializeArray<Expressions.Expression>(ref reader,Resolver);
            reader.ReadIsEndArrayWithVerify();
            return Expressions.Expression.Call(
                method,
                arguments
            );
        } else{
            var instance= this.Deserialize(ref reader,Resolver);
            reader.ReadIsValueSeparatorWithVerify();
            var arguments=DeserializeArray<Expressions.Expression>(ref reader,Resolver);
            reader.ReadIsEndArrayWithVerify();
            return Expressions.Expression.Call(
                instance,
                method,
                arguments
            );
        }
    }
}
