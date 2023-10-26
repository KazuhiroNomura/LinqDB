
using Utf8Json;
using Utf8Json.Formatters;

using Expressions = System.Linq.Expressions;
namespace LinqDB.Serializers.Utf8Json.Formatters;
using O = IJsonFormatterResolver;
using Writer = JsonWriter;
using Reader = JsonReader;
using T = Expressions.SymbolDocumentInfo;
public class SymbolDocumentInfo:IJsonFormatter<T> {
    public static readonly SymbolDocumentInfo Instance = new();
    internal static void Write(ref Writer writer,T value,O Resolver) {
        var Formatter = GuidFormatter.Default;
        writer.WriteBeginArray();
        writer.WriteString(value.FileName);
        writer.WriteValueSeparator();
        writer.Write(Formatter,value.Language,Resolver);
        writer.WriteValueSeparator();
        writer.Write(Formatter,value.LanguageVendor,Resolver);
        writer.WriteValueSeparator();
        writer.Write(Formatter,value.DocumentType,Resolver);
        writer.WriteEndArray();
    }
    public void Serialize(ref Writer writer,T? value,O Resolver) {
        if(writer.TryWriteNil(value)) return;
        Write(ref writer,value,Resolver);
    }
    internal static T Read(ref Reader reader,O Resolver) {
        var Formatter = GuidFormatter.Default;
        reader.ReadIsBeginArrayWithVerify();
        var fileName = reader.ReadString();
        reader.ReadIsValueSeparatorWithVerify();
        var language = reader.Read(Formatter,Resolver);
        reader.ReadIsValueSeparatorWithVerify();
        var languageVendor = reader.Read(Formatter,Resolver);
        reader.ReadIsValueSeparatorWithVerify();
        var documentType=reader.Read(Formatter,Resolver);
        reader.ReadIsEndArrayWithVerify();
        return Expressions.Expression.SymbolDocument(fileName,language,languageVendor,documentType);






    }
    public T Deserialize(ref Reader reader,O Resolver) {
        if(reader.TryReadNil()) return null!;
        return Read(ref reader,Resolver);
    }
}
