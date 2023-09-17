﻿using Utf8Json;

using Expressions = System.Linq.Expressions;
namespace LinqDB.Serializers.Utf8Json.Formatters;
using Writer = JsonWriter;
using Reader = JsonReader;
using T = Expressions.NewExpression;
using static Extension;
public class New:IJsonFormatter<T> {
    public static readonly New Instance=new();
    private static void PrivateSerialize(ref Writer writer,T value,IJsonFormatterResolver Resolver){
        Constructor.Instance.Serialize(ref writer,value.Constructor!,Resolver);
        writer.WriteValueSeparator();
        writer.WriteCollection(value.Arguments,Resolver);
    }
    internal static void Write(ref Writer writer,T value,IJsonFormatterResolver Resolver){
        writer.WriteNodeType(value);
        writer.WriteValueSeparator();
        PrivateSerialize(ref writer,value,Resolver);
    }
    public void Serialize(ref Writer writer,T? value,IJsonFormatterResolver Resolver){
        if(writer.WriteIsNull(value))return;
        writer.WriteBeginArray();
        PrivateSerialize(ref writer,value,Resolver);
        writer.WriteEndArray();
    }
    internal static T Read(ref Reader reader,IJsonFormatterResolver Resolver){
        var constructor= Constructor.Instance.Deserialize(ref reader,Resolver);
        reader.ReadIsValueSeparatorWithVerify();
        var arguments=reader.ReadArray<Expressions.Expression>(Resolver);
        return Expressions.Expression.New(
            constructor,
            arguments
        );
    }
    public T Deserialize(ref Reader reader,IJsonFormatterResolver Resolver){
        if(reader.ReadIsNull()) return null!;
        reader.ReadIsBeginArrayWithVerify();
        var value=Read(ref reader,Resolver);
        reader.ReadIsEndArrayWithVerify();
        return value;
    }
}
