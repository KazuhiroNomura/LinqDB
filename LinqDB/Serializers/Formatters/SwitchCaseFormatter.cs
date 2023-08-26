using Expressions=System.Linq.Expressions;
using MessagePack;
using MessagePack.Formatters;
using Utf8Json;
namespace LinqDB.Serializers.Formatters;
using static Common;
partial class ExpressionJsonFormatter:IJsonFormatter<Expressions.SwitchCase>{
    private IJsonFormatter<Expressions.SwitchCase> SwitchCase=>this;
    public void Serialize(ref JsonWriter writer,Expressions.SwitchCase value,IJsonFormatterResolver Resolver) {
        writer.WriteBeginArray();
        Serialize_T(ref writer,value.TestValues,Resolver);
        writer.WriteValueSeparator();
        this.Serialize(ref writer,value.Body,Resolver);
        writer.WriteEndArray();
    }
    Expressions.SwitchCase IJsonFormatter<Expressions.SwitchCase>.Deserialize(ref JsonReader reader,IJsonFormatterResolver Resolver) {
        reader.ReadIsBeginArrayWithVerify();
        var testValues=Deserialize_T<Expressions.Expression[]>(ref reader,Resolver);
        reader.ReadIsValueSeparatorWithVerify();
        var body= this.Deserialize(ref reader,Resolver);
        reader.ReadIsEndArrayWithVerify();
        return Expressions.Expression.SwitchCase(body,testValues);
    }
}
partial class ExpressionMessagePackFormatter:IMessagePackFormatter<Expressions.SwitchCase>{
    public void Serialize(ref MessagePackWriter writer,Expressions.SwitchCase value,MessagePackSerializerOptions Resolver){
        Serialize_T(ref writer,value.TestValues,Resolver);
        this.Serialize(ref writer,value.Body,Resolver);
    }
    Expressions.SwitchCase IMessagePackFormatter<Expressions.SwitchCase>.Deserialize(ref MessagePackReader reader,MessagePackSerializerOptions Resolver){
        var testValues=Deserialize_T<Expressions.Expression[]>(ref reader,Resolver);
        var body= this.Deserialize(ref reader,Resolver);
        return Expressions.Expression.SwitchCase(body,testValues);
    }
}
