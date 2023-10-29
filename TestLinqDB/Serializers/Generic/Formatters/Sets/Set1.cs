﻿using LinqDB.Sets;

namespace TestLinqDB.Serializers.Generic.Formatters.Sets;
public abstract class Set1:CollectionTest<Set<int>>{
    protected Set1(テストオプション テストオプション):base(テストオプション,new()){
        var Data=this.Data;
        for(var a=0;a<10;a++)
            Data.Add(a);
    }
    [Fact]public void Set3をSet1にキャスト(){
        this.AssertEqual((Set<Tables.Table>)new Set<Keys.Key,Tables.Table,LinqDB.Databases.Container> { new(1),new(2) });
    }
    [Fact]public void Anonymous_Set3をSet2にキャスト(){
        var C=new LinqDB.Databases.Container();
        this.AssertEqual(new { a = (Set<Tables.Table>)new Set<Keys.Key,Tables.Table,LinqDB.Databases.Container> { new(1),new(2) } });
        this.AssertEqual(new { a = (Set<Tables.Table>)new Set<Keys.Key,Tables.Table,LinqDB.Databases.Container>(C){ new(1),new(2) } });
    }
}
