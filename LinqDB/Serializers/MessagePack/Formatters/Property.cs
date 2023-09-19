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
    internal static void Write(ref Writer writer,T value,MessagePackSerializerOptions Resolver){
        writer.WriteArrayHeader(3);
        var type=value!.ReflectedType!;
        writer.WriteType(type);
        

        
        var array=Resolver.Serializer().TypeProperties.Get(type!);
        var index=Array.IndexOf(array,value);
        writer.WriteInt32(index);
        
    }
    internal static void WriteNullable(ref Writer writer,T? value,MessagePackSerializerOptions Resolver){
        if(writer.TryWriteNil(value)) return;
        Write(ref writer,value,Resolver);
    }
    public void Serialize(ref Writer writer,T? value,MessagePackSerializerOptions Resolver){
        WriteNullable(ref writer,value,Resolver);
    }
    internal static T Read(ref Reader reader,MessagePackSerializerOptions Resolver){
        var count=reader.ReadArrayHeader();Debug.Assert(count==3);
        var type=reader.ReadType();
        

        
        var array=Resolver.Serializer().TypeProperties.Get(type);
        var index=reader.ReadInt32();

        return array[index];
    }
    internal static T? ReadNullable(ref Reader reader,MessagePackSerializerOptions Resolver)=>reader.TryReadNil()?null:Read(ref reader,Resolver);
    public T Deserialize(ref Reader reader,MessagePackSerializerOptions Resolver)=>ReadNullable(ref reader,Resolver)!;
}
