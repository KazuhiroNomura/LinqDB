﻿
using MemoryPack;
using System.Linq;
namespace LinqDB.Serializers.MemoryPack.Formatters.Enumerables;


using Reader = MemoryPackReader;
using G = System.Collections;
public class IEnumerable : MemoryPackFormatter<G.IEnumerable>{
    public static readonly IEnumerable Instance = new();
    private IEnumerable(){}
    
    public override void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer, scoped ref G.IEnumerable? value){
        if (writer.TryWriteNil(value)) return;
        var List=value.Cast<object>().ToList();
        writer.WriteVarInt(List.Count);
        var Formatter=writer.GetFormatter<object>();
        foreach(var item in List)
            writer.Write(Formatter,item);
    }
    
    
    
    
    
    
    public override void Deserialize(ref Reader reader, scoped ref G.IEnumerable? value){
        if (reader.TryReadNil()) return;
        var Count=reader.ReadVarIntInt32();
        var Formatter=reader.GetFormatter<object>();
        var value0=new G.Generic.List<object>(Count);
        while(Count-->0)
            value0.Add(reader.Read(Formatter));
        value=value0;
    }
}
