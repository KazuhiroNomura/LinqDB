using Expressions=System.Linq.Expressions;
using MessagePack;
using MessagePack.Formatters;
using Utf8Json;
namespace LinqDB.Serializers.Utf8Json.Formatters;
using Writer=JsonWriter;
using Reader=JsonReader;
using static Common;
using T=Expressions.TryExpression;
public class Try:IJsonFormatter<T> {
    public static readonly Try Instance=new();
    internal static void InternalSerialize(ref Writer writer,T value,IJsonFormatterResolver Resolver){
        Expression.Instance.Serialize(ref writer,value.Body,Resolver);
        writer.WriteValueSeparator();
        Expression.Instance.Serialize(ref writer,value.Finally,Resolver);
        writer.WriteValueSeparator();
        SerializeReadOnlyCollection(ref writer,value.Handlers,Resolver);
    }
    public void Serialize(ref Writer writer,T value,IJsonFormatterResolver Resolver) {
        writer.WriteBeginArray();
        InternalSerialize(ref writer,value,Resolver);
        writer.WriteEndArray();
    }
    internal static Expressions.Expression InternalDeserialize(ref Reader reader,IJsonFormatterResolver Resolver,
        out Expressions.Expression @finally,out Expressions.CatchBlock[] handlers){
        var body=Expression.Instance.Deserialize(ref reader,Resolver);
        reader.ReadIsValueSeparatorWithVerify();
        @finally=Expression.Instance.Deserialize(ref reader,Resolver);
        reader.ReadIsValueSeparatorWithVerify();
        handlers=DeserializeArray<Expressions.CatchBlock>(ref reader,Resolver);
        return body;
    }
    public T Deserialize(ref Reader reader,IJsonFormatterResolver Resolver) {
        reader.ReadIsBeginArrayWithVerify();
        var body=InternalDeserialize(ref reader,Resolver,out var @finally,out var handlers);
        reader.ReadIsEndArrayWithVerify();
        return Expressions.Expression.TryCatchFinally(body,@finally,handlers);
    }
}
