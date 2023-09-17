using Utf8Json;
using Expressions=System.Linq.Expressions;
using System.Diagnostics;

namespace LinqDB.Serializers.Utf8Json.Formatters;
using Writer=JsonWriter;
using Reader=JsonReader;
using T=Expressions.LabelExpression;
public class Label:IJsonFormatter<T> {
    public static readonly Label Instance=new();
    private static void PrivateWrite(ref Writer writer,T value,IJsonFormatterResolver Resolver){
        LabelTarget.Write(ref writer,value.Target,Resolver);
        writer.WriteValueSeparator();
        Expression.WriteNullable(ref writer,value.DefaultValue,Resolver);
    }
    internal static void Write(ref Writer writer,T value,IJsonFormatterResolver Resolver){
        writer.WriteNodeType(value);
        writer.WriteValueSeparator();
        PrivateWrite(ref writer,value,Resolver);
    }
    public void Serialize(ref Writer writer,T? value,IJsonFormatterResolver Resolver){
        if(writer.WriteIsNull(value))return;
        writer.WriteBeginArray();
        PrivateWrite(ref writer,value,Resolver);
        writer.WriteEndArray();
    }
    internal static T Read(ref Reader reader,IJsonFormatterResolver Resolver){
        var target= LabelTarget.Read(ref reader,Resolver);
        reader.ReadIsValueSeparatorWithVerify();
        var defaultValue=Expression.ReadNullable(ref reader,Resolver);
        return Expressions.Expression.Label(target,defaultValue);
    }
    public T Deserialize(ref Reader reader,IJsonFormatterResolver Resolver){
        if(reader.ReadIsNull()) return null!;
        reader.ReadIsBeginArrayWithVerify();
        var value=Read(ref reader,Resolver);
        reader.ReadIsEndArrayWithVerify();
        return value;
    }
}
