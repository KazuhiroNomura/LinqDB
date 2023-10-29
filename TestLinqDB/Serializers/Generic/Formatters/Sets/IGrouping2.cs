namespace TestLinqDB.Serializers.Generic.Formatters.Sets;
using G = LinqDB.Sets;
public abstract class IGrouping2:CollectionTest<G.IGrouping<int,double>>{
    protected IGrouping2(テストオプション テストオプション):base(テストオプション,new G.GroupingSet<int,double>(1)){
        var Data=(G.GroupingSet<int,double>)this.Data;
        for(var a=0;a<10;a++)Data.Add(a);
    }
}
