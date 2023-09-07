using System;
using System.Linq;
using System.Reflection;

using LinqDB.Serializers.MemoryPack;

using MessagePack;
using MessagePack.Formatters;
using System.Diagnostics;

namespace LinqDB.Serializers.MessagePack.Formatters;
using Writer=MessagePackWriter;
using Reader=MessagePackReader;
using T=System.Reflection.EventInfo;
using C=LinqDB.Serializers.MessagePack.MessagePackCustomSerializer;
public class Event:IMessagePackFormatter<T>{
    public static readonly Event Instance=new();
    public void Serialize(ref MessagePackWriter writer,T? value,MessagePackSerializerOptions Resolver){
        Debug.Assert(value!=null,nameof(value)+" != null");
        var ReflectedType=value.ReflectedType!;
        writer.WriteType(ReflectedType);
        var Methods= C.Instance.TypeEvents.Get(ReflectedType);
        writer.WriteInt32(Array.IndexOf(Methods,value));
    }
    public T Deserialize(ref MessagePackReader reader,MessagePackSerializerOptions Resolver){
        var type=reader.ReadType();
        var results= C.Instance.TypeEvents.Get(type);
        var Index=reader.ReadInt32();
        return results[Index];
    }
}
