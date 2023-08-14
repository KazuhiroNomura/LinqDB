﻿using System;
using System.Linq.Expressions;
using MessagePack;
using MessagePack.Formatters;
using Utf8Json;
namespace LinqDB.Serializers.Formatters;
using static Common;

partial class ExpressionJsonFormatter<TLambdaExpression>:IJsonFormatter<TLambdaExpression>where TLambdaExpression:LambdaExpression{
    private readonly ExpressionJsonFormatter Instance;
    //private readonly List<ParameterExpression> ListParameter;
    public ExpressionJsonFormatter(ExpressionJsonFormatter Instance)=>this.Instance=Instance;
    public void Serialize(ref JsonWriter writer,TLambdaExpression value,IJsonFormatterResolver Resolver) {
        var ListParameter= this.Instance.ListParameter;
        var ListParameter_Count=ListParameter.Count;
        var Parameters=value.Parameters;
        ListParameter.AddRange(Parameters);

        writer.WriteBeginArray();
        //this._ExpressionFormatter.Serialize(ref writer,value.Type,Resolver);
        Serialize_Type(ref writer,value.Type,Resolver);
        writer.WriteValueSeparator();
        this.Instance.Serialize宣言Parameters(ref writer,value.Parameters,Resolver);
        writer.WriteValueSeparator();
        this.Instance.Serialize(ref writer,value.Body,Resolver);
        writer.WriteValueSeparator();
        writer.WriteBoolean(value.TailCall);
        writer.WriteEndArray();
        
        ListParameter.RemoveRange(ListParameter_Count,Parameters.Count);
    }
    TLambdaExpression IJsonFormatter<TLambdaExpression>.Deserialize(ref JsonReader reader,IJsonFormatterResolver Resolver) {
        var ListParameter= this.Instance.ListParameter;
        var ListParameter_Count=ListParameter.Count;

        reader.ReadIsBeginArrayWithVerify();
        var type = Deserialize_Type(ref reader,Resolver);
        reader.ReadIsValueSeparatorWithVerify();
        var parameters = this.Instance.Deserialize宣言Parameters(ref reader,Resolver);
        ListParameter.AddRange(parameters);

        reader.ReadIsValueSeparatorWithVerify();
        var body =Deserialize_T<Expression>(ref reader,Resolver);
        reader.ReadIsValueSeparatorWithVerify();
        var tailCall = reader.ReadBoolean();
        reader.ReadIsEndArrayWithVerify();
        ListParameter.RemoveRange(ListParameter_Count,parameters.Count);
        //var Func=typeof(TLambdaExpression).GetGenericArguments()[0];
        return (TLambdaExpression)Expression.Lambda(
            type,
            body,
            tailCall,
            parameters
        );
    }
}
partial class ExpressionMessagePackFormatter<TLambdaExpression>:IMessagePackFormatter<TLambdaExpression>where TLambdaExpression:LambdaExpression{
    private readonly ExpressionMessagePackFormatter Instance;
    public ExpressionMessagePackFormatter(ExpressionMessagePackFormatter Instance)=>this.Instance=Instance;
    public void Serialize(ref MessagePackWriter writer,TLambdaExpression value,MessagePackSerializerOptions Resolver){
        var ListParameter= this.Instance.ListParameter;
        var ListParameter_Count=ListParameter.Count;
        var Parameters=value.Parameters;
        ListParameter.AddRange(Parameters); 
        Serialize_Type(ref writer,value.Type,Resolver);
        this.Instance.Serialize宣言Parameters(ref writer,value.Parameters,Resolver);
        this.Instance.Serialize(ref writer,value.Body,Resolver);
        writer.Write(true);
        writer.Write(value.TailCall);        
        ListParameter.RemoveRange(ListParameter_Count,Parameters.Count);
    }
    public TLambdaExpression Deserialize(ref MessagePackReader reader,MessagePackSerializerOptions Resolver){
        var ListParameter= this.Instance.ListParameter;
        var ListParameter_Count=ListParameter.Count;
        var type=Deserialize_Type(ref reader,Resolver);
        //var type = Deserialize_Type(ref reader,Resolver);
        var parameters = this.Instance.Deserialize宣言Parameters(ref reader,Resolver);
        ListParameter.AddRange(parameters);
        var body =Deserialize_T<Expression>(ref reader,Resolver);
        var tailCall = reader.ReadBoolean();
        ListParameter.RemoveRange(ListParameter_Count,parameters.Length);
        return (TLambdaExpression)Expression.Lambda(
            type,
            body,
            tailCall,
            parameters
        );
    }
}
