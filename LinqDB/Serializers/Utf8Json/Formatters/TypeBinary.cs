using System;
using Expressions=System.Linq.Expressions;
using Utf8Json;
using System.Diagnostics;

namespace LinqDB.Serializers.Utf8Json.Formatters;
using Writer=JsonWriter;
using Reader=JsonReader;
using static Extension;
public class TypeBinary:IJsonFormatter<Expressions.TypeBinaryExpression>{
    public static readonly TypeBinary Instance=new();
    private static void PrivateSerialize(ref Writer writer,Expressions.TypeBinaryExpression value,IJsonFormatterResolver Resolver){
        Expression.Instance.Serialize(ref writer,value.Expression,Resolver);
        writer.WriteValueSeparator();
        writer.WriteType(value.TypeOperand);
    }
    internal static void InternalSerialize(ref Writer writer,Expressions.TypeBinaryExpression value,IJsonFormatterResolver Resolver){
        writer.WriteNodeType(value);
        writer.WriteValueSeparator();
        PrivateSerialize(ref writer,value,Resolver);
    }
    public void Serialize(ref Writer writer,Expressions.TypeBinaryExpression? value,IJsonFormatterResolver Resolver){
        //if(writer.WriteIsNull(value))return;
        Debug.Assert(value!=null,nameof(value)+" != null");
        writer.WriteBeginArray();
        writer.WriteNodeType(value);
        writer.WriteValueSeparator();
        PrivateSerialize(ref writer,value,Resolver);
        writer.WriteEndArray();
    }
    private static (Expressions.Expression expression,System.Type type)PrivateDeserialize(ref Reader reader,IJsonFormatterResolver Resolver){
        var expression=Expression.Instance.Deserialize(ref reader,Resolver);
        reader.ReadIsValueSeparatorWithVerify();
        var type=reader.ReadType();
        return (expression,type);
    }
    internal static Expressions.TypeBinaryExpression InternalDeserializeTypeEqual(ref Reader reader,IJsonFormatterResolver Resolver){
        var (expression,type)=PrivateDeserialize(ref reader,Resolver);
        return Expressions.Expression.TypeEqual(expression,type);
    }
    internal static Expressions.TypeBinaryExpression InternalDeserializeTypeIs(ref Reader reader,IJsonFormatterResolver Resolver){
        var (expression,type)=PrivateDeserialize(ref reader,Resolver);
        return Expressions.Expression.TypeIs(expression,type);
    }
    public Expressions.TypeBinaryExpression Deserialize(ref Reader reader,IJsonFormatterResolver Resolver){
        //if(reader.ReadIsNull()) return null!;
        reader.ReadIsBeginArrayWithVerify();
        var NodeTypeName=reader.ReadString();
        reader.ReadIsValueSeparatorWithVerify();
        var NodeType=Enum.Parse<Expressions.ExpressionType>(NodeTypeName);
        var value=NodeType switch{
            Expressions.ExpressionType.TypeEqual=>InternalDeserializeTypeEqual(ref reader,Resolver),
            Expressions.ExpressionType.TypeIs=>InternalDeserializeTypeIs(ref reader,Resolver),
            _=>throw new NotSupportedException(NodeTypeName)
        };
        reader.ReadIsEndArrayWithVerify();
        return value;
    }
}
