using System.Linq.Expressions;
using MessagePack;
using MessagePack.Formatters;
using Utf8Json;
namespace LinqDB.Serializers.Formatters;
partial class ExpressionFormatter:IJsonFormatter<ParameterExpression>,IMessagePackFormatter<ParameterExpression>{
    private IJsonFormatter<ParameterExpression> Parameter=>this;
    private IMessagePackFormatter<ParameterExpression> MSParameter=>this;

    public void Serialize(ref JsonWriter writer,ParameterExpression value,IJsonFormatterResolver Resolver) {
        writer.WriteInt32(this.ListParameter.LastIndexOf(value));
    }
    ParameterExpression IJsonFormatter<ParameterExpression>.Deserialize(ref JsonReader reader,IJsonFormatterResolver Resolver) {
        var index=reader.ReadInt32();
        var Parameter= this.ListParameter[index];
        return Parameter;
    }
    public void Serialize(ref MessagePackWriter writer,ParameterExpression value,MessagePackSerializerOptions Resolver){
        writer.WriteInt32(this.ListParameter.LastIndexOf(value));
    }
    ParameterExpression IMessagePackFormatter<ParameterExpression>.Deserialize(ref MessagePackReader reader,MessagePackSerializerOptions Resolver){
        var index=reader.ReadInt32();
        var Parameter= this.ListParameter[index];
        return Parameter;
    }
}
