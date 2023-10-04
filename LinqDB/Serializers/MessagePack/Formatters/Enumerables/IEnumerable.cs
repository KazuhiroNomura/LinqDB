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

public class IEnumerable : IMessagePackFormatter<IEnumerable>
{
    public static readonly IEnumerable Instance = new();

    public void Serialize(ref Writer writer, IEnumerable? value, O Resolver)
    {
        if (writer.TryWriteNil(value)) return;
        writer.WriteArrayHeader(2);
        var type = value!.GetType();
        writer.WriteType(type);
        writer.WriteValue(type, value, Resolver);
    }


    public IEnumerable Deserialize(ref Reader reader, O Resolver)
    {
        if (reader.TryReadNil()) return null!;
        var Count = reader.ReadArrayHeader();
        Debug.Assert(Count==2);
        var type = reader.ReadType();
        var o = type.GetValue("InstanceMemoryPack");
        var value = reader.ReadValue(type, Resolver);
        return (IEnumerable)value;
    }
}
