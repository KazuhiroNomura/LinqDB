using System.Buffers;
using MemoryPack;
namespace LinqDB.Serializers.MemoryPack.Formatters.Sets;

using Reader = MemoryPackReader;
using G = LinqDB.Sets;
public class Set<T>:MemoryPackFormatter<G.Set<T>>{
    public static readonly Set<T> Instance=new();
    private static void WriteNullable<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,G.Set<T>? value) where TBufferWriter:IBufferWriter<byte>{
        if(writer.TryWriteNil(value)) return;
        var type=value!.GetType();
        writer.WriteType(type);
        if(typeof(G.Set<T>)!=type){
            writer.Write(value);
            return;
        }
        var Count=value.LongCount;
        var Formatter=writer.GetFormatter<T>();
        writer.WriteVarInt(Count);
        foreach(var item in value)
            writer.Write(Formatter,item);
    }
    public override void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,scoped ref G.Set<T>? value)=>WriteNullable(ref writer,value);
    private static G.Set<T>? ReadNullable(ref Reader reader){
        if(reader.TryReadNil())return null;
        var type=reader.ReadType();
        if(typeof(G.Set<T>)!=type)return(G.Set<T>?)reader.Read(type);
        var Formatter=reader.GetFormatter<T>();
        var value=new G.Set<T>();
        var Count=reader.ReadVarIntInt64();
        for(long a=0;a<Count;a++) value.Add(reader.Read(Formatter));
        return value;

    }
    public override void Deserialize(ref Reader reader,scoped ref G.Set<T>? value)=>value=ReadNullable(ref reader);
}
