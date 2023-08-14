using System.Linq.Expressions;
using MessagePack;
using MessagePack.Formatters;
using Utf8Json;
namespace LinqDB.Serializers.Formatters;
using static Common;
partial class ExpressionFormatter:IJsonFormatter<TryExpression>,IMessagePackFormatter<TryExpression>{
    private IJsonFormatter<TryExpression> Try=>this;
    private IMessagePackFormatter<TryExpression> MSTry=>this;
    public void Serialize(ref JsonWriter writer,TryExpression value,IJsonFormatterResolver Resolver) {
        writer.WriteBeginArray();
        this.Serialize(ref writer,value.Body,Resolver);
        writer.WriteValueSeparator();
        this.Serialize(ref writer,value.Finally,Resolver);
        writer.WriteValueSeparator();
        Serialize_T(ref writer,value.Handlers,Resolver);
        writer.WriteEndArray();
    }
    TryExpression IJsonFormatter<TryExpression>.Deserialize(ref JsonReader reader,IJsonFormatterResolver Resolver) {
        reader.ReadIsBeginArrayWithVerify();
        var body= this.Deserialize(ref reader,Resolver);
        reader.ReadIsValueSeparatorWithVerify();
        var @finally= this.Deserialize(ref reader,Resolver);
        reader.ReadIsValueSeparatorWithVerify();
        var handlers=Deserialize_T<CatchBlock[]>(ref reader,Resolver);
        reader.ReadIsEndArrayWithVerify();
        return Expression.TryCatchFinally(body,@finally,handlers);
    }
    public void Serialize(ref MessagePackWriter writer,TryExpression value,MessagePackSerializerOptions Resolver){
        this.Serialize(ref writer,value.Body,Resolver);
        this.Serialize(ref writer,value.Finally,Resolver);
        Serialize_T(ref writer,value.Handlers,Resolver);
        //writer.Write("ABC");
    }
    TryExpression IMessagePackFormatter<TryExpression>.Deserialize(ref MessagePackReader reader,MessagePackSerializerOptions Resolver){
        var body= this.Deserialize(ref reader,Resolver);
        //var s=reader.ReadString();
        var @finally= this.Deserialize(ref reader,Resolver);
        var handlers=Deserialize_T<CatchBlock[]>(ref reader,Resolver);
        if(handlers is null)
            return Expression.TryFinally(body,@finally);
        //return (TryExpression)body;
        return Expression.TryCatchFinally(body,@finally,handlers);
    }
}
