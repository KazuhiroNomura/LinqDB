using System;
using System.Reflection;

using Utf8Json;

namespace LinqDB.Serializers.Utf8Json.Formatters.Reflection;
using Writer = JsonWriter;
using Reader = JsonReader;
using G = ConstructorInfo;
public class Constructor : IJsonFormatter<G>
{
    public static readonly Constructor Instance = new();

    internal static void Write(ref Writer writer, G value, IJsonFormatterResolver Resolver)
    {
        writer.WriteBeginArray();
        var type = value.ReflectedType!;
        writer.WriteType(type);
        writer.WriteValueSeparator();
        var array = Resolver.Serializer().TypeConstructors.Get(type);
        var index = Array.IndexOf(array, value);
        writer.WriteInt32(index);
        writer.WriteEndArray();
    }
    public void Serialize(ref Writer writer, G? value, IJsonFormatterResolver Resolver)
    {
        if (writer.TryWriteNil(value)) return;
        Write(ref writer, value, Resolver);
    }
    internal static G Read(ref Reader reader, IJsonFormatterResolver Resolver)
    {
        reader.ReadIsBeginArrayWithVerify();
        var type = reader.ReadType();
        reader.ReadIsValueSeparatorWithVerify();
        var array = Resolver.Serializer().TypeConstructors.Get(type);
        var index = reader.ReadInt32();
        reader.ReadIsEndArrayWithVerify();
        return array[index];
    }
    public G Deserialize(ref Reader reader, IJsonFormatterResolver Resolver)
    {
        if (reader.TryReadNil()) return null!;
        return Read(ref reader, Resolver);
    }
}
