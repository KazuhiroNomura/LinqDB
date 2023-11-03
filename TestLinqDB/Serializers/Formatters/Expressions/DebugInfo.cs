using System.Linq.Expressions;

namespace TestLinqDB.Serializers.Formatters.Expressions;
public class DebugInfo : 共通
{
    [Fact]
    public void Serialize()
    {
        //if(writer.TryWriteNil(value)) return;
        var SymbolDocument0 = Expression.SymbolDocument("ソースファイル名0.cs");
        this.ExpressionシリアライズAssertEqual(Expression.DebugInfo(SymbolDocument0, 1, 2, 3, 4));
    }
}

