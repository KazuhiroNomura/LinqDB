using MemoryPack;
using System.Buffers;
using Expressions = System.Linq.Expressions;
namespace LinqDB.Serializers.MemoryPack.Formatters;

using Reader = MemoryPackReader;
using T = Expressions.DebugInfoExpression;
public class DebugInfo:MemoryPackFormatter<T>{
    public static readonly DebugInfo Instance=new();
    
    
    private static void PrivateWrite<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,T value)where TBufferWriter:IBufferWriter<byte>{
        SymbolDocumentInfo.Write(ref writer,value.Document);
        
        writer.WriteVarInt(value.StartLine);
        
        writer.WriteVarInt(value.StartColumn);
        
        writer.WriteVarInt(value.EndLine);
        
        writer.WriteVarInt(value.EndColumn);
    }
    internal static void Write<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,T value)where TBufferWriter:IBufferWriter<byte>{

        writer.WriteNodeType(Expressions.ExpressionType.DebugInfo);
        PrivateWrite(ref writer,value);
    }
    public override void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,scoped ref T? value){
        if(writer.TryWriteNil(value)) return;

        PrivateWrite(ref writer,value);
        
    }
    internal static T Read(ref Reader reader){
        var document=SymbolDocumentInfo.Read(ref reader);
        
        var startLine=reader.ReadVarIntInt32();
        
        var startColumn=reader.ReadVarIntInt32();
        
        var endLine=reader.ReadVarIntInt32();
        
        var endColumn=reader.ReadVarIntInt32();
        return Expressions.Expression.DebugInfo(document,startLine,startColumn,endLine,endColumn);
    }
    public override void Deserialize(ref Reader reader,scoped ref T? value){
        if(reader.TryReadNil()) return;
        value=Read(ref reader);
        
        
        
    }
}