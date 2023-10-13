using S = LinqDB.Sets;
using E = System.Collections.Generic;
using Q = System.Linq;
using LinqDB.Sets;
//using Microsoft.SqlServer.Dac.Deployment;
//using System.Diagnostics;
//using System.Runtime.Serialization;

//using static Microsoft.FSharp.Core.ByRefKinds;

namespace TestLinqDB.Serializers.Formatters.Sets;
public class Set1:共通{
    [Fact]
    public void Serialize(){
        //this.Message_Assert(new Set<Tables.Table>{new(1),new(2)});
        ////this.Message_Assert(new{a=default(Set<Tables.Table>)});
        //this.Message_Assert(new{a=new Set<Tables.Table>{new(1),new(2)}});
        //this.Message_Assert(new{a=(Set<Tables.Table>)new Set<Keys.Key,Tables.Table,LinqDB.Databases.Container> { new(1),new(2) } });
        this.MemoryMessageJson_Assert(new { a = default(Set<Tables.Table>) });
        this.MemoryMessageJson_Assert(new { a = new Set<Tables.Table> { new(1),new(2) } });
        this.MemoryMessageJson_Assert(new { a = (Set<Tables.Table>)new Set<Keys.Key,Tables.Table,LinqDB.Databases.Container> { new(1),new(2) } });
    }
}
