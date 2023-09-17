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
    internal static void Write(ref Writer writer,T? value,MessagePackSerializerOptions Resolver){
        writer.WriteArrayHeader(3);
        var type=value!.ReflectedType!;
        writer.WriteType(type);
        
        writer.Write(value.Name);
        
        writer.WriteInt32(Array.IndexOf(Resolver.Serializer().TypeProperties.Get(type),value));
        
    }
    public void Serialize(ref Writer writer,T? value,MessagePackSerializerOptions Resolver){
        if(writer.TryWriteNil(value)) return;
        Write(ref writer,value,Resolver);
    }
    internal static T Read(ref Reader reader,MessagePackSerializerOptions Resolver){
        var count=reader.ReadArrayHeader();Debug.Assert(count==3);
        var type=reader.ReadType();
        
        var name=reader.ReadString();
        
        var index=reader.ReadInt32();

        return Resolver.Serializer().TypeProperties.Get(type)[index];
    }
    public T Deserialize(ref Reader reader,MessagePackSerializerOptions Resolver)=>reader.TryReadNil()?null!:Read(ref reader,Resolver);
}
