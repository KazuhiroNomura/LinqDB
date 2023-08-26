﻿using Expressions=System.Linq.Expressions;
using MessagePack;
using MessagePack.Formatters;
using Utf8Json;
namespace LinqDB.Serializers.Formatters;
using static Common;
partial class ExpressionJsonFormatter:IJsonFormatter<Expressions.BlockExpression>{
    private IJsonFormatter<Expressions.BlockExpression> Block=>this;
    public void Serialize(ref JsonWriter writer,Expressions.BlockExpression value,IJsonFormatterResolver Resolver) {
        var ListParameter=this.ListParameter;
        var ListParameter_Count=ListParameter.Count;
        var Variables=value.Variables;
        ListParameter.AddRange(Variables);
        writer.WriteBeginArray();
        //this.Serialize(ref writer,value.Type,Resolver);
        Serialize_Type(ref writer,value.Type,Resolver);
        writer.WriteValueSeparator();
        this.Serialize宣言Parameters(ref writer,value.Variables,Resolver);
        writer.WriteValueSeparator();
        Serialize_T(ref writer,value.Expressions,Resolver);
        writer.WriteEndArray();
        ListParameter.RemoveRange(ListParameter_Count,Variables.Count);
    }
    Expressions.BlockExpression IJsonFormatter<Expressions.BlockExpression>.Deserialize(ref JsonReader reader,IJsonFormatterResolver Resolver) {
        var ListParameter=this.ListParameter;
        var ListParameter_Count=ListParameter.Count;
        reader.ReadIsBeginArrayWithVerify();
        var type=Deserialize_Type(ref reader,Resolver);
        reader.ReadIsValueSeparatorWithVerify();
        var variables= this.Deserialize宣言Parameters(ref reader,Resolver);
        ListParameter.AddRange(variables);
        reader.ReadIsValueSeparatorWithVerify();
        var expressions=Deserialize_T<Expressions.Expression[]>(ref reader,Resolver);
        reader.ReadIsEndArrayWithVerify();
        ListParameter.RemoveRange(ListParameter_Count,variables.Count);
        return Expressions.Expression.Block(type,variables,expressions);
    }
}
partial class ExpressionMessagePackFormatter:IMessagePackFormatter<Expressions.BlockExpression>{
    private IMessagePackFormatter<Expressions.BlockExpression> Block=>this;
    public void Serialize(ref MessagePackWriter writer,Expressions.BlockExpression value,MessagePackSerializerOptions Resolver){
        var ListParameter=this.ListParameter;
        var ListParameter_Count=ListParameter.Count;
        var Variables=value.Variables;
        ListParameter.AddRange(Variables);
        Serialize_Type(ref writer,value.Type,Resolver);
        this.Serialize宣言Parameters(ref writer,value.Variables,Resolver);
        Serialize_T(ref writer,value.Expressions,Resolver);
        ListParameter.RemoveRange(ListParameter_Count,Variables.Count);
    }
    Expressions.BlockExpression IMessagePackFormatter<Expressions.BlockExpression>.Deserialize(ref MessagePackReader reader,MessagePackSerializerOptions Resolver){
        var ListParameter=this.ListParameter;
        var ListParameter_Count=ListParameter.Count;
        var type=Deserialize_Type(ref reader,Resolver);
        var variables= this.Deserialize宣言Parameters(ref reader,Resolver);
        ListParameter.AddRange(variables);
        var expressions=Deserialize_T<Expressions.Expression[]>(ref reader,Resolver);
        ListParameter.RemoveRange(ListParameter_Count,variables.Length);
        return Expressions.Expression.Block(type,variables,expressions);
    }
}
