using System.Linq.Expressions;
namespace TestLinqDB.Serializers.Generic.Formatters.Expressions;
public abstract class DebugInfo<TSerializer>:共通 where TSerializer:LinqDB.Serializers.Serializer,new(){
    protected DebugInfo():base(new AssertDefinition(new TSerializer())){}
    [Fact]
    public void Serialize(){
        //if(writer.TryWriteNil(value)) return;
        var SymbolDocument0=Expression.SymbolDocument("ソースファイル名0.cs");
        this.MemoryMessageJson_Expression_Assert全パターン(Expression.DebugInfo(SymbolDocument0,1,2,3,4));
    }
}

