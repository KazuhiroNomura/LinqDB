using Expressions=System.Linq.Expressions;
using MessagePack;
using MessagePack.Formatters;
using Utf8Json;
using System.Reflection;
using System.Diagnostics;

namespace LinqDB.Serializers.Utf8Json.Formatters;
using Writer=JsonWriter;
using Reader=JsonReader;
using T= Expressions.MethodCallExpression;
using static Serializer;
using static Common;
public class MethodCall:IJsonFormatter<T> {
    public static readonly MethodCall Instance=new();
    internal static T InternalDeserialize(ref Reader reader,IJsonFormatterResolver Resolver){
        var method=Method.Instance.Deserialize(ref reader,Resolver);
        reader.ReadIsValueSeparatorWithVerify();
        if(method.IsStatic){
            var arguments=DeserializeArray<Expressions.Expression>(ref reader,Resolver);
            return Expressions.Expression.Call(
                method,
                arguments
            );
        } else{
            var instance=Expression.Instance.Deserialize(ref reader,Resolver);
            reader.ReadIsValueSeparatorWithVerify();
            var arguments=DeserializeArray<Expressions.Expression>(ref reader,Resolver);
            return Expressions.Expression.Call(
                instance,
                method,
                arguments
            );
        }
    }
    public void Serialize(ref Writer writer,T? value,IJsonFormatterResolver Resolver){
        if(writer.WriteIsNull(value))return;
        Debug.Assert(value!=null,nameof(value)+" != null");
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
        var result=InternalDeserialize(ref reader,Resolver);
        reader.ReadIsEndArrayWithVerify();
        return result;
    }
}
