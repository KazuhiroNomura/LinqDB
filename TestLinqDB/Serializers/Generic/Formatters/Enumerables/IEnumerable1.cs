using Xunit.Sdk;

namespace TestLinqDB.Serializers.Generic.Formatters.Enumerables;
public abstract class IEnumerable1<TSerializer>:CollectionTest<System.Collections.Generic.IEnumerable<Tables.Table>,TSerializer> where TSerializer:LinqDB.Serializers.Serializer,new(){
    protected IEnumerable1():base(new System.Collections.Generic.List<Tables.Table>{new(1),new(2)}){}
}
