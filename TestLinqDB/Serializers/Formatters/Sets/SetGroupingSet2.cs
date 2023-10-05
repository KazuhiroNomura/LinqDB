using System.Reflection;
using S = LinqDB.Sets;
using E = System.Collections.Generic;
using Q = System.Linq;
using LinqDB.Sets;
//using Microsoft.SqlServer.Dac.Deployment;
//using System.Diagnostics;
//using System.Runtime.Serialization;

//using static Microsoft.FSharp.Core.ByRefKinds;

namespace TestLinqDB.Serializers.Formatters.Sets;
public class SetGroupingSet2:共通 {
    [Fact]public void Serialize(){
        this.MemoryMessageJson_Assert(new{a=default(S.SetGroupingSet<int,int>)});
        var s=new Set<Tables.Table>{new(1),new(2)};
        var input=s.GroupBy(p=>p);
        this.MemoryMessageJson_Assert(new{a=input});
    }
}
