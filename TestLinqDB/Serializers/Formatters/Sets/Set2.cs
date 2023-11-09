using LinqDB.Sets;
//using Microsoft.SqlServer.Dac.Deployment;
//using System.Diagnostics;
//using System.Runtime.Serialization;

//using static Microsoft.FSharp.Core.ByRefKinds;

namespace TestLinqDB.Serializers.Formatters.Sets;
public class Set2 : CollectionTest<Set<Keys.Key, Tables.Table>>
{
    public Set2() : base(C.O, new())
    {
        var Data = this.Data;
        for (var a = 0; a<10; a++)
            Data.Add(new(a));
    }
    [Fact]
    public void Set3をSet2にキャスト()
    {
        this.AssertEqual((Set<Keys.Key, Tables.Table>)new Set<Keys.Key, Tables.Table, LinqDB.Databases.Container> { new(1), new(2) });
    }
    [Fact]
    public void Anonymous_Set3をSet2にキャスト()
    {
        var C = new LinqDB.Databases.Container();
        this.AssertEqual(new { a = (Set<Keys.Key, Tables.Table>)new Set<Keys.Key, Tables.Table, LinqDB.Databases.Container> { new(1), new(2) } });
        this.AssertEqual(new { a = (Set<Keys.Key, Tables.Table>)new Set<Keys.Key, Tables.Table, LinqDB.Databases.Container>(C) { new(1), new(2) } });
    }
}
