using System;
using System.Reflection;
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
        var type=value!.ReflectedType!;
        writer.WriteType(type);
        var array= Resolver.Serializer().TypeEvents.Get(type);
        writer.WriteInt32(Array.IndexOf(array,value));
    }
    public T Deserialize(ref Reader reader,MessagePackSerializerOptions Resolver){
        var count=reader.ReadArrayHeader();
        Debug.Assert(count==ArrayHeader);
        var type=reader.ReadType();
        var array= Resolver.Serializer().TypeEvents.Get(type);
        var index=reader.ReadInt32();
        return array[index];
    }
}
