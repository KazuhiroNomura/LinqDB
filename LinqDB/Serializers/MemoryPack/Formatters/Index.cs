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
    internal static void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,T? value)where TBufferWriter:IBufferWriter<byte> =>
        Instance.Serialize(ref writer,ref value);
    internal static T DeserializeIndex(ref Reader reader){
        T? value=default;
        Instance.Deserialize(ref reader,ref value);
        return value!;
    }
    public override void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,scoped ref T? value){
        //if(value is null){
            //writer.WriteNil();
       //     return;
        //}
        Expression.Serialize(ref writer,value!.Object);
        Property.Serialize(ref writer,value.Indexer);
        writer.SerializeReadOnlyCollection(value.Arguments);
    }
    public override void Deserialize(ref Reader reader,scoped ref T? value){
        //if(reader.TryReadNil()) return;
        var instance= Expression.Deserialize(ref reader);
        var indexer= Property.Deserialize(ref reader);
        var arguments=reader.ReadArray<Expressions.Expression>();
        value=Expressions.Expression.MakeIndex(instance,indexer,arguments!);
    }
}
