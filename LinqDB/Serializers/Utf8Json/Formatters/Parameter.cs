
using Utf8Json;

using Expressions = System.Linq.Expressions;
namespace LinqDB.Serializers.Utf8Json.Formatters;
using Writer=JsonWriter;
using Reader=JsonReader;
using T=Expressions.ParameterExpression;
public class Parameter:IJsonFormatter<T> {
    public static readonly Parameter Instance=new();
    internal static void Write(ref Writer writer,T value,IJsonFormatterResolver Resolver){
        writer.WriteNodeType(Expressions.ExpressionType.Parameter);
        writer.WriteValueSeparator();
        var index=Resolver.Serializer().ListParameter.LastIndexOf(value);
        writer.WriteInt32(index);
        if(index<0){
            writer.WriteValueSeparator();
            writer.WriteType(value.Type);
            writer.WriteValueSeparator();
            writer.WriteString(value.Name);
        }
        
        
    }
    public void Serialize(ref Writer writer,T? value,IJsonFormatterResolver Resolver) {
        if(writer.WriteIsNull(value))return;
        writer.WriteBeginObject();
        writer.WriteType(value!.Type);
        writer.WriteNameSeparator();
        writer.WriteString(value.Name);
        writer.WriteEndObject();
    }
    internal static T Read(ref Reader reader,IJsonFormatterResolver Resolver){
        var index=reader.ReadInt32();
        var ListParameter=Resolver.Serializer().ListParameter;
        if(index>=0) return ListParameter[index];
        reader.ReadIsValueSeparatorWithVerify();
        var type=reader.ReadType();
        reader.ReadIsValueSeparatorWithVerify();
        var name=reader.ReadString();
        var Parameter=Expressions.Expression.Parameter(type,name);
        ListParameter.Add(Parameter);
        return Parameter;
    }
    public T Deserialize(ref Reader reader,IJsonFormatterResolver Resolver){
        if(reader.ReadIsNull()) return null!;
        reader.ReadIsBeginObjectWithVerify();
        var type=reader.ReadType();
        reader.ReadIsNameSeparatorWithVerify();
        var name=reader.ReadString();
        reader.ReadIsEndObjectWithVerify();
        return Expressions.Expression.Parameter(type,name);
    }
}
