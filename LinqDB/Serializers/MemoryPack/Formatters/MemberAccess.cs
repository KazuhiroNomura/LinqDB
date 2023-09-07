using System.Buffers;
using Expressions=System.Linq.Expressions;
using MemoryPack;
namespace LinqDB.Serializers.MemoryPack.Formatters;
using Reader=MemoryPackReader;
using T=Expressions.MemberExpression;


public class MemberAccess:MemoryPackFormatter<T> {
    public static readonly MemberAccess Instance=new();
    internal void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,T? value)where TBufferWriter:IBufferWriter<byte>{
        this.Serialize(ref writer,ref value);
    }
    internal T DeserializeMember(ref MemoryPackReader reader){
        T? value=default;
        this.Deserialize(ref reader,ref value);
        return value!;
    }
    public override void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,scoped ref T? value){
        Member.Instance.Serialize(ref writer,value!.Member);
        Expression.Instance.Serialize(ref writer,value.Expression);
    }
    public override void Deserialize(ref MemoryPackReader reader,scoped ref T? value){
        var member=Member.Instance.Deserialize(ref reader);
        var expression= Expression.Instance.Deserialize(ref reader);
        value=Expressions.Expression.MakeMemberAccess(expression,member);
    }
}
