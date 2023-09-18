using System;
using System.Diagnostics;
using System.Reflection;
using MessagePack;
using MessagePack.Formatters;
namespace LinqDB.Serializers.MessagePack.Formatters;
using Writer = MessagePackWriter;
using Reader = MessagePackReader;
using T = MemberInfo;
public class Member:IMessagePackFormatter<T>{
    public static readonly Member Instance=new();
    internal static void Write(ref Writer writer,T? value,MessagePackSerializerOptions Resolver){
        writer.WriteArrayHeader(2);
        var ReflectedType=value!.ReflectedType!;
        writer.WriteType(ReflectedType);


        
        var array= Resolver.Serializer().TypeMembers.Get(ReflectedType);
        var index=Array.IndexOf(array,value);
        writer.WriteInt32(index);
        
    }
    public void Serialize(ref Writer writer,T? value,MessagePackSerializerOptions Resolver){
        if(writer.TryWriteNil(value)) return;
        Write(ref writer,value,Resolver);
    }
    internal static T Read(ref Reader reader,MessagePackSerializerOptions Resolver){
        var count=reader.ReadArrayHeader();Debug.Assert(count==2);
        var type=reader.ReadType();



        var index=reader.ReadInt32();

        var array= Resolver.Serializer().TypeMembers.Get(type);
        return array[index];
    }
    public T Deserialize(ref Reader reader,MessagePackSerializerOptions Resolver){
        if(reader.TryReadNil()) return null!;
        return Read(ref reader,Resolver);
    }
}
