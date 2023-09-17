
using Utf8Json;

using Expressions = System.Linq.Expressions;
namespace LinqDB.Serializers.Utf8Json.Formatters;
using Writer = JsonWriter;
using Reader = JsonReader;
using T = Expressions.DebugInfoExpression;
public class DebugInfo:IJsonFormatter<T> {
    public static readonly DebugInfo Instance=new();
    
    
    private static void PrivateSerialize(ref Writer writer,T value,IJsonFormatterResolver Resolver){
        SymbolDocumentInfo.Write(ref writer,value.Document,Resolver);
        writer.WriteValueSeparator();
        writer.WriteInt32(value.StartLine);
        writer.WriteValueSeparator();
        writer.WriteInt32(value.StartColumn);
        writer.WriteValueSeparator();
        writer.WriteInt32(value.EndLine);
        writer.WriteValueSeparator();
        writer.WriteInt32(value.EndColumn);
    }
    internal static void Write(ref Writer writer,T value,IJsonFormatterResolver Resolver){
        writer.WriteNodeType(value);
        writer.WriteValueSeparator();
        PrivateSerialize(ref writer,value,Resolver);
    }
    public void Serialize(ref Writer writer,T? value,IJsonFormatterResolver Resolver){
        if(writer.WriteIsNull(value))return;
        writer.WriteBeginArray();
        PrivateSerialize(ref writer,value,Resolver);
        writer.WriteEndArray();
    }
    internal static T Read(ref Reader reader,IJsonFormatterResolver Resolver){
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
    public T Deserialize(ref Reader reader,IJsonFormatterResolver Resolver){
        if(reader.ReadIsNull())return null!;
        reader.ReadIsBeginArrayWithVerify();
        var value=Read(ref reader,Resolver);
        reader.ReadIsEndArrayWithVerify();
        return value;
    }
}
