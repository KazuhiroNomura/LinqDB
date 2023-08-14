using System.Linq.Expressions;
using MessagePack;
using MessagePack.Formatters;
using Utf8Json;
namespace LinqDB.Serializers.Formatters;
using static Common;
partial class ExpressionFormatter:IJsonFormatter<LabelExpression>,IMessagePackFormatter<LabelExpression>{
    private IJsonFormatter<LabelExpression> LabelExpression=>this;
    private IMessagePackFormatter<LabelExpression> MSLabelExpression=>this;
    public void Serialize(ref JsonWriter writer,LabelExpression? value,IJsonFormatterResolver Resolver){
        if(value is null){
            writer.WriteNull();
            return;
        }
        writer.WriteBeginArray();
        this.Serialize(ref writer,value.Target,Resolver);
        writer.WriteValueSeparator();
        this.Serialize(ref writer,value.DefaultValue,Resolver);
        writer.WriteEndArray();
    }
    LabelExpression IJsonFormatter<LabelExpression>.Deserialize(ref JsonReader reader,IJsonFormatterResolver Resolver){
        if(reader.ReadIsNull()) return null!;
        reader.ReadIsBeginArrayWithVerify();
        var target= this.LabelTarget.Deserialize(ref reader,Resolver);
        reader.ReadIsValueSeparatorWithVerify();
        var defaultValue=Deserialize_T<Expression>(ref reader,Resolver);
        reader.ReadIsEndArrayWithVerify();
        return Expression.Label(target,defaultValue);
    }
    public void Serialize(ref MessagePackWriter writer,LabelExpression? value,MessagePackSerializerOptions Resolver){
        if(value is null){
            writer.WriteNil();
            return;
        }
        this.Serialize(ref writer,value.Target,Resolver);
        this.Serialize(ref writer,value.DefaultValue,Resolver);
    }
    LabelExpression IMessagePackFormatter<LabelExpression>.Deserialize(ref MessagePackReader reader,MessagePackSerializerOptions Resolver){
        if(reader.TryReadNil()) return null!;
        var target= this.MSLabelTarget.Deserialize(ref reader,Resolver);
        var defaultValue=Deserialize_T<Expression>(ref reader,Resolver);
        return Expression.Label(target,defaultValue);
    }
}
