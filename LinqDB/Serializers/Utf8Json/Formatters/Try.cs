using Expressions=System.Linq.Expressions;
using Utf8Json;
namespace LinqDB.Serializers.Utf8Json.Formatters;
using Writer=JsonWriter;
using Reader=JsonReader;
using static Extension;
using T=Expressions.TryExpression;
public class Try:IJsonFormatter<T> {
    public static readonly Try Instance=new();
    internal static void InternalSerialize(ref Writer writer,T value,IJsonFormatterResolver Resolver){
        Expression.Instance.Serialize(ref writer,value.Body,Resolver);
        writer.WriteValueSeparator();
        Expression.Instance.Serialize(ref writer,value.Finally,Resolver);
        writer.WriteValueSeparator();
        writer.SerializeReadOnlyCollection(value.Handlers,Resolver);
    }
    public void Serialize(ref Writer writer,T value,IJsonFormatterResolver Resolver) {
        writer.WriteBeginArray();
        InternalSerialize(ref writer,value,Resolver);
        writer.WriteEndArray();
    }
    internal static T InternalDeserialize(ref Reader reader,IJsonFormatterResolver Resolver){
        var body=Expression.Instance.Deserialize(ref reader,Resolver);
        reader.ReadIsValueSeparatorWithVerify();
        var @finally=Expression.Instance.Deserialize(ref reader,Resolver);
        reader.ReadIsValueSeparatorWithVerify();
        var handlers=reader.DeserializeArray<Expressions.CatchBlock>(Resolver);
        return Expressions.Expression.TryCatchFinally(body,@finally,handlers);
    }
    public T Deserialize(ref Reader reader,IJsonFormatterResolver Resolver) {
        reader.ReadIsBeginArrayWithVerify();
        var value=InternalDeserialize(ref reader,Resolver);
        reader.ReadIsEndArrayWithVerify();
        return value;
    }
}
