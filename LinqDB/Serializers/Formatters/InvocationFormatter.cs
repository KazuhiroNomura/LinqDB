using System.Linq.Expressions;
using MessagePack;
using MessagePack.Formatters;
using Utf8Json;
namespace LinqDB.Serializers.Formatters;
using static Common;
partial class ExpressionJsonFormatter:IJsonFormatter<InvocationExpression>{
    private IJsonFormatter<InvocationExpression> Invocation=>this;
    public void Serialize(ref JsonWriter writer,InvocationExpression? value,IJsonFormatterResolver Resolver){
        if(value is null){
            writer.WriteNull();
            return;
        }
        writer.WriteBeginArray();
        this.Serialize(ref writer,value.Expression,Resolver);
        writer.WriteValueSeparator();
        Serialize_T(ref writer,value.Arguments,Resolver);
        writer.WriteEndArray();
    }
    InvocationExpression IJsonFormatter<InvocationExpression>.Deserialize(ref JsonReader reader,IJsonFormatterResolver Resolver){
        if(reader.ReadIsNull()) return null!;
        reader.ReadIsBeginArrayWithVerify();
        //var NodeTypeName=reader.ReadString();
        //reader.ReadIsValueSeparatorWithVerify();
        //var NodeType=Enum.Parse<ExpressionType>(NodeTypeName);
        var expression= this.Deserialize(ref reader,Resolver);
        reader.ReadIsValueSeparatorWithVerify();
        var arguments=Deserialize_T<Expression[]>(ref reader,Resolver);
        reader.ReadIsEndArrayWithVerify();
        return Expression.Invoke(expression,arguments);
    }
}
partial class ExpressionMessagePackFormatter:IMessagePackFormatter<InvocationExpression>{
    private IMessagePackFormatter<InvocationExpression> MSInvocation=>this;
    public void Serialize(ref MessagePackWriter writer,InvocationExpression? value,MessagePackSerializerOptions Resolver){
        if(value is null){
            writer.WriteNil();
            return;
        }
        this.Serialize(ref writer,value.Expression,Resolver);
        Serialize_T(ref writer,value.Arguments,Resolver);
    }
    InvocationExpression IMessagePackFormatter<InvocationExpression>.Deserialize(ref MessagePackReader reader,MessagePackSerializerOptions Resolver){
        if(reader.TryReadNil()) return null!;
        var expression= this.Deserialize(ref reader,Resolver);
        var arguments=Deserialize_T<Expression[]>(ref reader,Resolver);
        return Expression.Invoke(expression,arguments);
    }
}
