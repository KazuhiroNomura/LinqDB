using Expressions=System.Linq.Expressions;
using MessagePack;
using MessagePack.Formatters;
using Utf8Json;
namespace LinqDB.Serializers.Formatters;
using static Common;
partial class ExpressionJsonFormatter:IJsonFormatter<Expressions.LabelExpression>{
    private IJsonFormatter<Expressions.LabelExpression> LabelExpression=>this;
    public void Serialize(ref JsonWriter writer,Expressions.LabelExpression? value,IJsonFormatterResolver Resolver){
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
    Expressions.LabelExpression IJsonFormatter<Expressions.LabelExpression>.Deserialize(ref JsonReader reader,IJsonFormatterResolver Resolver){
        if(reader.ReadIsNull()) return null!;
        reader.ReadIsBeginArrayWithVerify();
        var target= this.LabelTarget.Deserialize(ref reader,Resolver);
        reader.ReadIsValueSeparatorWithVerify();
        var defaultValue=Deserialize_T<Expressions.Expression>(ref reader,Resolver);
        reader.ReadIsEndArrayWithVerify();
        return Expressions.Expression.Label(target,defaultValue);
    }
}
partial class ExpressionMessagePackFormatter:IMessagePackFormatter<Expressions.LabelExpression>{
    private IMessagePackFormatter<Expressions.LabelExpression> LabelExpression=>this;
    public void Serialize(ref MessagePackWriter writer,Expressions.LabelExpression? value,MessagePackSerializerOptions Resolver){
        if(value is null){
            writer.WriteNil();
            return;
        }
        this.Serialize(ref writer,value.Target,Resolver);
        this.Serialize(ref writer,value.DefaultValue,Resolver);
    }
    Expressions.LabelExpression IMessagePackFormatter<Expressions.LabelExpression>.Deserialize(ref MessagePackReader reader,MessagePackSerializerOptions Resolver){
        if(reader.TryReadNil()) return null!;
        var target= this.MSLabelTarget.Deserialize(ref reader,Resolver);
        var defaultValue=Deserialize_T<Expressions.Expression>(ref reader,Resolver);
        return Expressions.Expression.Label(target,defaultValue);
    }
}
