﻿using System;
using Expressions = System.Linq.Expressions;
using Utf8Json;

namespace LinqDB.Serializers.Utf8Json.Formatters;
using Writer = JsonWriter;
using Reader = JsonReader;
using T = Expressions.NewArrayExpression;
using static Extension;
public class NewArray:IJsonFormatter<T> {
    public static readonly NewArray Instance=new();
    private static void PrivateWrite(ref Writer writer,T value,IJsonFormatterResolver Resolver){
        writer.WriteType(value.Type.GetElementType());
        writer.WriteValueSeparator();
        writer.WriteCollection(value.Expressions,Resolver);
    }
    internal static void Write(ref Writer writer,T value,IJsonFormatterResolver Resolver){
        writer.WriteNodeType(value);
        writer.WriteValueSeparator();
        PrivateWrite(ref writer,value,Resolver);
    }
    public void Serialize(ref Writer writer,T? value,IJsonFormatterResolver Resolver){
        if(writer.TryWriteNil(value))return;
        writer.WriteBeginArray();
        writer.WriteString(value!.NodeType.ToString());
        writer.WriteValueSeparator();
        PrivateWrite(ref writer,value,Resolver);
        writer.WriteEndArray();
    }
    private static (Type type,Expressions.Expression[]expressions)PrivateDeserialize(ref Reader reader,IJsonFormatterResolver Resolver){
        var type=reader.ReadType();
        reader.ReadIsValueSeparatorWithVerify();
        var expressions=reader.ReadArray<Expressions.Expression>(Resolver);
        return (type,expressions);
    }
    internal static T ReadNewArrayBounds(ref Reader reader,IJsonFormatterResolver Resolver){
        var (type,expressions)=PrivateDeserialize(ref reader,Resolver);
        return Expressions.Expression.NewArrayBounds(type,expressions);
    }
    internal static T ReadNewArrayInit(ref Reader reader,IJsonFormatterResolver Resolver){
        var (type,expressions)=PrivateDeserialize(ref reader,Resolver);
        return Expressions.Expression.NewArrayInit(type,expressions);
    }
    public T Deserialize(ref Reader reader,IJsonFormatterResolver Resolver){
        if(reader.TryReadNil()) return null!;
        reader.ReadIsBeginArrayWithVerify();
        var NodeType=reader.ReadNodeType();
        reader.ReadIsValueSeparatorWithVerify();
        var value=NodeType switch{
            Expressions.ExpressionType.NewArrayBounds=>ReadNewArrayBounds(ref reader,Resolver),
            Expressions.ExpressionType.NewArrayInit=>ReadNewArrayInit(ref reader,Resolver),
            _=>throw new NotImplementedException(NodeType.ToString())
        };
        reader.ReadIsEndArrayWithVerify();
        return value;
    }
}
