using Utf8Json;
using Expressions=System.Linq.Expressions;
namespace LinqDB.Serializers.Utf8Json.Formatters;
using Writer=JsonWriter;
using Reader=JsonReader;
using static Extension;
using T=Expressions.DebugInfoExpression;
public class DebugInfo:IJsonFormatter<T> {
    public static readonly DebugInfo Instance=new();
    internal static void InternalSerialize(ref Writer writer,T value,IJsonFormatterResolver Resolver){
        SymbolDocumentInfo.InternalSerialize(ref writer,value.Document,Resolver);
        writer.WriteValueSeparator();
        writer.WriteInt32(value.StartColumn);
        writer.WriteValueSeparator();
        writer.WriteInt32(value.StartColumn);
        writer.WriteValueSeparator();
        writer.WriteInt32(value.EndLine);
        writer.WriteValueSeparator();
        writer.WriteInt32(value.EndColumn);
    }
    public void Serialize(ref Writer writer,T value,IJsonFormatterResolver Resolver){
        writer.WriteBeginArray();
        InternalSerialize(ref writer,value,Resolver);
        writer.WriteEndArray();
    }
    internal static T InternalDeserialize(ref Reader reader,IJsonFormatterResolver Resolver){
        var document=SymbolDocumentInfo.InternalDeserialize(ref reader,Resolver);
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
        reader.ReadIsBeginArrayWithVerify();
        var value=InternalDeserialize(ref reader,Resolver);
        reader.ReadIsEndArrayWithVerify();
        return value;
    }
}
