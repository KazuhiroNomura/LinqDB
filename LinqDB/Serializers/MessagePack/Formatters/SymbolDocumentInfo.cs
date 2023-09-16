using System.Diagnostics;
using Expressions = System.Linq.Expressions;
using MessagePack;
using MessagePack.Formatters;
namespace LinqDB.Serializers.MessagePack.Formatters;
using Writer = MessagePackWriter;
using Reader = MessagePackReader;
using T = Expressions.SymbolDocumentInfo;
public class SymbolDocumentInfo:IMessagePackFormatter<T> {
    public static readonly SymbolDocumentInfo Instance=new();
    private const int ArrayHeader=4;
    internal static void Write(ref Writer writer,T value,MessagePackSerializerOptions Resolver){
        writer.WriteArrayHeader(ArrayHeader);
        var Formatter=GuidFormatter.Instance;
        Debug.Assert(value!=null,nameof(value)+" != null");
        writer.Write(value.FileName);
        Formatter.Serialize(ref writer,value.Language,Resolver);
        Formatter.Serialize(ref writer,value.LanguageVendor,Resolver);
        Formatter.Serialize(ref writer,value.DocumentType,Resolver);
    }
    public void Serialize(ref Writer writer,T? value,MessagePackSerializerOptions Resolver){
        Write(ref writer,value,Resolver);
    }
    internal static T Read(ref Reader reader,MessagePackSerializerOptions Resolver){
        var count=reader.ReadArrayHeader();
        Debug.Assert(count==ArrayHeader);
        var Formatter=GuidFormatter.Instance;
        var fileName=reader.ReadString();
        var language=Formatter.Deserialize(ref reader,Resolver);
        var languageVendor=Formatter.Deserialize(ref reader,Resolver);
        var documentType=Formatter.Deserialize(ref reader,Resolver);
        return Expressions.Expression.SymbolDocument(fileName,language,languageVendor,documentType);
    }
    public T Deserialize(ref Reader reader,MessagePackSerializerOptions Resolver){
        return Read(ref reader,Resolver);
    }
}