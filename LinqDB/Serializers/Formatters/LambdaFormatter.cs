using System;
using System.Linq.Expressions;
using MessagePack;
using MessagePack.Formatters;
using Utf8Json;
namespace LinqDB.Serializers.Formatters;
using static Common;
partial class ExpressionJsonFormatter:IJsonFormatter<LambdaExpression>{
    private IJsonFormatter<LambdaExpression> Lambda=>this;
    public void Serialize(ref JsonWriter writer,LambdaExpression value,IJsonFormatterResolver Resolver) {
        var ListParameter=this.ListParameter;
        var ListParameter_Count=ListParameter.Count;
        var Parameters=value.Parameters;
        ListParameter.AddRange(Parameters);

        writer.WriteBeginArray();
        //this.Serialize(ref writer,value.Type,Resolver);
        Serialize_Type(ref writer,value.Type,Resolver);
        writer.WriteValueSeparator();
        this.Serialize宣言Parameters(ref writer,value.Parameters,Resolver);
        writer.WriteValueSeparator();
        this.Serialize(ref writer,value.Body,Resolver);
        writer.WriteValueSeparator();
        writer.WriteBoolean(value.TailCall);
        writer.WriteEndArray();
        
        ListParameter.RemoveRange(ListParameter_Count,Parameters.Count);
    }
    LambdaExpression IJsonFormatter<LambdaExpression>.Deserialize(ref JsonReader reader,IJsonFormatterResolver Resolver) {
        var ListParameter=this.ListParameter;
        var ListParameter_Count=ListParameter.Count;

        reader.ReadIsBeginArrayWithVerify();
        //var s=reader.ReadString();
        var type = Deserialize_Type(ref reader,Resolver);
        reader.ReadIsValueSeparatorWithVerify();
        var parameters = this.Deserialize宣言Parameters(ref reader,Resolver);
        ListParameter.AddRange(parameters);

        reader.ReadIsValueSeparatorWithVerify();
        var body =Deserialize_T<Expression>(ref reader,Resolver);
        reader.ReadIsValueSeparatorWithVerify();
        var tailCall = reader.ReadBoolean();
        reader.ReadIsEndArrayWithVerify();
        ListParameter.RemoveRange(ListParameter_Count,parameters.Count);
        return Expression.Lambda(
            type,
            body,
            tailCall,
            parameters
        );
    }
}
partial class ExpressionMessagePackFormatter:IMessagePackFormatter<LambdaExpression>{
    private IMessagePackFormatter<LambdaExpression> MSLambda=>this;
    public void Serialize(ref MessagePackWriter writer,LambdaExpression value,MessagePackSerializerOptions Resolver){
        var ListParameter=this.ListParameter;
        var ListParameter_Count=ListParameter.Count;
        var Parameters=value.Parameters;
        ListParameter.AddRange(Parameters);

        Serialize_Type(ref writer,value.Type,Resolver);
        this.Serialize宣言Parameters(ref writer,value.Parameters,Resolver);
        this.Serialize(ref writer,value.Body,Resolver);
        writer.Write(value.TailCall);
        
        ListParameter.RemoveRange(ListParameter_Count,Parameters.Count);
    }
    LambdaExpression IMessagePackFormatter<LambdaExpression>.Deserialize(ref MessagePackReader reader,MessagePackSerializerOptions Resolver){
        var ListParameter=this.ListParameter;
        var ListParameter_Count=ListParameter.Count;

        var type = Deserialize_Type(ref reader,Resolver);
        var parameters = this.Deserialize宣言Parameters(ref reader,Resolver);
        ListParameter.AddRange(parameters);

        var body =Deserialize_T<Expression>(ref reader,Resolver);
        var tailCall = reader.ReadBoolean();
        ListParameter.RemoveRange(ListParameter_Count,parameters.Length);
        return Expression.Lambda(
            type,
            body,
            tailCall,
            parameters
        );
    }
}
//partial class ExpressionFormatter<TLambdaExpression>:IJsonFormatter<TLambdaExpression>,IMessagePackFormatter<TLambdaExpression>where TLambdaExpression:LambdaExpression{
//    private readonly ExpressionFormatter _ExpressionFormatter;
//    //private readonly List<ParameterExpression> ListParameter;
//    public ExpressionFormatter(ExpressionFormatter ExpressionFormatter){
//        this._ExpressionFormatter=ExpressionFormatter;
//    }
//    private IJsonFormatter<TLambdaExpression> Lambda=>this;
//    private IMessagePackFormatter<TLambdaExpression> MSLambda=>this;
//    public void Serialize(ref JsonWriter writer,TLambdaExpression value,IJsonFormatterResolver Resolver) {
//        var ListParameter= this._ExpressionFormatter.ListParameter;
//        var ListParameter_Count=ListParameter.Count;
//        var Parameters=value.Parameters;
//        ListParameter.AddRange(Parameters);

