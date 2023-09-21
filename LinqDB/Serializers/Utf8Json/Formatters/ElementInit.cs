using Expressions = System.Linq.Expressions;
using Utf8Json;
using LinqDB.Serializers.Utf8Json.Formatters.Reflection;

namespace LinqDB.Serializers.Utf8Json.Formatters;
using Writer = JsonWriter;
using Reader = JsonReader;
using T = Expressions.ElementInit;
using static Extension;
public class ElementInit:IJsonFormatter<T> {
    public static readonly ElementInit Instance=new();
    public void Serialize(ref Writer writer,T? value,IJsonFormatterResolver Resolver){
        if(writer.TryWriteNil(value))return;
        writer.WriteBeginArray();
        Method.WriteNullable(ref writer,value!.AddMethod,Resolver);
        writer.WriteValueSeparator();
        writer.WriteCollection(value.Arguments,Resolver);
        writer.WriteEndArray();
    }
    public T Deserialize(ref Reader reader,IJsonFormatterResolver Resolver){
        if(reader.TryReadNil())return null!;
        reader.ReadIsBeginArrayWithVerify();
        var addMethod= Method.Read(ref reader,Resolver);
        reader.ReadIsValueSeparatorWithVerify();
        var arguments=reader.ReadArray<Expressions.Expression>(Resolver);
        reader.ReadIsEndArrayWithVerify();
        return Expressions.Expression.ElementInit(addMethod,arguments);
    }
}
