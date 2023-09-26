using System;
using System.Diagnostics;
using Utf8Json;

using Expressions = System.Linq.Expressions;
namespace LinqDB.Serializers.Utf8Json.Formatters;
using Reflection;
using O=IJsonFormatterResolver;
using Writer = JsonWriter;
using Reader = JsonReader;
using T = Expressions.MemberBinding;
public class MemberBinding:IJsonFormatter<T> {
    public static readonly MemberBinding Instance=new();
    private static void PrivateWrite(ref Writer writer,T value,O Resolver) {
        writer.WriteBeginArray();
        writer.WriteString(value!.BindingType.ToString());
        writer.WriteValueSeparator();
        Member.Write(ref writer,value.Member,Resolver);
        writer.WriteValueSeparator();
        switch(value.BindingType){
            case Expressions.MemberBindingType.Assignment:
                Expression.Write(ref writer,((Expressions.MemberAssignment)value).Expression,Resolver);
                break;
            case Expressions.MemberBindingType.MemberBinding:
                writer.WriteCollection(((Expressions.MemberMemberBinding)value).Bindings,Resolver);
                break;
            default:
                Debug.Assert(value.BindingType==Expressions.MemberBindingType.ListBinding);
                writer.WriteCollection(((Expressions.MemberListBinding)value).Initializers,Resolver);
                break;
        }
        writer.WriteEndArray();
    }
    public void Serialize(ref Writer writer,T? value,O Resolver) {
        if(writer.TryWriteNil(value)) return;

        PrivateWrite(ref writer,value,Resolver);
    }
    private static T Read(ref Reader reader,O Resolver) {
        reader.ReadIsBeginArrayWithVerify();
        var BindingType=Enum.Parse<Expressions.MemberBindingType>(reader.ReadString());
        reader.ReadIsValueSeparatorWithVerify();
        var member= Member.Read(ref reader,Resolver);
        reader.ReadIsValueSeparatorWithVerify();
        Debug.Assert(BindingType is Expressions.MemberBindingType.Assignment or Expressions.MemberBindingType.MemberBinding or Expressions.MemberBindingType.ListBinding);
        T MemberBinding =BindingType switch{
            Expressions.MemberBindingType.Assignment=>Expressions.Expression.Bind(member,Expression.Read(ref reader,Resolver)),
            Expressions.MemberBindingType.MemberBinding=>Expressions.Expression.MemberBind(member,reader.ReadArray<T>(Resolver)),
            _=>Expressions.Expression.ListBind(member,reader.ReadArray<Expressions.ElementInit>(Resolver))
        };
        reader.ReadIsEndArrayWithVerify();
        return MemberBinding;
    }
    public T Deserialize(ref Reader reader,O Resolver) {
        if(reader.TryReadNil()) return null!;
        return Read(ref reader,Resolver);
    }
}
