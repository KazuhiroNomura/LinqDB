using Expressions=System.Linq.Expressions;
using Utf8Json;
using System.Diagnostics;

namespace LinqDB.Serializers.Utf8Json.Formatters;
using Writer=JsonWriter;
using Reader=JsonReader;
using T= Expressions.MethodCallExpression;
using static Extension;
public class MethodCall:IJsonFormatter<T> {
    public static readonly MethodCall Instance=new();
    internal static T InternalDeserialize(ref Reader reader,IJsonFormatterResolver Resolver){
        var method=Method.Instance.Deserialize(ref reader,Resolver);
        reader.ReadIsValueSeparatorWithVerify();
        if(method.IsStatic){
            var arguments=reader.ReadArray<Expressions.Expression>(Resolver);
            return Expressions.Expression.Call(
                method,
                arguments
            );
        } else{
            var instance=Expression.Instance.Deserialize(ref reader,Resolver);
            reader.ReadIsValueSeparatorWithVerify();
            var arguments=reader.ReadArray<Expressions.Expression>(Resolver);
            return Expressions.Expression.Call(
                instance,
                method,
                arguments
            );
        }
    }
    public void Serialize(ref Writer writer,T? value,IJsonFormatterResolver Resolver){
        //if(writer.WriteIsNull(value))return;
        Debug.Assert(value!=null,nameof(value)+" != null");
        writer.WriteBeginArray();
        var method=value.Method;
        Method.Instance.Serialize(ref writer,method,Resolver);
        writer.WriteValueSeparator();
        if(!method.IsStatic){
            Expression.Instance.Serialize(ref writer,value.Object!,Resolver);
            writer.WriteValueSeparator();
        }
        writer.SerializeReadOnlyCollection(value.Arguments,Resolver);
        writer.WriteEndArray();
    }
    public T Deserialize(ref Reader reader,IJsonFormatterResolver Resolver){
        //i//f(reader.ReadIsNull()) return null!;
        reader.ReadIsBeginArrayWithVerify();
        var result=InternalDeserialize(ref reader,Resolver);
        reader.ReadIsEndArrayWithVerify();
        return result;
    }
}
