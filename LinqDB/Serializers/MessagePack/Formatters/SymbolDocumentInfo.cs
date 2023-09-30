using MessagePack;
using MessagePack.Formatters;
using Expressions = System.Linq.Expressions;
namespace LinqDB.Serializers.MessagePack.Formatters;
using O=MessagePackSerializerOptions;
using Writer = MessagePackWriter;
using Reader = MessagePackReader;
using T = Expressions.SymbolDocumentInfo;
public class SymbolDocumentInfo:IMessagePackFormatter<T> {
    public static readonly SymbolDocumentInfo Instance=new();
    internal static void Write(ref Writer writer,T value,O Resolver){
        var Formatter=GuidFormatter.Instance;
        writer.WriteArrayHeader(4);
        writer.Write(value.FileName);

        Formatter.Serialize(ref writer,value.Language,Resolver);

        Formatter.Serialize(ref writer,value.LanguageVendor,Resolver);

        Formatter.Serialize(ref writer,value.DocumentType,Resolver);

    }
    public void Serialize(ref Writer writer,T? value,O Resolver){
        if(writer.TryWriteNil(value)) return;
        Write(ref writer,value,Resolver);
    }
    internal static T Read(ref Reader reader,O Resolver){
        var Formatter=GuidFormatter.Instance;
        var count=reader.ReadArrayHeader();
        var fileName=reader.ReadString();
        
        var language=Formatter.Deserialize(ref reader,Resolver);
        
        var languageVendor=Formatter.Deserialize(ref reader,Resolver);
        
        var documentType=Formatter.Deserialize(ref reader,Resolver);
        
        return Expressions.Expression.SymbolDocument(fileName,language,languageVendor,documentType);
        
        
        
        
        
        
    }
    public T Deserialize(ref Reader reader,O Resolver){
        if(reader.TryReadNil()) return null!;
        return Read(ref reader,Resolver);
    }
}