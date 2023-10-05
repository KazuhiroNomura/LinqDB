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
public class Set3:共通 {
    [Fact]
    public void WriteNullable(){
        var C=new LinqDB.Databases.Container();
        this.MemoryMessageJson_Assert(new { a = default(Set<Keys.Key,Tables.Table,LinqDB.Databases.Container>) });
        this.MemoryMessageJson_Assert(new Set<Keys.Key,Tables.Table,LinqDB.Databases.Container>(C) { new(1), new(2) } );
        this.MemoryMessageJson_Assert(new { a = new Set<Keys.Key,Tables.Table,LinqDB.Databases.Container>(C) { new(1), new(2) } });
    }
}
