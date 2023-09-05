using System.Buffers;
using Expressions=System.Linq.Expressions;
using MemoryPack;
namespace LinqDB.Serializers.MemoryPack.Formatters;


public class Index:MemoryPackFormatter<Expressions.IndexExpression>{
    internal void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,Expressions.IndexExpression? value)where TBufferWriter:IBufferWriter<byte>{
        this.Serialize(ref writer,ref value);
    }
    internal Expressions.IndexExpression DeserializeIndex(ref MemoryPackReader reader){
        Expressions.IndexExpression? value=default;
        this.Deserialize(ref reader,ref value);
        return value!;
    }
    public override void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,scoped ref Expressions.IndexExpression? value){
        if(value is null){
            //writer.WriteNil();
            return;
        }
        CustomSerializerMemoryPack.Expression.Serialize(ref writer,value.Object);
        CustomSerializerMemoryPack.Property.Serialize(ref writer,value.Indexer);
        CustomSerializerMemoryPack.Serialize(ref writer,value.Arguments);
    }
    public override void Deserialize(ref MemoryPackReader reader,scoped ref Expressions.IndexExpression? value){
        //if(reader.TryReadNil()) return;
        var instance= CustomSerializerMemoryPack.Expression.Deserialize(ref reader);
        var indexer= CustomSerializerMemoryPack.Property.DeserializePropertyInfo(ref reader);
        var arguments=reader.ReadArray<Expressions.Expression>();
        value=Expressions.Expression.MakeIndex(instance,indexer,arguments!);
    }
}
