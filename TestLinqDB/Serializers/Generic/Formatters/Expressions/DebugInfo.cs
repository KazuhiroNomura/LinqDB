using System.Linq.Expressions;
namespace TestLinqDB.Serializers.Generic.Formatters.Expressions;
public abstract class DebugInfo:共通{
    protected DebugInfo(テストオプション テストオプション):base(テストオプション){}
    [Fact]
    public void Serialize(){
        //if(writer.TryWriteNil(value)) return;
        var SymbolDocument0=Expression.SymbolDocument("ソースファイル名0.cs");
        this.ExpressionAssertEqual(Expression.DebugInfo(SymbolDocument0,1,2,3,4));
    }
}

