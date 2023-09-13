using MemoryPack;

using System.Buffers;
using System.Diagnostics;
using Expressions=System.Linq.Expressions;
namespace LinqDB.Serializers.MemoryPack.Formatters;
using Reader=MemoryPackReader;
using T=Expressions.ConstantExpression;
public class Constant:MemoryPackFormatter<T> {
    public static readonly Constant Instance=new();
    internal static void InternalSerialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,T? value)where TBufferWriter:IBufferWriter<byte>{
        Instance.Serialize(ref writer,ref value);
    }
    public override void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,scoped ref T? value){
        Debug.Assert(value!=null,nameof(value)+" != null");
        writer.WriteType(value.Type);
        Object.Serialize(ref writer,value.Value);
    }
    internal static T InternalDeserialize(ref Reader reader) {
        T? value=default;
        Instance.Deserialize(ref reader,ref value);
        return value!;
    }
    public override void Deserialize(ref Reader reader,scoped ref T? value){
        var type=reader.ReadType();
        var value0=Object.Deserialize(ref reader);
        value=Expressions.Expression.Constant(value0,type);
    }
}
