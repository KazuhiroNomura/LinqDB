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
        
        Formatter_Serialize(ref writer,Formatter,value.Language);

        Formatter_Serialize(ref writer,Formatter,value.LanguageVendor);

        Formatter_Serialize(ref writer,Formatter,value.DocumentType);// ReSharper disable once InconsistentNaming
        static void Formatter_Serialize(ref MemoryPackWriter<TBufferWriter> writer,IMemoryPackFormatter<Guid> Formatter,Guid value)=>Formatter.Serialize(ref writer,ref value);
    }
    public override void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,scoped ref T? value){
        if(writer.TryWriteNil(value)) return;
        
        Write(ref writer,value);
        
    }
    internal static T Read(ref Reader reader) {
        var Formatter=reader.GetFormatter<Guid>();
        
        var fileName=reader.ReadString();
        
        var language=Formatter_Deserialize(ref reader,Formatter);
        
        var languageVendor=Formatter_Deserialize(ref reader,Formatter);
        
        var documentType=Formatter_Deserialize(ref reader,Formatter);
        
        return Expressions.Expression.SymbolDocument(fileName,language,languageVendor,documentType);
        // ReSharper disable once InconsistentNaming
        static Guid Formatter_Deserialize(ref Reader reader,IMemoryPackFormatter<Guid> Formatter){
            Guid value=default!;
            Formatter.Deserialize(ref reader,ref value);
            return value;
        }
    }
    public override void Deserialize(ref Reader reader,scoped ref T? value){
        
        
        value=Read(ref reader);
        
        
    }
}