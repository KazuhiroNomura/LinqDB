using G = System.Linq;
using LinqDB.Enumerables;
//using Microsoft.SqlServer.Dac.Deployment;
//using System.Diagnostics;
//using System.Runtime.Serialization;

//using static Microsoft.FSharp.Core.ByRefKinds;

namespace TestLinqDB.Serializers.Formatters.Enumerables;
public class IGrouping2:共通 {
    [Fact]public void Serialize(){
        //if(writer.TryWriteNil(value)) return;
        this.MemoryMessageJson_Assert(new{a=default(G.IGrouping<int,double>)});
        var input=new GroupingList<int,double>(1);
        for(var a=0;a<10;a++)input.Add(a);
        this.MemoryMessageJson_Assert(new{a=(G.IGrouping<int,double>)input});
    }
}
