using System;
using Expressions=System.Linq.Expressions;
using MessagePack;
using MessagePack.Formatters;
using System.Diagnostics;

namespace LinqDB.Serializers.MessagePack.Formatters;
using Writer=MessagePackWriter;
using Reader=MessagePackReader;
using T=Expressions.MemberBinding;
using static Common;
public class MemberBinding:IMessagePackFormatter<T> {
    public static readonly MemberBinding Instance=new();
    private const int ArrayHeader=3;
    public void Serialize(ref Writer writer,T value,MessagePackSerializerOptions Resolver){
        writer.WriteArrayHeader(ArrayHeader);
        writer.Write((byte)value.BindingType);
        Member.Instance.Serialize(ref writer,value.Member,Resolver);
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
    }
    public T Deserialize(ref Reader reader,MessagePackSerializerOptions Resolver){
        var count=reader.ReadArrayHeader();
        Debug.Assert(count==ArrayHeader);
        var BindingType=(Expressions.MemberBindingType)reader.ReadByte();
        var member= Member.Instance.Deserialize(ref reader,Resolver);
        T MemberBinding =BindingType switch{
            Expressions.MemberBindingType.Assignment=>Expressions.Expression.Bind(member,Expression.Instance.Deserialize(ref reader,Resolver)),
            Expressions.MemberBindingType.MemberBinding=>Expressions.Expression.MemberBind(member,DeserializeArray<T>(ref reader,Resolver)),
            Expressions.MemberBindingType.ListBinding=>Expressions.Expression.ListBind(member,DeserializeArray<Expressions.ElementInit>(ref reader,Resolver)),
            _=>throw new ArgumentOutOfRangeException(BindingType.ToString())
        };
        return MemberBinding;
    }
}
