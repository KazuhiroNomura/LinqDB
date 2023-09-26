
using Utf8Json;

using Expressions = System.Linq.Expressions;
namespace LinqDB.Serializers.Utf8Json.Formatters;
using O=IJsonFormatterResolver;
using Writer = JsonWriter;
using Reader = JsonReader;
using T = Expressions.DebugInfoExpression;
public class DebugInfo:IJsonFormatter<T> {
    public static readonly DebugInfo Instance=new();
    private static void PrivateWrite(ref Writer writer,T value,O Resolver){
        SymbolDocumentInfo.Write(ref writer,value.Document,Resolver);
        //Resolver.GetFormatter<Expressions.SymbolDocumentInfo>().Serialize(ref writer,value.Document,Resolver);
        writer.WriteValueSeparator();
        writer.WriteInt32(value.StartLine);
        writer.WriteValueSeparator();
        writer.WriteInt32(value.StartColumn);
        writer.WriteValueSeparator();
        writer.WriteInt32(value.EndLine);
        writer.WriteValueSeparator();
        writer.WriteInt32(value.EndColumn);
    }
    internal static void Write(ref Writer writer,T value,O Resolver){
        writer.WriteBeginArray();
        writer.WriteNodeType(Expressions.ExpressionType.DebugInfo);
        writer.WriteValueSeparator();
        PrivateWrite(ref writer,value,Resolver);
        writer.WriteEndArray(); 
    }
    public void Serialize(ref Writer writer,T? value,O Resolver){
        if(writer.TryWriteNil(value))return;
        writer.WriteBeginArray();
        PrivateWrite(ref writer,value,Resolver);
        writer.WriteEndArray();
    }
    internal static T Read(ref Reader reader,O Resolver){
        var document=SymbolDocumentInfo.Read(ref reader,Resolver);
        reader.ReadIsValueSeparatorWithVerify();
        var startLine=reader.ReadInt32();
        reader.ReadIsValueSeparatorWithVerify();
        var startColumn=reader.ReadInt32();
        reader.ReadIsValueSeparatorWithVerify();
        var endLine=reader.ReadInt32();
        reader.ReadIsValueSeparatorWithVerify();
        var endColumn=reader.ReadInt32();
        return Expressions.Expression.DebugInfo(document,startLine,startColumn,endLine,endColumn);
    }
    public T Deserialize(ref Reader reader,O Resolver){
        if(reader.TryReadNil())return null!;
        reader.ReadIsBeginArrayWithVerify();
        var value=Read(ref reader,Resolver);
        reader.ReadIsEndArrayWithVerify();
        return value;
    }
}
