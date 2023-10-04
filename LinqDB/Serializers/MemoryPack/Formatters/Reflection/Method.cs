
using MemoryPack;
using System.Buffers;

namespace LinqDB.Serializers.MemoryPack.Formatters.Reflection;

using Reader = MemoryPackReader;
using G = System.Reflection.MethodInfo;
public class Method:MemoryPackFormatter<G>{
    public static readonly Method Instance=new();

    internal static void Write<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,G value)
        where TBufferWriter:IBufferWriter<byte>{

        var type=value.ReflectedType!;
        writer.WriteType(type);



        var array=writer.Serializer().TypeMethods.Get(type);
        var index=System.Array.IndexOf(array,value);
        writer.WriteVarInt(index);

    }
    internal static void WriteNullable<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,G? value)
        where TBufferWriter:IBufferWriter<byte>{
        if(writer.TryWriteNil(value)) return;
        Write(ref writer,value);
    }
    public override void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,scoped ref G? value)=>WriteNullable(ref writer,value);
    internal static G Read(ref Reader reader){


        var type=reader.ReadType();


        var array=reader.Serializer().TypeMethods.Get(type);
        var index=reader.ReadVarIntInt32();

        return array[index];
    }
    internal static G? ReadNullable(ref Reader reader)=>reader.TryReadNil()?null:Read(ref reader);
    public override void Deserialize(ref Reader reader,scoped ref G? value)=>value=Read(ref reader);
}
