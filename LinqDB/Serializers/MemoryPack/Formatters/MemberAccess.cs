using System.Buffers;
using Expressions=System.Linq.Expressions;
using MemoryPack;
namespace LinqDB.Serializers.MemoryPack.Formatters;
using Reader=MemoryPackReader;
using T=Expressions.MemberExpression;


public class MemberAccess:MemoryPackFormatter<T> {
    public static readonly MemberAccess Instance=new();
    internal static void InternalSerialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,T? value)where TBufferWriter:IBufferWriter<byte> =>
        Instance.Serialize(ref writer,ref value);
    public override void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,scoped ref T? value){
        Member.Serialize(ref writer,value!.Member);
        Expression.SerializeNullable(ref writer,value.Expression);
    }
    internal static T InternalDeserialize(ref Reader reader){
        T? value=default;
        Instance.Deserialize(ref reader,ref value);
        return value!;
    }
    public override void Deserialize(ref Reader reader,scoped ref T? value){
        var member=Member.Deserialize(ref reader);
        var expression= Expression.InternalDeserializeNullable(ref reader);
        value=Expressions.Expression.MakeMemberAccess(expression,member);
    }
}
