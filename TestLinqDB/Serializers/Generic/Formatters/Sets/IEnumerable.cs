using G = LinqDB.Sets;
using LinqDB.Sets;
//using Microsoft.SqlServer.Dac.Deployment;
//using System.Diagnostics;
//using System.Runtime.Serialization;

//using static Microsoft.FSharp.Core.ByRefKinds;

namespace TestLinqDB.Serializers.Generic.Formatters.Sets;
public abstract class IEnumerable<TSerializer>:CollectionTest<G.IEnumerable,TSerializer> where TSerializer:LinqDB.Serializers.Serializer,new(){
    protected IEnumerable():base(new Set<Tables.Table>{ new(1),new(2) }){}
}
