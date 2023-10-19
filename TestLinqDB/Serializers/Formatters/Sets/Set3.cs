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
    [Fact]public void @default(){
        this.MemoryMessageJson_Assert(default(Set<Keys.Key,Tables.Table,LinqDB.Databases.Container>));
    }
    [Fact]public void Anonymous_default(){
        this.MemoryMessageJson_Assert(new { a = default(Set<Keys.Key,Tables.Table,LinqDB.Databases.Container>) });
    }
    [Fact]public void Test(){
        var C=new LinqDB.Databases.Container();
        this.MemoryMessageJson_Assert(new Set<Keys.Key,Tables.Table,LinqDB.Databases.Container>(C) { new(1), new(2) } );
    }
    [Fact]public void Anonymous_Set3(){
        var C=new LinqDB.Databases.Container();
        this.MemoryMessageJson_Assert(new { a = new Set<Keys.Key,Tables.Table,LinqDB.Databases.Container>(C) { new(1), new(2) } });
    }
    [Fact]public void Set3をSet2にキャスト(){
        var C=new LinqDB.Databases.Container();
        this.MemoryMessageJson_Assert((Set<Keys.Key,Tables.Table>)new Set<Keys.Key,Tables.Table,LinqDB.Databases.Container> { new(1),new(2) });
        this.MemoryMessageJson_Assert((Set<Keys.Key,Tables.Table>)new Set<Keys.Key,Tables.Table,LinqDB.Databases.Container>(C){ new(1),new(2) });
    }
    [Fact]public void Anonymous_Set3をSet2にキャスト(){
        var C=new LinqDB.Databases.Container();
        this.MemoryMessageJson_Assert(new { a = (Set<Keys.Key,Tables.Table>)new Set<Keys.Key,Tables.Table,LinqDB.Databases.Container> { new(1),new(2) } });
        this.MemoryMessageJson_Assert(new { a = (Set<Keys.Key,Tables.Table>)new Set<Keys.Key,Tables.Table,LinqDB.Databases.Container>(C){ new(1),new(2) } });
    }
}
