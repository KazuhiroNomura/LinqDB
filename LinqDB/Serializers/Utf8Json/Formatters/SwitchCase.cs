using Expressions=System.Linq.Expressions;
using Utf8Json;
namespace LinqDB.Serializers.Utf8Json.Formatters;
using Writer=JsonWriter;
using Reader=JsonReader;
using T=Expressions.SwitchCase;
using static Extension;
public class SwitchCase:IJsonFormatter<T> {
    public static readonly SwitchCase Instance=new();
    public void Serialize(ref Writer writer,T? value,IJsonFormatterResolver Resolver) {
        if(writer.WriteIsNull(value))return;
        writer.WriteBeginArray();
        writer.SerializeReadOnlyCollection(value!.TestValues,Resolver);
        writer.WriteValueSeparator();
        Expression.Write(ref writer,value.Body,Resolver);
        writer.WriteEndArray();
    }
    public T Deserialize(ref Reader reader,IJsonFormatterResolver Resolver) {
        if(reader.ReadIsNull())return null!;
        reader.ReadIsBeginArrayWithVerify();
        var testValues=reader.ReadArray<Expressions.Expression>(Resolver);
        reader.ReadIsValueSeparatorWithVerify();
        var body= Expression.Read(ref reader,Resolver);
        reader.ReadIsEndArrayWithVerify();
        return Expressions.Expression.SwitchCase(body,testValues);
    }
}
