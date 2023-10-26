using Expressions = System.Linq.Expressions;
namespace TestLinqDB.Serializers.Formatters;
public class DebugInfo:共通{
    [Fact]
    public void Serialize(){
        //if(writer.TryWriteNil(value)) return;
        var SymbolDocument0=Expressions.Expression.SymbolDocument("ソースファイル名0.cs");
        this.MemoryMessageJson_Expression_Assert全パターン(Expressions.Expression.DebugInfo(SymbolDocument0,1,2,3,4));
    }
}

