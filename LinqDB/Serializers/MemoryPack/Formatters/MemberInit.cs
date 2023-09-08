using System.Buffers;
using System.Diagnostics;
using Expressions = System.Linq.Expressions;
using MemoryPack;
using MemoryPack.Formatters;
namespace LinqDB.Serializers.MemoryPack.Formatters;
using Reader=MemoryPackReader;
using T=Expressions.MemberInitExpression;
using static Common;

public class MemberInit:MemoryPackFormatter<T> {
    public static readonly MemberInit Instance=new();
    internal void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,T? value)where TBufferWriter:IBufferWriter<byte>{
        this.Serialize(ref writer,ref value);
    }
    internal T DeserializeMemberInit(ref Reader reader){
        T? value=default;
        this.Deserialize(ref reader,ref value);
        return value!;
    }
    public override void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,scoped ref T? value){
        Debug.Assert(value!=null,nameof(value)+" != null");
        New.Instance.Serialize(ref writer,value.NewExpression);
        SerializeReadOnlyCollection(ref writer,value.Bindings);
        //this.Serialize(ref writer,value.Bindings);
    }
    public override void Deserialize(ref Reader reader,scoped ref T? value){
        var @new= New.Instance.DeserializeNew(ref reader);
        var bindings=reader.ReadArray<Expressions.MemberBinding>();
        value=Expressions.Expression.MemberInit(@new,bindings!);
    }
}
