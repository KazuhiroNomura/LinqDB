using S = LinqDB.Sets;
using LinqDB.Sets;

namespace TestLinqDB.Serializers.Generic.Formatters.Sets;
public abstract class IEnumerable1<TSerializer>:CollectionTest<S.IEnumerable<Tables.Table>,TSerializer> where TSerializer:LinqDB.Serializers.Serializer,new(){
    protected IEnumerable1():base(new Set<Tables.Table>{new(1),new(2)}){}
}
