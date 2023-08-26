using Expressions=System.Linq.Expressions;
using MessagePack;
using MessagePack.Formatters;
using Utf8Json;
namespace LinqDB.Serializers.Formatters;
using static Common;
partial class ExpressionJsonFormatter:IJsonFormatter<Expressions.InvocationExpression>{
    private IJsonFormatter<Expressions.InvocationExpression> Invocation=>this;
    public void Serialize(ref JsonWriter writer,Expressions.InvocationExpression? value,IJsonFormatterResolver Resolver){
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
    Expressions.InvocationExpression IJsonFormatter<Expressions.InvocationExpression>.Deserialize(ref JsonReader reader,IJsonFormatterResolver Resolver){
        if(reader.ReadIsNull()) return null!;
        reader.ReadIsBeginArrayWithVerify();
        //var NodeTypeName=reader.ReadString();
        //reader.ReadIsValueSeparatorWithVerify();
        //var NodeType=Enum.Parse<ExpressionType>(NodeTypeName);
        var expression= this.Deserialize(ref reader,Resolver);
        reader.ReadIsValueSeparatorWithVerify();
        var arguments=Deserialize_T<Expressions.Expression[]>(ref reader,Resolver);
        reader.ReadIsEndArrayWithVerify();
        return Expressions.Expression.Invoke(expression,arguments);
    }
}
partial class ExpressionMessagePackFormatter:IMessagePackFormatter<Expressions.InvocationExpression>{
    private IMessagePackFormatter<Expressions.InvocationExpression> MSInvocation=>this;
    public void Serialize(ref MessagePackWriter writer,Expressions.InvocationExpression? value,MessagePackSerializerOptions Resolver){
        if(value is null){
            writer.WriteNil();
            return;
        }
        this.Serialize(ref writer,value.Expression,Resolver);
        Serialize_T(ref writer,value.Arguments,Resolver);
    }
    Expressions.InvocationExpression IMessagePackFormatter<Expressions.InvocationExpression>.Deserialize(ref MessagePackReader reader,MessagePackSerializerOptions Resolver){
        if(reader.TryReadNil()) return null!;
        var expression= this.Deserialize(ref reader,Resolver);
        var arguments=Deserialize_T<Expressions.Expression[]>(ref reader,Resolver);
        return Expressions.Expression.Invoke(expression,arguments);
    }
}
