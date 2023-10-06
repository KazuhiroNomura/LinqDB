using MemoryPack;
using System.Buffers;
namespace LinqDB.Serializers.MemoryPack.Formatters.Others;

using Reader = MemoryPackReader;
using Reflection;
public class Delegate<T>:MemoryPackFormatter<T>where T:System.Delegate{
    public static readonly Delegate<T> Instance=new();

    internal static void Write<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,T? value) where TBufferWriter:IBufferWriter<byte>{

        writer.WriteType(value!.GetType());

        Method.Write(ref writer,value.Method);

        Object.WriteNullable(ref writer,value.Target);

    }
    public override void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,scoped ref T? value){
        if(writer.TryWriteNil(value)) return;
        Write(ref writer,value);
    }
    internal static T Read(ref Reader reader){

        var delegateType=reader.ReadType();

        var method=Method.Read(ref reader);

        var target=Object.ReadNullable(ref reader);

        return (T)method.CreateDelegate(delegateType,target);
    }
    public override void Deserialize(ref Reader reader,scoped ref T? value){
        if(reader.TryReadNil()) return;
        value=Read(ref reader);
    }
}
