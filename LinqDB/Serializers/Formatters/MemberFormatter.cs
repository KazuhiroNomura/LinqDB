using System.Linq.Expressions;
using System.Reflection;
using MessagePack;
using MessagePack.Formatters;
using Utf8Json;
namespace LinqDB.Serializers.Formatters;
using static Common;
partial class ExpressionFormatter:IJsonFormatter<MemberExpression>,IMessagePackFormatter<MemberExpression>{
    private IJsonFormatter<MemberExpression> Member=>this;
    private IMessagePackFormatter<MemberExpression> MSMember=>this;
    public void Serialize(ref JsonWriter writer,MemberExpression value,IJsonFormatterResolver Resolver){
        writer.WriteBeginArray();
        Serialize_T(ref writer,value.Member,Resolver);
        //this.Serialize(ref writer,value.Member,Resolver);
        writer.WriteValueSeparator();
        this.Serialize(ref writer,value.Expression,Resolver);
        writer.WriteEndArray();
    }
    MemberExpression IJsonFormatter<MemberExpression>.Deserialize(ref JsonReader reader,IJsonFormatterResolver Resolver){
        reader.ReadIsBeginArrayWithVerify();
        var member=Deserialize_T<MemberInfo>(ref reader,Resolver);
        reader.ReadIsValueSeparatorWithVerify();
        var expression= this.Deserialize(ref reader,Resolver);
        reader.ReadIsEndArrayWithVerify();
        return Expression.MakeMemberAccess(expression,member);
    }
    public void Serialize(ref MessagePackWriter writer,MemberExpression value,MessagePackSerializerOptions Resolver){
        Serialize_T(ref writer,value.Member,Resolver);
        this.Serialize(ref writer,value.Expression,Resolver);
    }
    MemberExpression IMessagePackFormatter<MemberExpression>.Deserialize(ref MessagePackReader reader,MessagePackSerializerOptions Resolver){
        var member=Deserialize_T<MemberInfo>(ref reader,Resolver);
        var expression= this.Deserialize(ref reader,Resolver);
        return Expression.MakeMemberAccess(expression,member);
    }
}
