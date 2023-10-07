using MemoryPack;
using MemoryPack.Formatters;
namespace LinqDB.Serializers.MemoryPack.Formatters.Enumerables;


using Reader = MemoryPackReader;
using G = System.Collections.Generic;
public class IEnumerable<T>: MemoryPackFormatter<G.IEnumerable<T>>{
    internal static readonly IEnumerable<T> Instance = new();
    private static readonly InterfaceEnumerableFormatter<T> Formatter=new();
    private IEnumerable(){}
    public override void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer, scoped ref G.IEnumerable<T>? value)=>writer.Write(Formatter,value);
    public override void Deserialize(ref Reader reader, scoped ref G.IEnumerable<T>? value)=>value=reader.Read(Formatter)!;
}
