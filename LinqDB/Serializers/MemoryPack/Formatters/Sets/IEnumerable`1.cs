
using MemoryPack;

namespace LinqDB.Serializers.MemoryPack.Formatters.Sets;


using Reader = MemoryPackReader;
using G = LinqDB.Sets;
public class IEnumerable<T>:MemoryPackFormatter<G.IEnumerable<T>>{
    public static readonly IEnumerable<T> Instance=new();
    private IEnumerable(){}
    public override void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,scoped ref G.IEnumerable<T>? value){
        //if(writer.TryWriteNil(value)) return;
        //var type=value!.GetType();
        //writer.WriteType(type);

        //writer.Write(value);
        if(writer.TryWriteNil(value)) return;
        //var type=value!.GetType();
        //writer.WriteType(type);
        //if(typeof(G.Set<T>)!=type){
        //    writer.Write(value);
        //    return;
        //}
        var Count=value!.LongCount;
        var Formatter=writer.GetFormatter<T>();
        writer.WriteVarInt(Count);
        foreach(var item in value)
            writer.Write(Formatter,item);

    }
    public override void Deserialize(ref Reader reader,scoped ref G.IEnumerable<T>? value){
        if(reader.TryReadNil()) return;


        //var type=reader.ReadType();

        //value=(G.IEnumerable<T>?)reader.ReadValue(type);
        //var type=reader.ReadType();
        var Formatter=reader.GetFormatter<T>();
        var value0=new G.Set<T>();
        var Count=reader.ReadVarIntInt64();
        for(long a=0;a<Count;a++) value0.Add(reader.Read(Formatter));
        value=value0;
    }
}
