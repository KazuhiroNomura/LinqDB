using System;
using MessagePack;
using MessagePack.Formatters;
using Expressions = System.Linq.Expressions;
namespace LinqDB.Serializers.MessagePack.Formatters;
using Reflection;
using O=MessagePackSerializerOptions;
using Writer = MessagePackWriter;
using Reader = MessagePackReader;
using T = Expressions.MemberBinding;
public class MemberBinding:IMessagePackFormatter<T> {
    public static readonly MemberBinding Instance=new();
    private static void PrivateWrite(ref Writer writer,T value,O Resolver){
 
        writer.Write((byte)value.BindingType);
        
        Member.Write(ref writer,value.Member,Resolver);

       switch(value.BindingType){
            case Expressions.MemberBindingType.Assignment:
                Expression.Write(ref writer,((Expressions.MemberAssignment)value).Expression,Resolver);
                break;
            case Expressions.MemberBindingType.MemberBinding:
                writer.WriteCollection(((Expressions.MemberMemberBinding)value).Bindings,Resolver);
                break;
            default:
                System.Diagnostics.Debug.Assert(value.BindingType==Expressions.MemberBindingType.ListBinding);
                writer.WriteCollection(((Expressions.MemberListBinding)value).Initializers,Resolver);
                break;
        }
        
    }
    public void Serialize(ref Writer writer,T value,O Resolver){
        if(writer.TryWriteNil(value)) return;
        writer.WriteArrayHeader(3);
        PrivateWrite(ref writer,value,Resolver);
    }
    internal static T Read(ref Reader reader,O Resolver){
        
        var BindingType=(Expressions.MemberBindingType)reader.ReadByte();
        
        var member= Member.Read(ref reader,Resolver);
        
        System.Diagnostics.Debug.Assert(BindingType is Expressions.MemberBindingType.Assignment or Expressions.MemberBindingType.MemberBinding or Expressions.MemberBindingType.ListBinding);
        T MemberBinding =BindingType switch{
            Expressions.MemberBindingType.Assignment=>Expressions.Expression.Bind(member,Expression.Read(ref reader,Resolver)),
            Expressions.MemberBindingType.MemberBinding=>Expressions.Expression.MemberBind(member,reader.ReadArray<T>(Resolver)),
            _=>Expressions.Expression.ListBind(member,reader.ReadArray<Expressions.ElementInit>(Resolver))
        };
        
        return MemberBinding;
    }
    public T Deserialize(ref Reader reader,O Resolver){
        if(reader.TryReadNil()) return null!;
        var count=reader.ReadArrayHeader();
        return Read(ref reader,Resolver);
    }
}
