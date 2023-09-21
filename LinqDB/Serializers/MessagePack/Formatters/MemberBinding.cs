﻿using System;
using System.Diagnostics;
using MessagePack;
using MessagePack.Formatters;
using Expressions = System.Linq.Expressions;
namespace LinqDB.Serializers.MessagePack.Formatters;
using O=MessagePackSerializerOptions;
using Reflection;
using Writer = MessagePackWriter;
using Reader = MessagePackReader;
using T = Expressions.MemberBinding;
public class MemberBinding:IMessagePackFormatter<T> {
    public static readonly MemberBinding Instance=new();
    private const int ArrayHeader=3;
    public void Serialize(ref Writer writer,T value,O Resolver){
        writer.WriteArrayHeader(3);
        writer.Write((byte)value.BindingType);
        
        Member.Write(ref writer,value.Member,Resolver);

        switch(value.BindingType){
            case Expressions.MemberBindingType.Assignment:
                Expression.Write(ref writer,((Expressions.MemberAssignment)value).Expression,Resolver);
                break;
            case Expressions.MemberBindingType.MemberBinding:
                writer.WriteCollection(((Expressions.MemberMemberBinding)value).Bindings,Resolver);
                break;
            case Expressions.MemberBindingType.ListBinding:
                writer.WriteCollection(((Expressions.MemberListBinding)value).Initializers,Resolver);
                break;
            default:throw new ArgumentOutOfRangeException(value.BindingType.ToString());
        }
        
    }
    public T Deserialize(ref Reader reader,O Resolver){
        var count=reader.ReadArrayHeader();
        Debug.Assert(count==ArrayHeader);
        var BindingType=(Expressions.MemberBindingType)reader.ReadByte();
        
        var member= Member.Read(ref reader,Resolver);
        
        T MemberBinding =BindingType switch{
            Expressions.MemberBindingType.Assignment=>Expressions.Expression.Bind(member,Expression.Read(ref reader,Resolver)),
            Expressions.MemberBindingType.MemberBinding=>Expressions.Expression.MemberBind(member,reader.ReadArray<T>(Resolver)),
            Expressions.MemberBindingType.ListBinding=>Expressions.Expression.ListBind(member,reader.ReadArray<Expressions.ElementInit>(Resolver)),
            _=>throw new ArgumentOutOfRangeException(BindingType.ToString())
        };
        
        return MemberBinding;
    }
}
