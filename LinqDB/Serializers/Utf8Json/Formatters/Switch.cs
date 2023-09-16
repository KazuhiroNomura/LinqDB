using Expressions=System.Linq.Expressions;
using Utf8Json;
namespace LinqDB.Serializers.Utf8Json.Formatters;
using Writer=JsonWriter;
using Reader=JsonReader;
using static Extension;
using T=Expressions.SwitchExpression;
public class Switch:IJsonFormatter<T> {
    public static readonly Switch Instance=new();
    private static void PrivateSerialize(ref Writer writer,T value,IJsonFormatterResolver Resolver){
        writer.WriteType(value.Type);
        writer.WriteValueSeparator();
        Expression.Write(ref writer,value.SwitchValue,Resolver);
        writer.WriteValueSeparator();
        Method.Instance.Serialize(ref writer,value.Comparison,Resolver);
        writer.WriteValueSeparator();
        writer.SerializeReadOnlyCollection(value.Cases,Resolver);
        writer.WriteValueSeparator();
        Expression.Write(ref writer,value.DefaultBody,Resolver);
    }
    internal static void Write(ref Writer writer,T value,IJsonFormatterResolver Resolver){
        writer.WriteNodeType(value);
        writer.WriteValueSeparator();
        PrivateSerialize(ref writer,value,Resolver);
    }
    public void Serialize(ref Writer writer,T value,IJsonFormatterResolver Resolver) {
        writer.WriteBeginArray();
        PrivateSerialize(ref writer,value,Resolver);
        writer.WriteEndArray();
    }
    internal static T Read(ref Reader reader,IJsonFormatterResolver Resolver){
        var type=reader.ReadType();
        reader.ReadIsValueSeparatorWithVerify();
        var switchValue=Expression.Read(ref reader,Resolver);
        reader.ReadIsValueSeparatorWithVerify();
        var comparison=Method.Instance.Deserialize(ref reader,Resolver);
        reader.ReadIsValueSeparatorWithVerify();
        var cases=reader.ReadArray<Expressions.SwitchCase>(Resolver);
        reader.ReadIsValueSeparatorWithVerify();
        var defaultBody=Expression.Read(ref reader,Resolver);
        var value=Expressions.Expression.Switch(type,switchValue,defaultBody,comparison,cases);
        return value;
    }
    public T Deserialize(ref Reader reader,IJsonFormatterResolver Resolver) {
        reader.ReadIsBeginArrayWithVerify();
        var value=Read(ref reader,Resolver);
        reader.ReadIsEndArrayWithVerify();
        return value;
    }
}
