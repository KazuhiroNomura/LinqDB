using MemoryPack;
namespace LinqDB.Serializers.MemoryPack.Formatters.Sets;
using Sets = LinqDB.Sets;


using Reader = MemoryPackReader;
// ReSharper disable once InconsistentNaming
public class IEnumerable:MemoryPackFormatter<Sets.IEnumerable> {
    public static readonly IEnumerable Instance=new();
    private IEnumerable(){}
    public override void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,scoped ref Sets.IEnumerable? value){
        if(writer.TryWriteNil(value)) return;
        var type=value!.GetType();
        writer.WriteType(type);
        writer.Write(value);
    }
    public override void Deserialize(ref Reader reader,scoped ref Sets.IEnumerable? value){
        if(reader.TryReadNil()) return;
        
        
        var type = reader.ReadType();
        value=(Sets.IEnumerable?)reader.ReadValue(type);
    }
}
