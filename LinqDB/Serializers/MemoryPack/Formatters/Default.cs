using System.Buffers;
using System.Diagnostics;
using MemoryPack;
using Expressions = System.Linq.Expressions;

namespace LinqDB.Serializers.MemoryPack.Formatters;
using Reader=MemoryPackReader;
using T=Expressions.DefaultExpression;

using static Common;
public class Default:MemoryPackFormatter<Expressions.DefaultExpression>{
    public static readonly Default Instance=new();
    internal void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,Expressions.DefaultExpression? value) where TBufferWriter:IBufferWriter<byte>{
        this.Serialize(ref writer,ref value);
    }
    internal Expressions.DefaultExpression DeserializeDefault(ref MemoryPackReader reader){
        Expressions.DefaultExpression? value=default;
        this.Deserialize(ref reader,ref value);
        return value!;
    }
    public override void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,scoped ref Expressions.DefaultExpression? value){
        Debug.Assert(value!=null,nameof(value)+" != null");
        writer.WriteType(value.Type);
    }
    public override void Deserialize(ref MemoryPackReader reader,scoped ref Expressions.DefaultExpression? value){
        //var type=options.Resolver.GetFormatter<Type>().Deserialize(ref reader,options);
        value=Expressions.Expression.Default(reader.ReadType());
    }
}
