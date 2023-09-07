using Expressions=System.Linq.Expressions;
using MessagePack;
using MessagePack.Formatters;
using Utf8Json;
using System.Reflection;
using System.Linq.Expressions;

namespace LinqDB.Serializers.Utf8Json.Formatters;
using Writer=JsonWriter;
using Reader=JsonReader;
using T=ConditionalExpression;
public class Conditional:IJsonFormatter<T> {
    public static readonly Conditional Instance=new();
    public void Serialize(ref Writer writer,T? value,IJsonFormatterResolver Resolver){
    if(value is null){
        writer.WriteNull();
        return;
    }
    writer.WriteBeginArray();
    Expression.Instance.Serialize(ref writer,value.Test,Resolver);
    writer.WriteValueSeparator();
    Expression.Instance.Serialize(ref writer,value.IfTrue,Resolver);
    writer.WriteValueSeparator();
    Expression.Instance.Serialize(ref writer,value.IfFalse,Resolver);
    writer.WriteEndArray();
}
    public T Deserialize(ref Reader reader,IJsonFormatterResolver Resolver){
        if(reader.ReadIsNull()) return null!;
        reader.ReadIsBeginArrayWithVerify();
        var test= Expression.Instance.Deserialize(ref reader,Resolver);
        reader.ReadIsValueSeparatorWithVerify();
        var ifTrue= Expression.Instance.Deserialize(ref reader,Resolver);
        reader.ReadIsValueSeparatorWithVerify();
        var ifFalse= Expression.Instance.Deserialize(ref reader,Resolver);
        reader.ReadIsEndArrayWithVerify();
        return Expressions.Expression.Condition(
            test,
            ifTrue,
            ifFalse
        );
    }
}

