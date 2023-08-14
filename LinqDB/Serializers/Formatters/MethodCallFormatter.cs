using System.Linq.Expressions;
using MessagePack;
using MessagePack.Formatters;
using Utf8Json;
namespace LinqDB.Serializers.Formatters;
using static Common;
partial class ExpressionFormatter:IJsonFormatter<MethodCallExpression>,IMessagePackFormatter<MethodCallExpression>{
    private IJsonFormatter<MethodCallExpression> MethodCall=>this;
    private IMessagePackFormatter<MethodCallExpression> MSMethodCall=>this;
    public void Serialize(ref JsonWriter writer,MethodCallExpression? value,IJsonFormatterResolver Resolver){
        if(value is null){
            writer.WriteNull();
            return;
        }
        writer.WriteBeginArray();
        var Method=value.Method;
        this.Serialize(ref writer,Method,Resolver);
        writer.WriteValueSeparator();
        if(!Method.IsStatic){
            this.Serialize(ref writer,value.Object!,Resolver);
            writer.WriteValueSeparator();
        }
        Serialize_T(ref writer,value.Arguments,Resolver);
        writer.WriteEndArray();
    }
    MethodCallExpression IJsonFormatter<MethodCallExpression>.Deserialize(ref JsonReader reader,IJsonFormatterResolver Resolver){
        if(reader.ReadIsNull()) return null!;
        reader.ReadIsBeginArrayWithVerify();
        var method= this.MethodInfo.Deserialize(ref reader,Resolver);
        reader.ReadIsValueSeparatorWithVerify();
        if(method.IsStatic){
            var arguments=Deserialize_T<Expression[]>(ref reader,Resolver);
            reader.ReadIsEndArrayWithVerify();
            return Expression.Call(
                method,
                arguments
            );
        } else{
            var instance= this.Deserialize(ref reader,Resolver);
            reader.ReadIsValueSeparatorWithVerify();
            var arguments=Deserialize_T<Expression[]>(ref reader,Resolver);
            reader.ReadIsEndArrayWithVerify();
            return Expression.Call(
                instance,
                method,
                arguments
            );
        }
    }
    public void Serialize(ref MessagePackWriter writer,MethodCallExpression? value,MessagePackSerializerOptions Resolver){
        if(value is null){
            writer.WriteNil();
            return;
        }
        var Method=value.Method;
        this.Serialize(ref writer,Method,Resolver);
        if(!Method.IsStatic){
            this.Serialize(ref writer,value.Object!,Resolver);
        }
        Serialize_T(ref writer,value.Arguments,Resolver);
    }
    MethodCallExpression IMessagePackFormatter<MethodCallExpression>.Deserialize(ref MessagePackReader reader,MessagePackSerializerOptions Resolver){
        if(reader.TryReadNil()) return null!;
        var method= this.MSMethodInfo.Deserialize(ref reader,Resolver);
        if(method.IsStatic){
            var arguments=Deserialize_T<Expression[]>(ref reader,Resolver);
            return Expression.Call(
                method,
                arguments
            );
        } else{
            var instance= this.Deserialize(ref reader,Resolver);
            var arguments=Deserialize_T<Expression[]>(ref reader,Resolver);
            return Expression.Call(
                instance,
                method,
                arguments
            );
        }
    }
}
