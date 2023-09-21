﻿using LinqDB.Serializers.MessagePack.Formatters.Reflection;
using MessagePack;
using MessagePack.Formatters;
namespace LinqDB.Serializers.MessagePack.Formatters.Others;
using Writer = MessagePackWriter;
using Reader = MessagePackReader;
using T = System.Delegate;
public class Delegate : IMessagePackFormatter<T>
{
    public static readonly Delegate Instance = new();
    private const int ArrayHeader = 3;
    internal static void Write(ref Writer writer, T? value, MessagePackSerializerOptions Resolver)
    {
        writer.WriteArrayHeader(ArrayHeader);
        writer.WriteType(value!.GetType());

        Method.Instance.Serialize(ref writer, value.Method, Resolver);

        Object.Instance.Serialize(ref writer, value.Target, Resolver);

    }
    public void Serialize(ref Writer writer, T? value, MessagePackSerializerOptions Resolver)
    {
        if (writer.TryWriteNil(value)) return;
        Write(ref writer, value, Resolver);
    }
    internal static T Read(ref Reader reader, MessagePackSerializerOptions Resolver)
    {
        var count = reader.ReadArrayHeader();
        var delegateType = Type.Instance.Deserialize(ref reader, Resolver);

        var method = Method.Read(ref reader, Resolver);

        var target = Object.ReadNullable(ref reader, Resolver);

        return method.CreateDelegate(delegateType, target);
    }
    public T Deserialize(ref Reader reader, MessagePackSerializerOptions Resolver)
    {
        if (reader.TryReadNil()) return null!;
        return Read(ref reader, Resolver);
    }
}