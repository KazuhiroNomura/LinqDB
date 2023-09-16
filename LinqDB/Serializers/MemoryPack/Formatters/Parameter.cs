using System.Buffers;
using System.Linq.Expressions;
using MemoryPack;
namespace LinqDB.Serializers.MemoryPack.Formatters;
using Reader=MemoryPackReader;
using T=ParameterExpression;
using C=Serializer;

public class Parameter:MemoryPackFormatter<T> {
    public static readonly Parameter Instance=new();
    private static void PrivateSerialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,T? value) where TBufferWriter:IBufferWriter<byte>{
        writer.WriteVarInt(writer.Serializer().ListParameter.LastIndexOf(value));
    }
    internal static void InternalSerialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,T? value) where TBufferWriter:IBufferWriter<byte>{
        writer.WriteNodeType(ExpressionType.Parameter);
        PrivateSerialize(ref writer,value);
    }
    public override void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,scoped ref T? value){
        writer.WriteVarInt(writer.Serializer().ListParameter.LastIndexOf(value));
    }
    internal static T InternalDeserialize(ref Reader reader){
        var index=reader.ReadVarIntInt32();
        var Parameter= reader.Serializer().ListParameter[index];
        return Parameter;
    }
    public override void Deserialize(ref Reader reader,scoped ref T? value){
        Instance.Deserialize(ref reader,ref value);
    }
}
