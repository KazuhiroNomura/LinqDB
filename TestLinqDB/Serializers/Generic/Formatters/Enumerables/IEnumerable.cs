using C = System.Collections;
namespace TestLinqDB.Serializers.Generic.Formatters.Enumerables;
public abstract class IEnumerable<TSerializer>:CollectionTest<C.IEnumerable,TSerializer> where TSerializer:LinqDB.Serializers.Serializer,new(){
    protected IEnumerable():base(new List<Tables.Table>{new(1),new(2)}){}
}
