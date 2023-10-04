using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection.PortableExecutable;

using MessagePack;
using MessagePack.Formatters;
namespace LinqDB.Serializers.MessagePack.Formatters.Enumerables;
using O = MessagePackSerializerOptions;
using Writer = MessagePackWriter;
using Reader = MessagePackReader;

public class IEnumerable<T> : IMessagePackFormatter<IEnumerable<T>>
{
    public static readonly IEnumerable<T> Instance = new();

    public void Serialize(ref Writer writer, IEnumerable<T>? value, O Resolver)
    {
        if (writer.TryWriteNil(value)) return;
        writer.WriteArrayHeader(2);
        var type = value!.GetType();
        writer.WriteType(type);
        writer.WriteValue(type, value, Resolver);
    }


    public IEnumerable<T> Deserialize(ref Reader reader, O Resolver)
    {
        if (reader.TryReadNil()) return null!;
        var Count = reader.ReadArrayHeader();
        Debug.Assert(Count==2);
        var type = reader.ReadType();
        var o = type.GetValue("InstanceMemoryPack");
        var value = reader.ReadValue(type, Resolver);
        return (IEnumerable<T>)value;
    }
}
