using System.Xml.Linq;




using MemoryPack;
using System.Buffers;

namespace LinqDB.Serializers.MemoryPack.Formatters.Enumerables;
using G = System.Collections.Generic;


using Reader = MemoryPackReader;
// ReSharper disable once InconsistentNaming
public class IEnumerable<T> : MemoryPackFormatter<G.IEnumerable<T>>
{
    public static readonly IEnumerable<T> Instance = new();
    private IEnumerable() { }
    public override void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer, scoped ref G.IEnumerable<T>? value)
    {
        if (writer.TryWriteNil(value)) return;
        var type = value!.GetType();
        writer.WriteType(type);
        writer.WriteValue(type, value);
    }
    public override void Deserialize(ref Reader reader, scoped ref G.IEnumerable<T>? value)
    {
        if (reader.TryReadNil()) return;


        var type = reader.ReadType();
        value=(G.IEnumerable<T>?)reader.ReadValue(type);
    }
}
