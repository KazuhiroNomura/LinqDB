﻿using System.Diagnostics;
using MessagePack;
using MessagePack.Formatters;
using Expressions=System.Linq.Expressions;
namespace LinqDB.Serializers.MessagePack.Formatters;
using O=MessagePackSerializerOptions;
using Writer=MessagePackWriter;
using Reader=MessagePackReader;
using T=Expressions.MemberInitExpression;
public class MemberInit:IMessagePackFormatter<T> {
    public static readonly MemberInit Instance=new();
    private static void PrivateWrite(ref Writer writer,T? value,O Resolver){
        New.WriteNew(ref writer,value!.NewExpression,Resolver);
        
        writer.WriteCollection(value.Bindings,Resolver);
    }
    internal static void Write(ref Writer writer,T value,O Resolver){
        writer.WriteArrayHeader(3);
        writer.WriteNodeType(Expressions.ExpressionType.MemberInit);
        
        PrivateWrite(ref writer,value,Resolver);
        
    }
    public void Serialize(ref Writer writer,T? value,O Resolver){
        if(writer.TryWriteNil(value)) return;
        writer.WriteArrayHeader(2);
        PrivateWrite(ref writer,value,Resolver);
        
    }
    internal static T Read(ref Reader reader,O Resolver){
        var @new=New.ReadNew(ref reader,Resolver);
        
        var bindings=reader.ReadArray<Expressions.MemberBinding>(Resolver);
        return Expressions.Expression.MemberInit(@new,bindings);
    }
    public T Deserialize(ref Reader reader,O Resolver){
        if(reader.TryReadNil()) return null!;
        var count=reader.ReadArrayHeader();
        Debug.Assert(count==2);
        
        
        return Read(ref reader,Resolver);
    }
}
