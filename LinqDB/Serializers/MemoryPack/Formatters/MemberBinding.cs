using System;
using Expressions = System.Linq.Expressions;
using MemoryPack;

namespace LinqDB.Serializers.MemoryPack.Formatters;
using Reader = MemoryPackReader;
using T = Expressions.MemberBinding;
using static Extension;

public class MemberBinding:MemoryPackFormatter<T> {
    public static readonly MemberBinding Instance=new();
    public override void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,scoped ref T? value){
        writer.WriteVarInt((byte)value!.BindingType);
        Member.Write(ref writer,value.Member);
        switch(value.BindingType){
            case Expressions.MemberBindingType.Assignment:
                Expression.Write(ref writer,((Expressions.MemberAssignment)value).Expression);
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
        var member= Member.Read(ref reader);
        T MemberBinding =BindingType switch{
            Expressions.MemberBindingType.Assignment=>Expressions.Expression.Bind(member,Expression.Read(ref reader)),
            Expressions.MemberBindingType.MemberBinding=>Expressions.Expression.MemberBind(member,reader.ReadArray<T>()!),
            Expressions.MemberBindingType.ListBinding=>Expressions.Expression.ListBind(member,reader.ReadArray<Expressions.ElementInit>()!),
            _=>throw new ArgumentOutOfRangeException(BindingType.ToString())
        };
        value=MemberBinding;
    }
}
