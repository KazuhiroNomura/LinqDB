using LinqDB.Enumerables;
using LinqDB.Sets;

namespace TestLinqDB.Serializers.Formatters.Enumerables;
public class List1:CollectionTest<LinqDB.Enumerables.List<int>>{
    public List1():base(new()){
        //global::MemoryPack.MemoryPackFormatterProvider.RegisterCollection<LinqDB.Enumerables.List<int>,int>();
        var Data=this.Data;
        for(var a=0;a<10;a++){
            Data.Add(a);
        }
    }
}
