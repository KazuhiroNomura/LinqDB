using System.Buffers;
using Expressions=System.Linq.Expressions;
using MemoryPack;
namespace LinqDB.Serializers.MemoryPack.Formatters;
using Reader=MemoryPackReader;
using T=Expressions.MemberExpression;


public class MemberAccess:MemoryPackFormatter<T> {
    public static readonly MemberAccess Instance=new();
    internal static void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,T? value)where TBufferWriter:IBufferWriter<byte> =>
        Instance.Serialize(ref writer,ref value);
    internal static T DeserializeMember(ref Reader reader){
        T? value=default;
        Instance.Deserialize(ref reader,ref value);
        return value!;
    }
    public override void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,scoped ref T? value){
        Member.Serialize(ref writer,value!.Member);
        Expression.Serialize(ref writer,value.Expression);
    }
    public override void Deserialize(ref Reader reader,scoped ref T? value){
        var member=Member.Deserialize(ref reader);
        var expression= Expression.Deserialize(ref reader);
        value=Expressions.Expression.MakeMemberAccess(expression,member);
    }
}
