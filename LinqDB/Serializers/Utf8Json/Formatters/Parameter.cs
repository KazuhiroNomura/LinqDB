using Expressions = System.Linq.Expressions;
using MessagePack;
using MessagePack.Formatters;
using Utf8Json;
namespace LinqDB.Serializers.Utf8Json.Formatters;
using Writer=JsonWriter;
using Reader=JsonReader;
using T=Expressions.ParameterExpression;
using C=Serializer;
public class Parameter:IJsonFormatter<T> {
    public static readonly Parameter Instance=new();
    public void Serialize(ref Writer writer,T value,IJsonFormatterResolver Resolver) {
        writer.WriteInt32(C.Instance.ListParameter.LastIndexOf(value));
    }
    public T Deserialize(ref Reader reader,IJsonFormatterResolver Resolver) {
        var index=reader.ReadInt32();
        var Parameter= C.Instance.ListParameter[index];
        return Parameter;
    }
}
