using Utf8Json;
using Expressions=System.Linq.Expressions;
using System.Diagnostics;

namespace LinqDB.Serializers.Utf8Json.Formatters;
using Writer=JsonWriter;
using Reader=JsonReader;
using T=Expressions.LabelExpression;
public class Label:IJsonFormatter<T> {
    public static readonly Label Instance=new();
    public void Serialize(ref Writer writer,T? value,IJsonFormatterResolver Resolver){
        //if(writer.WriteIsNull(value))return;
        Debug.Assert(value!=null,nameof(value)+" != null");
        writer.WriteBeginArray();
        LabelTarget.Instance.Serialize(ref writer,value.Target,Resolver);
        writer.WriteValueSeparator();
        Expression.SerializeNullable(ref writer,value.DefaultValue,Resolver);
        writer.WriteEndArray();
    }
    public T Deserialize(ref Reader reader,IJsonFormatterResolver Resolver){
        //if(reader.ReadIsNull()) return null!;
        reader.ReadIsBeginArrayWithVerify();
        var target= LabelTarget.Instance.Deserialize(ref reader,Resolver);
        reader.ReadIsValueSeparatorWithVerify();
        var defaultValue=Expression.DeserializeNullable(ref reader,Resolver);
        reader.ReadIsEndArrayWithVerify();
        return Expressions.Expression.Label(target,defaultValue);
    }
}
