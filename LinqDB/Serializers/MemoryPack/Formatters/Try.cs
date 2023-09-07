using MemoryPack;
using Expressions = System.Linq.Expressions;
using System.Buffers;
using MemoryPack.Formatters;
namespace LinqDB.Serializers.MemoryPack.Formatters;
using Reader=MemoryPackReader;
using T=Expressions.TryExpression;

public class Try:MemoryPackFormatter<T> {
    public static readonly Try Instance=new();
    private static readonly ReadOnlyCollectionFormatter<Expressions.CatchBlock?>CatchBlocks=new();
    internal void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,T? value)where TBufferWriter:IBufferWriter<byte> =>this.Serialize(ref writer,ref value);
    internal T DeserializeTry(ref MemoryPackReader reader){
        T? value=default;
        this.Deserialize(ref reader,ref value);
        return value!;
    }
    public override void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,scoped ref T? value){
        Expression.Instance.Serialize(ref writer,value!.Body);
        Expression.Instance.Serialize(ref writer,value.Finally);
        var Handlers=value.Handlers;
        CatchBlocks.Serialize(ref writer,ref Handlers!);
        //this.Serialize(ref writer,value.Handlers);
        //writer.Write("ABC");
    }
    public override void Deserialize(ref MemoryPackReader reader,scoped ref T? value){
        var body= Expression.Instance.Deserialize(ref reader);
        //var s=reader.ReadString();
        var @finally= Expression.Instance.Deserialize(ref reader);
        var handlers=reader.ReadArray<Expressions.CatchBlock>();
        if(handlers!.Length==0)
            value=Expressions.Expression.TryFinally(body,@finally);
        else
            value=Expressions.Expression.TryCatchFinally(body,@finally,handlers!);
    }
}
