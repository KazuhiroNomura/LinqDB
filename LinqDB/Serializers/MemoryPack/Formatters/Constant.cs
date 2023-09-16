using System.Diagnostics;
using MemoryPack;
using System.Buffers;
using Expressions=System.Linq.Expressions;
namespace LinqDB.Serializers.MemoryPack.Formatters;

using Reader=MemoryPackReader;
using T=Expressions.ConstantExpression;
public class Constant:MemoryPackFormatter<T> {
    public static readonly Constant Instance=new();
    
    
    private static void PrivateSerialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,T? value)where TBufferWriter:IBufferWriter<byte>{
        writer.WriteType(value!.Type);
        Object.InternalSerialize(ref writer,value.Value);
    }
    internal static void InternalSerialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,T? value)where TBufferWriter:IBufferWriter<byte>{
        writer.WriteNodeType(Expressions.ExpressionType.Constant);
        PrivateSerialize(ref writer,value);
    }
    public override void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,scoped ref T? value){
        PrivateSerialize(ref writer,value);
    }
    internal static T InternalDeserialize(ref Reader reader) {
        var type=reader.ReadType();
        var value0=Object.InternalDeserialize(ref reader);
        return Expressions.Expression.Constant(value0,type);
    }
    public override void Deserialize(ref Reader reader,scoped ref T? value){
        value=InternalDeserialize(ref reader);
    }
}
