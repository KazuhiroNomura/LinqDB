using MemoryPack;
using MemoryPack.Formatters;
namespace LinqDB.Serializers.MemoryPack.Formatters.Enumerables;


using Reader = MemoryPackReader;
using G = System.Collections;
public class IEnumerable : MemoryPackFormatter<G.IEnumerable>{
    public static readonly IEnumerable Instance = new();
    private IEnumerable(){}
    private static readonly InterfaceEnumerableFormatter<object> Formatter=new();
    public override void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer, scoped ref G.IEnumerable? value){
        if (writer.TryWriteNil(value)) return;
        writer.Write(Formatter,value);
    }
    
    
    
    
    
    
    
    public override void Deserialize(ref Reader reader, scoped ref G.IEnumerable? value){
        if (reader.TryReadNil()) return;
        value=reader.Read(Formatter)!;
    }
}
