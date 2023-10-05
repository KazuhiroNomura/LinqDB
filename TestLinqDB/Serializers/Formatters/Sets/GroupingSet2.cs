using System.Reflection;
using System.Transactions;
using S = LinqDB.Sets;
using E = System.Collections.Generic;
using Q = System.Linq;
using LinqDB.Sets;
using System.Xml.Linq;
using TestLinqDB.Sets;
//using Microsoft.SqlServer.Dac.Deployment;
//using System.Diagnostics;
//using System.Runtime.Serialization;

//using static Microsoft.FSharp.Core.ByRefKinds;

namespace TestLinqDB.Serializers.Formatters.Sets;
public class GroupingSet2:共通 {
    [Fact]public void Serialize(){
        //if(writer.TryWriteNil(value)) return;
        this.MemoryMessageJson_Assert(new{a=default(GroupingSet<int,double>)});
        var input=new GroupingSet<int,double>(1);
        for(var a=0;a<10;a++)input.Add(a);
        this.MemoryMessageJson_Assert(new{a=input});
    }
}
