﻿
using LinqDB.Enumerables;
using TestLinqDB.Sets;
namespace TestLinqDB.Serializers.Formatters.Enumerables;
public class GroupingList2:共通 {
    [Fact]public void Serialize(){
        //if(writer.TryWriteNil(value)) return;
        this.MemoryMessageJson_Assert(new{a=default(GroupingList<int,double>)});
        var input=new GroupingList<int,double>(1);
        for(var a=0;a<10;a++)input.Add(a);
        this.MemoryMessageJson_Assert(new{a=input});
    }
}
