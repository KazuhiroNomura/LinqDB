using System.Buffers;
using System.Linq.Expressions;
using MemoryPack;
namespace LinqDB.Serializers.MemoryPack.Formatters;
using Reader=MemoryPackReader;
using T=ParameterExpression;
using C=Serializer;

public class Parameter:MemoryPackFormatter<T> {
    public static readonly Parameter Instance=new();
    internal static void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,T? value)where TBufferWriter:IBufferWriter<byte> =>
        Instance.Serialize(ref writer,ref value);
    internal static T DeserializeParameter(ref Reader reader){
        T? value=default;
        Instance.Deserialize(ref reader,ref value);
        return value!;
    }
    public override void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,scoped ref T? value){
        writer.WriteVarInt(C.Instance.ListParameter.LastIndexOf(value));
    }
    public override void Deserialize(ref Reader reader,scoped ref T? value){
        var index=reader.ReadVarIntInt32();
        var Parameter= C.Instance.ListParameter[index];
        value=Parameter;
    }
}
