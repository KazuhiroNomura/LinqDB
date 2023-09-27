﻿using System;

using System.Reflection;
using Utf8Json;

namespace LinqDB.Serializers.Utf8Json.Formatters.Reflection;
using Writer = JsonWriter;
using Reader = JsonReader;
using T = MemberInfo;
public class Member:IJsonFormatter<T>{
    public static readonly Member Instance=new();
    internal static void Write(ref Writer writer,T value,IJsonFormatterResolver Resolver){
        writer.WriteBeginArray();
        var type=value.ReflectedType;
        writer.WriteType(type);
        writer.WriteValueSeparator();
        writer.WriteString(value.Name);
        writer.WriteValueSeparator();
        var array=Resolver.Serializer().TypeMembers.Get(type);
        var index=Array.IndexOf(array,value);
        writer.WriteInt32(index);
        writer.WriteEndArray();
    }
    public void Serialize(ref Writer writer,T value,IJsonFormatterResolver Resolver){
        if(writer.TryWriteNil(value)) return;
        Write(ref writer,value,Resolver);
    }
    internal static T Read(ref Reader reader,IJsonFormatterResolver Resolver){
        reader.ReadIsBeginArrayWithVerify();
        var type=reader.ReadType();
        reader.ReadIsValueSeparatorWithVerify();
        var name=reader.ReadString();
        reader.ReadIsValueSeparatorWithVerify();
        var index=reader.ReadInt32();
        reader.ReadIsEndArrayWithVerify();
        var array=Resolver.Serializer().TypeMembers.Get(type);
        return array[index];
    }
    public T Deserialize(ref Reader reader,IJsonFormatterResolver Resolver)=>reader.TryReadNil()?null!:Read(ref reader,Resolver);
}
