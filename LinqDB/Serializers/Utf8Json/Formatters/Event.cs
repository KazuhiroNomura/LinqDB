﻿using System;
using System.Diagnostics;
using System.Reflection;
using Utf8Json;
namespace LinqDB.Serializers.Utf8Json.Formatters;
using Writer = JsonWriter;
using Reader = JsonReader;
using T = EventInfo;
using static Extension;

public class Event:IJsonFormatter<T> {
    public static readonly Event Instance=new();
    internal static void Write(ref Writer writer,T? value,IJsonFormatterResolver Resolver){
  //      if(writer.WriteIsNull(value))return;
        Debug.Assert(value!=null,nameof(value)+" != null");
        writer.WriteBeginArray();
        var type=value.ReflectedType;
        writer.WriteType(type);
        writer.WriteValueSeparator();
        writer.WriteString(value.Name);
        writer.WriteValueSeparator();
        writer.WriteInt32(Array.IndexOf(Resolver.Serializer().TypeEvents.Get(type),value));
        writer.WriteEndArray();
    }
    public void Serialize(ref Writer writer,T? value,IJsonFormatterResolver Resolver){
        Write(ref writer,value,Resolver);
    }
    internal static  T Read(ref Reader reader,IJsonFormatterResolver Resolver){
        //if(reader.ReadIsNull()) return null!;
        reader.ReadIsBeginArrayWithVerify();
        var type= reader.ReadType();
        reader.ReadIsValueSeparatorWithVerify();
        var name=reader.ReadString();
        reader.ReadIsValueSeparatorWithVerify();
        var index=reader.ReadInt32();
        reader.ReadIsEndArrayWithVerify();
        return Resolver.Serializer().TypeEvents.Get(type)[index];
    }
    public T Deserialize(ref Reader reader,IJsonFormatterResolver Resolver){
        return Read(ref reader,Resolver);
    }
}
