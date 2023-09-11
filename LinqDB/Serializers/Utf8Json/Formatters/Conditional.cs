using Utf8Json;
using System.Diagnostics;
using Expressions=System.Linq.Expressions;

namespace LinqDB.Serializers.Utf8Json.Formatters;
using Writer=JsonWriter;
using Reader=JsonReader;
using T=Expressions.ConditionalExpression;
public class Conditional:IJsonFormatter<T> {
    public static readonly Conditional Instance=new();
    internal static void InternalSerialize(ref Writer writer,T value,IJsonFormatterResolver Resolver){
        Expression.Instance.Serialize(ref writer,value.Test,Resolver);
        writer.WriteValueSeparator();
        Expression.Instance.Serialize(ref writer,value.IfTrue,Resolver);
        writer.WriteValueSeparator();
        Expression.Instance.Serialize(ref writer,value.IfFalse,Resolver);
        writer.WriteValueSeparator();
        writer.WriteType(value.Type);
    }
    public void Serialize(ref Writer writer,T? value,IJsonFormatterResolver Resolver){
    //    if(writer.WriteIsNull(value))return;
        Debug.Assert(value!=null,nameof(value)+" != null");
        writer.WriteBeginArray();
        InternalSerialize(ref writer,value,Resolver);
        writer.WriteEndArray();
    }
    internal static T InternalDeserialize(ref Reader reader,IJsonFormatterResolver Resolver){
        var test=Expression.Instance.Deserialize(ref reader,Resolver);
        reader.ReadIsValueSeparatorWithVerify();
        var ifTrue=Expression.Instance.Deserialize(ref reader,Resolver);
        reader.ReadIsValueSeparatorWithVerify();
        var ifFalse=Expression.Instance.Deserialize(ref reader,Resolver);
        reader.ReadIsValueSeparatorWithVerify();
        var type=reader.ReadType();
        return Expressions.Expression.Condition(test,ifTrue,ifFalse,type);
    }
    public T Deserialize(ref Reader reader,IJsonFormatterResolver Resolver){
        //if(reader.ReadIsNull()) return null!;
        reader.ReadIsBeginArrayWithVerify();
        var value=InternalDeserialize(ref reader,Resolver);
        reader.ReadIsEndArrayWithVerify();
        return value;
    }
}

