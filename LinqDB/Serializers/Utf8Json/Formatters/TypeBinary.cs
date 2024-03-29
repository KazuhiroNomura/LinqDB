﻿using System;

using Utf8Json;

using Expressions = System.Linq.Expressions;
namespace LinqDB.Serializers.Utf8Json.Formatters;
using O=IJsonFormatterResolver;
using Writer = JsonWriter;
using Reader = JsonReader;
using T = Expressions.TypeBinaryExpression;
public class TypeBinary:IJsonFormatter<T> {
    public static readonly TypeBinary Instance=new();
    internal static void Write(ref Writer writer,T value,O Resolver){
        writer.WriteBeginArray();
        writer.WriteNodeType(value);
        writer.WriteValueSeparator();
        Expression.Write(ref writer,value.Expression,Resolver);
        writer.WriteValueSeparator();
        writer.WriteType(value.TypeOperand);
        writer.WriteEndArray(); 
    }
    public void Serialize(ref Writer writer,T? value,O Resolver){
        if(writer.TryWriteNil(value))return;
        Write(ref writer,value,Resolver);
    }
    private static (Expressions.Expression expression,Type type)PrivateRead(ref Reader reader,O Resolver){
        var expression=Expression.Read(ref reader,Resolver);
        reader.ReadIsValueSeparatorWithVerify();
        var type=reader.ReadType();
        return (expression,type);
    }
    internal static T ReadTypeEqual(ref Reader reader,O Resolver){
        var (expression,type)=PrivateRead(ref reader,Resolver);
        return Expressions.Expression.TypeEqual(expression,type);
    }
    internal static T ReadTypeIs(ref Reader reader,O Resolver){
        var (expression,type)=PrivateRead(ref reader,Resolver);
        return Expressions.Expression.TypeIs(expression,type);
    }
    public T Deserialize(ref Reader reader,O Resolver){
        if(reader.TryReadNil()) return null!;
        reader.ReadIsBeginArrayWithVerify();
        
        var NodeType=reader.ReadNodeType();
        System.Diagnostics.Debug.Assert(NodeType is Expressions.ExpressionType.TypeEqual or Expressions.ExpressionType.TypeIs);
        reader.ReadIsValueSeparatorWithVerify();
        var value=NodeType switch{
            Expressions.ExpressionType.TypeEqual=>ReadTypeEqual(ref reader,Resolver),
            _                                   =>ReadTypeIs(ref reader,Resolver)
        };
        reader.ReadIsEndArrayWithVerify();
        return(reader.TryReadNil()?null:value)!;
    }
}
