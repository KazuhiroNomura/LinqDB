using Expressions=System.Linq.Expressions;
using MessagePack;
using MessagePack.Formatters;
using Utf8Json;
namespace LinqDB.Serializers.Formatters;
using static Common;
partial class ExpressionJsonFormatter:IJsonFormatter<Expressions.TryExpression>{
    private IJsonFormatter<Expressions.TryExpression> Try=>this;
    public void Serialize(ref JsonWriter writer,Expressions.TryExpression value,IJsonFormatterResolver Resolver) {
        writer.WriteBeginArray();
        this.Serialize(ref writer,value.Body,Resolver);
        writer.WriteValueSeparator();
        this.Serialize(ref writer,value.Finally,Resolver);
        writer.WriteValueSeparator();
        Serialize_T(ref writer,value.Handlers,Resolver);
        writer.WriteEndArray();
    }
    Expressions.TryExpression IJsonFormatter<Expressions.TryExpression>.Deserialize(ref JsonReader reader,IJsonFormatterResolver Resolver) {
        reader.ReadIsBeginArrayWithVerify();
        var body= this.Deserialize(ref reader,Resolver);
        reader.ReadIsValueSeparatorWithVerify();
        var @finally= this.Deserialize(ref reader,Resolver);
        reader.ReadIsValueSeparatorWithVerify();
        var handlers=Deserialize_T<Expressions.CatchBlock[]>(ref reader,Resolver);
        reader.ReadIsEndArrayWithVerify();
        return Expressions.Expression.TryCatchFinally(body,@finally,handlers);
    }
}
partial class ExpressionMessagePackFormatter:IMessagePackFormatter<Expressions.TryExpression>{
    private IMessagePackFormatter<Expressions.TryExpression> MSTry=>this;
    public void Serialize(ref MessagePackWriter writer,Expressions.TryExpression value,MessagePackSerializerOptions Resolver){
        this.Serialize(ref writer,value.Body,Resolver);
        this.Serialize(ref writer,value.Finally,Resolver);
        Serialize_T(ref writer,value.Handlers,Resolver);
        //writer.Write("ABC");
    }
    Expressions.TryExpression IMessagePackFormatter<Expressions.TryExpression>.Deserialize(ref MessagePackReader reader,MessagePackSerializerOptions Resolver){
        var body= this.Deserialize(ref reader,Resolver);
        //var s=reader.ReadString();
        var @finally= this.Deserialize(ref reader,Resolver);
        var handlers=Deserialize_T<Expressions.CatchBlock[]>(ref reader,Resolver);
        if(handlers is null)
            return Expressions.Expression.TryFinally(body,@finally);
        //return (TryExpression)body;
        return Expressions.Expression.TryCatchFinally(body,@finally,handlers);
    }
}
