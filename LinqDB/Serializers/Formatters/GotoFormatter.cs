using System;
using System.Linq.Expressions;
using MessagePack;
using MessagePack.Formatters;
using Utf8Json;
namespace LinqDB.Serializers.Formatters;
using static Common;
partial class ExpressionJsonFormatter:IJsonFormatter<GotoExpression>{
    private IJsonFormatter<GotoExpression> Goto=>this;
    public void Serialize(ref JsonWriter writer,GotoExpression value,IJsonFormatterResolver Resolver){
        writer.WriteBeginArray();
        writer.WriteInt32((int)value.Kind);
        writer.WriteValueSeparator();
        this.Serialize(ref writer,value.Target,Resolver);
        writer.WriteValueSeparator();
        this.Serialize(ref writer,value.Value,Resolver);
        writer.WriteValueSeparator();
        //this.Serialize(ref writer,value.Type,Resolver);
        Serialize_Type(ref writer,value.Type,Resolver);
        writer.WriteEndArray();
    }
    GotoExpression IJsonFormatter<GotoExpression>.Deserialize(ref JsonReader reader,IJsonFormatterResolver Resolver){
        reader.ReadIsBeginArrayWithVerify();

        var kind=(GotoExpressionKind)reader.ReadInt32();
        reader.ReadIsValueSeparatorWithVerify();
        //var target=Deserialize_T<LabelTarget>(ref reader,Resolver);
        var target= this.LabelTarget.Deserialize(ref reader,Resolver);
        reader.ReadIsValueSeparatorWithVerify();
        var value=Deserialize_T<Expression>(ref reader,Resolver);
        reader.ReadIsValueSeparatorWithVerify();
        var type=Deserialize_Type(ref reader,Resolver);
        reader.ReadIsEndArrayWithVerify();
        return Expression.MakeGoto(kind,target,value,type);
    }
}
partial class ExpressionMessagePackFormatter:IMessagePackFormatter<GotoExpression>{
    private IMessagePackFormatter<GotoExpression> Goto=>this;
    public void Serialize(ref MessagePackWriter writer,GotoExpression value,MessagePackSerializerOptions Resolver){
        writer.Write((byte)value.Kind);
        this.Serialize(ref writer,value.Target,Resolver);
        this.Serialize(ref writer,value.Value,Resolver);
        Serialize_Type(ref writer,value.Type,Resolver);
    }
    GotoExpression IMessagePackFormatter<GotoExpression>.Deserialize(ref MessagePackReader reader,MessagePackSerializerOptions Resolver){
        var kind=(GotoExpressionKind)reader.ReadByte();
        var target= this.MSLabelTarget.Deserialize(ref reader,Resolver);
        var value=Deserialize_T<Expression>(ref reader,Resolver);
        var type=Deserialize_Type(ref reader,Resolver);
        return Expression.MakeGoto(kind,target,value,type);
    }
}