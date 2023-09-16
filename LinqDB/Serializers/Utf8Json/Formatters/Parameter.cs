using Expressions = System.Linq.Expressions;
using Utf8Json;
namespace LinqDB.Serializers.Utf8Json.Formatters;
using Writer=JsonWriter;
using Reader=JsonReader;
using T=Expressions.ParameterExpression;
using C=Serializer;
public class Parameter:IJsonFormatter<T> {
    public static readonly Parameter Instance=new();
    private static void PrivateSerialize(ref Writer writer,T value,IJsonFormatterResolver Resolver){
        writer.WriteInt32(Resolver.Serializer().ListParameter.LastIndexOf(value));
    }
    internal static void Write(ref Writer writer,T value,IJsonFormatterResolver Resolver){
        writer.WriteNodeType(value);
        writer.WriteValueSeparator();
        PrivateSerialize(ref writer,value,Resolver);
    }

    public void Serialize(ref Writer writer,T value,IJsonFormatterResolver Resolver) {
        writer.WriteBeginObject();
        writer.WriteString(value.Name);
        writer.WriteNameSeparator();
        writer.WriteType(value.Type);
        writer.WriteEndObject();
    }
    internal static T Read(ref Reader reader,IJsonFormatterResolver Resolver){
        var index=reader.ReadInt32();
        var Parameter= Resolver.Serializer().ListParameter[index];
        return Parameter;
    }
    public T Deserialize(ref Reader reader,IJsonFormatterResolver Resolver) {
        reader.ReadIsBeginObjectWithVerify();
        var name=reader.ReadString();
        reader.ReadIsNameSeparatorWithVerify();
        var type=reader.ReadType();
        reader.ReadIsEndObjectWithVerify();
        return Expressions.Expression.Parameter(type,name);
    }
}
