﻿using Expressions=System.Linq.Expressions;
using MessagePack;
using MessagePack.Formatters;
using System.Diagnostics;

namespace LinqDB.Serializers.MessagePack.Formatters;
using Writer=MessagePackWriter;
using Reader=MessagePackReader;
using T=Expressions.SwitchExpression;
using static Common;
public class Switch:IMessagePackFormatter<T> {
    public static readonly Switch Instance=new();
    private const int ArrayHeader=5;
    private const int InternalArrayHeader=ArrayHeader+1;
    private static void PrivateSerialize(ref Writer writer,T? value,MessagePackSerializerOptions Resolver){
        writer.WriteType(value!.Type);
        Expression.Instance.Serialize(ref writer,value.SwitchValue,Resolver);
        Method.InternalSerializeNullable(ref writer,value.Comparison,Resolver);
        SerializeReadOnlyCollection(ref writer,value.Cases,Resolver);
        Expression.Instance.Serialize(ref writer,value.DefaultBody,Resolver);
    }
    internal static void InternalSerialize(ref Writer writer,T value,MessagePackSerializerOptions Resolver){
        writer.WriteArrayHeader(InternalArrayHeader);
        writer.WriteNodeType(Expressions.ExpressionType.Switch);
        PrivateSerialize(ref writer,value,Resolver);
    }
    public void Serialize(ref Writer writer,T? value,MessagePackSerializerOptions Resolver){
        //if(writer.TryWriteNil(value)) return;
        writer.WriteArrayHeader(ArrayHeader);
        PrivateSerialize(ref writer,value,Resolver);
    }
    internal static T InternalDeserialize(ref Reader reader,MessagePackSerializerOptions Resolver){
        var type=reader.ReadType();
        var switchValue= Expression.Instance.Deserialize(ref reader,Resolver);
        var comparison= Method.InternalDeserializeNullable(ref reader,Resolver);
        var cases=DeserializeArray<Expressions.SwitchCase>(ref reader,Resolver);
        var defaultBody= Expression.Instance.Deserialize(ref reader,Resolver);
        return Expressions.Expression.Switch(type,switchValue,defaultBody,comparison,cases);
    }
    public T Deserialize(ref Reader reader,MessagePackSerializerOptions Resolver){
        //if(reader.TryReadNil()) return null!;
        var count=reader.ReadArrayHeader();
        Debug.Assert(count==ArrayHeader);
        return InternalDeserialize(ref reader,Resolver);
    }
}
