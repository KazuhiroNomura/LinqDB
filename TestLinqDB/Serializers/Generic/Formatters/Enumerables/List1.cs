namespace TestLinqDB.Serializers.Generic.Formatters.Enumerables;
public abstract class List1<TSerializer>:CollectionTest<LinqDB.Enumerables.List<int>,TSerializer> where TSerializer:LinqDB.Serializers.Serializer,new(){
    protected List1():base(new()){
        //global::MemoryPack.MemoryPackFormatterProvider.RegisterCollection<LinqDB.Enumerables.List<int>,int>();
        var Data=this.Data;
        for(var a=0;a<10;a++){
            Data.Add(a);
        }
    }
}
