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
        var type=value.ReflectedType!;
        writer.WriteType(type);
        var array= Resolver.Serializer().TypeConstructors.Get(type!);
        writer.WriteInt32(Array.IndexOf(array,value));
    }
    public void Serialize(ref Writer writer,T value,MessagePackSerializerOptions Resolver){
        if(writer.TryWriteNil(value)) return;
        Write(ref writer,value,Resolver);
    }
    internal static T Read(ref Reader reader,MessagePackSerializerOptions Resolver){
        var count=reader.ReadArrayHeader();
        var type=reader.ReadType();
        Debug.Assert(count==ArrayHeader);
        var array= Resolver.Serializer().TypeConstructors.Get(type);
        var index=reader.ReadInt32();
        
        return array[index];
    }
    public T Deserialize(ref Reader reader,MessagePackSerializerOptions Resolver){
        return Read(ref reader,Resolver);
    }
}
