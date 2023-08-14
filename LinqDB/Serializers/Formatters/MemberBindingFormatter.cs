using System;
using System.Linq.Expressions;
using MessagePack;
using MessagePack.Formatters;
using Utf8Json;
namespace LinqDB.Serializers.Formatters;
using static Common;
partial class ExpressionFormatter:IJsonFormatter<MemberBinding>,IMessagePackFormatter<MemberBinding>{
    private IJsonFormatter<MemberBinding> MemberBinding=>this;
    public void Serialize(ref JsonWriter writer,MemberBinding value,IJsonFormatterResolver Resolver) {
        writer.WriteBeginArray();
        writer.WriteString(value.BindingType.ToString());
        writer.WriteValueSeparator();
        Serialize_T(ref writer,value.Member,Resolver);
        writer.WriteValueSeparator();
        switch(value.BindingType){
            case MemberBindingType.Assignment:
                this.Serialize(ref writer,((MemberAssignment)value).Expression,Resolver);
                break;
            case MemberBindingType.MemberBinding:
                Serialize_T(ref writer,((MemberMemberBinding)value).Bindings,Resolver);
                break;
            case MemberBindingType.ListBinding:
                Serialize_T(ref writer,((MemberListBinding)value).Initializers,Resolver);
                break;
            default:
                throw new ArgumentOutOfRangeException(value.BindingType.ToString());
        }
        writer.WriteEndArray();
    }
    MemberBinding IJsonFormatter<MemberBinding>.Deserialize(ref JsonReader reader,IJsonFormatterResolver Resolver) {
        reader.ReadIsBeginArrayWithVerify();
        var BindingTypeName=reader.ReadString();
        var BindingType=Enum.Parse<MemberBindingType>(BindingTypeName);
        reader.ReadIsValueSeparatorWithVerify();
        var member= this.MemberInfo.Deserialize(ref reader,Resolver);
        reader.ReadIsValueSeparatorWithVerify();
        MemberBinding MemberBinding=BindingType switch{
            MemberBindingType.Assignment=>Expression.Bind(member,this.Deserialize(ref reader,Resolver)),
            MemberBindingType.MemberBinding=>Expression.MemberBind(member,Deserialize_T<MemberBinding[]>(ref reader,Resolver)),
            MemberBindingType.ListBinding=>Expression.ListBind(member,Deserialize_T<ElementInit[]>(ref reader,Resolver)),
            _=>throw new ArgumentOutOfRangeException(BindingTypeName)
        };
        reader.ReadIsEndArrayWithVerify();
        return MemberBinding;
    }
    public void Serialize(ref MessagePackWriter writer,MemberBinding value,MessagePackSerializerOptions Resolver){
        writer.Write((byte)value.BindingType);
        Serialize_T(ref writer,value.Member,Resolver);
        switch(value.BindingType){
            case MemberBindingType.Assignment:
                this.Serialize(ref writer,((MemberAssignment)value).Expression,Resolver);
                break;
            case MemberBindingType.MemberBinding:
                Serialize_T(ref writer,((MemberMemberBinding)value).Bindings,Resolver);
                break;
            case MemberBindingType.ListBinding:
                Serialize_T(ref writer,((MemberListBinding)value).Initializers,Resolver);
                break;
            default:
                throw new ArgumentOutOfRangeException(value.BindingType.ToString());
        }
    }
    MemberBinding IMessagePackFormatter<MemberBinding>.Deserialize(ref MessagePackReader reader,MessagePackSerializerOptions Resolver){
        var BindingType=(MemberBindingType)reader.ReadByte();
        var member= this.MSMemberInfo.Deserialize(ref reader,Resolver);
        MemberBinding MemberBinding=BindingType switch{
            MemberBindingType.Assignment=>Expression.Bind(member,this.Deserialize(ref reader,Resolver)),
            MemberBindingType.MemberBinding=>Expression.MemberBind(member,Deserialize_T<MemberBinding[]>(ref reader,Resolver)),
            MemberBindingType.ListBinding=>Expression.ListBind(member,Deserialize_T<ElementInit[]>(ref reader,Resolver)),
            _=>throw new ArgumentOutOfRangeException(BindingType.ToString())
        };
        return MemberBinding;
    }
}
