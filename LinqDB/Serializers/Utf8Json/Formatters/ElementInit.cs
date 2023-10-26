using Utf8Json;

using Expressions = System.Linq.Expressions;
namespace LinqDB.Serializers.Utf8Json.Formatters;
using Reflection;
using O=IJsonFormatterResolver;
using Writer = JsonWriter;
using Reader = JsonReader;
using T = Expressions.ElementInit;
public class ElementInit:IJsonFormatter<T> {
    public static readonly ElementInit Instance=new();
    public void Serialize(ref Writer writer,T? value,O Resolver){
        writer.WriteBeginArray();
        Method.WriteNullable(ref writer,value!.AddMethod,Resolver);
        writer.WriteValueSeparator();
        writer.WriteCollection(value.Arguments,Resolver);
        writer.WriteEndArray();
    }
    public T Deserialize(ref Reader reader,O Resolver){
        reader.ReadIsBeginArrayWithVerify();
        var addMethod= Method.Read(ref reader,Resolver);
        reader.ReadIsValueSeparatorWithVerify();
        var arguments=reader.ReadArray<Expressions.Expression>(Resolver);
        reader.ReadIsEndArrayWithVerify();
        return Expressions.Expression.ElementInit(addMethod,arguments);
    }
}
