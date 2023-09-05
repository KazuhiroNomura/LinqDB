using MemoryPack;
using Expressions = System.Linq.Expressions;
using System.Buffers;

namespace LinqDB.Serializers.MemoryPack.Formatters;


public class Try:MemoryPackFormatter<Expressions.TryExpression>{
    //private static readonly ArrayFormatter<CatchBlock>CatchBlocks=new();
    internal void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,Expressions.TryExpression? value)where TBufferWriter:IBufferWriter<byte> =>this.Serialize(ref writer,ref value);
    internal Expressions.TryExpression DeserializeTry(ref MemoryPackReader reader){
        Expressions.TryExpression? value=default;
        this.Deserialize(ref reader,ref value);
        return value!;
    }
    public override void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,scoped ref Expressions.TryExpression? value){
        MemoryPackCustomSerializer.Expression.Serialize(ref writer,value!.Body);
        MemoryPackCustomSerializer.Expression.Serialize(ref writer,value.Finally);
        var Handlers=value.Handlers;
        MemoryPackCustomSerializer.CatchBlocks.Serialize(ref writer,ref Handlers!);
        //this.Serialize(ref writer,value.Handlers);
        //writer.Write("ABC");
    }
    public override void Deserialize(ref MemoryPackReader reader,scoped ref Expressions.TryExpression? value){
        var body= MemoryPackCustomSerializer.Expression.Deserialize(ref reader);
        //var s=reader.ReadString();
        var @finally= MemoryPackCustomSerializer.Expression.Deserialize(ref reader);
        var handlers=reader.ReadArray<Expressions.CatchBlock>();
        if(handlers!.Length==0)
            value=Expressions.Expression.TryFinally(body,@finally);
        else
            value=Expressions.Expression.TryCatchFinally(body,@finally,handlers!);
    }
}
