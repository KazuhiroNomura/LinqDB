using System;
using Expressions=System.Linq.Expressions;
using MessagePack;
using MessagePack.Formatters;
using System.Diagnostics;

namespace LinqDB.Serializers.MessagePack.Formatters;
using Writer=MessagePackWriter;
using Reader=MessagePackReader;
using T=Expressions.BlockExpression;
using static Common;
public class Block:IMessagePackFormatter<T> {
    public static readonly Block Instance=new();
    private const int ArrayHeader=3;
    private const int InternalArrayHeader=ArrayHeader+1;
    private static void PrivateSerialize(ref Writer writer,T value,MessagePackSerializerOptions Resolver){
        var ListParameter=Serializer.Instance.ListParameter;
        var ListParameter_Count=ListParameter.Count;
        var Variables=value.Variables;
        ListParameter.AddRange(Variables);
        writer.WriteType(value.Type);
        Serialize宣言Parameters(ref writer,value.Variables);
        SerializeReadOnlyCollection(ref writer,value.Expressions,Resolver);
        ListParameter.RemoveRange(ListParameter_Count,Variables.Count);
    }
    /// <summary>
    /// Expressionから呼ばれる。Serializeとの違いはNodeTypeを書き込むこと
    /// </summary>
    /// <param name="writer"></param>
    /// <param name="value"></param>
    /// <param name="Resolver"></param>
    internal static void InternalSerialize(ref Writer writer,T value,MessagePackSerializerOptions Resolver){
        writer.WriteArrayHeader(InternalArrayHeader);
        writer.WriteNodeType(Expressions.ExpressionType.Block);
        PrivateSerialize(ref writer,value,Resolver);
    }
    public void Serialize(ref Writer writer,T value,MessagePackSerializerOptions Resolver){
        writer.WriteArrayHeader(ArrayHeader);
        PrivateSerialize(ref writer,value,Resolver);
    }
    internal static T InternalDeserialize(ref Reader reader,MessagePackSerializerOptions Resolver){
        var ListParameter=Serializer.Instance.ListParameter;
        var ListParameter_Count=ListParameter.Count;
        var type=reader.ReadType();
        var variables= Deserialize宣言Parameters(ref reader,Resolver);
        ListParameter.AddRange(variables);
        var expressions=DeserializeArray<Expressions.Expression>(ref reader,Resolver);
        ListParameter.RemoveRange(ListParameter_Count,variables.Length);
        return Expressions.Expression.Block(type,variables,expressions);
    }
    public T Deserialize(ref Reader reader,MessagePackSerializerOptions Resolver){
        var count=reader.ReadArrayHeader();
        Debug.Assert(count==ArrayHeader);
        return InternalDeserialize(ref reader,Resolver);
    }
}
