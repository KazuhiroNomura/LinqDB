using Expressions=System.Linq.Expressions;
using MessagePack;
using MessagePack.Formatters;
using Utf8Json;
namespace LinqDB.Serializers.Formatters;
using static Common;
partial class ExpressionJsonFormatter:IJsonFormatter<Expressions.NewExpression>{
    private IJsonFormatter<Expressions.NewExpression> New=>this;
    public void Serialize(ref JsonWriter writer,Expressions.NewExpression? value,IJsonFormatterResolver Resolver){
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
    Expressions.NewExpression IJsonFormatter<Expressions.NewExpression>.Deserialize(ref JsonReader reader,IJsonFormatterResolver Resolver){
        if(reader.ReadIsNull()) return null!;
        reader.ReadIsBeginArrayWithVerify();
        var constructor= this.ConstructorInfo.Deserialize(ref reader,Resolver);
        reader.ReadIsValueSeparatorWithVerify();
        var arguments=Deserialize_T<Expressions.Expression[]>(ref reader,Resolver);
        reader.ReadIsEndArrayWithVerify();
        return Expressions.Expression.New(
            constructor,
            arguments
        );
    }
}
partial class ExpressionMessagePackFormatter:IMessagePackFormatter<Expressions.NewExpression>{
    private IMessagePackFormatter<Expressions.NewExpression> MSNew=>this;
    public void Serialize(ref MessagePackWriter writer,Expressions.NewExpression? value,MessagePackSerializerOptions Resolver){
        if(value is null){
            writer.WriteNil();
            return;
        }
        this.Serialize(ref writer,value.Constructor!,Resolver);
        Serialize_T(ref writer,value.Arguments,Resolver);
    }
    Expressions.NewExpression IMessagePackFormatter<Expressions.NewExpression>.Deserialize(ref MessagePackReader reader,MessagePackSerializerOptions Resolver){
        if(reader.TryReadNil()) return null!;
        var constructor= this.ConstructorInfo.Deserialize(ref reader,Resolver);
        var arguments=Deserialize_T<Expressions.Expression[]>(ref reader,Resolver);
        return Expressions.Expression.New(
            constructor,
            arguments
        );
    }
}
