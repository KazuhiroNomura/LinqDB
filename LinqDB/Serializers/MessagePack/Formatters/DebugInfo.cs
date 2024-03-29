﻿using System.Diagnostics;
using MessagePack;
using MessagePack.Formatters;
using Expressions = System.Linq.Expressions;
namespace LinqDB.Serializers.MessagePack.Formatters;
using O=MessagePackSerializerOptions;
using Writer = MessagePackWriter;
using Reader = MessagePackReader;
using T = Expressions.DebugInfoExpression;
public class DebugInfo:IMessagePackFormatter<T>{
    public static readonly DebugInfo Instance=new();
    private static void PrivateWrite(ref Writer writer,T value,O Resolver){
        SymbolDocumentInfo.Write(ref writer,value.Document,Resolver);
        
        writer.WriteInt32(value.StartLine);
        
        writer.WriteInt32(value.StartColumn);
        
        writer.WriteInt32(value.EndLine);
        
        writer.WriteInt32(value.EndColumn);
        
    }
    internal static void Write(ref Writer writer,T value,O Resolver){
        writer.WriteArrayHeader(5);
        writer.WriteNodeType(Expressions.ExpressionType.DebugInfo);

        PrivateWrite(ref writer,value,Resolver);
        
    }
    public void Serialize(ref Writer writer,T? value,O Resolver){
        if(writer.TryWriteNil(value)) return;
        writer.WriteArrayHeader(4);
        PrivateWrite(ref writer,value,Resolver);
        
    }
    internal static T Read(ref Reader reader,O Resolver){
        var document=SymbolDocumentInfo.Read(ref reader,Resolver);
        
        var startLine=reader.ReadInt32();
        
        var startColumn=reader.ReadInt32();
        
        var endLine=reader.ReadInt32();
        
        var endColumn=reader.ReadInt32();
        return Expressions.Expression.DebugInfo(document,startLine,startColumn,endLine,endColumn);
    }
    public T Deserialize(ref Reader reader,O Resolver){
        if(reader.TryReadNil()) return null!;
        var count=reader.ReadArrayHeader();
        Debug.Assert(count==4);
        return Read(ref reader,Resolver);

    }
}