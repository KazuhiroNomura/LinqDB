using Expressions=System.Linq.Expressions;
using MessagePack;
using MessagePack.Formatters;
using Utf8Json;
namespace LinqDB.Serializers.Formatters;
partial class ExpressionJsonFormatter:IJsonFormatter<Expressions.LoopExpression>{
    private IJsonFormatter<Expressions.LoopExpression> Loop=>this;
    public void Serialize(ref JsonWriter writer,Expressions.LoopExpression value,IJsonFormatterResolver Resolver){
        writer.WriteBeginArray();
        //writer.WriteString(nameof(ExpressionType.Loop));
        //writer.WriteValueSeparator();
        this.Serialize(ref writer,value.BreakLabel,Resolver);
        writer.WriteValueSeparator();
        this.Serialize(ref writer,value.ContinueLabel,Resolver);
        writer.WriteValueSeparator();
        this.Serialize(ref writer,value.Body,Resolver);
        writer.WriteEndArray();
    }
    Expressions.LoopExpression IJsonFormatter<Expressions.LoopExpression>.Deserialize(ref JsonReader reader,IJsonFormatterResolver Resolver){
        reader.ReadIsBeginArrayWithVerify();
        var breakLabel= this.LabelTarget.Deserialize(ref reader,Resolver);
        reader.ReadIsValueSeparatorWithVerify();
        var continueLabel= this.LabelTarget.Deserialize(ref reader,Resolver);
        reader.ReadIsValueSeparatorWithVerify();
        var body= this.Deserialize(ref reader,Resolver);
        //var type=Deserialize_Type(ref reader,Resolver);
        reader.ReadIsEndArrayWithVerify();
        return Expressions.Expression.Loop(body,breakLabel,continueLabel);
    }
}
partial class ExpressionMessagePackFormatter:IMessagePackFormatter<Expressions.LoopExpression>{
    private IMessagePackFormatter<Expressions.LoopExpression> MSLoop=>this;
    public void Serialize(ref MessagePackWriter writer,Expressions.LoopExpression value,MessagePackSerializerOptions Resolver){
        this.Serialize(ref writer,value.BreakLabel,Resolver);
        this.Serialize(ref writer,value.ContinueLabel,Resolver);
        this.Serialize(ref writer,value.Body,Resolver);
    }
    Expressions.LoopExpression IMessagePackFormatter<Expressions.LoopExpression>.Deserialize(ref MessagePackReader reader,MessagePackSerializerOptions Resolver){
        var breakLabel= this.MSLabelTarget.Deserialize(ref reader,Resolver);
        var continueLabel= this.MSLabelTarget.Deserialize(ref reader,Resolver);
        var body= this.Deserialize(ref reader,Resolver);
        return Expressions.Expression.Loop(body,breakLabel,continueLabel);
    }
}
