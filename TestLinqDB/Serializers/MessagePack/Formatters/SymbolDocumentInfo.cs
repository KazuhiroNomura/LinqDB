using System.Diagnostics;
using System.Drawing;
using Reflection = System.Reflection;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Expressions = System.Linq.Expressions;
using RuntimeBinder=Microsoft.CSharp.RuntimeBinder;
namespace Serializers.MessagePack.Formatters;
using Sets;
public class SymbolDocumentInfo:共通 {
    [Fact]
    public void Serialize() {
        //if(writer.TryWriteNil(value)) return;
        this.MemoryMessageJson_Assert(new { a = default(Expressions.SymbolDocumentInfo) },output => { });
        var SymbolDocument = Expressions.Expression.SymbolDocument("ソースファイル名0.cs");
        this.MemoryMessageJson_Assert(
            new {
                SymbolDocument,SymbolDocumentObject = (object)SymbolDocument
            },output => { }
        );
    }
}
