using System;
using Expressions=System.Linq.Expressions;
using MessagePack;
using MessagePack.Formatters;
using Utf8Json;
namespace LinqDB.Serializers.Utf8Json.Formatters;
using Writer=JsonWriter;
using Reader=JsonReader;
using static Common;
public class TypeBinary:IJsonFormatter<Expressions.TypeBinaryExpression>{
    public static readonly TypeBinary Instance=new();
    public void Serialize(ref JsonWriter writer,Expressions.TypeBinaryExpression? value,IJsonFormatterResolver Resolver){
        if(value is null){
            writer.WriteNull();
            return;
        }
        writer.WriteBeginArray();
        writer.WriteString(value.NodeType.ToString());
        writer.WriteValueSeparator();
        Expression.Instance.Serialize(ref writer,value.Expression,Resolver);
        writer.WriteValueSeparator();
        Type.Instance.Serialize(ref writer,value.TypeOperand,Resolver);
        writer.WriteEndArray();
    }
    public Expressions.TypeBinaryExpression Deserialize(ref JsonReader reader,IJsonFormatterResolver Resolver){
        if(reader.ReadIsNull()) return null!;
        reader.ReadIsBeginArrayWithVerify();
        var NodeTypeName=reader.ReadString();
        reader.ReadIsValueSeparatorWithVerify();
        var NodeType=Enum.Parse<Expressions.ExpressionType>(NodeTypeName);
        var expression= this.Deserialize(ref reader,Resolver);
        reader.ReadIsValueSeparatorWithVerify();
        var type=reader.ReadType();
        reader.ReadIsEndArrayWithVerify();
        return NodeType switch{
            Expressions.ExpressionType.TypeEqual=>Expressions.Expression.TypeEqual(expression,type),
            Expressions.ExpressionType.TypeIs=>Expressions.Expression.TypeIs(expression,type),
            _=>throw new NotSupportedException(NodeTypeName)
        };
    }
}
