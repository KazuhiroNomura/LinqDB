using Expressions=System.Linq.Expressions;
using MessagePack;
using MessagePack.Formatters;
using Utf8Json;
namespace LinqDB.Serializers.Formatters;
using static Common;
partial class ExpressionJsonFormatter:IJsonFormatter<Expressions.LambdaExpression>{
    private IJsonFormatter<Expressions.LambdaExpression> Lambda=>this;
    public void Serialize(ref JsonWriter writer,Expressions.LambdaExpression value,IJsonFormatterResolver Resolver) {
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
    Expressions.LambdaExpression IJsonFormatter<Expressions.LambdaExpression>.Deserialize(ref JsonReader reader,IJsonFormatterResolver Resolver) {
        var ListParameter=this.ListParameter;
        var ListParameter_Count=ListParameter.Count;

        reader.ReadIsBeginArrayWithVerify();
        //var s=reader.ReadString();
        var type = Deserialize_Type(ref reader,Resolver);
        reader.ReadIsValueSeparatorWithVerify();
        var parameters = this.Deserialize宣言Parameters(ref reader,Resolver);
        ListParameter.AddRange(parameters);

        reader.ReadIsValueSeparatorWithVerify();
        var body =Deserialize_T<Expressions.Expression>(ref reader,Resolver);
        reader.ReadIsValueSeparatorWithVerify();
        var tailCall = reader.ReadBoolean();
        reader.ReadIsEndArrayWithVerify();
        ListParameter.RemoveRange(ListParameter_Count,parameters.Count);
        return Expressions.Expression.Lambda(
            type,
            body,
            tailCall,
            parameters
        );
    }
}
partial class ExpressionMessagePackFormatter:IMessagePackFormatter<Expressions.LambdaExpression>{
    private IMessagePackFormatter<Expressions.LambdaExpression> MSLambda=>this;
    public void Serialize(ref MessagePackWriter writer,Expressions.LambdaExpression value,MessagePackSerializerOptions Resolver){
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
    Expressions.LambdaExpression IMessagePackFormatter<Expressions.LambdaExpression>.Deserialize(ref MessagePackReader reader,MessagePackSerializerOptions Resolver){
        var ListParameter=this.ListParameter;
        var ListParameter_Count=ListParameter.Count;

        var type = Deserialize_Type(ref reader,Resolver);
        var parameters = this.Deserialize宣言Parameters(ref reader,Resolver);
        ListParameter.AddRange(parameters);

        var body =Deserialize_T<Expressions.Expression>(ref reader,Resolver);
        var tailCall = reader.ReadBoolean();
        ListParameter.RemoveRange(ListParameter_Count,parameters.Length);
        return Expressions.Expression.Lambda(
            type,
            body,
            tailCall,
            parameters
        );
    }
}
