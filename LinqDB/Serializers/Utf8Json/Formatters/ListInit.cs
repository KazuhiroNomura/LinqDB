using Expressions=System.Linq.Expressions;
using Utf8Json;
namespace LinqDB.Serializers.Utf8Json.Formatters;
using Writer=JsonWriter;
using Reader=JsonReader;
using static Extension;
using T=Expressions.ListInitExpression;
public class ListInit:IJsonFormatter<T> {
    public static readonly ListInit Instance=new();
    public void Serialize(ref Writer writer,T value,IJsonFormatterResolver Resolver) {
        writer.WriteBeginArray();
        New.Instance.Serialize(ref writer,value.NewExpression,Resolver);
        writer.WriteValueSeparator();
        writer.SerializeReadOnlyCollection(value.Initializers,Resolver);
        writer.WriteEndArray();
    }
    public T Deserialize(ref Reader reader,IJsonFormatterResolver Resolver) {
        reader.ReadIsBeginArrayWithVerify();
        var @new=New.Instance.Deserialize(ref reader,Resolver);
        reader.ReadIsValueSeparatorWithVerify();
        var Initializers=reader.DeserializeArray<Expressions.ElementInit>(Resolver);
        reader.ReadIsEndArrayWithVerify();
        return Expressions.Expression.ListInit(@new,Initializers);
    }
}
