using System.Linq.Expressions;
using MessagePack;
using MessagePack.Formatters;
using Utf8Json;
namespace LinqDB.Serializers.Formatters;
partial class ExpressionJsonFormatter:IJsonFormatter<LoopExpression>{
    private IJsonFormatter<LoopExpression> Loop=>this;
    public void Serialize(ref JsonWriter writer,LoopExpression value,IJsonFormatterResolver Resolver){
        writer.WriteBeginArray();
        //writer.WriteString(nameof(ExpressionType.Loop));
        //writer.WriteValueSeparator();
        this.Serialize(ref writer,value.Body,Resolver);
        writer.WriteValueSeparator();
        this.Serialize(ref writer,value.BreakLabel,Resolver);
        writer.WriteValueSeparator();
        this.Serialize(ref writer,value.ContinueLabel,Resolver);
        writer.WriteEndArray();
    }
    LoopExpression IJsonFormatter<LoopExpression>.Deserialize(ref JsonReader reader,IJsonFormatterResolver Resolver){
        reader.ReadIsBeginArrayWithVerify();
        var body= this.Deserialize(ref reader,Resolver);
        //var type=Deserialize_Type(ref reader,Resolver);
        reader.ReadIsValueSeparatorWithVerify();
        var breakLabel= this.LabelTarget.Deserialize(ref reader,Resolver);
        reader.ReadIsValueSeparatorWithVerify();
        var continueLabel= this.LabelTarget.Deserialize(ref reader,Resolver);
        reader.ReadIsEndArrayWithVerify();
        return Expression.Loop(body,breakLabel,continueLabel);
    }
}
partial class ExpressionMessagePackFormatter:IMessagePackFormatter<LoopExpression>{
    private IMessagePackFormatter<LoopExpression> MSLoop=>this;
    public void Serialize(ref MessagePackWriter writer,LoopExpression value,MessagePackSerializerOptions Resolver){
        this.Serialize(ref writer,value.Body,Resolver);
        this.Serialize(ref writer,value.BreakLabel,Resolver);
        this.Serialize(ref writer,value.ContinueLabel,Resolver);
    }
    LoopExpression IMessagePackFormatter<LoopExpression>.Deserialize(ref MessagePackReader reader,MessagePackSerializerOptions Resolver){
        var body= this.Deserialize(ref reader,Resolver);
        var breakLabel= this.MSLabelTarget.Deserialize(ref reader,Resolver);
        var continueLabel= this.MSLabelTarget.Deserialize(ref reader,Resolver);
        return Expression.Loop(body,breakLabel,continueLabel);
    }
}
