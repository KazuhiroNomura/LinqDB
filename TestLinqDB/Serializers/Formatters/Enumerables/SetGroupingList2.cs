using System.Reflection;
using S = LinqDB.Sets;
using E = System.Collections.Generic;
using Q = System.Linq;
using LinqDB.Enumerables;
//using Microsoft.SqlServer.Dac.Deployment;
//using System.Diagnostics;
//using System.Runtime.Serialization;

//using static Microsoft.FSharp.Core.ByRefKinds;

namespace TestLinqDB.Serializers.Formatters.Enumerables;
public class SetGroupingList2:共通 {
    [Fact]public void Serialize(){
        this.MemoryMessageJson_Assert(new{a=default(S.SetGroupingList<int,int>)});
        var input=new S.SetGroupingList<int,int>{new(0){0,1},new(1){2,3}};
        this.MemoryMessageJson_Assert(new{a=input});
    }
}
