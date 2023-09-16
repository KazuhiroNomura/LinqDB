﻿using Expressions=System.Linq.Expressions;
using Utf8Json;
namespace LinqDB.Serializers.Utf8Json.Formatters;
using Writer=JsonWriter;
using Reader=JsonReader;
using static Extension;
using T=Expressions.ListInitExpression;
public class ListInit:IJsonFormatter<T> {
    public static readonly ListInit Instance=new();
    private static void PrivateSerialize(ref Writer writer,T value,IJsonFormatterResolver Resolver){
        New.Instance.Serialize(ref writer,value.NewExpression,Resolver);
        writer.WriteValueSeparator();
        writer.SerializeReadOnlyCollection(value.Initializers,Resolver);
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
        var @new=New.Instance.Deserialize(ref reader,Resolver);
        reader.ReadIsValueSeparatorWithVerify();
        var Initializers=reader.ReadArray<Expressions.ElementInit>(Resolver);
        return Expressions.Expression.ListInit(@new,Initializers);
    }
    public T Deserialize(ref Reader reader,IJsonFormatterResolver Resolver) {
        reader.ReadIsBeginArrayWithVerify();
        var value=Read(ref reader,Resolver);
        reader.ReadIsEndArrayWithVerify();
        return value;
    }
}