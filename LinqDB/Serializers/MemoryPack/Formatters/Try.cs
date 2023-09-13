using MemoryPack;
using Expressions = System.Linq.Expressions;
using System.Buffers;
using System.Diagnostics.CodeAnalysis;
using MemoryPack.Formatters;
namespace LinqDB.Serializers.MemoryPack.Formatters;
using Reader=MemoryPackReader;
using T=Expressions.TryExpression;

public class Try:MemoryPackFormatter<T> {
    public static readonly Try Instance=new();
    internal static void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,T? value)where TBufferWriter:IBufferWriter<byte> =>
        Instance.Serialize(ref writer,ref value);
    public override void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,scoped ref T? value){
        Expression.Serialize(ref writer,value!.Body);
        Expression.SerializeNullable(ref writer,value.Finally);
        if(value.Finally is not null){
            writer.SerializeReadOnlyCollection(value.Handlers);
        } else{
            Expression.SerializeNullable(ref writer,value.Fault);
            if(value.Fault is null){
                writer.SerializeReadOnlyCollection(value.Handlers);
            }
        }
    }
    internal static T DeserializeTry(ref Reader reader){
        T? value=default;
        Instance.Deserialize(ref reader,ref value);
        return value!;
    }
    [SuppressMessage("ReSharper","ConvertIfStatementToConditionalTernaryExpression")]
    public override void Deserialize(ref Reader reader,scoped ref T? value){
        var body= Expression.Deserialize(ref reader);
        var @finally= Expression.DeserializeNullable(ref reader);
        if(@finally is not null){
            var handlers=reader.ReadArray<Expressions.CatchBlock>()!;
            if(handlers.Length>0) {
                value=Expressions.Expression.TryCatchFinally(body,@finally,handlers!);
            } else {
                value=Expressions.Expression.TryFinally(body,@finally);
            }
        } else{
            var fault= Expression.DeserializeNullable(ref reader);
            if(fault is not null){
                value=Expressions.Expression.TryFault(body,fault);
            } else{
                var handlers=reader.ReadArray<Expressions.CatchBlock>()!;
                value=Expressions.Expression.TryCatch(body,handlers!);
            }
        }
        //value=@finally is not null
        //    ?
        //    handlers.Length>0
        //        ?Expressions.Expression.TryCatchFinally(body,@finally,handlers!)
        //        :Expressions.Expression.TryFinally(body,@finally)
        //    :fault is not null
        //        ?Expressions.Expression.TryFault(body,fault)
        //        :Expressions.Expression.TryCatch(body,handlers!);
    }
}
