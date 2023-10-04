using MemoryPack;
namespace LinqDB.Serializers.MemoryPack.Formatters.Enumerables;


using Reader = MemoryPackReader;
using G = System.Collections;
public class IEnumerable : MemoryPackFormatter<G.IEnumerable>
{
    public static readonly IEnumerable Instance = new();
    private IEnumerable() { }
    public override void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer, scoped ref G.IEnumerable? value)
    {
        if (writer.TryWriteNil(value)) return;
        var type = value!.GetType();
        writer.WriteType(type);
        writer.WriteValue(type, value);
    }
    public override void Deserialize(ref Reader reader, scoped ref G.IEnumerable? value)
    {
        if (reader.TryReadNil()) return;


        var type = reader.ReadType();
        value=(G.IEnumerable?)reader.ReadValue(type);
    }
}
