using Expressions = System.Linq.Expressions;
using MessagePack;
using MessagePack.Formatters;
using Utf8Json;
namespace LinqDB.Serializers.Formatters;
partial class ExpressionJsonFormatter:IJsonFormatter<Expressions.ParameterExpression>{
    private IJsonFormatter<Expressions.ParameterExpression> Parameter=>this;
    public void Serialize(ref JsonWriter writer,Expressions.ParameterExpression value,IJsonFormatterResolver Resolver) {
        writer.WriteInt32(this.ListParameter.LastIndexOf(value));
    }
    Expressions.ParameterExpression IJsonFormatter<Expressions.ParameterExpression>.Deserialize(ref JsonReader reader,IJsonFormatterResolver Resolver) {
        var index=reader.ReadInt32();
        var Parameter= this.ListParameter[index];
        return Parameter;
    }
}
partial class ExpressionMessagePackFormatter:IMessagePackFormatter<Expressions.ParameterExpression>{
    private IMessagePackFormatter<Expressions.ParameterExpression> MSParameter=>this;
    public void Serialize(ref MessagePackWriter writer,Expressions.ParameterExpression value,MessagePackSerializerOptions Resolver){
        writer.WriteInt32(this.ListParameter.LastIndexOf(value));
    }
    Expressions.ParameterExpression IMessagePackFormatter<Expressions.ParameterExpression>.Deserialize(ref MessagePackReader reader,MessagePackSerializerOptions Resolver){
        var index=reader.ReadInt32();
        var Parameter= this.ListParameter[index];
        return Parameter;
    }
}
