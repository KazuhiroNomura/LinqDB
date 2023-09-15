using System;
using Expressions = System.Linq.Expressions;
using Utf8Json;
namespace LinqDB.Serializers.Utf8Json.Formatters;
using Writer = JsonWriter;
using Reader = JsonReader;
using T = Expressions.MemberBinding;
using static Extension;
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
                writer.SerializeReadOnlyCollection(((Expressions.MemberMemberBinding)value).Bindings,Resolver);
                break;
            case Expressions.MemberBindingType.ListBinding:
                writer.SerializeReadOnlyCollection(((Expressions.MemberListBinding)value).Initializers,Resolver);
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
            Expressions.MemberBindingType.MemberBinding=>Expressions.Expression.MemberBind(member,reader.ReadArray<T>(Resolver)),
            Expressions.MemberBindingType.ListBinding=>Expressions.Expression.ListBind(member,reader.ReadArray<Expressions.ElementInit>(Resolver)),
            _=>throw new ArgumentOutOfRangeException(BindingTypeName)
        };
        reader.ReadIsEndArrayWithVerify();
        return MemberBinding;
    }
}
