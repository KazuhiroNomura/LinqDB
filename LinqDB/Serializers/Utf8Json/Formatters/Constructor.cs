using System;
using System.Reflection;

using Utf8Json;

namespace LinqDB.Serializers.Utf8Json.Formatters;
using Writer = JsonWriter;
using Reader = JsonReader;
using T = ConstructorInfo;
public class Constructor:IJsonFormatter<T> {
    public static readonly Constructor Instance=new();
    
    internal static void Write(ref Writer writer,T value,IJsonFormatterResolver Resolver){
        writer.WriteBeginArray();
        var type=value.ReflectedType!;
        writer.WriteType(type);
        var array= Resolver.Serializer().TypeConstructors.Get(type!);
        writer.WriteValueSeparator();
        writer.WriteInt32(Array.IndexOf(array,value));
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
        var array= Resolver.Serializer().TypeConstructors.Get(type);
        var index=reader.ReadInt32();
        reader.ReadIsEndArrayWithVerify();
        return array[index];
    }
    public T Deserialize(ref Reader reader,IJsonFormatterResolver Resolver){
        if(reader.TryReadNil())return null!;
        return Read(ref reader,Resolver);
    }
}
