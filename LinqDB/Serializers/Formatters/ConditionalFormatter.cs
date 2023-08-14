using System.Linq.Expressions;
using MessagePack;
using MessagePack.Formatters;
using Utf8Json;
namespace LinqDB.Serializers.Formatters;
partial class ExpressionFormatter:IJsonFormatter<ConditionalExpression>,IMessagePackFormatter<ConditionalExpression>{
    //public static readonly ConditionalFormatter Instance=new();
    private IJsonFormatter<ConditionalExpression> Conditional=>this;
    private IMessagePackFormatter<ConditionalExpression> MSConditional=>this;
    public void Serialize(ref JsonWriter writer,ConditionalExpression? value,IJsonFormatterResolver Resolver){
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
    ConditionalExpression IJsonFormatter<ConditionalExpression>.Deserialize(ref JsonReader reader,IJsonFormatterResolver Resolver){
        if(reader.ReadIsNull()) return null!;
        reader.ReadIsBeginArrayWithVerify();
        var test= this.Deserialize(ref reader,Resolver);
        reader.ReadIsValueSeparatorWithVerify();
        var ifTrue= this.Deserialize(ref reader,Resolver);
        reader.ReadIsValueSeparatorWithVerify();
        var ifFalse= this.Deserialize(ref reader,Resolver);
        reader.ReadIsEndArrayWithVerify();
        return Expression.Condition(
            test,
            ifTrue,
            ifFalse
        );
    }
    public void Serialize(ref MessagePackWriter writer,ConditionalExpression? value,MessagePackSerializerOptions Resolver){
        if(value is null){
            writer.WriteNil();
            return;
        }
        this.Serialize(ref writer,value.Test,Resolver);
        this.Serialize(ref writer,value.IfTrue,Resolver);
        this.Serialize(ref writer,value.IfFalse,Resolver);
    }
    ConditionalExpression IMessagePackFormatter<ConditionalExpression>.Deserialize(ref MessagePackReader reader,MessagePackSerializerOptions Resolver){
        if(reader.TryReadNil()) return null!;
        var test= this.Deserialize(ref reader,Resolver);
        var ifTrue= this.Deserialize(ref reader,Resolver);
        var ifFalse= this.Deserialize(ref reader,Resolver);
        return Expression.Condition(
            test,
            ifTrue,
            ifFalse
        );
    }
}
