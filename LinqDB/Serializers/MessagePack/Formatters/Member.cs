using System;
using System.Reflection;
using MessagePack;
using MessagePack.Formatters;
using System.Diagnostics;
using System.Linq;

namespace LinqDB.Serializers.MessagePack.Formatters;
using Writer=MessagePackWriter;
using Reader=MessagePackReader;
using T=System.Reflection.MemberInfo;
using C=LinqDB.Serializers.MessagePack.MessagePackCustomSerializer;
public class Member:IMessagePackFormatter<T>{
    public static readonly Member Instance=new();
    public void Serialize(ref MessagePackWriter writer,T? value,MessagePackSerializerOptions Resolver){
        Debug.Assert(value!=null,nameof(value)+" != null");
        var ReflectedType=value.ReflectedType!;
        writer.WriteType(ReflectedType);
        var array= C.Instance.TypeMembers.Get(ReflectedType);
        writer.WriteInt32(Array.IndexOf(array,value));
    }
    public T Deserialize(ref MessagePackReader reader,MessagePackSerializerOptions Resolver){
        var type=reader.ReadType();
        var array= C.Instance.TypeMembers.Get(type);
        var Index=reader.ReadInt32();
        return array[Index];
    }
}
