using MemoryPack;

using System.Buffers;
using Microsoft.SqlServer.TransactSql.ScriptDom;
using Expressions=System.Linq.Expressions;

namespace LinqDB.Serializers.MemoryPack.Formatters;
using Reader=MemoryPackReader;
using T=Expressions.GotoExpression;

public class Goto:MemoryPackFormatter<T>{
    public static readonly Goto Instance=new();
    internal void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,T? value)where TBufferWriter:IBufferWriter<byte> =>this.Serialize(ref writer,ref value);
    internal T DeserializeGoto(ref Reader reader){
        T? value=default;
        this.Deserialize(ref reader,ref value);
        return value!;
    }
    public override void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,scoped ref T? value){
        writer.WriteVarInt((byte)value!.Kind);
        LabelTarget.Instance.Serialize(ref writer,value.Target);
        if(!writer.TryWriteNil(value.Value))Expression.Instance.Serialize(ref writer,value.Value);
        writer.WriteType(value.Type);
    }
    public override void Deserialize(ref Reader reader,scoped ref T? value){
        var kind=(Expressions.GotoExpressionKind)reader.ReadVarIntByte();
        var target= LabelTarget.Instance.DeserializeLabelTarget(ref reader);
        var value0=reader.TryReadNil()?null:Expression.Instance.Deserialize(ref reader);
        var type=reader.ReadType();
        value=Expressions.Expression.MakeGoto(kind,target,value0,type);
    }
}