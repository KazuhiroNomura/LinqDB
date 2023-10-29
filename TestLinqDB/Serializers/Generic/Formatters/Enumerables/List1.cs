namespace TestLinqDB.Serializers.Generic.Formatters.Enumerables;
public abstract class List1:CollectionTest<LinqDB.Enumerables.List<int>>{
    protected List1(テストオプション テストオプション):base(テストオプション,new()){
        //global::MemoryPack.MemoryPackFormatterProvider.RegisterCollection<LinqDB.Enumerables.List<int>,int>();
        var Data=this.Data;
        for(var a=0;a<10;a++){
            Data.Add(a);
        }
    }
}
