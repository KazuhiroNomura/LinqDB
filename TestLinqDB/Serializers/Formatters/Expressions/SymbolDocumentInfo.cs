using System.Linq.Expressions;

namespace TestLinqDB.Serializers.Formatters.Expressions;
public class SymbolDocumentInfo : 共通
{
    [Fact]
    public void Serialize()
    {
        //if(writer.TryWriteNil(value)) return;
        var input = Expression.SymbolDocument("ソースファイル名0.cs");
        this.ObjectシリアライズAssertEqual(input);
    }
}