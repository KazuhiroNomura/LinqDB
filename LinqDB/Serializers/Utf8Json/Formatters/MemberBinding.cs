using System;
using Expressions=System.Linq.Expressions;
using MessagePack;
using MessagePack.Formatters;
using Utf8Json;
using LinqDB.Serializers.MessagePack;

namespace LinqDB.Serializers.Utf8Json.Formatters;
using Writer=JsonWriter;
using Reader=JsonReader;
using T=Expressions.MemberBinding;
using static Common;
using C=Utf8JsonCustomSerializer;
public class MemberBinding:IJsonFormatter<T> {
    public static readonly MemberBinding Instance=new();
    public void Serialize(ref Writer writer,T value,IJsonFormatterResolver Resolver) {
        writer.WriteBeginArray();
        writer.WriteString(value.BindingType.ToString());
        writer.WriteValueSeparator();
        Member.Instance.Serialize(ref writer,value.Member,Resolver);
        writer.WriteValueSeparator();
        switch(value.BindingType){
            case Expressions.MemberBindingType.Assignment:
                Expression.Instance.Serialize(ref writer,((Expressions.MemberAssignment)value).Expression,Resolver);
                break;
            case Expressions.MemberBindingType.MemberBinding:
                SerializeReadOnlyCollection(ref writer,((Expressions.MemberMemberBinding)value).Bindings,Resolver);
                break;
            case Expressions.MemberBindingType.ListBinding:
                SerializeReadOnlyCollection(ref writer,((Expressions.MemberListBinding)value).Initializers,Resolver);
                break;
            default:
                throw new ArgumentOutOfRangeException(value.BindingType.ToString());
        }
        writer.WriteEndArray();
    }
    public T Deserialize(ref Reader reader,IJsonFormatterResolver Resolver) {
        reader.ReadIsBeginArrayWithVerify();
        var BindingTypeName=reader.ReadString();
        var BindingType=Enum.Parse<Expressions.MemberBindingType>(BindingTypeName);
        reader.ReadIsValueSeparatorWithVerify();
        var member= Member.Instance.Deserialize(ref reader,Resolver);
        reader.ReadIsValueSeparatorWithVerify();
        T MemberBinding =BindingType switch{
            Expressions.MemberBindingType.Assignment=>Expressions.Expression.Bind(member,Expression.Instance.Deserialize(ref reader,Resolver)),
            Expressions.MemberBindingType.MemberBinding=>Expressions.Expression.MemberBind(member,DeserializeArray<T>(ref reader,Resolver)),
            Expressions.MemberBindingType.ListBinding=>Expressions.Expression.ListBind(member,DeserializeArray<Expressions.ElementInit>(ref reader,Resolver)),
            _=>throw new ArgumentOutOfRangeException(BindingTypeName)
        };
        reader.ReadIsEndArrayWithVerify();
        return MemberBinding;
    }
}
