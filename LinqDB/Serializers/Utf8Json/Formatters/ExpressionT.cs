﻿
using Utf8Json;

using Expressions = System.Linq.Expressions;
namespace LinqDB.Serializers.Utf8Json.Formatters;
using O=IJsonFormatterResolver;
using Writer = JsonWriter;
using Reader = JsonReader;
public class ExpressionT<T>:IJsonFormatter<T>where T:Expressions.LambdaExpression {
    public static readonly ExpressionT<T>Instance=new();
    public void Serialize(ref Writer writer,T? value,O Resolver){
        if(writer.TryWriteNil(value))return;
        var ListParameter= Resolver.Serializer().Parameters;
        var ListParameter_Count=ListParameter.Count;
        var Parameters=value!.Parameters;
        ListParameter.AddRange(Parameters);
        writer.WriteBeginArray();
        writer.WriteType(value.Type);
        writer.WriteValueSeparator();
        writer.Serialize宣言Parameters(value.Parameters,Resolver);
        writer.WriteValueSeparator();
        Expression.Write(ref writer,value.Body,Resolver);
        writer.WriteValueSeparator();
        writer.WriteBoolean(value.TailCall);
        writer.WriteEndArray();
        ListParameter.RemoveRange(ListParameter_Count,Parameters.Count);
    }
    public T Deserialize(ref Reader reader,O Resolver){
        if(reader.TryReadNil())return null!;
        var ListParameter= Resolver.Serializer().Parameters;
        var ListParameter_Count=ListParameter.Count;
        reader.ReadIsBeginArrayWithVerify();
        var type = reader.ReadType();
        reader.ReadIsValueSeparatorWithVerify();
        var parameters= reader.Deserialize宣言Parameters(Resolver);
        reader.ReadIsValueSeparatorWithVerify();
        ListParameter.AddRange(parameters!);
        var body = Expression.Read(ref reader,Resolver);
        reader.ReadIsValueSeparatorWithVerify();
        var tailCall = reader.ReadBoolean();
        reader.ReadIsEndArrayWithVerify();
        ListParameter.RemoveRange(ListParameter_Count,parameters.Count);
        return(T)Expressions.Expression.Lambda(
            type,
            body,
            tailCall,
            parameters!
        );
    }
}
