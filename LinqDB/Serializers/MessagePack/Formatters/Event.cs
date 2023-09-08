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
using T= EventInfo;
public class Event:IMessagePackFormatter<T>{
    public static readonly Event Instance=new();
    private const int ArrayHeader=2;
    public void Serialize(ref Writer writer,T? value,MessagePackSerializerOptions Resolver){
        writer.WriteArrayHeader(ArrayHeader);
        var ReflectedType=value!.ReflectedType!;
        writer.WriteType(ReflectedType);
        var Methods= Serializer.Instance.TypeEvents.Get(ReflectedType);
        writer.WriteInt32(Array.IndexOf(Methods,value));
    }
    public T Deserialize(ref Reader reader,MessagePackSerializerOptions Resolver){
        var count=reader.ReadArrayHeader();
        Debug.Assert(count==ArrayHeader);
        var type=reader.ReadType();
        var results= Serializer.Instance.TypeEvents.Get(type);
        var Index=reader.ReadInt32();
        return results[Index];
    }
}
