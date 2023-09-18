using System;

using Utf8Json;

using Expressions = System.Linq.Expressions;
namespace LinqDB.Serializers.Utf8Json.Formatters;
using Writer = JsonWriter;
using Reader = JsonReader;
using T = Expressions.MemberBinding;
public class MemberBinding:IJsonFormatter<T> {
    public static readonly MemberBinding Instance=new();
    
    public void Serialize(ref Writer writer,T? value,IJsonFormatterResolver Resolver) {
        writer.WriteBeginArray();
        writer.WriteString(value!.BindingType.ToString());
        writer.WriteValueSeparator();
        Member.Instance.Serialize(ref writer,value.Member,Resolver);
        writer.WriteValueSeparator();
        switch(value.BindingType){
            case Expressions.MemberBindingType.Assignment:
                Expression.Write(ref writer,((Expressions.MemberAssignment)value).Expression,Resolver);
                break;
            case Expressions.MemberBindingType.MemberBinding:
                writer.WriteCollection(((Expressions.MemberMemberBinding)value).Bindings,Resolver);
                break;
            case Expressions.MemberBindingType.ListBinding:
                writer.WriteCollection(((Expressions.MemberListBinding)value).Initializers,Resolver);
                break;
            default:throw new ArgumentOutOfRangeException(value.BindingType.ToString());
        }
        writer.WriteEndArray();
    }
    public T Deserialize(ref Reader reader,IJsonFormatterResolver Resolver) {
        if(reader.TryReadNil())return null!;
        reader.ReadIsBeginArrayWithVerify();
        var BindingType=reader.Read<Expressions.MemberBindingType>();// Enum.Parse<Expressions.MemberBindingType>(reader.ReadString());
        reader.ReadIsValueSeparatorWithVerify();
        var member= Member.Instance.Deserialize(ref reader,Resolver);
        reader.ReadIsValueSeparatorWithVerify();
        T MemberBinding =BindingType switch{
            Expressions.MemberBindingType.Assignment=>Expressions.Expression.Bind(member,Expression.Read(ref reader,Resolver)),
            Expressions.MemberBindingType.MemberBinding=>Expressions.Expression.MemberBind(member,reader.ReadArray<T>(Resolver)),
            Expressions.MemberBindingType.ListBinding=>Expressions.Expression.ListBind(member,reader.ReadArray<Expressions.ElementInit>(Resolver)),
            _=>throw new ArgumentOutOfRangeException(BindingType.ToString())
        };
        reader.ReadIsEndArrayWithVerify();
        return MemberBinding;
    }
}
