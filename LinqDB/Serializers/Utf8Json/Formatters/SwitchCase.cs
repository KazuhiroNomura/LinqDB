using Expressions=System.Linq.Expressions;
using Utf8Json;
namespace LinqDB.Serializers.Utf8Json.Formatters;
using Writer=JsonWriter;
using Reader=JsonReader;
using T=Expressions.SwitchCase;
using static Extension;
public class SwitchCase:IJsonFormatter<T> {
    public static readonly SwitchCase Instance=new();
    public void Serialize(ref Writer writer,T value,IJsonFormatterResolver Resolver) {
        writer.WriteBeginArray();
        writer.SerializeReadOnlyCollection(value.TestValues,Resolver);
        writer.WriteValueSeparator();
        Expression.Instance.Serialize(ref writer,value.Body,Resolver);
        writer.WriteEndArray();
    }
    public T Deserialize(ref Reader reader,IJsonFormatterResolver Resolver) {
        reader.ReadIsBeginArrayWithVerify();
        var testValues=reader.DeserializeArray<Expressions.Expression>(Resolver);
        reader.ReadIsValueSeparatorWithVerify();
        var body= Expression.Instance.Deserialize(ref reader,Resolver);
        reader.ReadIsEndArrayWithVerify();
        return Expressions.Expression.SwitchCase(body,testValues);
    }
}
