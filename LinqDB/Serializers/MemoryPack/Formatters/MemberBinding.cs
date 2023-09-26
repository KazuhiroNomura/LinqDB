using System;
using MemoryPack;
using System.Buffers;
using Expressions = System.Linq.Expressions;
namespace LinqDB.Serializers.MemoryPack.Formatters;
using Reflection;


using Reader = MemoryPackReader;
using T = Expressions.MemberBinding;
public class MemberBinding:MemoryPackFormatter<T> {
    public static readonly MemberBinding Instance=new();
    private static void PrivateWrite<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,T value)where TBufferWriter:IBufferWriter<byte>{

        writer.WriteVarInt((byte)value!.BindingType);

        Member.Write(ref writer,value.Member);
        
        switch(value.BindingType){
            case Expressions.MemberBindingType.Assignment:
                Expression.Write(ref writer,((Expressions.MemberAssignment)value).Expression);
                break;
            case Expressions.MemberBindingType.MemberBinding:
                writer.WriteCollection(((Expressions.MemberMemberBinding)value).Bindings);
                break;
            default:
                System.Diagnostics.Debug.Assert(value.BindingType==Expressions.MemberBindingType.ListBinding);
                writer.WriteCollection(((Expressions.MemberListBinding)value).Initializers);
                break;
        }
        
    }
    public override void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,scoped ref T? value){
        if(writer.TryWriteNil(value)) return;

        PrivateWrite(ref writer,value);
    }
    private static T Read(ref Reader reader){

        var BindingType=(Expressions.MemberBindingType)reader.ReadVarIntByte();
        
        var member= Member.Read(ref reader);
        
        System.Diagnostics.Debug.Assert(BindingType is Expressions.MemberBindingType.Assignment or Expressions.MemberBindingType.MemberBinding or Expressions.MemberBindingType.ListBinding);
        T MemberBinding =BindingType switch{
            Expressions.MemberBindingType.Assignment=>Expressions.Expression.Bind(member,Expression.Read(ref reader)),
            Expressions.MemberBindingType.MemberBinding=>Expressions.Expression.MemberBind(member,reader.ReadArray<T>()!),
            _=>Expressions.Expression.ListBind(member,reader.ReadArray<Expressions.ElementInit>()!)
        };
        
        return MemberBinding;
    }
    public override void Deserialize(ref Reader reader,scoped ref T? value){
        if(reader.TryReadNil()) return;
        value=Read(ref reader);
    }
}
