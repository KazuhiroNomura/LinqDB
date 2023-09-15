using System;
using System.Diagnostics;
using System.Reflection;
using MessagePack;
using MessagePack.Formatters;
namespace LinqDB.Serializers.MessagePack.Formatters;
using Writer=MessagePackWriter;
using Reader=MessagePackReader;
using T=PropertyInfo;
public class Property:IMessagePackFormatter<T> {
    public static readonly Property Instance=new();
    private const int ArrayHeader=3;
    public void Serialize(ref Writer writer,T? value,MessagePackSerializerOptions Resolver){
        //if(writer.TryWriteNil(value)) return;
        writer.WriteArrayHeader(ArrayHeader);
        var ReflectedType=value!.ReflectedType!;
        writer.WriteType(ReflectedType);
        writer.Write(value.Name);
        writer.WriteInt32(Array.IndexOf(Resolver.Serializer().TypeProperties.Get(ReflectedType),value));
    }
    public T Deserialize(ref Reader reader,MessagePackSerializerOptions Resolver){
        //if(reader.TryReadNil()) return null!;
        var count=reader.ReadArrayHeader();
        Debug.Assert(count==ArrayHeader);
        var ReflectedType=reader.ReadType();
        var Name=reader.ReadString();
        var array= Resolver.Serializer().TypeProperties.Get(ReflectedType);
        var Index=reader.ReadInt32();
        return array[Index];
    }
}
