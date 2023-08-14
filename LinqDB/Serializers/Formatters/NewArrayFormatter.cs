using System;
using System.Linq.Expressions;
using MessagePack;
using MessagePack.Formatters;
using Utf8Json;
namespace LinqDB.Serializers.Formatters;
using static Common;
partial class ExpressionJsonFormatter:IJsonFormatter<NewArrayExpression>{
    private IJsonFormatter<NewArrayExpression> NewArray=>this;
    public void Serialize(ref JsonWriter writer,NewArrayExpression? value,IJsonFormatterResolver Resolver){
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
    NewArrayExpression IJsonFormatter<NewArrayExpression>.Deserialize(ref JsonReader reader,IJsonFormatterResolver Resolver){
        if(reader.ReadIsNull()) return null!;
        reader.ReadIsBeginArrayWithVerify();
        var NodeTypeName=reader.ReadString();
        reader.ReadIsValueSeparatorWithVerify();
        var NodeType=Enum.Parse<ExpressionType>(NodeTypeName);
        var type=Deserialize_Type(ref reader,Resolver);
        reader.ReadIsValueSeparatorWithVerify();
        var expressions= Deserialize_T<Expression[]>(ref reader,Resolver);
        reader.ReadIsEndArrayWithVerify();
        return NodeType switch{
            ExpressionType.NewArrayBounds=>Expression.NewArrayBounds(type,expressions),
            ExpressionType.NewArrayInit=>Expression.NewArrayInit(type,expressions),
            _=>throw new NotImplementedException(NodeTypeName)
        };
    }
}
partial class ExpressionMessagePackFormatter:IMessagePackFormatter<NewArrayExpression>{
    private IMessagePackFormatter<NewArrayExpression> MSNewArray=>this;
    public void Serialize(ref MessagePackWriter writer,NewArrayExpression? value,MessagePackSerializerOptions Resolver){
        if(value is null){
            writer.WriteNil();
            return;
        }
        writer.Write((byte)value.NodeType);
        Serialize_Type(ref writer,value.Type.GetElementType(),Resolver);
        Serialize_T(ref writer,value.Expressions,Resolver);
    }
    NewArrayExpression IMessagePackFormatter<NewArrayExpression>.Deserialize(ref MessagePackReader reader,MessagePackSerializerOptions Resolver){
        if(reader.TryReadNil()) return null!;
        var NodeType=(ExpressionType)reader.ReadByte();
        var type=Deserialize_Type(ref reader,Resolver);
        var expressions= Deserialize_T<Expression[]>(ref reader,Resolver);
        return NodeType switch{
            ExpressionType.NewArrayBounds=>Expression.NewArrayBounds(type,expressions),
            ExpressionType.NewArrayInit=>Expression.NewArrayInit(type,expressions),
            _=>throw new NotImplementedException(Resolver.ToString())
        };
    }
}
//class NewArrayFormatter:IMessagePackFormatter<NewArrayExpression>{
//    private IMessagePackFormatter<NewArrayExpression> MSNewArray=>this;
//    public void Serialize(ref MessagePackWriter writer,NewArrayExpression? value,MessagePackSerializerOptions options){
//        if(value is null){
//            writer.WriteNil();
//            return;
//        }
//        writer.Write((byte)value.NodeType);
//        Serialize_Type(ref writer,value.Type.GetElementType(),options);
//        Serialize_T(ref writer,value.Expressions,options);
//    }
//    NewArrayExpression IMessagePackFormatter<NewArrayExpression>.Deserialize(ref MessagePackReader reader,MessagePackSerializerOptions options){
//        if(reader.TryReadNil()) return null!;
//        var NodeType=(ExpressionType)reader.ReadByte();
//        var type=Deserialize_Type(ref reader,options);
//        var expressions= Deserialize_T<Expression[]>(ref reader,options);
//        return NodeType switch{
//            ExpressionType.NewArrayBounds=>Expression.NewArrayBounds(type,expressions),
//            ExpressionType.NewArrayInit=>Expression.NewArrayInit(type,expressions),
//            _=>throw new NotImplementedException(options.ToString())
//        };
//    }
//}
