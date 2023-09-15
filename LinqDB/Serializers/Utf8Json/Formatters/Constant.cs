using Utf8Json;
using Expressions = System.Linq.Expressions;
namespace LinqDB.Serializers.Utf8Json.Formatters;
using Writer = JsonWriter;
using Reader = JsonReader;
using T = Expressions.ConstantExpression;
using static Extension;
public class Constant:IJsonFormatter<T> {
    public static readonly Constant Instance=new();
    internal static void InternalSerialize(ref Writer writer,T value,IJsonFormatterResolver Resolver){
        writer.WriteType(value.Type);
        writer.WriteValueSeparator();
        Object.Instance.Serialize(ref writer,value.Value,Resolver);
    }
    public void Serialize(ref Writer writer,T value,IJsonFormatterResolver Resolver){
        writer.WriteBeginArray();
        InternalSerialize(ref writer,value,Resolver);
        writer.WriteEndArray();
    }
    internal static T InternalDeserialize(ref Reader reader,IJsonFormatterResolver Resolver){
        var type=reader.ReadType();
        reader.ReadIsValueSeparatorWithVerify();
        var value=Object.Instance.Deserialize(ref reader,Resolver);
        return Expressions.Expression.Constant(value,type);
    }
    public T Deserialize(ref Reader reader,IJsonFormatterResolver Resolver){
        reader.ReadIsBeginArrayWithVerify();
        var value=InternalDeserialize(ref reader,Resolver);
        reader.ReadIsEndArrayWithVerify();
        return value;
    }
}
