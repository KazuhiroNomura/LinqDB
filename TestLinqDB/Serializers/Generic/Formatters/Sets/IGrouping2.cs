using S = LinqDB.Sets;
//using Microsoft.SqlServer.Dac.Deployment;
//using System.Diagnostics;
//using System.Runtime.Serialization;

//using static Microsoft.FSharp.Core.ByRefKinds;

namespace TestLinqDB.Serializers.Generic.Formatters.Sets;
public abstract class IGrouping2<TSerializer>:CollectionTest<S.IGrouping<int,double>,TSerializer> where TSerializer:LinqDB.Serializers.Serializer,new(){
    protected IGrouping2():base(new S.GroupingSet<int,double>(1)){
        var Data=(S.GroupingSet<int,double>)this.Data;
        for(var a=0;a<10;a++)Data.Add(a);
    }
}
