using System.Buffers;
using MemoryPack;
using Expressions=System.Linq.Expressions;

namespace LinqDB.Serializers.MemoryPack.Formatters;


public class Default:MemoryPackFormatter<Expressions.DefaultExpression>{
    private readonly 必要なFormatters Formatters;
    public Default(必要なFormatters Formatters)=>this.Formatters=Formatters;
    internal void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,Expressions.DefaultExpression? value) where TBufferWriter:IBufferWriter<byte>{
        this.Serialize(ref writer,ref value);
    }
    internal Expressions.DefaultExpression DeserializeDefault(ref MemoryPackReader reader){
        Expressions.DefaultExpression? value=default;
        this.Deserialize(ref reader,ref value);
        return value!;
    }
    public override void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,scoped ref Expressions.DefaultExpression? value){
        this.Formatters.Type.Serialize(ref writer,value!.Type);
    }
    public override void Deserialize(ref MemoryPackReader reader,scoped ref Expressions.DefaultExpression? value){
        //var type=options.Resolver.GetFormatter<Type>().Deserialize(ref reader,options);
        value=Expressions.Expression.Default(this.Formatters.Type.DeserializeType(ref reader));
    }
}
