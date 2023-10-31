using MessagePack;
using MessagePack.Formatters;
using Expressions = System.Linq.Expressions;
namespace LinqDB.Serializers.MessagePack.Formatters;
using O=MessagePackSerializerOptions;
using Writer = MessagePackWriter;
using Reader = MessagePackReader;
using T = Expressions.MemberBinding;
public class MemberBinding:IMessagePackFormatter<T> {
    public static readonly MemberBinding Instance=new();
    public void Serialize(ref Writer writer,T value,O Resolver){
        if(writer.TryWriteNil(value)) return;
        System.Diagnostics.Debug.Assert(value.BindingType is Expressions.MemberBindingType.Assignment or Expressions.MemberBindingType.ListBinding or Expressions.MemberBindingType.MemberBinding);
        switch(value.BindingType){
            case Expressions.MemberBindingType.Assignment :MemberAssignment   .Write(ref writer,(Expressions.MemberAssignment   )value,Resolver); break; 
            case Expressions.MemberBindingType.ListBinding:MemberListBinding  .Write(ref writer,(Expressions.MemberListBinding  )value,Resolver); break;
            default                                       :MemberMemberBinding.Write(ref writer,(Expressions.MemberMemberBinding)value,Resolver); break;
        }
    }
    public T Deserialize(ref Reader reader,O Resolver){
        if(reader.TryReadNil()) return null!;
        var _=reader.ReadArrayHeader();
        var BindingType=(Expressions.MemberBindingType)reader.ReadByte();
       
        System.Diagnostics.Debug.Assert(BindingType is Expressions.MemberBindingType.Assignment or Expressions.MemberBindingType.MemberBinding or Expressions.MemberBindingType.ListBinding);
        return BindingType switch{
            Expressions.MemberBindingType.Assignment =>MemberAssignment   .Read(ref reader,Resolver),
            Expressions.MemberBindingType.ListBinding=>MemberListBinding  .Read(ref reader,Resolver),
            _                                        =>MemberMemberBinding.Read(ref reader,Resolver)
        };
    }
}
