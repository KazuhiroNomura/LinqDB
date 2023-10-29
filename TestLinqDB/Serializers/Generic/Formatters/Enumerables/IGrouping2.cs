namespace TestLinqDB.Serializers.Generic.Formatters.Enumerables;
using G = LinqDB.Enumerables;

public abstract class IGrouping2:CollectionTest<IGrouping<int,double>>{
    protected IGrouping2(テストオプション テストオプション) : base(テストオプション,new G.GroupingList<int,double>(1)) {
        var Data=(G.GroupingList<int,double>)this.Data;
        for(var a=0;a<10;a++)Data.Add(a);
    }
}
