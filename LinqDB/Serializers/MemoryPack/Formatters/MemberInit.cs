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
    internal static void InternalSerialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,T? value)where TBufferWriter:IBufferWriter<byte>{
        Instance.Serialize(ref writer,ref value);
    }
    public override void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,scoped ref T? value){
        Debug.Assert(value!=null,nameof(value)+" != null");
        New.InternalSerialize(ref writer,value.NewExpression);
        writer.SerializeReadOnlyCollection(value.Bindings);
    }
    internal static T InternalDeserialize(ref Reader reader){
        T? value=default;
        Instance.Deserialize(ref reader,ref value);
        return value!;
    }
    public override void Deserialize(ref Reader reader,scoped ref T? value){
        var @new= New.InternaDeserialize(ref reader);
        var bindings=reader.ReadArray<Expressions.MemberBinding>();
        value=Expressions.Expression.MemberInit(@new,bindings!);
    }
}
