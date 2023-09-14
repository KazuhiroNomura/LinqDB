using System;
using MemoryPack;

using System.Buffers;
using System.Diagnostics;
using Microsoft.SqlServer.TransactSql.ScriptDom;
using Expressions=System.Linq.Expressions;

namespace LinqDB.Serializers.MemoryPack.Formatters;
using Reader=MemoryPackReader;
using T=Expressions.SymbolDocumentInfo;

public class SymbolDocumentInfo:MemoryPackFormatter<T>{
    public static readonly SymbolDocumentInfo Instance=new();
    internal static void InternalSerialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,T? value)where TBufferWriter:IBufferWriter<byte> =>
        Instance.Serialize(ref writer,ref value);
    private static void Write<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,IMemoryPackFormatter<Guid> Formatter,Guid value)where TBufferWriter:IBufferWriter<byte>{
        Formatter.Serialize(ref writer,ref value);
    }
    public override void Serialize<TBufferWriter>(ref MemoryPackWriter<TBufferWriter> writer,scoped ref T? value){
        var Formatter=writer.GetFormatter<Guid>();
        Debug.Assert(value!=null,nameof(value)+" != null");
        writer.WriteString(value.FileName);
        Write(ref writer,Formatter,value.Language);
        Write(ref writer,Formatter,value.LanguageVendor);
        Write(ref writer,Formatter,value.DocumentType);
    }
    internal static T InternalDeserialize(ref Reader reader) {
        T? value = default;
        Instance.Deserialize(ref reader,ref value);
        return value!;
    }
    private static Guid Read(ref Reader reader,IMemoryPackFormatter<Guid> Formatter){
        Guid value=default!;
        Formatter.Deserialize(ref reader,ref value);
        return value;
    }
    public override void Deserialize(ref Reader reader,scoped ref T? value){
        var Formatter=reader.GetFormatter<Guid>();
        var fileName=reader.ReadString();
        var language=Read(ref reader,Formatter);
        var languageVendor=Read(ref reader,Formatter);
        var documentType=Read(ref reader,Formatter);
        value=Expressions.Expression.SymbolDocument(fileName,language,languageVendor,documentType);
    }
}