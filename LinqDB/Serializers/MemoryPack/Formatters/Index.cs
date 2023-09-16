using System.Buffers;
using Expressions = System.Linq.Expressions;
using MemoryPack;
using System.Linq.Expressions;

namespace LinqDB.Serializers.MemoryPack.Formatters;
using Reader=MemoryPackReader;
using T= IndexExpression;
using static Extension;

public class Index:MemoryPackFormatter<T> {
    public static readonly Index Instance=new();
    private static void PrivateSerialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,T? value) where TBufferWriter:IBufferWriter<byte>{
        Expression.InternalSerialize(ref writer,value!.Object);
        Property.Serialize(ref writer,value.Indexer);
        writer.SerializeReadOnlyCollection(value.Arguments);
    }
    internal static void InternalSerialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,T? value) where TBufferWriter:IBufferWriter<byte>{
        writer.WriteNodeType(ExpressionType.Index);
        PrivateSerialize(ref writer,value);
    }
    public override void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,scoped ref T? value){
        PrivateSerialize(ref writer,value);
    }
    internal static T InternalDeserialize(ref Reader reader){
        var instance= Expression.InternalDeserialize(ref reader);
        var indexer= Property.Deserialize(ref reader);
        var arguments=reader.ReadArray<Expressions.Expression>();
        return Expressions.Expression.MakeIndex(instance,indexer,arguments!);
    }
    public override void Deserialize(ref Reader reader,scoped ref T? value){
        value=InternalDeserialize(ref reader);
    }
}
