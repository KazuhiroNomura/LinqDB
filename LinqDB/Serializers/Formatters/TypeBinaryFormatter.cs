using System;
using Expressions=System.Linq.Expressions;
using MessagePack;
using MessagePack.Formatters;
using Utf8Json;
namespace LinqDB.Serializers.Formatters;
using static Common;
partial class ExpressionJsonFormatter:IJsonFormatter<Expressions.TypeBinaryExpression>{
    private IJsonFormatter<Expressions.TypeBinaryExpression> TypeBinary=>this;
    public void Serialize(ref JsonWriter writer,Expressions.TypeBinaryExpression? value,IJsonFormatterResolver Resolver){
        if(value is null){
            writer.WriteNull();
            return;
        }
        writer.WriteBeginArray();
        writer.WriteString(value.NodeType.ToString());
        writer.WriteValueSeparator();
        this.Serialize(ref writer,value.Expression,Resolver);
        writer.WriteValueSeparator();
        Serialize_Type(ref writer,value.TypeOperand,Resolver);
        writer.WriteEndArray();
    }
    Expressions.TypeBinaryExpression IJsonFormatter<Expressions.TypeBinaryExpression>.Deserialize(ref JsonReader reader,IJsonFormatterResolver Resolver){
        if(reader.ReadIsNull()) return null!;
        reader.ReadIsBeginArrayWithVerify();
        var NodeTypeName=reader.ReadString();
        reader.ReadIsValueSeparatorWithVerify();
        var NodeType=Enum.Parse<Expressions.ExpressionType>(NodeTypeName);
        var expression= this.Deserialize(ref reader,Resolver);
        reader.ReadIsValueSeparatorWithVerify();
        var type=Deserialize_Type(ref reader,Resolver);
        reader.ReadIsEndArrayWithVerify();
        return NodeType switch{
            Expressions.ExpressionType.TypeEqual=>Expressions.Expression.TypeEqual(expression,type),
            Expressions.ExpressionType.TypeIs=>Expressions.Expression.TypeIs(expression,type),
            _=>throw new NotSupportedException(NodeTypeName)
        };
    }
}
partial class ExpressionMessagePackFormatter:IMessagePackFormatter<Expressions.TypeBinaryExpression>{
    private IMessagePackFormatter<Expressions.TypeBinaryExpression> TypeBinary=>this;
    public void Serialize(ref MessagePackWriter writer,Expressions.TypeBinaryExpression? value,MessagePackSerializerOptions Resolver){
        if(value is null){
            writer.WriteNil();
            return;
        }
        writer.Write((byte)value.NodeType);
        this.Serialize(ref writer,value.Expression,Resolver);
        Serialize_Type(ref writer,value.TypeOperand,Resolver);
    }
    Expressions.TypeBinaryExpression IMessagePackFormatter<Expressions.TypeBinaryExpression>.Deserialize(ref MessagePackReader reader,MessagePackSerializerOptions Resolver){
        if(reader.TryReadNil()) return null!;
        var NodeType=(Expressions.ExpressionType)reader.ReadByte();
        var expression= this.Deserialize(ref reader,Resolver);
        var type=Deserialize_Type(ref reader,Resolver);
        return NodeType switch{
            Expressions.ExpressionType.TypeEqual=>Expressions.Expression.TypeEqual(expression,type),
            Expressions.ExpressionType.TypeIs=>Expressions.Expression.TypeIs(expression,type),
            _=>throw new NotSupportedException(NodeType.ToString())
        };
    }
}
