using System.Buffers;
using Expressions = System.Linq.Expressions;
using MemoryPack;

namespace LinqDB.Serializers.MemoryPack.Formatters;
using Reader = MemoryPackReader;
using T = Expressions.ListInitExpression;



public class ListInit:MemoryPackFormatter<T> {
    public static readonly ListInit Instance=new();
    private static void PrivateSerialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,T? value) where TBufferWriter:IBufferWriter<byte>{
        New.InternalSerializeNew(ref writer,value!.NewExpression);
        writer.SerializeReadOnlyCollection(value.Initializers);
    }
    internal static void InternalSerialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,T? value) where TBufferWriter:IBufferWriter<byte>{
        writer.WriteNodeType(Expressions.ExpressionType.ListInit);
        PrivateSerialize(ref writer,value);
    }
    public override void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,scoped ref T? value){
        PrivateSerialize(ref writer,value);
    }
    internal static T InternalDeserialize(ref Reader reader){
        var @new=New.InternaDeserialize(ref reader);
        var Initializers=reader.ReadArray<Expressions.ElementInit>();
        return Expressions.Expression.ListInit(@new,Initializers!);
    }
    public override void Deserialize(ref Reader reader,scoped ref T? value){
        value=InternalDeserialize(ref reader);
    }
}
