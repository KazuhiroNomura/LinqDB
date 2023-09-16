using MemoryPack;

using System.Buffers;
using System.Diagnostics;
using Expressions = System.Linq.Expressions;

namespace LinqDB.Serializers.MemoryPack.Formatters;
using Reader = MemoryPackReader;
using T = Expressions.DebugInfoExpression;

public class DebugInfo:MemoryPackFormatter<T>{
    public static readonly DebugInfo Instance=new();
    private static void PrivateSerialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,T value)where TBufferWriter:IBufferWriter<byte>{
        SymbolDocumentInfo.InternalSerialize(ref writer,value.Document);
        writer.WriteVarInt(value.StartLine);
        writer.WriteVarInt(value.StartColumn);
        writer.WriteVarInt(value.EndLine);
        writer.WriteVarInt(value.EndColumn);
    }
    internal static void InternalSerialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,T value)where TBufferWriter:IBufferWriter<byte>{
        writer.WriteNodeType(Expressions.ExpressionType.DebugInfo);
        PrivateSerialize(ref writer,value);
    }
    public override void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,scoped ref T? value){
        Debug.Assert(value!=null,nameof(value)+" != null");
        PrivateSerialize(ref writer,value);
    }
    internal static T InternalDeserialize(ref Reader reader){
        T? value=default;
        Instance.Deserialize(ref reader,ref value);
        return value!;
    }
    public override void Deserialize(ref Reader reader,scoped ref T? value){
        var document=SymbolDocumentInfo.InternalDeserialize(ref reader);
        var startLine=reader.ReadVarIntInt32();
        var startColumn=reader.ReadVarIntInt32();
        var endLine=reader.ReadVarIntInt32();
        var endColumn=reader.ReadVarIntInt32();
        value=Expressions.Expression.DebugInfo(document,startLine,startColumn,endLine,endColumn);
    }
}