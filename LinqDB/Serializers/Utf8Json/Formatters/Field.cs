using System;
using System.Diagnostics;
using System.Reflection;
using Utf8Json;
namespace LinqDB.Serializers.Utf8Json.Formatters;
using Writer = JsonWriter;
using Reader = JsonReader;
using T = FieldInfo;
using static Extension;
public class Field:IJsonFormatter<T> {
    public static readonly Field Instance=new();
    internal static void Write(ref Writer writer,T? value,IJsonFormatterResolver Resolver){
        Debug.Assert(value!=null,nameof(value)+" != null");
        writer.WriteBeginArray();
        var type=value.ReflectedType;
        writer.WriteType(type);
        writer.WriteValueSeparator();
        writer.WriteString(value.Name);
        writer.WriteValueSeparator();
        var index=Array.IndexOf(Resolver.Serializer().TypeFields.Get(type),value);
        writer.WriteInt32(index);
        writer.WriteEndArray();
    }
    public void Serialize(ref Writer writer,T? value,IJsonFormatterResolver Resolver){
        if(writer.TryWriteNil(value))return;
        Write(ref writer,value,Resolver);
    }
    internal static T Read(ref Reader reader,IJsonFormatterResolver Resolver){
        reader.ReadIsBeginArrayWithVerify();
        var type= reader.ReadType();
        reader.ReadIsValueSeparatorWithVerify();
        var name=reader.ReadString();
        reader.ReadIsValueSeparatorWithVerify();
        var index=reader.ReadInt32();
        reader.ReadIsEndArrayWithVerify();
        return Resolver.Serializer().TypeFields.Get(type)[index];
    }
    public T Deserialize(ref Reader reader,IJsonFormatterResolver Resolver){
        if(reader.TryReadNil()) return null!;
        return Read(ref reader,Resolver);
    }
}
