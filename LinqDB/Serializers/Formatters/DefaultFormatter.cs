using Expressions=System.Linq.Expressions;
using MessagePack;
using MessagePack.Formatters;
using Utf8Json;
namespace LinqDB.Serializers.Formatters;
using static Common;
partial class ExpressionJsonFormatter:IJsonFormatter<Expressions.DefaultExpression>{
    private IJsonFormatter<Expressions.DefaultExpression> Default=>this;
    public void Serialize(ref JsonWriter writer,Expressions.DefaultExpression? value,IJsonFormatterResolver Resolver){
        if(value is null){
            writer.WriteNull();
            return;
        }
        writer.WriteBeginArray();
        Serialize_Type(ref writer,value.Type,Resolver);
        //this.Serialize(ref writer,value.Type,Resolver);
        writer.WriteEndArray();
    }
    Expressions.DefaultExpression IJsonFormatter<Expressions.DefaultExpression>.Deserialize(ref JsonReader reader,IJsonFormatterResolver Resolver){
        if(reader.ReadIsNull()) return null!;
        reader.ReadIsBeginArrayWithVerify();
        //var type=this.Type.Deserialize(ref reader,Resolver);
        var type=Deserialize_Type(ref reader,Resolver);
        reader.ReadIsEndArrayWithVerify();
        return Expressions.Expression.Default(type);
    }
}
partial class ExpressionMessagePackFormatter:IMessagePackFormatter<Expressions.DefaultExpression>{
    private IMessagePackFormatter<Expressions.DefaultExpression> Default=>this;
    public void Serialize(ref MessagePackWriter writer,Expressions.DefaultExpression value,MessagePackSerializerOptions Resolver){
        //options.Resolver.GetFormatter<Type>().Serialize(ref writer,value.Type,options);
        Serialize_Type(ref writer,value.Type,Resolver);
    }
    Expressions.DefaultExpression IMessagePackFormatter<Expressions.DefaultExpression>.Deserialize(ref MessagePackReader reader,MessagePackSerializerOptions Resolver){
        var type=Deserialize_Type(ref reader,Resolver);
        //var type=options.Resolver.GetFormatter<Type>().Deserialize(ref reader,options);
        return Expressions.Expression.Default(type);
    }
}
