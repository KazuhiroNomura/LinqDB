using G = System.Collections.Generic;
namespace TestLinqDB.Serializers.Formatters.Enumerables;
public class IEnumerable1:共通{
    [Fact]
    public void Serialize(){
        LinqDB.Serializers.MessagePack.Formatters.Enumerables.IEnumerable<TestLinqDB.Tables.Table>f=default;
        global::MessagePack.Formatters.IMessagePackFormatter<System.Collections.Generic.IEnumerable<TestLinqDB.Tables.Table>> x;
        x=f;

        this.MemoryMessageJson_Assert(new{a=default(G.IEnumerable<Tables.Table>)});
        this.MemoryMessageJson_Assert(new{a=(G.IEnumerable<Tables.Table>)new List<Tables.Table> {new(1)}});
    }
}
