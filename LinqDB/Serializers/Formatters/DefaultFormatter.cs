using System;
using System.Linq.Expressions;
using MessagePack;
using MessagePack.Formatters;
using Utf8Json;
namespace LinqDB.Serializers.Formatters;
using static Common;
partial class ExpressionFormatter:IJsonFormatter<DefaultExpression>,IMessagePackFormatter<DefaultExpression>{
    private IJsonFormatter<DefaultExpression> Default=>this;
    private IMessagePackFormatter<DefaultExpression> MSDefault=>this;
    public void Serialize(ref JsonWriter writer,DefaultExpression? value,IJsonFormatterResolver Resolver){
        if(value is null){
            writer.WriteNull();
            return;
        }
        writer.WriteBeginArray();
        Serialize_Type(ref writer,value.Type,Resolver);
        //this.Serialize(ref writer,value.Type,Resolver);
        writer.WriteEndArray();
    }
    DefaultExpression IJsonFormatter<DefaultExpression>.Deserialize(ref JsonReader reader,IJsonFormatterResolver Resolver){
        if(reader.ReadIsNull()) return null!;
        reader.ReadIsBeginArrayWithVerify();
        //var type=this.Type.Deserialize(ref reader,Resolver);
        var type=Deserialize_Type(ref reader,Resolver);
        reader.ReadIsEndArrayWithVerify();
        return Expression.Default(type);
    }
    public void Serialize(ref MessagePackWriter writer,DefaultExpression value,MessagePackSerializerOptions Resolver){
        //options.Resolver.GetFormatter<Type>().Serialize(ref writer,value.Type,options);
        Serialize_Type(ref writer,value.Type,Resolver);
    }
    DefaultExpression IMessagePackFormatter<DefaultExpression>.Deserialize(ref MessagePackReader reader,MessagePackSerializerOptions Resolver){
        var type=Deserialize_Type(ref reader,Resolver);
        //var type=options.Resolver.GetFormatter<Type>().Deserialize(ref reader,options);
        return Expression.Default(type);
    }
}
class DefaultFormatter:IMessagePackFormatter<DefaultExpression>{
    public void Serialize(ref MessagePackWriter writer,DefaultExpression value,MessagePackSerializerOptions options){
        options.Resolver.GetFormatter<Type>().Serialize(ref writer,value.Type,options);
    }
    public DefaultExpression Deserialize(ref MessagePackReader reader,MessagePackSerializerOptions options){
        var type=options.Resolver.GetFormatter<Type>().Deserialize(ref reader,options);
        return Expression.Default(type);
    }
}
