using System.Buffers;
using System.Linq.Expressions;
using MemoryPack;
namespace LinqDB.Serializers.MemoryPack.Formatters;


public class MemberAccess:MemoryPackFormatter<MemberExpression>{
    internal void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,MemberExpression? value)where TBufferWriter:IBufferWriter<byte>{
        this.Serialize(ref writer,ref value);
    }
    internal MemberExpression DeserializeMember(ref MemoryPackReader reader){
        MemberExpression? value=default;
        this.Deserialize(ref reader,ref value);
        return value!;
    }
    public override void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,scoped ref MemberExpression? value){
        MemoryPackCustomSerializer.Member.Serialize(ref writer,value!.Member);
        MemoryPackCustomSerializer.Expression.Serialize(ref writer,value.Expression);
    }
    public override void Deserialize(ref MemoryPackReader reader,scoped ref MemberExpression? value){
        var member=MemoryPackCustomSerializer.Member.DeserializeMemberInfo(ref reader);
        var expression= MemoryPackCustomSerializer.Expression.Deserialize(ref reader);
        value=System.Linq.Expressions.Expression.MakeMemberAccess(expression,member);
    }
}
