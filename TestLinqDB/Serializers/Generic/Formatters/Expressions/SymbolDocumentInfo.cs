using System.Linq.Expressions;
namespace TestLinqDB.Serializers.Generic.Formatters.Expressions;
public abstract class SymbolDocumentInfo:共通{
    protected SymbolDocumentInfo(テストオプション テストオプション):base(テストオプション){}
    [Fact]
    public void Serialize()
    {
        //if(writer.TryWriteNil(value)) return;
        this.AssertEqual(new { a = default(SymbolDocumentInfo) }, output => { });
        var SymbolDocument = Expression.SymbolDocument("ソースファイル名0.cs");
        this.AssertEqual(
            new
            {
                SymbolDocument,
                SymbolDocumentObject = (object)SymbolDocument
            }, output => { }
        );
    }
}
