using G = LinqDB.Enumerables;

namespace TestLinqDB.Serializers.Generic.Formatters.Enumerables;
public abstract class IGrouping2<TSerializer>:CollectionTest<IGrouping<int,double>,TSerializer> where TSerializer:LinqDB.Serializers.Serializer,new(){
    protected IGrouping2():base(new G.GroupingList<int,double>(1)){
        var Data=(G.GroupingList<int,double>)this.Data;
        for(var a=0;a<10;a++)Data.Add(a);
    }
}
