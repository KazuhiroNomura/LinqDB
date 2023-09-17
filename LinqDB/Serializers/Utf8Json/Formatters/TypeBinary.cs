using System;
using Utf8Json;

using Expressions = System.Linq.Expressions;
namespace LinqDB.Serializers.Utf8Json.Formatters;
using Writer = JsonWriter;
using Reader = JsonReader;
using T = Expressions.TypeBinaryExpression;
public class TypeBinary:IJsonFormatter<T> {
    public static readonly TypeBinary Instance=new();
    private static void PrivateWrite(ref Writer writer,T value,IJsonFormatterResolver Resolver){
        Expression.Write(ref writer,value.Expression,Resolver);
        writer.WriteValueSeparator();
        writer.WriteType(value.TypeOperand);
    }
    internal static void Write(ref Writer writer,T value,IJsonFormatterResolver Resolver){
        writer.WriteNodeType(value);
        writer.WriteValueSeparator();
        PrivateWrite(ref writer,value,Resolver);
    }
    public void Serialize(ref Writer writer,T? value,IJsonFormatterResolver Resolver){
        if(writer.WriteIsNull(value))return;
        writer.WriteBeginArray();
        writer.WriteNodeType(value);
        writer.WriteValueSeparator();
        PrivateWrite(ref writer,value,Resolver);
        writer.WriteEndArray();
    }
    private static (Expressions.Expression expression,System.Type type)PrivateRead(ref Reader reader,IJsonFormatterResolver Resolver){
        var expression=Expression.Read(ref reader,Resolver);
        reader.ReadIsValueSeparatorWithVerify();
        var type=reader.ReadType();
        return (expression,type);
    }
    internal static T ReadTypeEqual(ref Reader reader,IJsonFormatterResolver Resolver){
        var (expression,type)=PrivateRead(ref reader,Resolver);
        return Expressions.Expression.TypeEqual(expression,type);
    }
    internal static T ReadTypeIs(ref Reader reader,IJsonFormatterResolver Resolver){
        var (expression,type)=PrivateRead(ref reader,Resolver);
        return Expressions.Expression.TypeIs(expression,type);
    }
    public T Deserialize(ref Reader reader,IJsonFormatterResolver Resolver){
        if(reader.ReadIsNull()) return null!;
        reader.ReadIsBeginArrayWithVerify();
        var NodeTypeName=reader.ReadString();
        reader.ReadIsValueSeparatorWithVerify();
        var NodeType=Enum.Parse<Expressions.ExpressionType>(NodeTypeName);
        var value=NodeType switch{
            Expressions.ExpressionType.TypeEqual=>ReadTypeEqual(ref reader,Resolver),
            Expressions.ExpressionType.TypeIs=>ReadTypeIs(ref reader,Resolver),
            _=>throw new NotSupportedException(NodeTypeName)
        };
        reader.ReadIsEndArrayWithVerify();
        return(reader.ReadIsNull()?null:value)!;
    }
}
