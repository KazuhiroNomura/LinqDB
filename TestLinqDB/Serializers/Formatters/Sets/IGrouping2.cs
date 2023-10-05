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
public class IGrouping2:共通 {
    [Fact]public void Serialize(){
        //if(writer.TryWriteNil(value)) return;
        this.MemoryMessageJson_Assert(new{a=default(S.IGrouping<int,double>)});
        var input=new GroupingSet<int,double>(1);
        for(var a=0;a<10;a++)input.Add(a);
        this.MemoryMessageJson_Assert(new{a=(S.IGrouping<int,double>)input});
    }
}
