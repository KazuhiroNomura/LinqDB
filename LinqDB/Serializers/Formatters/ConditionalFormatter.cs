using Expressions=System.Linq.Expressions;
using MessagePack;
using MessagePack.Formatters;
using Utf8Json;
namespace LinqDB.Serializers.Formatters;
partial class ExpressionJsonFormatter:IJsonFormatter<Expressions.ConditionalExpression>{
    private IJsonFormatter<Expressions.ConditionalExpression> Conditional=>this;
    public void Serialize(ref JsonWriter writer,Expressions.ConditionalExpression? value,IJsonFormatterResolver Resolver){
    if(value is null){
        writer.WriteNull();
        return;
    }
    writer.WriteBeginArray();
    this.Serialize(ref writer,value.Test,Resolver);
    writer.WriteValueSeparator();
    this.Serialize(ref writer,value.IfTrue,Resolver);
    writer.WriteValueSeparator();
    this.Serialize(ref writer,value.IfFalse,Resolver);
    writer.WriteEndArray();
}
    Expressions.ConditionalExpression IJsonFormatter<Expressions.ConditionalExpression>.Deserialize(ref JsonReader reader,IJsonFormatterResolver Resolver){
        if(reader.ReadIsNull()) return null!;
        reader.ReadIsBeginArrayWithVerify();
        var test= this.Deserialize(ref reader,Resolver);
        reader.ReadIsValueSeparatorWithVerify();
        var ifTrue= this.Deserialize(ref reader,Resolver);
        reader.ReadIsValueSeparatorWithVerify();
        var ifFalse= this.Deserialize(ref reader,Resolver);
        reader.ReadIsEndArrayWithVerify();
        return Expressions.Expression.Condition(
            test,
            ifTrue,
            ifFalse
        );
    }
}

partial class ExpressionMessagePackFormatter:IMessagePackFormatter<Expressions.ConditionalExpression>{
    private IMessagePackFormatter<Expressions.ConditionalExpression> Conditional=>this;
    public void Serialize(ref MessagePackWriter writer,Expressions.ConditionalExpression? value,MessagePackSerializerOptions Resolver){
        if(value is null){
            writer.WriteNil();
            return;
        }
        this.Serialize(ref writer,value.Test,Resolver);
        this.Serialize(ref writer,value.IfTrue,Resolver);
        this.Serialize(ref writer,value.IfFalse,Resolver);
    }
    Expressions.ConditionalExpression IMessagePackFormatter<Expressions.ConditionalExpression>.Deserialize(ref MessagePackReader reader,MessagePackSerializerOptions Resolver){
        if(reader.TryReadNil()) return null!;
        var test= this.Deserialize(ref reader,Resolver);
        var ifTrue= this.Deserialize(ref reader,Resolver);
        var ifFalse= this.Deserialize(ref reader,Resolver);
        return Expressions.Expression.Condition(
            test,
            ifTrue,
            ifFalse
        );
    }
}
