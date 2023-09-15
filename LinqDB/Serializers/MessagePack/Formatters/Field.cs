using System;
using System.Reflection;
using MessagePack;
using MessagePack.Formatters;
using System.Diagnostics;

namespace LinqDB.Serializers.MessagePack.Formatters;
using Writer=MessagePackWriter;
using Reader=MessagePackReader;
using T=FieldInfo;
public class Field:IMessagePackFormatter<T>{
    public static readonly Field Instance=new();
    private const int ArrayHeader=2;
    public void Serialize(ref Writer writer,T? value,MessagePackSerializerOptions Resolver){
        Debug.Assert(value!=null,nameof(value)+" != null");
        writer.WriteArrayHeader(ArrayHeader);
        var ReflectedType=value.ReflectedType!;
        writer.WriteType(ReflectedType);
        var Methods= Resolver.Serializer().TypeFields.Get(ReflectedType);
        writer.WriteInt32(Array.IndexOf(Methods,value));
    }
    public T Deserialize(ref Reader reader,MessagePackSerializerOptions Resolver){
        var count=reader.ReadArrayHeader();
        Debug.Assert(count==ArrayHeader);
        var type=reader.ReadType();
        var results= Resolver.Serializer().TypeFields.Get(type);
        var Index=reader.ReadInt32();
        return results[Index];
    }
}
