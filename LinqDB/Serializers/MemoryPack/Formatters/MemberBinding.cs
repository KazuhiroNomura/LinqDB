using MemoryPack;
using System.Buffers;
using Expressions = System.Linq.Expressions;
namespace LinqDB.Serializers.MemoryPack.Formatters;


using Reader = MemoryPackReader;
using T = Expressions.MemberBinding;
public class MemberBinding:MemoryPackFormatter<T> {
    public static readonly MemberBinding Instance=new();
    internal static void Write<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,T value)where TBufferWriter:IBufferWriter<byte>{

        writer.WriteVarInt((byte)value.BindingType);

        System.Diagnostics.Debug.Assert(value.BindingType is Expressions.MemberBindingType.Assignment or Expressions.MemberBindingType.ListBinding or Expressions.MemberBindingType.MemberBinding);
        switch(value.BindingType){
            case Expressions.MemberBindingType.Assignment :MemberAssignment   .Write(ref writer,(Expressions.MemberAssignment   )value);break; 
            case Expressions.MemberBindingType.ListBinding:MemberListBinding  .Write(ref writer,(Expressions.MemberListBinding  )value);break;
            default                                       :MemberMemberBinding.Write(ref writer,(Expressions.MemberMemberBinding)value);break;
        }
        
    }
    public override void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,scoped ref T? value){
        if(writer.TryWriteNil(value)) return;

        Write(ref writer,value);
    }
    public override void Deserialize(ref Reader reader,scoped ref T? value){
        if(reader.TryReadNil()) return;
        
        var BindingType=(Expressions.MemberBindingType)reader.ReadVarIntByte();
        
        System.Diagnostics.Debug.Assert(BindingType is Expressions.MemberBindingType.Assignment or Expressions.MemberBindingType.ListBinding or Expressions.MemberBindingType.MemberBinding);
        T MemberBinding =BindingType switch{
            Expressions.MemberBindingType.Assignment =>MemberAssignment   .Read(ref reader),
            Expressions.MemberBindingType.ListBinding=>MemberListBinding  .Read(ref reader),
            _                                        =>MemberMemberBinding.Read(ref reader)
        };
        
        value=MemberBinding;
    }
}
