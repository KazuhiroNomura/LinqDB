using System;
using System.Reflection;
using Utf8Json;

namespace LinqDB.Serializers.Utf8Json.Formatters.Reflection;
using O=IJsonFormatterResolver;
using Writer = JsonWriter;
using Reader = JsonReader;
using T = ConstructorInfo;
public class Constructor :IJsonFormatter<T>{
    public static readonly Constructor Instance = new();
    internal static void Write(ref Writer writer, T value,O Resolver){
        writer.WriteBeginArray();
        var type = value.ReflectedType!;
        writer.WriteType(type);
        writer.WriteValueSeparator();
        var array = Resolver.Serializer().TypeConstructors.Get(type);
        var index = Array.IndexOf(array, value);
        writer.WriteInt32(index);
        writer.WriteEndArray();
    }
    internal static void WriteNullable(ref Writer writer,T? value,O Resolver){
        if (writer.TryWriteNil(value)) return;
        Write(ref writer, value, Resolver);
    }
    public void Serialize(ref Writer writer, T? value,O Resolver)=>WriteNullable(ref writer,value,Resolver);
    internal static T Read(ref Reader reader,O Resolver){
        reader.ReadIsBeginArrayWithVerify();
        var type = reader.ReadType();
        reader.ReadIsValueSeparatorWithVerify();
        var array = Resolver.Serializer().TypeConstructors.Get(type);
        var index = reader.ReadInt32();
        reader.ReadIsEndArrayWithVerify();
        return array[index];
    }
    internal static T? ReadNullable(ref Reader reader,O Resolver)=>reader.TryReadNil()?null:Read(ref reader,Resolver);
    public T Deserialize(ref Reader reader,O Resolver)=>ReadNullable(ref reader,Resolver)!;
}
