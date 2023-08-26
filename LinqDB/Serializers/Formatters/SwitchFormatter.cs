using Expressions=System.Linq.Expressions;
using MessagePack;
using MessagePack.Formatters;
using Utf8Json;
namespace LinqDB.Serializers.Formatters;
using static Common;
partial class ExpressionJsonFormatter:IJsonFormatter<Expressions.SwitchExpression>{
    private IJsonFormatter<Expressions.SwitchExpression> Switch=>this;
    public void Serialize(ref JsonWriter writer,Expressions.SwitchExpression value,IJsonFormatterResolver Resolver) {
        writer.WriteBeginArray();
        Serialize_Type(ref writer,value.Type,Resolver);
        //this.Serialize(ref writer,value.Type,Resolver);
        writer.WriteValueSeparator();
        this.Serialize(ref writer,value.SwitchValue,Resolver);
        writer.WriteValueSeparator();
        this.Serialize(ref writer,value.Comparison,Resolver);
        writer.WriteValueSeparator();
        Serialize_T(ref writer,value.Cases,Resolver);
        writer.WriteValueSeparator();
        this.Serialize(ref writer,value.DefaultBody,Resolver);
        writer.WriteEndArray();
    }
    Expressions.SwitchExpression IJsonFormatter<Expressions.SwitchExpression>.Deserialize(ref JsonReader reader,IJsonFormatterResolver Resolver) {
        reader.ReadIsBeginArrayWithVerify();
        //var type= this.Type.Deserialize(ref reader,Resolver);
        var type= Deserialize_Type(ref reader,Resolver);
        reader.ReadIsValueSeparatorWithVerify();
        var switchValue= this.Deserialize(ref reader,Resolver);
        reader.ReadIsValueSeparatorWithVerify();
        var comparison= this.MethodInfo.Deserialize(ref reader,Resolver);
        reader.ReadIsValueSeparatorWithVerify();
        var cases=Deserialize_T<Expressions.SwitchCase[]>(ref reader,Resolver);
        reader.ReadIsValueSeparatorWithVerify();
        var defaultBody= this.Deserialize(ref reader,Resolver);
        reader.ReadIsEndArrayWithVerify();
        return Expressions.Expression.Switch(type,switchValue,defaultBody,comparison,cases);
    }
}
partial class ExpressionMessagePackFormatter:IMessagePackFormatter<Expressions.SwitchExpression>{
    private IMessagePackFormatter<Expressions.SwitchExpression> MSSwitch=>this;
    public void Serialize(ref MessagePackWriter writer,Expressions.SwitchExpression value,MessagePackSerializerOptions Resolver){
        Serialize_Type(ref writer,value.Type,Resolver);
        this.Serialize(ref writer,value.SwitchValue,Resolver);
        this.Serialize(ref writer,value.Comparison,Resolver);
        Serialize_T(ref writer,value.Cases,Resolver);
        this.Serialize(ref writer,value.DefaultBody,Resolver);
    }
    Expressions.SwitchExpression IMessagePackFormatter<Expressions.SwitchExpression>.Deserialize(ref MessagePackReader reader,MessagePackSerializerOptions Resolver){
        var type= Deserialize_Type(ref reader,Resolver);
        var switchValue= this.Deserialize(ref reader,Resolver);
        var comparison= this.MSMethodInfo.Deserialize(ref reader,Resolver);
        var cases=Deserialize_T<Expressions.SwitchCase[]>(ref reader,Resolver);
        var defaultBody= this.Deserialize(ref reader,Resolver);
        return Expressions.Expression.Switch(type,switchValue,defaultBody,comparison,cases);
    }
}