//        writer.WriteBeginArray();
//        //this._ExpressionFormatter.Serialize(ref writer,value.Type,Resolver);
//        Serialize_T(ref writer,value.Type,Resolver);
//        writer.WriteValueSeparator();
//        this._ExpressionFormatter.Serialize宣言Parameters(ref writer,value.Parameters,Resolver);
//        writer.WriteValueSeparator();
//        this._ExpressionFormatter.Serialize(ref writer,value.Body,Resolver);
//        writer.WriteValueSeparator();
//        writer.WriteBoolean(value.TailCall);
//        writer.WriteEndArray();
        
//        ListParameter.RemoveRange(ListParameter_Count,Parameters.Count);
//    }
//    TLambdaExpression IJsonFormatter<TLambdaExpression>.Deserialize(ref JsonReader reader,IJsonFormatterResolver Resolver) {
//        var ListParameter= this._ExpressionFormatter.ListParameter;
//        var ListParameter_Count=ListParameter.Count;

//        reader.ReadIsBeginArrayWithVerify();
//        var type = Deserialize_Type(ref reader,Resolver);
//        reader.ReadIsValueSeparatorWithVerify();
//        var parameters = this._ExpressionFormatter.Deserialize宣言Parameters(ref reader,Resolver);
//        ListParameter.AddRange(parameters);

//        reader.ReadIsValueSeparatorWithVerify();
//        var body =Deserialize_T<Expression>(ref reader,Resolver);
//        reader.ReadIsValueSeparatorWithVerify();
//        var tailCall = reader.ReadBoolean();
//        reader.ReadIsEndArrayWithVerify();
//        ListParameter.RemoveRange(ListParameter_Count,parameters.Count);
//        var Func=typeof(TLambdaExpression).GetGenericArguments()[0];
//        return (TLambdaExpression)Expression.Lambda(
//            Func,
//            body,
//            tailCall,
//            parameters
//        );
//    }
//    public void Serialize(ref MessagePackWriter writer,TLambdaExpression value,MessagePackSerializerOptions Resolver){
//        var ListParameter= this._ExpressionFormatter.ListParameter;
//        var ListParameter_Count=ListParameter.Count;
//        var Parameters=value.Parameters;
//        ListParameter.AddRange(Parameters); 
//        Serialize_T(ref writer,value.Type,Resolver);
//        this._ExpressionFormatter.Serialize宣言Parameters(ref writer,value.Parameters,Resolver);
//        this._ExpressionFormatter.Serialize(ref writer,value.Body,Resolver);
//        writer.Write(true);
//        writer.Write(value.TailCall);        
//        ListParameter.RemoveRange(ListParameter_Count,Parameters.Count);
//    }
//    public TLambdaExpression Deserialize(ref MessagePackReader reader,MessagePackSerializerOptions Resolver){
//        var ListParameter= this._ExpressionFormatter.ListParameter;
//        var ListParameter_Count=ListParameter.Count;

//        var type = Deserialize_Type(ref reader,Resolver);
//        var parameters = this._ExpressionFormatter.Deserialize宣言Parameters(ref reader,Resolver);
//        ListParameter.AddRange(parameters);
//        var body =Deserialize_T<Expression>(ref reader,Resolver);
//        var tailCall = reader.ReadBoolean();
//        ListParameter.RemoveRange(ListParameter_Count,parameters.Length);
//        //var Func=typeof(TLambdaExpression).GetGenericArguments()[0];
//        return (TLambdaExpression)Expression.Lambda(
//            type,
//            body,
//            tailCall,
//            parameters
//        );
//    }
//}
