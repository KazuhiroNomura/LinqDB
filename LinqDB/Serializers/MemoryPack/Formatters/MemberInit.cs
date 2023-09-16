using System.Buffers;
using System.Diagnostics;
using Expressions = System.Linq.Expressions;
using MemoryPack;
namespace LinqDB.Serializers.MemoryPack.Formatters;
using Reader=MemoryPackReader;
using T=Expressions.MemberInitExpression;
using static Extension;

public class MemberInit:MemoryPackFormatter<T> {
    public static readonly MemberInit Instance=new();
    private static void PrivateSerialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,T value)where TBufferWriter:IBufferWriter<byte>{
        New.InternalSerializeNew(ref writer,value.NewExpression);
        writer.SerializeReadOnlyCollection(value.Bindings);
    }
    internal static void InternalSerialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,T value)where TBufferWriter:IBufferWriter<byte>{
        writer.WriteNodeType(Expressions.ExpressionType.MemberInit);
        PrivateSerialize(ref writer,value);
    }
    public override void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,scoped ref T? value){
        Debug.Assert(value!=null,nameof(value)+" != null");
        PrivateSerialize(ref writer,value);
    }
    internal static T InternalDeserialize(ref Reader reader){
        var @new= New.InternaDeserialize(ref reader);
        var bindings=reader.ReadArray<Expressions.MemberBinding>();
        return Expressions.Expression.MemberInit(@new,bindings!);
    }
    public override void Deserialize(ref Reader reader,scoped ref T? value){
        value=InternalDeserialize(ref reader);
    }
}
