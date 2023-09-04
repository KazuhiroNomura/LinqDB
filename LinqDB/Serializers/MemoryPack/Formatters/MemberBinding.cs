using System;
using Expressions=System.Linq.Expressions;
using MemoryPack;
using MemoryPack.Formatters;
using System.Buffers;

namespace LinqDB.Serializers.MemoryPack.Formatters;


public class MemberBinding:MemoryPackFormatter<Expressions.MemberBinding>{
    private readonly 必要なFormatters Formatters;
    public MemberBinding(必要なFormatters Formatters)=>this.Formatters=Formatters;
    internal void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,Expressions.MemberBinding? value)where TBufferWriter:IBufferWriter<byte> =>this.Serialize(ref writer,ref value);
    internal Expressions.MemberBinding DeserializeMemberBinding(ref MemoryPackReader reader){
        Expressions.MemberBinding? value=default;
        this.Deserialize(ref reader,ref value);
        return value!;
    }
    //private static readonly ReadOnlyCollectionFormatter<Expressions.MemberBinding>SerializeBindings=new();
    //private static readonly ReadOnlyCollectionFormatter<Expressions.ElementInit>SerializeElementInits=new();
    public override void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,scoped ref Expressions.MemberBinding? value){
        writer.WriteVarInt((byte)value!.BindingType);
        this.Formatters.Member.Serialize(ref writer,value.Member);
        switch(value.BindingType){
            case Expressions.MemberBindingType.Assignment:
                this.Formatters.Expression.Serialize(ref writer,((Expressions.MemberAssignment)value).Expression);
                break;
            case Expressions.MemberBindingType.MemberBinding:{
                var Bindings=((Expressions.MemberMemberBinding)value).Bindings;
                必要なFormatters.MemberBindings.Serialize(ref writer,ref Bindings!);
                break;
            }
            case Expressions.MemberBindingType.ListBinding:{
                var Initializers=((Expressions.MemberListBinding)value).Initializers;
                必要なFormatters.ElementInits.Serialize(ref writer,ref Initializers!);
                break;
            }
            default:
                throw new ArgumentOutOfRangeException(value.BindingType.ToString());
        }
    }
    public override void Deserialize(ref MemoryPackReader reader,scoped ref Expressions.MemberBinding? value){
        var BindingType=(Expressions.MemberBindingType)reader.ReadVarIntByte();
        //MemberInfo?member=default;
        var member=this.Formatters.Member.DeserializeMemberInfo(ref reader);
        Expressions.MemberBinding MemberBinding=BindingType switch{
            Expressions.MemberBindingType.Assignment=>Expressions.Expression.Bind(member,this.Formatters.Expression.Deserialize(ref reader)),
            Expressions.MemberBindingType.MemberBinding=>Expressions.Expression.MemberBind(member,reader.ReadArray<Expressions.MemberBinding>()!),
            Expressions.MemberBindingType.ListBinding=>Expressions.Expression.ListBind(member,reader.ReadArray<Expressions.ElementInit>()!),
            _=>throw new ArgumentOutOfRangeException(BindingType.ToString())
        };
        value=MemberBinding;
    }
}
