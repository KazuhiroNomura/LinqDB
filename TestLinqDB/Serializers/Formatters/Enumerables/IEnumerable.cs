using C = System.Collections;
using G = System.Collections.Generic;
namespace TestLinqDB.Serializers.Formatters.Enumerables;
public class IEnumerable:共通{
    [Fact]
    public void Serialize(){
        this.MemoryMessageJson_Assert(new{a=default(C.IEnumerable)});
        this.MemoryMessageJson_Assert(new{a=(C.IEnumerable)new G.List<Tables.Table> {new(1)}});
    }
}
