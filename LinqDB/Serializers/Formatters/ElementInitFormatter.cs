using System.Linq.Expressions;
using MessagePack;
using MessagePack.Formatters;
using Utf8Json;
namespace LinqDB.Serializers.Formatters;
using static Common;
partial class ExpressionJsonFormatter:IJsonFormatter<ElementInit>{
    //public static readonly ElementInitFormatter Instance=new();
    private IJsonFormatter<ElementInit> ElementInit=>this;
    public void Serialize(ref JsonWriter writer,ElementInit value,IJsonFormatterResolver Resolver){
        writer.WriteBeginArray();
        this.Serialize(ref writer,value.AddMethod,Resolver);
        writer.WriteValueSeparator();
        Serialize_T(ref writer,value.Arguments,Resolver);
        writer.WriteEndArray();
    }
    ElementInit IJsonFormatter<ElementInit>.Deserialize(ref JsonReader reader,IJsonFormatterResolver Resolver){
        reader.ReadIsBeginArrayWithVerify();
        var addMethod= this.MethodInfo.Deserialize(ref reader,Resolver);
        reader.ReadIsValueSeparatorWithVerify();
        var arguments=Deserialize_T<Expression[]>(ref reader,Resolver);
        reader.ReadIsEndArrayWithVerify();
        return Expression.ElementInit(addMethod,arguments);
    }
}
partial class ExpressionMessagePackFormatter:IMessagePackFormatter<ElementInit>{
    //public static readonly ElementInitFormatter Instance=new();
    private IMessagePackFormatter<ElementInit> ElementInit=>this;
    public void Serialize(ref MessagePackWriter writer,ElementInit value,MessagePackSerializerOptions Resolver){
        this.Serialize(ref writer,value.AddMethod,Resolver);
        Serialize_T(ref writer,value.Arguments,Resolver);
    }
    ElementInit IMessagePackFormatter<ElementInit>.Deserialize(ref MessagePackReader reader,MessagePackSerializerOptions Resolver){
        var addMethod= this.MSMethodInfo.Deserialize(ref reader,Resolver);
        var arguments=Deserialize_T<Expression[]>(ref reader,Resolver);
        return Expression.ElementInit(addMethod,arguments);
    }
}
