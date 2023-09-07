using Expressions=System.Linq.Expressions;
using MessagePack;
using MessagePack.Formatters;
using Utf8Json;
namespace LinqDB.Serializers.Utf8Json.Formatters;
using Writer=JsonWriter;
using Reader=JsonReader;
using static Utf8JsonCustomSerializer;
using static Common;
using T=Expressions.SwitchExpression;
public class Switch:IJsonFormatter<T> {
    public static readonly Switch Instance=new();
    public void Serialize(ref JsonWriter writer,T value,IJsonFormatterResolver Resolver) {
        writer.WriteBeginArray();
        Type.Instance.Serialize(ref writer,value.Type,Resolver);
        //this.Serialize(ref writer,value.Type,Resolver);
        writer.WriteValueSeparator();
        Expression.Instance.Serialize(ref writer,value.SwitchValue,Resolver);
        writer.WriteValueSeparator();
        Method.Instance.Serialize(ref writer,value.Comparison,Resolver);
        writer.WriteValueSeparator();
        SerializeReadOnlyCollection(ref writer,value.Cases,Resolver);
        writer.WriteValueSeparator();
        Expression.Instance.Serialize(ref writer,value.DefaultBody,Resolver);
        writer.WriteEndArray();
    }
    public T Deserialize(ref JsonReader reader,IJsonFormatterResolver Resolver) {
        reader.ReadIsBeginArrayWithVerify();
        //var type= this.Type.Deserialize(ref reader,Resolver);
        var type= Type.Instance.Deserialize(ref reader,Resolver);
        reader.ReadIsValueSeparatorWithVerify();
        var switchValue= Expression.Instance.Deserialize(ref reader,Resolver);
        reader.ReadIsValueSeparatorWithVerify();
        var comparison= Method.Instance.Deserialize(ref reader,Resolver);
        reader.ReadIsValueSeparatorWithVerify();
        var cases=DeserializeArray<Expressions.SwitchCase>(ref reader,Resolver);
        reader.ReadIsValueSeparatorWithVerify();
        var defaultBody= this.Deserialize(ref reader,Resolver);
        reader.ReadIsEndArrayWithVerify();
        return Expressions.Expression.Switch(type,switchValue,defaultBody,comparison,cases);
    }
}
