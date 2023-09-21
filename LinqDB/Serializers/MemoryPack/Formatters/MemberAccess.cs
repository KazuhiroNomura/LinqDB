using System.Buffers;
using Expressions = System.Linq.Expressions;
using MemoryPack;
using LinqDB.Serializers.MemoryPack.Formatters.Reflection;

namespace LinqDB.Serializers.MemoryPack.Formatters;
using Reader = MemoryPackReader;
using T = Expressions.MemberExpression;


public class MemberAccess:MemoryPackFormatter<T> {
    public static readonly MemberAccess Instance=new();
    private static void PrivateWrite<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,T? value) where TBufferWriter:IBufferWriter<byte>{
        Member.Write(ref writer,value!.Member);
        Expression.WriteNullable(ref writer,value.Expression);
    }
    internal static void Write<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,T? value) where TBufferWriter:IBufferWriter<byte>{
        writer.WriteNodeType(Expressions.ExpressionType.MemberAccess);
        PrivateWrite(ref writer,value);
    }
    public override void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,scoped ref T? value){
        if(writer.TryWriteNil(value)) return;
        PrivateWrite(ref writer,value);
    }
    internal static T Read(ref Reader reader){
        var member=Member.Read(ref reader);
        var expression= Expression.ReadNullable(ref reader);
        return Expressions.Expression.MakeMemberAccess(expression,member);
    }
    public override void Deserialize(ref Reader reader,scoped ref T? value){
        if(reader.TryReadNil()) return;
        value=Read(ref reader);
    }
}
