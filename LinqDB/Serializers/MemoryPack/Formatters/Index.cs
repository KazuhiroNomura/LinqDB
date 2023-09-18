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
        Expression.Write(ref writer,value!.Object);
        Property.Write(ref writer,value.Indexer);
        writer.WriteCollection(value.Arguments);
    }
    internal static void Write<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,T? value) where TBufferWriter:IBufferWriter<byte>{
        writer.WriteNodeType(ExpressionType.Index);
        PrivateSerialize(ref writer,value);
    }
    public override void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,scoped ref T? value){
        if(writer.TryWriteNil(value)) return;
        PrivateSerialize(ref writer,value);
    }
    internal static T Read(ref Reader reader){
        var instance= Expression.Read(ref reader);
        var indexer= Property.Read(ref reader);
        var arguments=reader.ReadArray<Expressions.Expression>();
        return Expressions.Expression.MakeIndex(instance,indexer,arguments!);
    }
    public override void Deserialize(ref Reader reader,scoped ref T? value){
        if(reader.TryReadNil()) return;
        value=Read(ref reader);
    }
}
