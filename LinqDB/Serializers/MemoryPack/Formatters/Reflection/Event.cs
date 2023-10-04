using System.Reflection;
using MemoryPack;
using System.Buffers;

namespace LinqDB.Serializers.MemoryPack.Formatters.Reflection;

using Reader = MemoryPackReader;
using G = System.Reflection.EventInfo;
public class Event:MemoryPackFormatter<G>{
    public static readonly Event Instance=new();
    internal static void Write<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,G value)
        where TBufferWriter:IBufferWriter<byte>{

        var type=value.ReflectedType!;
        writer.WriteType(type);



        var array=writer.Serializer().TypeEvents.Get(type);
        var index=System.Array.IndexOf(array,value);
        writer.WriteVarInt(index);

    }
    public override void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,scoped ref G? value){
        if(writer.TryWriteNil(value)) return;
        Write(ref writer,value);
    }
    internal static G Read(ref Reader reader){

        var type=reader.ReadType();



        var array=reader.Serializer().TypeEvents.Get(type);
        var index=reader.ReadVarIntInt32();

        return array[index];
    }
    public override void Deserialize(ref Reader reader,scoped ref G? value)=>
        value=reader.TryReadNil()?null:Read(ref reader);
}