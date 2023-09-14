using System.Diagnostics;
using Expressions=System.Linq.Expressions;
using MessagePack;
using MessagePack.Formatters;
namespace LinqDB.Serializers.MessagePack.Formatters;
using Writer=MessagePackWriter;
using Reader=MessagePackReader;
using T=Expressions.DebugInfoExpression;
using static Extension;
public class DebugInfo:IMessagePackFormatter<T>{
    public static readonly DebugInfo Instance=new();
    private const int ArrayHeader=5;
    private const int InternalArrayHeader=ArrayHeader+1;
    private static void PrivateSerialize(ref Writer writer,T value,MessagePackSerializerOptions Resolver){
        Debug.Assert(value!=null,nameof(value)+" != null");
        SymbolDocumentInfo.InternalSerialize(ref writer,value.Document,Resolver);
        writer.WriteInt32(value.StartLine);
        writer.WriteInt32(value.StartColumn);
        writer.WriteInt32(value.EndLine);
        writer.WriteInt32(value.EndColumn);
    }
    internal static void InternalSerialize(ref Writer writer,T value,MessagePackSerializerOptions Resolver){
        writer.WriteArrayHeader(InternalArrayHeader);
        writer.WriteNodeType(Expressions.ExpressionType.DebugInfo);
        PrivateSerialize(ref writer,value,Resolver);
    }
    public void Serialize(ref Writer writer,T? value,MessagePackSerializerOptions Resolver){
        //if(writer.TryWriteNil(value)) return;
        writer.WriteArrayHeader(ArrayHeader);
        PrivateSerialize(ref writer,value,Resolver);
    }
    internal static T InternalDeserialize(ref Reader reader,MessagePackSerializerOptions Resolver){
        var document=SymbolDocumentInfo.InternalDeserialize(ref reader,Resolver);
        var startLine=reader.ReadInt32();
        var startColumn=reader.ReadInt32();
        var endLine=reader.ReadInt32();
        var endColumn=reader.ReadInt32();
        return Expressions.Expression.DebugInfo(document,startLine,startColumn,endLine,endColumn);
    }
    public T Deserialize(ref Reader reader,MessagePackSerializerOptions Resolver){
        //   if(reader.TryReadNil()) return null!;
        var count=reader.ReadArrayHeader();
        Debug.Assert(count==ArrayHeader);
        return InternalDeserialize(ref reader,Resolver);
    }
}