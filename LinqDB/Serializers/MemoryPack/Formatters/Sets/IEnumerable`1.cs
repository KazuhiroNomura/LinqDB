
using MemoryPack;

namespace LinqDB.Serializers.MemoryPack.Formatters.Sets;


using Reader = MemoryPackReader;
using G = LinqDB.Sets;
public class IEnumerable<T>:MemoryPackFormatter<G.IEnumerable<T>>{
    public static readonly IEnumerable<T> Instance=new();
    private IEnumerable(){}
    public override void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,scoped ref G.IEnumerable<T>? value){
        if(writer.TryWriteNil(value)) return;
        var type=value!.GetType();
        writer.WriteType(type);
        writer.Write(type,value);
    }
    
    
    
    
    public override void Deserialize(ref Reader reader,scoped ref G.IEnumerable<T>? value){
        if(reader.TryReadNil()) return;
        value=(G.IEnumerable<T>?)reader.Read(reader.ReadType());
    }
}
