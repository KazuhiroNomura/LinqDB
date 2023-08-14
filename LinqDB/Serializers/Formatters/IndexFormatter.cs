using System.Linq.Expressions;
using MessagePack;
using MessagePack.Formatters;
using Utf8Json;
namespace LinqDB.Serializers.Formatters;
using static Common;
partial class ExpressionFormatter:IJsonFormatter<IndexExpression>,IMessagePackFormatter<IndexExpression>{
    private IJsonFormatter<IndexExpression> Index=>this;
    private IMessagePackFormatter<IndexExpression> MSIndex=>this;
    public void Serialize(ref JsonWriter writer,IndexExpression? value,IJsonFormatterResolver Resolver){
        if(value is null){
            writer.WriteNull();
            return;
        }
        writer.WriteBeginArray();
        this.Serialize(ref writer,value.Object,Resolver);
        writer.WriteValueSeparator();
        this.Serialize(ref writer,value.Indexer,Resolver);
        writer.WriteValueSeparator();
        Serialize_T(ref writer,value.Arguments,Resolver);
        writer.WriteEndArray();
    }
    IndexExpression IJsonFormatter<IndexExpression>.Deserialize(ref JsonReader reader,IJsonFormatterResolver Resolver){
        if(reader.ReadIsNull()) return null!;
        reader.ReadIsBeginArrayWithVerify();
        var instance= this.Deserialize(ref reader,Resolver);
        reader.ReadIsValueSeparatorWithVerify();
        var indexer= this.PropertyInfo.Deserialize(ref reader,Resolver);
        reader.ReadIsValueSeparatorWithVerify();
        var arguments=Deserialize_T<Expression[]>(ref reader,Resolver);
        reader.ReadIsEndArrayWithVerify();
        return Expression.MakeIndex(instance,indexer,arguments);
    }
    public void Serialize(ref MessagePackWriter writer,IndexExpression? value,MessagePackSerializerOptions Resolver){
        if(value is null){
            writer.WriteNil();
            return;
        }
        this.Serialize(ref writer,value.Object,Resolver);
        this.Serialize(ref writer,value.Indexer,Resolver);
        Serialize_T(ref writer,value.Arguments,Resolver);
    }
    IndexExpression IMessagePackFormatter<IndexExpression>.Deserialize(ref MessagePackReader reader,MessagePackSerializerOptions Resolver){
        if(reader.TryReadNil()) return null!;
        var instance= this.Deserialize(ref reader,Resolver);
        var indexer= this.MSPropertyInfo.Deserialize(ref reader,Resolver);
        var arguments=Deserialize_T<Expression[]>(ref reader,Resolver);
        return Expression.MakeIndex(instance,indexer,arguments);
    }
}
