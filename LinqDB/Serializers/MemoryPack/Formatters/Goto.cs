using MemoryPack;

using System.Buffers;
using Microsoft.SqlServer.TransactSql.ScriptDom;
using Expressions=System.Linq.Expressions;

namespace LinqDB.Serializers.MemoryPack.Formatters;
using Reader=MemoryPackReader;
using T=Expressions.GotoExpression;

public class Goto:MemoryPackFormatter<T>{
    public static readonly Goto Instance=new();
    internal static void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,T? value)where TBufferWriter:IBufferWriter<byte> =>
        Instance.Serialize(ref writer,ref value);
    internal static T DeserializeGoto(ref Reader reader){
        T? value=default;
        Instance.Deserialize(ref reader,ref value);
        return value!;
    }
    public override void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,scoped ref T? value){
        writer.WriteVarInt((byte)value!.Kind);
        LabelTarget.Serialize(ref writer,value.Target);
        Expression.SerializeNullable(ref writer,value.Value);
        writer.WriteType(value.Type);
    }
    public override void Deserialize(ref Reader reader,scoped ref T? value){
        var kind=(Expressions.GotoExpressionKind)reader.ReadVarIntByte();
        var target= LabelTarget.DeserializeLabelTarget(ref reader);
        var value0=Expression.DeserializeNullable(ref reader);
        var type=reader.ReadType();
        value=Expressions.Expression.MakeGoto(kind,target,value0,type);
    }
}