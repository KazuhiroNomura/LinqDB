using System.Diagnostics.CodeAnalysis;
using MemoryPack;

using System.Linq;
namespace LinqDB.Serializers.MemoryPack.Formatters.Enumerables;


using Reader = MemoryPackReader;
using G = System.Collections.Generic;
public class IEnumerable<T>: MemoryPackFormatter<G.IEnumerable<T>>{
    internal static readonly IEnumerable<T> Instance = new();
    private IEnumerable(){}
    [SuppressMessage("ReSharper","PossibleMultipleEnumeration")]
    public override void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,scoped ref G.IEnumerable<T>? value){
        if(writer.TryWriteNil(value)) return;
        writer.WriteVarInt(value.Count());
        var Formatter=writer.GetFormatter<T>();
        foreach(var item in value)
            writer.Write(Formatter,item);

    }
    
    
    
    
    
    
    
    public override void Deserialize(ref Reader reader,scoped ref G.IEnumerable<T>? value){
        if(reader.TryReadNil()) return;
        var Count=reader.ReadVarIntInt64();
        var Formatter=reader.GetFormatter<T>();
        var value0=new G.List<T>();
        while(Count-->0)
            value0.Add(reader.Read(Formatter));
        value=value0;
    }
}
