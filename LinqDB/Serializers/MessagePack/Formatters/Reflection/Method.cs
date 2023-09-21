﻿using System;
using System.Diagnostics;
using System.Reflection;
using MessagePack;
using MessagePack.Formatters;
namespace LinqDB.Serializers.MessagePack.Formatters.Reflection;
using Writer = MessagePackWriter;
using Reader = MessagePackReader;
using T = MethodInfo;
public class Method:IMessagePackFormatter<T>{
    public static readonly Method Instance=new();
    private const int ArrayHeader=2;
    internal static void Write(ref Writer writer,T? value,MessagePackSerializerOptions Resolver){
        writer.WriteArrayHeader(ArrayHeader);
        var type=value!.ReflectedType;
        writer.WriteType(type);



        var array=Resolver.Serializer().TypeMethods.Get(type);
        var index=Array.IndexOf(array,value);
        writer.WriteInt32(index);

    }
    internal static void WriteNullable(ref Writer writer,T? value,MessagePackSerializerOptions Resolver){
        if(writer.TryWriteNil(value)) return;
        Write(ref writer,value,Resolver);
    }
    public void Serialize(ref Writer writer,T? value,MessagePackSerializerOptions Resolver)=>WriteNullable(ref writer,value,Resolver);
    internal static T Read(ref Reader reader,MessagePackSerializerOptions Resolver){
        var count=reader.ReadArrayHeader();
        Debug.Assert(count==ArrayHeader);
        var type=reader.ReadType();



        var array=Resolver.Serializer().TypeMethods.Get(type);
        var index=reader.ReadInt32();

        return array[index];
    }
    internal static T? ReadNullable(ref Reader reader,MessagePackSerializerOptions Resolver)=>reader.TryReadNil()?null:Read(ref reader,Resolver);
    public T Deserialize(ref Reader reader,MessagePackSerializerOptions Resolver){
        return ReadNullable(ref reader,Resolver)!;
    }
}