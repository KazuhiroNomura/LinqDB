using System;
using Utf8Json;
using Expressions = System.Linq.Expressions;
namespace LinqDB.Serializers.Utf8Json.Formatters;
using O=IJsonFormatterResolver;
using Writer = JsonWriter;
using Reader = JsonReader;
using T = Expressions.MemberBinding;
public class MemberBinding:IJsonFormatter<T> {
    public static readonly MemberBinding Instance=new();
    public void Serialize(ref Writer writer,T? value,O Resolver) {
        if(writer.TryWriteNil(value)) return;
        writer.WriteBeginArray();
        writer.WriteString(value!.BindingType.ToString());
        writer.WriteValueSeparator();
        System.Diagnostics.Debug.Assert(value!.BindingType is Expressions.MemberBindingType.Assignment or Expressions.MemberBindingType.ListBinding or Expressions.MemberBindingType.MemberBinding);
        switch(value.BindingType){
            case Expressions.MemberBindingType.Assignment :MemberAssignment   .Write(ref writer,(Expressions.MemberAssignment   )value,Resolver);break; 
            case Expressions.MemberBindingType.ListBinding:MemberListBinding  .Write(ref writer,(Expressions.MemberListBinding  )value,Resolver);break;
            default                                       :MemberMemberBinding.Write(ref writer,(Expressions.MemberMemberBinding)value,Resolver);break;
        }
        writer.WriteEndArray(); 
    }
    public T Deserialize(ref Reader reader,O Resolver) {
        if(reader.TryReadNil()) return null!;
        reader.ReadIsBeginArrayWithVerify();
        var BindingType=Enum.Parse<Expressions.MemberBindingType>(reader.ReadString());
        reader.ReadIsValueSeparatorWithVerify();
        System.Diagnostics.Debug.Assert(BindingType is Expressions.MemberBindingType.Assignment or Expressions.MemberBindingType.MemberBinding or Expressions.MemberBindingType.ListBinding);
        T memberBinding =BindingType switch{
            Expressions.MemberBindingType.Assignment =>MemberAssignment   .Read(ref reader,Resolver),
            Expressions.MemberBindingType.ListBinding=>MemberListBinding  .Read(ref reader,Resolver),
            _                                        =>MemberMemberBinding.Read(ref reader,Resolver)
        };
        reader.ReadIsEndArrayWithVerify();
        return memberBinding;
    }
}
