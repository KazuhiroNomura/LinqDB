using System;
using Expressions = System.Linq.Expressions;
using MemoryPack;
using System.Buffers;

namespace LinqDB.Serializers.MemoryPack.Formatters;
using Reader = MemoryPackReader;
using T = Expressions.MemberBinding;
using static Extension;

public class MemberBinding:MemoryPackFormatter<T> {
    public static readonly MemberBinding Instance=new();
    internal static void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,T? value)where TBufferWriter:IBufferWriter<byte> =>
        Instance.Serialize(ref writer,ref value);
    internal static T DeserializeMemberBinding(ref Reader reader){
        T? value=default;
        Instance.Deserialize(ref reader,ref value);
        return value!;
    }
    //private static readonly ReadOnlyCollectionFormatter<Expressions.MemberBinding>SerializeBindings=new();
    //private static readonly ReadOnlyCollectionFormatter<Expressions.ElementInit>SerializeElementInits=new();
    public override void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,scoped ref T? value){
        writer.WriteVarInt((byte)value!.BindingType);
        Member.Serialize(ref writer,value.Member);
        switch(value.BindingType){
            case Expressions.MemberBindingType.Assignment:
                Expression.InternalSerialize(ref writer,((Expressions.MemberAssignment)value).Expression);
                break;
            case Expressions.MemberBindingType.MemberBinding:{
                writer.SerializeReadOnlyCollection(((Expressions.MemberMemberBinding)value).Bindings);
                break;
            }
            case Expressions.MemberBindingType.ListBinding:{
                writer.SerializeReadOnlyCollection(((Expressions.MemberListBinding)value).Initializers);
                break;
            }
            default:
                throw new ArgumentOutOfRangeException(value.BindingType.ToString());
        }
    }
    public override void Deserialize(ref Reader reader,scoped ref T? value){
        var BindingType=(Expressions.MemberBindingType)reader.ReadVarIntByte();
        //MemberInfo?member=default;
        var member= Member.Deserialize(ref reader);
        T MemberBinding =BindingType switch{
            Expressions.MemberBindingType.Assignment=>Expressions.Expression.Bind(member,Expression.InternalDeserialize(ref reader)),
            Expressions.MemberBindingType.MemberBinding=>Expressions.Expression.MemberBind(member,reader.ReadArray<T>()!),
            Expressions.MemberBindingType.ListBinding=>Expressions.Expression.ListBind(member,reader.ReadArray<Expressions.ElementInit>()!),
            _=>throw new ArgumentOutOfRangeException(BindingType.ToString())
        };
        value=MemberBinding;
    }
}
