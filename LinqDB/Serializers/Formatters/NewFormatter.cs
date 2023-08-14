using System.Linq.Expressions;
using MessagePack;
using MessagePack.Formatters;
using Utf8Json;
namespace LinqDB.Serializers.Formatters;
using static Common;
partial class ExpressionFormatter:IJsonFormatter<NewExpression>,IMessagePackFormatter<NewExpression>{
    private IJsonFormatter<NewExpression> New=>this;
    private IMessagePackFormatter<NewExpression> MSNew=>this;
    public void Serialize(ref JsonWriter writer,NewExpression? value,IJsonFormatterResolver Resolver){
        if(value is null){
            writer.WriteNull();
            return;
        }
        writer.WriteBeginArray();
        this.Serialize(ref writer,value.Constructor!,Resolver);
        writer.WriteValueSeparator();
        Serialize_T(ref writer,value.Arguments,Resolver);
        writer.WriteEndArray();
        //var Arguments=value.Arguments;
        //var Arguments_Count=Arguments.Count;
        //writer.WriteBeginArray();
        //for(var a=0;a<Arguments_Count;a++)
        //    _Expression.Serialize(ref writer,Arguments[a],Resolver);
        //writer.WriteEndArray();
    }
    NewExpression IJsonFormatter<NewExpression>.Deserialize(ref JsonReader reader,IJsonFormatterResolver Resolver){
        if(reader.ReadIsNull()) return null!;
        reader.ReadIsBeginArrayWithVerify();
        var constructor= this.ConstructorInfo.Deserialize(ref reader,Resolver);
        reader.ReadIsValueSeparatorWithVerify();
        var arguments=Deserialize_T<Expression[]>(ref reader,Resolver);
        reader.ReadIsEndArrayWithVerify();
        return Expression.New(
            constructor,
            arguments
        );
    }
    public void Serialize(ref MessagePackWriter writer,NewExpression? value,MessagePackSerializerOptions Resolver){
        if(value is null){
            writer.WriteNil();
            return;
        }
        this.Serialize(ref writer,value.Constructor!,Resolver);
        Serialize_T(ref writer,value.Arguments,Resolver);
    }
    NewExpression IMessagePackFormatter<NewExpression>.Deserialize(ref MessagePackReader reader,MessagePackSerializerOptions Resolver){
        if(reader.TryReadNil()) return null!;
        var constructor= this.MSConstructorInfo.Deserialize(ref reader,Resolver);
        var arguments=Deserialize_T<Expression[]>(ref reader,Resolver);
        return Expression.New(
            constructor,
            arguments
        );
    }
}
