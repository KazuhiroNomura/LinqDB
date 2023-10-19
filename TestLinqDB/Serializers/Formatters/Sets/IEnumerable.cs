using G = LinqDB.Sets;
using LinqDB.Sets;
//using Microsoft.SqlServer.Dac.Deployment;
//using System.Diagnostics;
//using System.Runtime.Serialization;

//using static Microsoft.FSharp.Core.ByRefKinds;

namespace TestLinqDB.Serializers.Formatters.Sets;
public class IEnumerable:CollectionTest<G.IEnumerable>{
    public IEnumerable():base(new Set<Tables.Table>{new(1)}){}
}
