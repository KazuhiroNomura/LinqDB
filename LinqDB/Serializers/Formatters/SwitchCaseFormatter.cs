using System.Linq.Expressions;
using MessagePack;
using MessagePack.Formatters;
using Utf8Json;
namespace LinqDB.Serializers.Formatters;
using static Common;
partial class ExpressionFormatter:IJsonFormatter<SwitchCase>,IMessagePackFormatter<SwitchCase>{
    private IJsonFormatter<SwitchCase> SwitchCase=>this;
    public void Serialize(ref JsonWriter writer,SwitchCase value,IJsonFormatterResolver Resolver) {
        writer.WriteBeginArray();
        Serialize_T(ref writer,value.TestValues,Resolver);
        writer.WriteValueSeparator();
        this.Serialize(ref writer,value.Body,Resolver);
        writer.WriteEndArray();
    }
    SwitchCase IJsonFormatter<SwitchCase>.Deserialize(ref JsonReader reader,IJsonFormatterResolver Resolver) {
        reader.ReadIsBeginArrayWithVerify();
        var testValues=Deserialize_T<Expression[]>(ref reader,Resolver);
        reader.ReadIsValueSeparatorWithVerify();
        var body= this.Deserialize(ref reader,Resolver);
        reader.ReadIsEndArrayWithVerify();
        return Expression.SwitchCase(body,testValues);
    }
    public void Serialize(ref MessagePackWriter writer,SwitchCase value,MessagePackSerializerOptions Resolver){
        Serialize_T(ref writer,value.TestValues,Resolver);
        this.Serialize(ref writer,value.Body,Resolver);
    }
    SwitchCase IMessagePackFormatter<SwitchCase>.Deserialize(ref MessagePackReader reader,MessagePackSerializerOptions Resolver){
        var testValues=Deserialize_T<Expression[]>(ref reader,Resolver);
        var body= this.Deserialize(ref reader,Resolver);
        return Expression.SwitchCase(body,testValues);
    }
}
