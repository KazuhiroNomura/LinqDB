using Expressions=System.Linq.Expressions;
using Utf8Json;
namespace LinqDB.Serializers.Utf8Json.Formatters;
using Writer=JsonWriter;
using Reader=JsonReader;
using T=Expressions.ElementInit;
using static Extension;
public class ElementInit:IJsonFormatter<T> {
    public static readonly ElementInit Instance=new();
    public void Serialize(ref Writer writer,T value,IJsonFormatterResolver Resolver){
        writer.WriteBeginArray();
        Method.Instance.Serialize(ref writer,value.AddMethod,Resolver);
        writer.WriteValueSeparator();
        writer.SerializeReadOnlyCollection(value.Arguments,Resolver);
        writer.WriteEndArray();
    }
    public T Deserialize(ref Reader reader,IJsonFormatterResolver Resolver){
        reader.ReadIsBeginArrayWithVerify();
        var addMethod= Method.Instance.Deserialize(ref reader,Resolver);
        reader.ReadIsValueSeparatorWithVerify();
        var arguments=reader.DeserializeArray<Expressions.Expression>(Resolver);
        reader.ReadIsEndArrayWithVerify();
        return Expressions.Expression.ElementInit(addMethod,arguments);
    }
}
