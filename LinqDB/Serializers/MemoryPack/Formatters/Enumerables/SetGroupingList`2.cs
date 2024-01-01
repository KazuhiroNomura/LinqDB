using LinqDB.Enumerables;
using MemoryPack;

namespace LinqDB.Serializers.MemoryPack.Formatters.Enumerables;


using Reader = MemoryPackReader;
using G = LinqDB.Sets;
public class SetGroupingList<TKey,TElement>:MemoryPackFormatter<Lookup<TKey,TElement>>{
    public static readonly SetGroupingList<TKey,TElement>Instance=new();
    public override void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,scoped ref Lookup<TKey,TElement>? value){
        if(writer.TryWriteNil(value)) return;
        writer.WriteVarInt(value!.LongCount);
        var Formatter=GroupingList<TKey,TElement>.Instance;
        foreach(var item in value)
            writer.Write(Formatter,item);
    }
    
    
    
    
    
    
    
    public override void Deserialize(ref Reader reader,scoped ref Lookup<TKey,TElement>? value){
        if(reader.TryReadNil())return;
        var Count = reader.ReadVarIntInt64();
        var Formatter=GroupingList<TKey,TElement>.Instance;
        var value0=new Lookup<TKey,TElement>();
        while(Count-->0)
            value0.Add(reader.Read(Formatter));
            
            
            
            
            
            
        value=value0;
    }
}
