
using MemoryPack;

namespace LinqDB.Serializers.MemoryPack.Formatters.Sets;


using Reader = MemoryPackReader;
using G = LinqDB.Sets;
public class IEnumerable<T>:MemoryPackFormatter<G.IEnumerable<T>>{
    public static readonly IEnumerable<T> Instance=new();
    private IEnumerable(){}
    public override void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,scoped ref G.IEnumerable<T>? value){
        if(writer.TryWriteNil(value)) return;
        var Count=value!.Count;
        var Formatter=writer.GetFormatter<T>();
        writer.WriteVarInt(Count);
        foreach(var item in value)
            writer.Write(Formatter,item);
    }
    
    
    
    
    public override void Deserialize(ref Reader reader,scoped ref G.IEnumerable<T>? value){
        if(reader.TryReadNil()) return;
        var Count=reader.ReadVarIntInt64();
        var value0=new G.Set<T>();//todo デシリアライズ専用Sets.IEnumerable<T>実装クラスを作りたい
        var Formatter=reader.GetFormatter<T>();
        while(Count-->0)
            value0.Add(reader.Read(Formatter));
        value=value0;
    }
}
