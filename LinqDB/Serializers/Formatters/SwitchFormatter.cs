using System;
using System.Linq.Expressions;
using MessagePack;
using MessagePack.Formatters;
using Utf8Json;
namespace LinqDB.Serializers.Formatters;
using static Common;
partial class ExpressionJsonFormatter:IJsonFormatter<SwitchExpression>{
    private IJsonFormatter<SwitchExpression> Switch=>this;
    public void Serialize(ref JsonWriter writer,SwitchExpression value,IJsonFormatterResolver Resolver) {
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
    SwitchExpression IJsonFormatter<SwitchExpression>.Deserialize(ref JsonReader reader,IJsonFormatterResolver Resolver) {
        reader.ReadIsBeginArrayWithVerify();
        //var type= this.Type.Deserialize(ref reader,Resolver);
        var type= Deserialize_Type(ref reader,Resolver);
        reader.ReadIsValueSeparatorWithVerify();
        var switchValue= this.Deserialize(ref reader,Resolver);
        reader.ReadIsValueSeparatorWithVerify();
        var comparison= this.MethodInfo.Deserialize(ref reader,Resolver);
        reader.ReadIsValueSeparatorWithVerify();
        var cases=Deserialize_T<SwitchCase[]>(ref reader,Resolver);
        reader.ReadIsValueSeparatorWithVerify();
        var defaultBody= this.Deserialize(ref reader,Resolver);
        reader.ReadIsEndArrayWithVerify();
        return Expression.Switch(type,switchValue,defaultBody,comparison,cases);
    }
}
partial class ExpressionMessagePackFormatter:IMessagePackFormatter<SwitchExpression>{
    private IMessagePackFormatter<SwitchExpression> MSSwitch=>this;
    public void Serialize(ref MessagePackWriter writer,SwitchExpression value,MessagePackSerializerOptions Resolver){
        Serialize_Type(ref writer,value.Type,Resolver);
        this.Serialize(ref writer,value.SwitchValue,Resolver);
        this.Serialize(ref writer,value.Comparison,Resolver);
        Serialize_T(ref writer,value.Cases,Resolver);
        this.Serialize(ref writer,value.DefaultBody,Resolver);
    }
    SwitchExpression IMessagePackFormatter<SwitchExpression>.Deserialize(ref MessagePackReader reader,MessagePackSerializerOptions Resolver){
        var type= Deserialize_Type(ref reader,Resolver);
        var switchValue= this.Deserialize(ref reader,Resolver);
        var comparison= this.MSMethodInfo.Deserialize(ref reader,Resolver);
        var cases=Deserialize_T<SwitchCase[]>(ref reader,Resolver);
        var defaultBody= this.Deserialize(ref reader,Resolver);
        return Expression.Switch(type,switchValue,defaultBody,comparison,cases);
    }
}
