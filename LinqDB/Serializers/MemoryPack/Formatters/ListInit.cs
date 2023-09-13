using System.Buffers;
using Expressions = System.Linq.Expressions;
using MemoryPack;
using MemoryPack.Formatters;

namespace LinqDB.Serializers.MemoryPack.Formatters;
using Reader=MemoryPackReader;
using T=Expressions.ListInitExpression;



public class ListInit:MemoryPackFormatter<T> {
    public static readonly ListInit Instance=new();
    internal static void InternalSerialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,T? value)where TBufferWriter:IBufferWriter<byte> =>
        Instance.Serialize(ref writer,ref value);
    public override void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,scoped ref T? value){
        New.InternalSerialize(ref writer,value!.NewExpression);
        var Initializers=value.Initializers;
        writer.SerializeReadOnlyCollection(value.Initializers);
    }
    internal static T InternalDeserialize(ref Reader reader){
        T? value=default;
        Instance.Deserialize(ref reader,ref value);
        return value!;
    }
    public override void Deserialize(ref Reader reader,scoped ref T? value){
        var @new=New.InternaDeserialize(ref reader);
        var Initializers=reader.ReadArray<Expressions.ElementInit>();
        value=Expressions.Expression.ListInit(@new,Initializers!);
    }
}
