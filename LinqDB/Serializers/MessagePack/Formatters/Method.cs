using System;
using System.Diagnostics;
using System.Reflection;
using MessagePack;
using MessagePack.Formatters;
namespace LinqDB.Serializers.MessagePack.Formatters;
using Writer=MessagePackWriter;
using Reader=MessagePackReader;
using T=MethodInfo;
public class Method:IMessagePackFormatter<T>{
    public static readonly Method Instance=new();
    private const int ArrayHeader=2;
    private static void PrivateSerialize(ref Writer writer,T? value,MessagePackSerializerOptions Resolver){
        writer.WriteArrayHeader(ArrayHeader);
        var ReflectedType=value!.ReflectedType!;
        writer.WriteType(ReflectedType);
        var array= Serializer.Instance.TypeMethods.Get(ReflectedType!);
        writer.WriteInt32(Array.IndexOf(array,value));
    }
    internal static void InternalSerializeNullable(ref Writer writer,T? value,MessagePackSerializerOptions Resolver){
        if(value is null)writer.WriteNil();
        else PrivateSerialize(ref writer,value,Resolver);
    }
    public void Serialize(ref Writer writer,T? value,MessagePackSerializerOptions Resolver){
        PrivateSerialize(ref writer,value,Resolver);
    }
    private static T PrivateDeserialize(ref Reader reader,MessagePackSerializerOptions Resolver){
        var count=reader.ReadArrayHeader();
        Debug.Assert(count==ArrayHeader);
        var type=reader.ReadType();
        var array= Serializer.Instance.TypeMethods.Get(type);
        var index=reader.ReadInt32();
        return array[index];
    }
    internal static T? InternalDeserializeNullable(ref Reader reader,MessagePackSerializerOptions Resolver){
        return reader.TryReadNil()?null:PrivateDeserialize(ref reader,Resolver);
    }
    public T Deserialize(ref Reader reader,MessagePackSerializerOptions Resolver){
        return PrivateDeserialize(ref reader,Resolver);
    }
}
