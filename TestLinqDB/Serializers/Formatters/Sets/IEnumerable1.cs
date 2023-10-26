using S = LinqDB.Sets;
using LinqDB.Sets;
//using Microsoft.SqlServer.Dac.Deployment;
//using System.Diagnostics;
//using System.Runtime.Serialization;

//using static Microsoft.FSharp.Core.ByRefKinds;

namespace TestLinqDB.Serializers.Formatters.Sets;
public class IEnumerable1:CollectionTest<S.IEnumerable<Tables.Table>>{
    public IEnumerable1():base(new Set<Tables.Table>{new(1),new(2)}){}
}
