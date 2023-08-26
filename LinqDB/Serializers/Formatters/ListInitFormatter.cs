using Expressions=System.Linq.Expressions;
using MessagePack;
using MessagePack.Formatters;
using Utf8Json;
namespace LinqDB.Serializers.Formatters;
using static Common;
partial class ExpressionJsonFormatter:IJsonFormatter<Expressions.ListInitExpression>{
    private IJsonFormatter<Expressions.ListInitExpression> ListInit=>this;
    public void Serialize(ref JsonWriter writer,Expressions.ListInitExpression value,IJsonFormatterResolver Resolver) {
        writer.WriteBeginArray();
        this.Serialize(ref writer,value.NewExpression,Resolver);
        writer.WriteValueSeparator();
        Serialize_T(ref writer,value.Initializers,Resolver);
        writer.WriteEndArray();
    }
    Expressions.ListInitExpression IJsonFormatter<Expressions.ListInitExpression>.Deserialize(ref JsonReader reader,IJsonFormatterResolver Resolver) {
        reader.ReadIsBeginArrayWithVerify();
        var New=this.New.Deserialize(ref reader,Resolver);
        reader.ReadIsValueSeparatorWithVerify();
        var Initializers=Deserialize_T<Expressions.ElementInit[]>(ref reader,Resolver);
        reader.ReadIsEndArrayWithVerify();
        return Expressions.Expression.ListInit(New,Initializers);
    }
}
partial class ExpressionMessagePackFormatter:IMessagePackFormatter<Expressions.ListInitExpression>{
    private IMessagePackFormatter<Expressions.ListInitExpression> MSListInit=>this;
    public void Serialize(ref MessagePackWriter writer,Expressions.ListInitExpression value,MessagePackSerializerOptions Resolver){
        this.Serialize(ref writer,value.NewExpression,Resolver);
        Serialize_T(ref writer,value.Initializers,Resolver);
    }
    Expressions.ListInitExpression IMessagePackFormatter<Expressions.ListInitExpression>.Deserialize(ref MessagePackReader reader,MessagePackSerializerOptions Resolver){
        var New=this.MSNew.Deserialize(ref reader,Resolver);
        var Initializers=Deserialize_T<Expressions.ElementInit[]>(ref reader,Resolver);
        return Expressions.Expression.ListInit(New,Initializers);
    }
}
