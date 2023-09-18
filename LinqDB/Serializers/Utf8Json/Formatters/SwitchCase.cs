using Utf8Json;

using Expressions=System.Linq.Expressions;
namespace LinqDB.Serializers.Utf8Json.Formatters;
using Writer=JsonWriter;
using Reader=JsonReader;
using T=Expressions.SwitchCase;
public class SwitchCase:IJsonFormatter<T> {
    public static readonly SwitchCase Instance=new();
    public void Serialize(ref Writer writer,T? value,IJsonFormatterResolver Resolver) {
        if(writer.TryWriteNil(value))return;
        writer.WriteBeginArray();
        writer.WriteCollection(value!.TestValues,Resolver);
        writer.WriteValueSeparator();
        Expression.Write(ref writer,value.Body,Resolver);
        writer.WriteEndArray();
    }
    public T Deserialize(ref Reader reader,IJsonFormatterResolver Resolver) {
        if(reader.TryReadNil())return null!;
        reader.ReadIsBeginArrayWithVerify();
        var testValues=reader.ReadArray<Expressions.Expression>(Resolver);
        reader.ReadIsValueSeparatorWithVerify();
        var body= Expression.Read(ref reader,Resolver);
        reader.ReadIsEndArrayWithVerify();
        return Expressions.Expression.SwitchCase(body,testValues);
    }
}
