using System;
using MemoryPack;
using System.Buffers;
using Expressions = System.Linq.Expressions;
namespace LinqDB.Serializers.MemoryPack.Formatters;


using Reader = MemoryPackReader;
using T = Expressions.SymbolDocumentInfo;
public class SymbolDocumentInfo:MemoryPackFormatter<T>{
    public static readonly SymbolDocumentInfo Instance=new();
    internal static void Write<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,T value)where TBufferWriter:IBufferWriter<byte>{
        var Formatter=writer.GetFormatter<Guid>();
        
        writer.WriteString(value.FileName);
        
        writer.Write(Formatter,value.Language);

        writer.Write(Formatter,value.LanguageVendor);

        writer.Write(Formatter,value.DocumentType);// ReSharper disable once InconsistentNaming
    }
    public override void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,scoped ref T? value){
        if(writer.TryWriteNil(value)) return;
        Write(ref writer,value);
    }
    internal static T Read(ref Reader reader) {
        var Formatter=reader.GetFormatter<Guid>();
        
        var fileName=reader.ReadString();
        
        var language=reader.Read(Formatter);
        
        var languageVendor=reader.Read(Formatter);
        
        var documentType=reader.Read(Formatter);
        
        return Expressions.Expression.SymbolDocument(fileName,language,languageVendor,documentType);
    }
    public override void Deserialize(ref Reader reader,scoped ref T? value){
        if(reader.TryReadNil()) return;
        value=Read(ref reader);
    }
}