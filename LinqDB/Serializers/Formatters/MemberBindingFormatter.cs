using System;
using Expressions=System.Linq.Expressions;
using MessagePack;
using MessagePack.Formatters;
using Utf8Json;
namespace LinqDB.Serializers.Formatters;
using static Common;
partial class ExpressionJsonFormatter:IJsonFormatter<Expressions.MemberBinding>{
    public void Serialize(ref JsonWriter writer,Expressions.MemberBinding value,IJsonFormatterResolver Resolver) {
        writer.WriteBeginArray();
        writer.WriteString(value.BindingType.ToString());
        writer.WriteValueSeparator();
        Serialize_T(ref writer,value.Member,Resolver);
        writer.WriteValueSeparator();
        switch(value.BindingType){
            case Expressions.MemberBindingType.Assignment:
                this.Serialize(ref writer,((Expressions.MemberAssignment)value).Expression,Resolver);
                break;
            case Expressions.MemberBindingType.MemberBinding:
                Serialize_T(ref writer,((Expressions.MemberMemberBinding)value).Bindings,Resolver);
                break;
            case Expressions.MemberBindingType.ListBinding:
                Serialize_T(ref writer,((Expressions.MemberListBinding)value).Initializers,Resolver);
                break;
            default:
                throw new ArgumentOutOfRangeException(value.BindingType.ToString());
        }
        writer.WriteEndArray();
    }
    Expressions.MemberBinding IJsonFormatter<Expressions.MemberBinding>.Deserialize(ref JsonReader reader,IJsonFormatterResolver Resolver) {
        reader.ReadIsBeginArrayWithVerify();
        var BindingTypeName=reader.ReadString();
        var BindingType=Enum.Parse<Expressions.MemberBindingType>(BindingTypeName);
        reader.ReadIsValueSeparatorWithVerify();
        var member= this.MemberInfo.Deserialize(ref reader,Resolver);
        reader.ReadIsValueSeparatorWithVerify();
        Expressions.MemberBinding MemberBinding=BindingType switch{
            Expressions.MemberBindingType.Assignment=>Expressions.Expression.Bind(member,this.Deserialize(ref reader,Resolver)),
            Expressions.MemberBindingType.MemberBinding=>Expressions.Expression.MemberBind(member,Deserialize_T<Expressions.MemberBinding[]>(ref reader,Resolver)),
            Expressions.MemberBindingType.ListBinding=>Expressions.Expression.ListBind(member,Deserialize_T<Expressions.ElementInit[]>(ref reader,Resolver)),
            _=>throw new ArgumentOutOfRangeException(BindingTypeName)
        };
        reader.ReadIsEndArrayWithVerify();
        return MemberBinding;
    }
}
partial class ExpressionMessagePackFormatter:IMessagePackFormatter<Expressions.MemberBinding>{
    public void Serialize(ref MessagePackWriter writer,Expressions.MemberBinding value,MessagePackSerializerOptions Resolver){
        writer.Write((byte)value.BindingType);
        Serialize_T(ref writer,value.Member,Resolver);
        switch(value.BindingType){
            case Expressions.MemberBindingType.Assignment:
                this.Serialize(ref writer,((Expressions.MemberAssignment)value).Expression,Resolver);
                break;
            case Expressions.MemberBindingType.MemberBinding:
                Serialize_T(ref writer,((Expressions.MemberMemberBinding)value).Bindings,Resolver);
                break;
            case Expressions.MemberBindingType.ListBinding:
                Serialize_T(ref writer,((Expressions.MemberListBinding)value).Initializers,Resolver);
                break;
            default:
                throw new ArgumentOutOfRangeException(value.BindingType.ToString());
        }
    }
    Expressions.MemberBinding IMessagePackFormatter<Expressions.MemberBinding>.Deserialize(ref MessagePackReader reader,MessagePackSerializerOptions Resolver){
        var BindingType=(Expressions.MemberBindingType)reader.ReadByte();
        var member= this.MSMemberInfo.Deserialize(ref reader,Resolver);
        Expressions.MemberBinding MemberBinding=BindingType switch{
            Expressions.MemberBindingType.Assignment=>Expressions.Expression.Bind(member,this.Deserialize(ref reader,Resolver)),
            Expressions.MemberBindingType.MemberBinding=>Expressions.Expression.MemberBind(member,Deserialize_T<Expressions.MemberBinding[]>(ref reader,Resolver)),
            Expressions.MemberBindingType.ListBinding=>Expressions.Expression.ListBind(member,Deserialize_T<Expressions.ElementInit[]>(ref reader,Resolver)),
            _=>throw new ArgumentOutOfRangeException(BindingType.ToString())
        };
        return MemberBinding;
    }
}
