using Expressions=System.Linq.Expressions;
using Utf8Json;
namespace LinqDB.Serializers.Utf8Json.Formatters;
using Writer=JsonWriter;
using Reader=JsonReader;
using T=Expressions.SwitchCase;
using static Common;
public class SwitchCase:IJsonFormatter<T> {
    public static readonly SwitchCase Instance=new();
    public void Serialize(ref Writer writer,T value,IJsonFormatterResolver Resolver) {
        writer.WriteBeginArray();
        SerializeReadOnlyCollection(ref writer,value.TestValues,Resolver);
        writer.WriteValueSeparator();
        Expression.Instance.Serialize(ref writer,value.Body,Resolver);
        writer.WriteEndArray();
    }
    public T Deserialize(ref Reader reader,IJsonFormatterResolver Resolver) {
        reader.ReadIsBeginArrayWithVerify();
        var testValues=DeserializeArray<Expressions.Expression>(ref reader,Resolver);
        reader.ReadIsValueSeparatorWithVerify();
        var body= Expression.Instance.Deserialize(ref reader,Resolver);
        reader.ReadIsEndArrayWithVerify();
        return Expressions.Expression.SwitchCase(body,testValues);
    }
}
