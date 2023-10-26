using C = System.Collections;
namespace TestLinqDB.Serializers.Formatters.Enumerables;
public class IEnumerable:CollectionTest<C.IEnumerable>{
    public IEnumerable():base(new List<Tables.Table>{new(1),new(2)}){}
}
