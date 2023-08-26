using System;
using Expressions=System.Linq.Expressions;
using MessagePack;
using MessagePack.Formatters;
using Utf8Json;
namespace LinqDB.Serializers.Formatters;
using static Common;
partial class ExpressionJsonFormatter:IJsonFormatter<Expressions.NewArrayExpression>{
    private IJsonFormatter<Expressions.NewArrayExpression> NewArray=>this;
    public void Serialize(ref JsonWriter writer,Expressions.NewArrayExpression? value,IJsonFormatterResolver Resolver){
        if(value is null){
            writer.WriteNull();
            return;
        }
        writer.WriteBeginArray();
        writer.WriteString(value.NodeType.ToString());
        writer.WriteValueSeparator();
        Serialize_Type(ref writer,value.Type.GetElementType(),Resolver);
        writer.WriteValueSeparator();
        Serialize_T(ref writer,value.Expressions,Resolver);
        writer.WriteEndArray();
    }
    Expressions.NewArrayExpression IJsonFormatter<Expressions.NewArrayExpression>.Deserialize(ref JsonReader reader,IJsonFormatterResolver Resolver){
        if(reader.ReadIsNull()) return null!;
        reader.ReadIsBeginArrayWithVerify();
        var NodeTypeName=reader.ReadString();
        reader.ReadIsValueSeparatorWithVerify();
        var NodeType=Enum.Parse<Expressions.ExpressionType>(NodeTypeName);
        var type=Deserialize_Type(ref reader,Resolver);
        reader.ReadIsValueSeparatorWithVerify();
        var expressions= Deserialize_T<Expressions.Expression[]>(ref reader,Resolver);
        reader.ReadIsEndArrayWithVerify();
        return NodeType switch{
            Expressions.ExpressionType.NewArrayBounds=>Expressions.Expression.NewArrayBounds(type,expressions),
            Expressions.ExpressionType.NewArrayInit=>Expressions.Expression.NewArrayInit(type,expressions),
            _=>throw new NotImplementedException(NodeTypeName)
        };
    }
}
partial class ExpressionMessagePackFormatter:IMessagePackFormatter<Expressions.NewArrayExpression>{
    private IMessagePackFormatter<Expressions.NewArrayExpression> MSNewArray=>this;
    public void Serialize(ref MessagePackWriter writer,Expressions.NewArrayExpression? value,MessagePackSerializerOptions Resolver){
        if(value is null){
            writer.WriteNil();
            return;
        }
        writer.Write((byte)value.NodeType);
        Serialize_Type(ref writer,value.Type.GetElementType(),Resolver);
        Serialize_T(ref writer,value.Expressions,Resolver);
    }
    Expressions.NewArrayExpression IMessagePackFormatter<Expressions.NewArrayExpression>.Deserialize(ref MessagePackReader reader,MessagePackSerializerOptions Resolver){
        if(reader.TryReadNil()) return null!;
        var NodeType=(Expressions.ExpressionType)reader.ReadByte();
        var type=Deserialize_Type(ref reader,Resolver);
        var expressions= Deserialize_T<Expressions.Expression[]>(ref reader,Resolver);
        return NodeType switch{
            Expressions.ExpressionType.NewArrayBounds=>Expressions.Expression.NewArrayBounds(type,expressions),
            Expressions.ExpressionType.NewArrayInit=>Expressions.Expression.NewArrayInit(type,expressions),
            _=>throw new NotImplementedException(Resolver.ToString())
        };
    }
}
