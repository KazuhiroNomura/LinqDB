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
    internal static void Write(ref Writer writer,T? value,MessagePackSerializerOptions Resolver){
        Debug.Assert(value!=null,nameof(value)+" != null");
        writer.WriteArrayHeader(ArrayHeader);
        var type=value.ReflectedType!;
        writer.WriteType(type);
        var methods= Resolver.Serializer().TypeFields.Get(type);
        writer.WriteInt32(Array.IndexOf(methods,value));
    }
    public void Serialize(ref Writer writer,T? value,MessagePackSerializerOptions Resolver){
        Write(ref writer,value,Resolver);
    }
    internal static T Read(ref Reader reader,MessagePackSerializerOptions Resolver){
        var count=reader.ReadArrayHeader();
        Debug.Assert(count==ArrayHeader);
        var type=reader.ReadType();
        var results= Resolver.Serializer().TypeFields.Get(type);
        var index=reader.ReadInt32();
        return results[index];
    }
    public T Deserialize(ref Reader reader,MessagePackSerializerOptions Resolver){
        return Read(ref reader,Resolver);
    }
}
