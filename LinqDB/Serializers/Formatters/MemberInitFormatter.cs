using Expressions=System.Linq.Expressions;
using MessagePack;
using MessagePack.Formatters;
using Utf8Json;
namespace LinqDB.Serializers.Formatters;
using static Common;
partial class ExpressionJsonFormatter:IJsonFormatter<Expressions.MemberInitExpression>{
    private IJsonFormatter<Expressions.MemberInitExpression> MemberInit=>this;
    public void Serialize(ref JsonWriter writer,Expressions.MemberInitExpression value,IJsonFormatterResolver Resolver) {
        writer.WriteBeginArray();
        this.Serialize(ref writer,value.NewExpression,Resolver);
        writer.WriteValueSeparator();
        Serialize_T(ref writer,value.Bindings,Resolver);
        writer.WriteEndArray();
    }
    Expressions.MemberInitExpression IJsonFormatter<Expressions.MemberInitExpression>.Deserialize(ref JsonReader reader,IJsonFormatterResolver Resolver) {
        reader.ReadIsBeginArrayWithVerify();
        var New=this.New.Deserialize(ref reader,Resolver);
        reader.ReadIsValueSeparatorWithVerify();
        var bindings=Deserialize_T<Expressions.MemberBinding[]>(ref reader,Resolver);
        reader.ReadIsEndArrayWithVerify();
        return Expressions.Expression.MemberInit(New,bindings);
    }
}
partial class ExpressionMessagePackFormatter:IMessagePackFormatter<Expressions.MemberInitExpression>{
    private IMessagePackFormatter<Expressions.MemberInitExpression> MSMemberInit=>this;
    public void Serialize(ref MessagePackWriter writer,Expressions.MemberInitExpression value,MessagePackSerializerOptions Resolver){
        this.Serialize(ref writer,value.NewExpression,Resolver);
        Serialize_T(ref writer,value.Bindings,Resolver);
    }
    Expressions.MemberInitExpression IMessagePackFormatter<Expressions.MemberInitExpression>.Deserialize(ref MessagePackReader reader,MessagePackSerializerOptions Resolver){
        var New=this.MSNew.Deserialize(ref reader,Resolver);
        var bindings=Deserialize_T<Expressions.MemberBinding[]>(ref reader,Resolver);
        return Expressions.Expression.MemberInit(New,bindings);
    }
}
