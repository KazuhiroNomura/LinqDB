﻿using LinqDB.Sets;
namespace TestLinqDB.Serializers.Formatters.Sets;
public class GroupingSet2:CollectionTest<GroupingSet<int,double>>{
    public GroupingSet2():base(new GroupingSet<int,double>(1)){
        var Data=this.Data;
        for(var a=0;a<10;a++)Data.Add(a);
    }
}
