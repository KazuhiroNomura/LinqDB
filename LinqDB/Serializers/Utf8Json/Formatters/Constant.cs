using System;
using Expressions=System.Linq.Expressions;
using MessagePack;
using MessagePack.Formatters;
using Utf8Json;
using System.Reflection;

namespace LinqDB.Serializers.Utf8Json.Formatters;
using Writer=JsonWriter;
using Reader=JsonReader;
using T=Expressions.ConstantExpression;
using C=Utf8JsonCustomSerializer;
using static Common;
public class Constant:IJsonFormatter<T> {
    public static readonly Constant Instance=new();
    public void Serialize(ref Writer writer,T value,IJsonFormatterResolver Resolver){
        writer.WriteBeginArray();
        writer.WriteType(value.Type);
        writer.WriteValueSeparator();
        Object.Instance.Serialize(ref writer,value.Value,Resolver);
        writer.WriteEndArray();
    }
    //private readonly object[] Objects2=new object[2];
    public T Deserialize(ref Reader reader,IJsonFormatterResolver Resolver){
        reader.ReadIsBeginArrayWithVerify();
        var type=reader.ReadType();
        reader.ReadIsValueSeparatorWithVerify();
        var value=Object.Instance.Deserialize(ref reader,Resolver);
        reader.ReadIsEndArrayWithVerify();
        return Expressions.Expression.Constant(value,type);
    }
}
