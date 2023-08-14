using System.Linq.Expressions;
using MessagePack;
using MessagePack.Formatters;
using Utf8Json;
namespace LinqDB.Serializers.Formatters;
using static Common;
partial class ExpressionJsonFormatter:IJsonFormatter<ListInitExpression>{
    private IJsonFormatter<ListInitExpression> ListInit=>this;
    public void Serialize(ref JsonWriter writer,ListInitExpression value,IJsonFormatterResolver Resolver) {
        writer.WriteBeginArray();
        this.Serialize(ref writer,value.NewExpression,Resolver);
        writer.WriteValueSeparator();
        Serialize_T(ref writer,value.Initializers,Resolver);
        writer.WriteEndArray();
    }
    ListInitExpression IJsonFormatter<ListInitExpression>.Deserialize(ref JsonReader reader,IJsonFormatterResolver Resolver) {
        reader.ReadIsBeginArrayWithVerify();
        var New=this.New.Deserialize(ref reader,Resolver);
        reader.ReadIsValueSeparatorWithVerify();
        var Initializers=Deserialize_T<ElementInit[]>(ref reader,Resolver);
        reader.ReadIsEndArrayWithVerify();
        return Expression.ListInit(New,Initializers);
    }
}
partial class ExpressionMessagePackFormatter:IMessagePackFormatter<ListInitExpression>{
    private IMessagePackFormatter<ListInitExpression> MSListInit=>this;
    public void Serialize(ref MessagePackWriter writer,ListInitExpression value,MessagePackSerializerOptions Resolver){
        this.Serialize(ref writer,value.NewExpression,Resolver);
        Serialize_T(ref writer,value.Initializers,Resolver);
    }
    ListInitExpression IMessagePackFormatter<ListInitExpression>.Deserialize(ref MessagePackReader reader,MessagePackSerializerOptions Resolver){
        var New=this.MSNew.Deserialize(ref reader,Resolver);
        var Initializers=Deserialize_T<ElementInit[]>(ref reader,Resolver);
        return Expression.ListInit(New,Initializers);
    }
}
