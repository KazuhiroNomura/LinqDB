using Expressions=System.Linq.Expressions;
using MessagePack;
using MessagePack.Formatters;
using Utf8Json;
namespace LinqDB.Serializers.Formatters;
using static Common;
partial class ExpressionJsonFormatter:IJsonFormatter<Expressions.ElementInit>{
    //public static readonly ElementInitFormatter Instance=new();
    private IJsonFormatter<Expressions.ElementInit> ElementInit=>this;
    public void Serialize(ref JsonWriter writer,Expressions.ElementInit value,IJsonFormatterResolver Resolver){
        writer.WriteBeginArray();
        this.Serialize(ref writer,value.AddMethod,Resolver);
        writer.WriteValueSeparator();
        Serialize_T(ref writer,value.Arguments,Resolver);
        writer.WriteEndArray();
    }
    Expressions.ElementInit IJsonFormatter<Expressions.ElementInit>.Deserialize(ref JsonReader reader,IJsonFormatterResolver Resolver){
        reader.ReadIsBeginArrayWithVerify();
        var addMethod= this.MethodInfo.Deserialize(ref reader,Resolver);
        reader.ReadIsValueSeparatorWithVerify();
        var arguments=Deserialize_T<Expressions.Expression[]>(ref reader,Resolver);
        reader.ReadIsEndArrayWithVerify();
        return Expressions.Expression.ElementInit(addMethod,arguments);
    }
}
partial class ExpressionMessagePackFormatter:IMessagePackFormatter<Expressions.ElementInit>{
    //public static readonly ElementInitFormatter Instance=new();
    private IMessagePackFormatter<Expressions.ElementInit> ElementInit=>this;
    public void Serialize(ref MessagePackWriter writer,Expressions.ElementInit value,MessagePackSerializerOptions Resolver){
        this.Serialize(ref writer,value.AddMethod,Resolver);
        Serialize_T(ref writer,value.Arguments,Resolver);
    }
    Expressions.ElementInit IMessagePackFormatter<Expressions.ElementInit>.Deserialize(ref MessagePackReader reader,MessagePackSerializerOptions Resolver){
        var addMethod= this.MSMethodInfo.Deserialize(ref reader,Resolver);
        var arguments=Deserialize_T<Expressions.Expression[]>(ref reader,Resolver);
        return Expressions.Expression.ElementInit(addMethod,arguments);
    }
}
