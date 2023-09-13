using System.Buffers;
using System.Diagnostics;
using MemoryPack;
using Expressions = System.Linq.Expressions;

namespace LinqDB.Serializers.MemoryPack.Formatters;
using Reader=MemoryPackReader;
using T=Expressions.DefaultExpression;

using static Extension;
public class Default:MemoryPackFormatter<T> {
    public static readonly Default Instance=new();
    internal static void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,T? value) where TBufferWriter:IBufferWriter<byte> =>
        Instance.Serialize(ref writer,ref value);
    internal static T Deserialize(ref Reader reader) {
        T? value=default;
        Instance.Deserialize(ref reader,ref value);
        return value!;
    }
    public override void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,scoped ref T? value){
        Debug.Assert(value!=null,nameof(value)+" != null");
        writer.WriteType(value.Type);
    }
    public override void Deserialize(ref Reader reader,scoped ref T? value){
        //var type=options.Resolver.GetFormatter<Type>().Deserialize(ref reader,options);
        value=Expressions.Expression.Default(reader.ReadType());
    }
}
