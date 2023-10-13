using S = LinqDB.Sets;
using E = System.Collections.Generic;
using Q = System.Linq;
using LinqDB.Sets;
//using Microsoft.SqlServer.Dac.Deployment;
//using System.Diagnostics;
//using System.Runtime.Serialization;

//using static Microsoft.FSharp.Core.ByRefKinds;

namespace TestLinqDB.Serializers.Formatters.Sets;
public class IEnumerable1:共通{
    [Fact]
    public void Serialize(){
        this.MemoryMessageJson_Assert(new{a=(S.IEnumerable<Tables.Table>)new Set<Tables.Table> {new(1)}});
        this.MemoryMessageJson_Assert(new{a=default(S.IEnumerable<Tables.Table>)});
    }
}
