using Expressions=System.Linq.Expressions;
using System.Reflection;
using MessagePack;
using MessagePack.Formatters;
using Utf8Json;
namespace LinqDB.Serializers.Formatters;
using static Common;
partial class ExpressionJsonFormatter:IJsonFormatter<Expressions.MemberExpression> {
    private IJsonFormatter<Expressions.MemberExpression> Member => this;
    public void Serialize(ref JsonWriter writer,Expressions.MemberExpression value,IJsonFormatterResolver Resolver){
        writer.WriteBeginArray();
        Serialize_T(ref writer,value.Member,Resolver);
        //this.Serialize(ref writer,value.Member,Resolver);
        writer.WriteValueSeparator();
        this.Serialize(ref writer,value.Expression,Resolver);
        writer.WriteEndArray();
    }
    Expressions.MemberExpression IJsonFormatter<Expressions.MemberExpression>.Deserialize(ref JsonReader reader,IJsonFormatterResolver Resolver) {
        reader.ReadIsBeginArrayWithVerify();
        var member = Deserialize_T<MemberInfo>(ref reader,Resolver);
        reader.ReadIsValueSeparatorWithVerify();
        var expression = this.Deserialize(ref reader,Resolver);
        reader.ReadIsEndArrayWithVerify();
        return Expressions.Expression.MakeMemberAccess(expression,member);
    }
}

partial class ExpressionMessagePackFormatter:IMessagePackFormatter<Expressions.MemberExpression>{
    private IMessagePackFormatter<Expressions.MemberExpression> MSMember=>this;
    public void Serialize(ref MessagePackWriter writer,Expressions.MemberExpression value,MessagePackSerializerOptions Resolver){
        Serialize_T(ref writer,value.Member,Resolver);
        this.Serialize(ref writer,value.Expression,Resolver);
    }
    Expressions.MemberExpression IMessagePackFormatter<Expressions.MemberExpression>.Deserialize(ref MessagePackReader reader,MessagePackSerializerOptions Resolver){
        var member=Deserialize_T<MemberInfo>(ref reader,Resolver);
        var expression= this.Deserialize(ref reader,Resolver);
        return Expressions.Expression.MakeMemberAccess(expression,member);
    }
}
