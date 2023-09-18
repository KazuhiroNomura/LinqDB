using System;

using System.Reflection;
using Utf8Json;

namespace LinqDB.Serializers.Utf8Json.Formatters;
using Writer = JsonWriter;
using Reader = JsonReader;
using T = MethodInfo;
public class Method:IJsonFormatter<T> {
    public static readonly Method Instance=new();
    
    internal static void Write(ref Writer writer,T value,IJsonFormatterResolver Resolver){
        writer.WriteBeginArray();
        var type=value!.ReflectedType;
        writer.WriteType(type);
        writer.WriteValueSeparator();
        writer.WriteString(value.Name);
        writer.WriteValueSeparator();
        var array= Resolver.Serializer().TypeMethods.Get(type!);
        writer.WriteInt32(Array.IndexOf(array,value));
        writer.WriteEndArray();
    }
    internal static void WriteNullable(ref Writer writer,T? value,IJsonFormatterResolver Resolver){
        if(writer.TryWriteNil(value))return;
        Write(ref writer,value,Resolver);
    }
    public void Serialize(ref Writer writer,T? value,IJsonFormatterResolver Resolver){
        WriteNullable(ref writer,value,Resolver);
    }
    internal static T Read(ref Reader reader,IJsonFormatterResolver Resolver){
        reader.ReadIsBeginArrayWithVerify();
        var type= reader.ReadType();
        reader.ReadIsValueSeparatorWithVerify();
        var name=reader.ReadString();
        reader.ReadIsValueSeparatorWithVerify();
        var array= Resolver.Serializer().TypeMethods.Get(type);
        var index=reader.ReadInt32();
        reader.ReadIsEndArrayWithVerify();
        return array[index];
    }
    internal static T? ReadNullable(ref Reader reader,IJsonFormatterResolver Resolver){
        if(reader.TryReadNil()) return null;
        return Read(ref reader,Resolver);
    }
    public T Deserialize(ref Reader reader,IJsonFormatterResolver Resolver){
        return ReadNullable(ref reader,Resolver)!;
    }
}
