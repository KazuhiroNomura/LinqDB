using Expressions = System.Linq.Expressions;
namespace TestLinqDB.Serializers.Formatters;
public class DebugInfo:共通{
    [Fact]
    public void Serialize(){
        //if(writer.TryWriteNil(value)) return;
        var SymbolDocument0=Expressions.Expression.SymbolDocument("ソースファイル名0.cs");
        var input=Expressions.Expression.DebugInfo(SymbolDocument0,1,2,3,4);
        this.MemoryMessageJson_Assert(new{a=input,b=(Expressions.Expression)input,c=(object)input,d=default(Expressions.DebugInfoExpression)});
    }
}

