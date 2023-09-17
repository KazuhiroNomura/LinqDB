using System;
using System.Reflection;
using Utf8Json;
namespace LinqDB.Serializers.Utf8Json.Formatters;
using Writer = JsonWriter;
using Reader = JsonReader;
using static Extension;
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
        writer.WriteInt32(Array.IndexOf(Resolver.Serializer().TypeMethods.Get(type),value));
        writer.WriteEndArray();
    }
    internal static void WriteNullable(ref Writer writer,T? value,IJsonFormatterResolver Resolver){
        if(writer.WriteIsNull(value))return;
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
        var index=reader.ReadInt32();
        reader.ReadIsEndArrayWithVerify();
        return Resolver.Serializer().TypeMethods.Get(type)[index];
    }
    internal static T? ReadNullable(ref Reader reader,IJsonFormatterResolver Resolver){
        if(reader.ReadIsNull()) return null;
        return Read(ref reader,Resolver);
    }
    public T Deserialize(ref Reader reader,IJsonFormatterResolver Resolver){
        return ReadNullable(ref reader,Resolver)!;
    }
}
