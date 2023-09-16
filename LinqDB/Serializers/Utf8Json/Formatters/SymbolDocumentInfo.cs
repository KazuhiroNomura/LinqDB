using Utf8Json;
using Utf8Json.Formatters;
using Expressions = System.Linq.Expressions;
namespace LinqDB.Serializers.Utf8Json.Formatters;
using Writer = JsonWriter;
using Reader = JsonReader;
using T = Expressions.SymbolDocumentInfo;
public class SymbolDocumentInfo:IJsonFormatter<T> {
    public static readonly SymbolDocumentInfo Instance=new();
    internal static void Write(ref Writer writer,T value,IJsonFormatterResolver Resolver){
        var Formatter= GuidFormatter.Default;
        writer.WriteBeginArray();
        writer.WriteString(value.FileName);
        writer.WriteValueSeparator();
        Formatter.Serialize(ref writer,value.Language,Resolver);
        writer.WriteValueSeparator();
        Formatter.Serialize(ref writer,value.LanguageVendor,Resolver);
        writer.WriteValueSeparator();
        Formatter.Serialize(ref writer,value.DocumentType,Resolver);
        writer.WriteEndArray();
    }
    public void Serialize(ref Writer writer,T value,IJsonFormatterResolver Resolver){
        writer.WriteBeginArray();
        Write(ref writer,value,Resolver);
        writer.WriteEndArray();
    }
    internal static T Read(ref Reader reader,IJsonFormatterResolver Resolver){
        var Formatter= GuidFormatter.Default;
        reader.ReadIsBeginArrayWithVerify();
        var fileName=reader.ReadString();
        reader.ReadIsValueSeparatorWithVerify();
        var language=Formatter.Deserialize(ref reader,Resolver);
        reader.ReadIsValueSeparatorWithVerify();
        var languageVendor=Formatter.Deserialize(ref reader,Resolver);
        reader.ReadIsValueSeparatorWithVerify();
        var documentType=Formatter.Deserialize(ref reader,Resolver);
        reader.ReadIsEndArrayWithVerify();
        return Expressions.Expression.SymbolDocument(fileName,language,languageVendor,documentType);
    }
    public T Deserialize(ref Reader reader,IJsonFormatterResolver Resolver){
        reader.ReadIsBeginArrayWithVerify();
        var value=Read(ref reader,Resolver);
        reader.ReadIsEndArrayWithVerify();
        return value;
    }
}
