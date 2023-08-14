using System.Linq.Expressions;
using MessagePack;
using MessagePack.Formatters;
using Utf8Json;
namespace LinqDB.Serializers.Formatters;
using static Common;
partial class ExpressionJsonFormatter:IJsonFormatter<MemberInitExpression>{
    private IJsonFormatter<MemberInitExpression> MemberInit=>this;
    public void Serialize(ref JsonWriter writer,MemberInitExpression value,IJsonFormatterResolver Resolver) {
        writer.WriteBeginArray();
        this.Serialize(ref writer,value.NewExpression,Resolver);
        writer.WriteValueSeparator();
        Serialize_T(ref writer,value.Bindings,Resolver);
        writer.WriteEndArray();
    }
    MemberInitExpression IJsonFormatter<MemberInitExpression>.Deserialize(ref JsonReader reader,IJsonFormatterResolver Resolver) {
        reader.ReadIsBeginArrayWithVerify();
        var New=this.New.Deserialize(ref reader,Resolver);
        reader.ReadIsValueSeparatorWithVerify();
        var bindings=Deserialize_T<MemberBinding[]>(ref reader,Resolver);
        reader.ReadIsEndArrayWithVerify();
        return Expression.MemberInit(New,bindings);
    }
}
partial class ExpressionMessagePackFormatter:IMessagePackFormatter<MemberInitExpression>{
    private IMessagePackFormatter<MemberInitExpression> MSMemberInit=>this;
    public void Serialize(ref MessagePackWriter writer,MemberInitExpression value,MessagePackSerializerOptions Resolver){
        this.Serialize(ref writer,value.NewExpression,Resolver);
        Serialize_T(ref writer,value.Bindings,Resolver);
    }
    MemberInitExpression IMessagePackFormatter<MemberInitExpression>.Deserialize(ref MessagePackReader reader,MessagePackSerializerOptions Resolver){
        var New=this.MSNew.Deserialize(ref reader,Resolver);
        var bindings=Deserialize_T<MemberBinding[]>(ref reader,Resolver);
        return Expression.MemberInit(New,bindings);
    }
}
