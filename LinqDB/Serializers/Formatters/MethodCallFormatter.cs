using Expressions=System.Linq.Expressions;
using MessagePack;
using MessagePack.Formatters;
using Utf8Json;
namespace LinqDB.Serializers.Formatters;
using static Common;
partial class ExpressionJsonFormatter:IJsonFormatter<Expressions.MethodCallExpression>{
    private IJsonFormatter<Expressions.MethodCallExpression> MethodCall=>this;
    public void Serialize(ref JsonWriter writer,Expressions.MethodCallExpression? value,IJsonFormatterResolver Resolver){
        if(value is null){
            writer.WriteNull();
            return;
        }
        writer.WriteBeginArray();
        var Method=value.Method;
        this.Serialize(ref writer,Method,Resolver);
        writer.WriteValueSeparator();
        if(!Method.IsStatic){
            this.Serialize(ref writer,value.Object!,Resolver);
            writer.WriteValueSeparator();
        }
        Serialize_T(ref writer,value.Arguments,Resolver);
        writer.WriteEndArray();
    }
    Expressions.MethodCallExpression IJsonFormatter<Expressions.MethodCallExpression>.Deserialize(ref JsonReader reader,IJsonFormatterResolver Resolver){
        if(reader.ReadIsNull()) return null!;
        reader.ReadIsBeginArrayWithVerify();
        var method= this.MethodInfo.Deserialize(ref reader,Resolver);
        reader.ReadIsValueSeparatorWithVerify();
        if(method.IsStatic){
            var arguments=Deserialize_T<Expressions.Expression[]>(ref reader,Resolver);
            reader.ReadIsEndArrayWithVerify();
            return Expressions.Expression.Call(
                method,
                arguments
            );
        } else{
            var instance= this.Deserialize(ref reader,Resolver);
            reader.ReadIsValueSeparatorWithVerify();
            var arguments=Deserialize_T<Expressions.Expression[]>(ref reader,Resolver);
            reader.ReadIsEndArrayWithVerify();
            return Expressions.Expression.Call(
                instance,
                method,
                arguments
            );
        }
    }
}
partial class ExpressionMessagePackFormatter:IMessagePackFormatter<Expressions.MethodCallExpression>{
    private IMessagePackFormatter<Expressions.MethodCallExpression> MSMethodCall=>this;
    public void Serialize(ref MessagePackWriter writer,Expressions.MethodCallExpression? value,MessagePackSerializerOptions Resolver){
        if(value is null){
            writer.WriteNil();
            return;
        }
        var Method=value.Method;
        this.Serialize(ref writer,Method,Resolver);
        if(!Method.IsStatic){
            this.Serialize(ref writer,value.Object!,Resolver);
        }
        Serialize_T(ref writer,value.Arguments,Resolver);
    }
    Expressions.MethodCallExpression IMessagePackFormatter<Expressions.MethodCallExpression>.Deserialize(ref MessagePackReader reader,MessagePackSerializerOptions Resolver){
        if(reader.TryReadNil()) return null!;
        var method= this.MSMethodInfo.Deserialize(ref reader,Resolver);
        if(method.IsStatic){
            var arguments=Deserialize_T<Expressions.Expression[]>(ref reader,Resolver);
            return Expressions.Expression.Call(
                method,
                arguments
            );
        } else{
            var instance= this.Deserialize(ref reader,Resolver);
            var arguments=Deserialize_T<Expressions.Expression[]>(ref reader,Resolver);
            return Expressions.Expression.Call(
                instance,
                method,
                arguments
            );
        }
    }
}
