﻿using Expressions = System.Linq.Expressions;
namespace TestLinqDB.Serializers.Formatters;
public class SymbolDocumentInfo : 共通
{
    [Fact]
    public void Serialize()
    {
        //if(writer.TryWriteNil(value)) return;
        this.MemoryMessageJson_T_Assert(new { a = default(Expressions.SymbolDocumentInfo) }, output => { });
        var SymbolDocument = Expressions.Expression.SymbolDocument("ソースファイル名0.cs");
        this.MemoryMessageJson_T_Assert(
            new
            {
                SymbolDocument,
                SymbolDocumentObject = (object)SymbolDocument
            }, output => { }
        );
    }
}
