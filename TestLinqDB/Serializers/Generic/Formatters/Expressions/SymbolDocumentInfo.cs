using System.Linq.Expressions;
namespace TestLinqDB.Serializers.Generic.Formatters.Expressions;
public abstract class SymbolDocumentInfo<TSerializer>:共通 where TSerializer:LinqDB.Serializers.Serializer,new(){
    protected SymbolDocumentInfo():base(new AssertDefinition(new TSerializer())){}
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
