using System.Buffers;
using System.Linq.Expressions;
using MemoryPack;
namespace LinqDB.Serializers.MemoryPack.Formatters;
using Reader=MemoryPackReader;
using T=ParameterExpression;
using C=MemoryPackCustomSerializer;

public class Parameter:MemoryPackFormatter<T> {
    public static readonly Parameter Instance=new();
    internal void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,T? value)where TBufferWriter:IBufferWriter<byte> =>this.Serialize(ref writer,ref value);
    internal T DeserializeParameter(ref MemoryPackReader reader){
        T? value=default;
        this.Deserialize(ref reader,ref value);
        return value!;
    }
    public override void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,scoped ref T? value){
        writer.WriteVarInt(C.Instance.ListParameter.LastIndexOf(value));
    }
    public override void Deserialize(ref MemoryPackReader reader,scoped ref T? value){
        var index=reader.ReadVarIntInt32();
        var Parameter= C.Instance.ListParameter[index];
        value=Parameter;
    }
}
