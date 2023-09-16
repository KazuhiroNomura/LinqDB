using System;
using System.Diagnostics;
using System.Reflection;
using MessagePack;
using MessagePack.Formatters;
namespace LinqDB.Serializers.MessagePack.Formatters;
using Writer = MessagePackWriter;
using Reader = MessagePackReader;
using T = ConstructorInfo;
public class Constructor:IMessagePackFormatter<T> {
    public static readonly Constructor Instance=new();
    private const int ArrayHeader=2;
    internal static void Write(ref Writer writer,T value,MessagePackSerializerOptions Resolver){
        writer.WriteArrayHeader(ArrayHeader);
        var ReflectedType=value.ReflectedType!;
        writer.WriteType(ReflectedType);
        var array= Resolver.Serializer().TypeConstructors.Get(ReflectedType!);
        writer.WriteInt32(Array.IndexOf(array,value));
    }
    public void Serialize(ref Writer writer,T value,MessagePackSerializerOptions Resolver){
        Write(ref writer,value,Resolver);
    }
    internal static T Read(ref Reader reader,MessagePackSerializerOptions Resolver){
        var count=reader.ReadArrayHeader();
        Debug.Assert(count==ArrayHeader);
        var type=reader.ReadType();
        var array= Resolver.Serializer().TypeConstructors.Get(type);
        var index=reader.ReadInt32();
        return array[index];
    }
    public T Deserialize(ref Reader reader,MessagePackSerializerOptions Resolver){
        return Read(ref reader,Resolver);
    }
}
