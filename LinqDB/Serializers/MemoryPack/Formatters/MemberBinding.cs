using System;
using Expressions = System.Linq.Expressions;
using MemoryPack;
using System.Buffers;

namespace LinqDB.Serializers.MemoryPack.Formatters;
using Reader=MemoryPackReader;
using T=Expressions.MemberBinding;
using C=Serializer;
using static Common;

public class MemberBinding:MemoryPackFormatter<T> {
    public static readonly MemberBinding Instance=new();
    internal void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,T? value)where TBufferWriter:IBufferWriter<byte> =>this.Serialize(ref writer,ref value);
    internal T DeserializeMemberBinding(ref Reader reader){
        T? value=default;
        this.Deserialize(ref reader,ref value);
        return value!;
    }
    //private static readonly ReadOnlyCollectionFormatter<Expressions.MemberBinding>SerializeBindings=new();
    //private static readonly ReadOnlyCollectionFormatter<Expressions.ElementInit>SerializeElementInits=new();
    public override void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,scoped ref T? value){
        writer.WriteVarInt((byte)value!.BindingType);
        Member.Instance.Serialize(ref writer,value.Member);
        switch(value.BindingType){
            case Expressions.MemberBindingType.Assignment:
                Expression.Instance.Serialize(ref writer,((Expressions.MemberAssignment)value).Expression);
                break;
            case Expressions.MemberBindingType.MemberBinding:{
                SerializeReadOnlyCollection(ref writer,((Expressions.MemberMemberBinding)value).Bindings);
                break;
            }
            case Expressions.MemberBindingType.ListBinding:{
                SerializeReadOnlyCollection(ref writer,((Expressions.MemberListBinding)value).Initializers);
                break;
            }
            default:
                throw new ArgumentOutOfRangeException(value.BindingType.ToString());
        }
    }
    public override void Deserialize(ref Reader reader,scoped ref T? value){
        var BindingType=(Expressions.MemberBindingType)reader.ReadVarIntByte();
        //MemberInfo?member=default;
        var member= Member.Instance.Deserialize(ref reader);
        T MemberBinding =BindingType switch{
            Expressions.MemberBindingType.Assignment=>Expressions.Expression.Bind(member,Expression.Instance.Deserialize(ref reader)),
            Expressions.MemberBindingType.MemberBinding=>Expressions.Expression.MemberBind(member,reader.ReadArray<T>()!),
            Expressions.MemberBindingType.ListBinding=>Expressions.Expression.ListBind(member,reader.ReadArray<Expressions.ElementInit>()!),
            _=>throw new ArgumentOutOfRangeException(BindingType.ToString())
        };
        value=MemberBinding;
    }
}
