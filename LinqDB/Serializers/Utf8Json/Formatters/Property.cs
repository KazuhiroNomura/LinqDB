﻿using System;
using System.Diagnostics;
using System.Reflection;
using Utf8Json;
namespace LinqDB.Serializers.Utf8Json.Formatters;
using Writer=JsonWriter;
using Reader=JsonReader;
using static Extension;
using C=Serializer;
public class Property:IJsonFormatter<PropertyInfo>{
    public static readonly Property Instance=new();
    internal static void Write(ref Writer writer,PropertyInfo? value,IJsonFormatterResolver Resolver){
        //if(writer.WriteIsNull(value))return;
        writer.WriteBeginArray();
        Debug.Assert(value!=null,nameof(value)+" != null");
        var type=value.ReflectedType;
        writer.WriteType(type);
        writer.WriteValueSeparator();
        writer.WriteString(value.Name);
        writer.WriteValueSeparator();
        writer.WriteInt32(Array.IndexOf(Resolver.Serializer().TypeProperties.Get(type),value));
        writer.WriteEndArray();
    }
    public void Serialize(ref Writer writer,PropertyInfo? value,IJsonFormatterResolver Resolver){
        Write(ref writer,value,Resolver);
    }
    internal static PropertyInfo Read(ref Reader reader,IJsonFormatterResolver Resolver){
        //if(reader.ReadIsNull()) return null!;
        reader.ReadIsBeginArrayWithVerify();
        var type= reader.ReadType();
        reader.ReadIsValueSeparatorWithVerify();
        var name=reader.ReadString();
        reader.ReadIsValueSeparatorWithVerify();
        var index=reader.ReadInt32();
        reader.ReadIsEndArrayWithVerify();
        return Resolver.Serializer().TypeProperties.Get(type)[index];
    }
    public PropertyInfo Deserialize(ref Reader reader,IJsonFormatterResolver Resolver){
        return Read(ref reader,Resolver);
    }
}