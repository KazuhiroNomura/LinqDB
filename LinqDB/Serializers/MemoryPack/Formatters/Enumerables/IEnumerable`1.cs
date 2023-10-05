using MemoryPack;
namespace LinqDB.Serializers.MemoryPack.Formatters.Enumerables;


using Reader = MemoryPackReader;
using G = System.Collections.Generic;
public class IEnumerable<T> : MemoryPackFormatter<G.IEnumerable<T>>
{
    public static readonly IEnumerable<T> Instance = new();
    private IEnumerable() { }
    public override void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer, scoped ref G.IEnumerable<T>? value)
    {
        if (writer.TryWriteNil(value)) return;
        var type = value!.GetType();
        writer.WriteType(type);
        //writer.RegisterFormatter(type);
        Extension.Write(ref writer,value);
        //writer.Write(type, value);
    }
    public override void Deserialize(ref Reader reader, scoped ref G.IEnumerable<T>? value)
    {
        if (reader.TryReadNil()) return;


        var type = reader.ReadType();
        //reader.RegisterFormatter(type);
        value=(G.IEnumerable<T>?)Extension.Read(ref reader,type);
    }
}
