using Expressions=System.Linq.Expressions;
using MessagePack;
using MessagePack.Formatters;
using Utf8Json;
namespace LinqDB.Serializers.Formatters;
using static Common;
partial class ExpressionJsonFormatter:IJsonFormatter<Expressions.GotoExpression>{
    private IJsonFormatter<Expressions.GotoExpression> Goto=>this;
    public void Serialize(ref JsonWriter writer,Expressions.GotoExpression value,IJsonFormatterResolver Resolver){
        writer.WriteBeginArray();
        writer.WriteInt32((int)value.Kind);
        writer.WriteValueSeparator();
        this.Serialize(ref writer,value.Target,Resolver);
        writer.WriteValueSeparator();
        this.Serialize(ref writer,value.Value,Resolver);
        writer.WriteValueSeparator();
        //this.Serialize(ref writer,value.Type,Resolver);
        Serialize_Type(ref writer,value.Type,Resolver);
        writer.WriteEndArray();
    }
    Expressions.GotoExpression IJsonFormatter<Expressions.GotoExpression>.Deserialize(ref JsonReader reader,IJsonFormatterResolver Resolver){
        reader.ReadIsBeginArrayWithVerify();

        var kind=(Expressions.GotoExpressionKind)reader.ReadInt32();
        reader.ReadIsValueSeparatorWithVerify();
        //var target=Deserialize_T<LabelTarget>(ref reader,Resolver);
        var target= this.LabelTarget.Deserialize(ref reader,Resolver);
        reader.ReadIsValueSeparatorWithVerify();
        var value=Deserialize_T<Expressions.Expression>(ref reader,Resolver);
        reader.ReadIsValueSeparatorWithVerify();
        var type=Deserialize_Type(ref reader,Resolver);
        reader.ReadIsEndArrayWithVerify();
        return Expressions.Expression.MakeGoto(kind,target,value,type);
    }
}
partial class ExpressionMessagePackFormatter:IMessagePackFormatter<Expressions.GotoExpression>{
    private IMessagePackFormatter<Expressions.GotoExpression> Goto=>this;
    public void Serialize(ref MessagePackWriter writer,Expressions.GotoExpression value,MessagePackSerializerOptions Resolver){
        writer.Write((byte)value.Kind);
        this.Serialize(ref writer,value.Target,Resolver);
        this.Serialize(ref writer,value.Value,Resolver);
        Serialize_Type(ref writer,value.Type,Resolver);
    }
    Expressions.GotoExpression IMessagePackFormatter<Expressions.GotoExpression>.Deserialize(ref MessagePackReader reader,MessagePackSerializerOptions Resolver){
        var kind=(Expressions.GotoExpressionKind)reader.ReadByte();
        var target= this.MSLabelTarget.Deserialize(ref reader,Resolver);
        var value=Deserialize_T<Expressions.Expression>(ref reader,Resolver);
        var type=Deserialize_Type(ref reader,Resolver);
        return Expressions.Expression.MakeGoto(kind,target,value,type);
    }
}